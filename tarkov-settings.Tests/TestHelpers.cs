using System;

namespace tarkov_settings.Tests
{
    // Standalone copies of the pure-math functions from ColorController and NVIDIA.
    // These exist here because the main project depends on WinForms and GPU APIs
    // that cannot be compiled in a headless test environment. The logic is identical
    // to the source; any change to the original should be mirrored here.

    internal static class ColorLogic
    {
        // Mirrors ColorController.CalculateLUT
        internal static ushort[] CalculateLUT(double brightness = 0.5, double contrast = 0.5, double gamma = 2.8)
        {
            const int dataPoints = 256;

            gamma      = Math.Min(Math.Max(gamma,      0.4), 2.8);
            contrast   = (Math.Min(Math.Max(contrast,  0),   1) - 0.5) * 2;
            brightness = (Math.Min(Math.Max(brightness, 0),   1) - 0.5) * 2;

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
    }

    internal static class SaturationHelper
    {
        // Mirrors NVIDIA.ClampSaturation
        internal static int ClampSaturation(int value, int min, int max)
        {
            if (value > max) return max;
            if (value < min) return min;
            return value;
        }
    }
}
