using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace TFT_Overlay
{
    public static class ProcessHelper
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        public static string GetActiveProcessName()
        {
            var hwnd = GetForegroundWindow();
            GetWindowThreadProcessId(hwnd, out uint pid);

            return Process.GetProcessById((int)pid).ProcessName;
        }
    }
}
