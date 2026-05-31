using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace tarkov_settings
{
    static class NativeMethods
    {
        private const uint WINEVENT_OUTOFCONTEXT = 0;
        public const uint EVENT_SYSTEM_FOREGROUND = 3;
        public const uint EVENT_SYSTEM_DESKTOPSWITCH = 0x0020;

        public delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hWnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

        public static WinEventDelegate dele = null;

        private static IntPtr m_hhook;
        private static IntPtr m_hhookDesktop;

        public static void SetHook()
        {
            m_hhook = SetWinEventHook(EVENT_SYSTEM_FOREGROUND,
                EVENT_SYSTEM_FOREGROUND,
                IntPtr.Zero,
                dele,
                0, 0, WINEVENT_OUTOFCONTEXT | 2);
            m_hhookDesktop = SetWinEventHook(EVENT_SYSTEM_DESKTOPSWITCH,
                EVENT_SYSTEM_DESKTOPSWITCH,
                IntPtr.Zero,
                dele,
                0, 0, WINEVENT_OUTOFCONTEXT);
        }

        public static void UnHook()
        {
            UnhookWinEvent(m_hhook);
            UnhookWinEvent(m_hhookDesktop);
        }

        #region Win32 API Calls
        [DllImport("user32.dll")]
        public static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        [DllImport("user32.dll")]
        public static extern IntPtr UnhookWinEvent(IntPtr hWinEventHook);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        #endregion
        public static string GetActiveWindowTitle()
        {
            try
            {
                IntPtr handle = GetForegroundWindow();
                uint threadID = GetWindowThreadProcessId(handle, out uint processID);
                return Process.GetProcessById(Convert.ToInt32(processID)).ProcessName;
            }
            catch
            {
                return null;
            }
        }
    }
    class ProcessMonitor
    {
        private NativeMethods.WinEventDelegate processHook;

        private readonly ColorController cController = ColorController.Instance;

        private HashSet<string> pTargets = new HashSet<string>();

        #region Singleton Pattern implement
        private static readonly Lazy<ProcessMonitor> instance =
            new Lazy<ProcessMonitor>(() => new ProcessMonitor());

        public static ProcessMonitor Instance
        {
            get
            {
                return instance.Value;
            }
        }
        #endregion

        public MainForm Parent { get; set; }

        private ProcessMonitor() { }

        public void Add(string process)
        {
            this.pTargets.Add(process);
        }

        public void Init()
        {
            processHook = new NativeMethods.WinEventDelegate(WinEventProc);
            NativeMethods.dele += processHook;
            NativeMethods.SetHook();

            // Init ColorController
            cController.Init();
        }

        /**
         * Window Focus changed Event Handler
         */
        public void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hWnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            if (eventType == NativeMethods.EVENT_SYSTEM_DESKTOPSWITCH)
            {
                // Reset immediately; EVENT_SYSTEM_FOREGROUND will re-apply if the
                // new desktop has a tracked window in focus.
                _ = cController.ChangeColorRamp(reset: true);
                cController.ResetDVL();
                return;
            }
            Console.WriteLine("Running Tasks : {0}", GetWorkingThreads());
            Console.WriteLine("Focused Process : {0}", NativeMethods.GetActiveWindowTitle());
            ApplyCurrentState();
        }

        /**
         * Evaluates the current focused window and mode, then applies or resets colors.
         * Called on focus change and when the Always On toggle is switched.
         */
        public void ApplyCurrentState()
        {
            if (Parent == null) return;
            var active = NativeMethods.GetActiveWindowTitle()?.ToLower();
            if (Parent.IsEnabled && (Parent.IsAlwaysOn || (active != null && this.pTargets.Contains(active))))
            {
                Console.WriteLine("[pMonitor] Applying color settings");
                var (b, c, g, dvl) = Parent.GetColorValue();
                _ = cController.ChangeColorRamp(brightness: b, contrast: c, gamma: g, reset: false);
                cController.DVL = dvl;
            }
            else
            {
                Console.WriteLine("[pMonitor] Resetting color settings");
                _ = cController.ChangeColorRamp(reset: true);
                cController.ResetDVL();
            }
        }

        /**
         * Reset to original color settings before exit
         */
        public void Close()
        {
            Console.WriteLine("[pMonitor] Remove Delegates");
            NativeMethods.dele -= processHook;
            NativeMethods.UnHook();

            Console.WriteLine("[pMonitor] Resetting Color");
            cController.Close();
        }

        private static int GetWorkingThreads()
        {
            System.Threading.ThreadPool.GetMaxThreads(out int maxThreads, out int _);
            System.Threading.ThreadPool.GetAvailableThreads(out int availableThreads, out _);
            return maxThreads - availableThreads;
        }
    }
}
