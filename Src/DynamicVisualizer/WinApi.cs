using System;
using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming

namespace DynamicVisualizer
{
    internal static class WinApi
    {
        public enum ScrollBarDirection
        {
            SB_HORZ = 0,
            SB_VERT = 1,
            SB_CTL = 2,
            SB_BOTH = 3
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);
    }
}