using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace Production.Windows
{

    /// <summary>
    /// 枚举win32 api
    /// </summary>
    class Win32API
    {
        //窗体消息
        public const int WM_CLOSE = 0x0010;
        public const int WM_SETTEXT = 0x000C;
        public const int BM_CLICK = 0x00F5;

        public const int USER = 0x0400;
        public const int WM_COMRX = USER + 1;

        // usb消息定义
        public const int WM_DEVICE_CHANGE = 0x219;
        public const int DBT_DEVICEARRIVAL = 0x8000;
        public const int DBT_DEVICE_REMOVE_COMPLETE = 0x8004;
        public const UInt32 DBT_DEVTYP_PORT = 0x00000003;


        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, string lParam);

        [DllImport("user32.dll", EntryPoint = "PostMessage")]
        public static extern void PostMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern void SetForegroundWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern int EnumChildWindows(IntPtr hWndParent, CallBack lpfn, int lParam);
        public delegate bool CallBack(IntPtr hwnd, int lParam);

        [DllImport("kernel32", EntryPoint = "WritePrivateProfileString")]
        public static extern bool WritePrivateProfileString(
            string lpAppName, string lpKeyName, string lpString, string lpFileName);

        [DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
        public static extern int GetPrivateProfileString(
            string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString,
            int nSize, string lpFileName);

        [DllImport("kernel32", SetLastError = true)]
        public static extern void Sleep(UInt32 dwMilliseconds);
    }
}
