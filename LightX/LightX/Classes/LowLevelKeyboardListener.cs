﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Interop;

namespace LightX.Classes
{
    public class LowLevelKeyboardListener
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_SYSKEYUP = 0x0105;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        public event EventHandler<KeyPressedArgs> OnKeyPressed;
        //public event EventHandler<KeyEventArgs> OnKeyPressed;
        public event EventHandler<KeyPressedArgs> OnKeyReleased;

        private LowLevelKeyboardProc _proc;
        private IntPtr _hookID = IntPtr.Zero;

        public LowLevelKeyboardListener()
        {
            _proc = HookCallback;
        }

        public void HookKeyboard()
        {
            _hookID = SetHook(_proc);
        }

        public void UnHookKeyboard()
        {
            UnhookWindowsHookEx(_hookID);
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);

                if (OnKeyPressed != null) { OnKeyPressed(this, new KeyPressedArgs(KeyInterop.KeyFromVirtualKey(vkCode))); }
                //if (OnKeyPressed != null) { OnKeyPressed(this, new KeyEventArgs(Keyboard.PrimaryDevice, new HwndSource(0, 0, 0, 0, 0, "", IntPtr.Zero), 0, KeyInterop.KeyFromVirtualKey(vkCode))); }
            }
            else if (nCode >= 0 && wParam == (IntPtr)WM_KEYUP || wParam == (IntPtr)WM_SYSKEYUP)
            {
                int vkCode = Marshal.ReadInt32(lParam);

                if (OnKeyReleased != null) { OnKeyReleased(this, new KeyPressedArgs(KeyInterop.KeyFromVirtualKey(vkCode))); }
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
    }

    public class KeyPressedArgs : EventArgs
    {
        public Key KeyPressed { get; private set; }

        public KeyPressedArgs(Key key)
        {
            KeyPressed = key;
        }
    }
}
