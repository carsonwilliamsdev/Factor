using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using Point = System.Drawing.Point;

namespace Factor
{
    public class User32
    {
        private const int GwlExstyle = (-20);
        public const int WsExTransparent = 0x00000020;
        public const int WsExToolWindow = 0x00000080;

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern IntPtr GetClientRect(IntPtr hWnd, ref Rect rect);

        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        public static void SetWindowExStyle(IntPtr hwnd, int style) => SetWindowLong(hwnd, GwlExstyle, GetWindowLong(hwnd, GwlExstyle) | style);

        public static Rectangle GetWindowRect(IntPtr hWnd, bool dpiScaling)
        {
            // Returns the co-ordinates of Hearthstone's client area in screen co-ordinates
            var rect = new Rect();
            var ptUL = new Point();
            var ptLR = new Point();

            GetClientRect(hWnd, ref rect);

            ptUL.X = rect.left;
            ptUL.Y = rect.top;

            ptLR.X = rect.right;
            ptLR.Y = rect.bottom;

            ClientToScreen(hWnd, ref ptUL);
            ClientToScreen(hWnd, ref ptLR);

            if (dpiScaling)
            {
                ptUL.X = (int)(ptUL.X / 1.0);
                ptUL.Y = (int)(ptUL.Y / 1.0);
                ptLR.X = (int)(ptLR.X / 1.0);
                ptLR.Y = (int)(ptLR.Y / 1.0);
            }

            return new Rectangle(ptUL.X, ptUL.Y, ptLR.X - ptUL.X, ptLR.Y - ptUL.Y);
        }
    }
}
