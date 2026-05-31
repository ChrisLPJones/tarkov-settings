using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

using tarkov_settings.GPU;

namespace tarkov_settings
{
    class ColorController
    {
        IGPU gpu = GPUDevice.Instance;

        private RAMP currentRamps;
        private RAMP originalRamps;

        // True  → loop re-applies originalRamps every 250 ms (reset/idle state)
        // False → loop re-applies currentRamps  every 250 ms (active/custom state)
        // Running the loop in BOTH states means Windows can never permanently revert either ramp.
        private volatile bool _resetMode = true;
        private CancellationTokenSource _loopCanceller;

        // Preview thread — applies slider values off the UI thread so dragging stays smooth.
        private volatile float _previewB = 0.5f, _previewC = 0.5f, _previewG = 1.0f;
        private volatile int _previewDvl = 50;
        private readonly AutoResetEvent _previewSignal = new AutoResetEvent(false);

        #region Singleton Pattern
        private static readonly Lazy<ColorController> instance =
            new Lazy<ColorController>(() => new ColorController());

        public static ColorController Instance => instance.Value;
        #endregion

        #region Win32 API Calls
        [DllImport("gdi32")]
        private static extern bool GetDeviceGammaRamp(IntPtr hDc, ref RAMP lpRamp);

        [DllImport("gdi32")]
        private static extern bool SetDeviceGammaRamp(IntPtr hDc, ref RAMP lpRamp);
        #endregion

        // DVL is expressed as a percentage (0–100) matching the Nvidia Control Panel scale.
        public int DVL
        {
            get
            {
                try
                {
                    int min = gpu.MinSaturation;
                    int max = gpu.MaxSaturation;
                    if (max <= min) return 50;
                    return (int)Math.Round((gpu.Saturation - min) / (double)(max - min) * 100);
                }
                catch (NotImplementedException) { return 50; }
            }
            set
            {
                try
                {
                    int min = gpu.MinSaturation;
                    int max = gpu.MaxSaturation;
                    gpu.Saturation = min + (int)Math.Round(value / 100.0 * (max - min));
                }
                catch (NotImplementedException) { }
            }
        }

        private ColorController() { }

        public void Init()
        {
            // Capture the original gamma ramp so we can restore it later.
            var hdc = IntPtr.Zero;
            try
            {
                hdc = Display.CreateDC(null, Display.Primary, null, IntPtr.Zero);
                currentRamps  = new RAMP();
                originalRamps = new RAMP();
                GetDeviceGammaRamp(hdc, ref originalRamps);
            }
            finally
            {
                if (!IntPtr.Zero.Equals(hdc))
                    Display.DeleteDC(hdc);
            }

            // Persistent gamma loop.
            // Runs regardless of mode so Windows can never permanently revert the ramp.
            _loopCanceller = new CancellationTokenSource();
            var token = _loopCanceller.Token;
            new Thread(() =>
            {
                var loopHdc = IntPtr.Zero;
                try
                {
                    loopHdc = Display.CreateDC(null, Display.Primary, null, IntPtr.Zero);
                    while (!token.IsCancellationRequested)
                    {
                        if (_resetMode)
                            SetDeviceGammaRamp(loopHdc, ref originalRamps);
                        else
                            SetDeviceGammaRamp(loopHdc, ref currentRamps);
                        Thread.Sleep(50);
                    }
                }
                finally
                {
                    if (!IntPtr.Zero.Equals(loopHdc))
                        Display.DeleteDC(loopHdc);
                }
            }) { IsBackground = true }.Start();

            new Thread(PreviewLoop) { IsBackground = true }.Start();
        }

        private void PreviewLoop()
        {
            while (_previewSignal.WaitOne())
            {
                var hdc = IntPtr.Zero;
                try
                {
                    hdc = Display.CreateDC(null, Display.Primary, null, IntPtr.Zero);
                    var lut = CalculateLUT(_previewB, _previewC, _previewG);
                    currentRamps.Red = currentRamps.Blue = currentRamps.Green = lut;
                    SetDeviceGammaRamp(hdc, ref currentRamps);
                }
                finally
                {
                    if (!IntPtr.Zero.Equals(hdc))
                        Display.DeleteDC(hdc);
                }
                try { DVL = _previewDvl; } catch (NotImplementedException) { }
            }
        }

        internal void SignalPreview(double b, double c, double g, int dvl)
        {
            _previewB = (float)b; _previewC = (float)c; _previewG = (float)g; _previewDvl = dvl;
            _previewSignal.Set();
        }

        public Task ChangeColorRamp(double brightness = 0.5, double contrast = 0.5, double gamma = 1.0, bool reset = true)
        {
            if (reset)
            {
                _resetMode = true;
                // Apply immediately so there is no 250 ms gap before the loop picks it up.
                var hdc = IntPtr.Zero;
                try
                {
                    hdc = Display.CreateDC(null, Display.Primary, null, IntPtr.Zero);
                    SetDeviceGammaRamp(hdc, ref originalRamps);
                }
                finally
                {
                    if (!IntPtr.Zero.Equals(hdc)) Display.DeleteDC(hdc);
                }
            }
            else
            {
                ushort[] lut = CalculateLUT(brightness, contrast, gamma);
                currentRamps.Red = currentRamps.Blue = currentRamps.Green = lut;
                _resetMode = false;
            }
            return Task.CompletedTask;
        }

        /*
         * Code from
         * https://github.com/falahati/NvAPIWrapper/issues/20#issuecomment-634551206
         */
        internal static ushort[] CalculateLUT(double brightness = 0.5, double contrast = 0.5, double gamma = 2.8)
        {
            const int dataPoints = 256;

            gamma      = Math.Min(Math.Max(gamma,      0.4), 2.8);
            contrast   = (Math.Min(Math.Max(contrast,  0), 1) - 0.5) * 2;
            brightness = (Math.Min(Math.Max(brightness, 0), 1) - 0.5) * 2;

            var offset = contrast > 0 ? contrast * -25.4 : contrast * -32;
            var range  = (dataPoints - 1) + offset * 2;
            offset    += brightness * (range / 5);

            var result = new ushort[dataPoints];
            for (var i = 0; i < result.Length; i++)
            {
                var factor = (i + offset) / range;
                factor = Math.Pow(factor, 1 / gamma);
                factor = Math.Min(Math.Max(factor, 0), 1);
                result[i] = (ushort)Math.Round(factor * ushort.MaxValue);
            }
            return result;
        }

        public void ResetDVL()
        {
            try
            {
                gpu.ResetSaturation();
                Console.WriteLine("[DVL] Reset to : {0}", gpu.InitSaturation);
            }
            catch (NotImplementedException) { }
        }

        internal void Close()
        {
            _loopCanceller?.Cancel();
            _loopCanceller?.Dispose();
            ResetDVL();
        }
    }
}
