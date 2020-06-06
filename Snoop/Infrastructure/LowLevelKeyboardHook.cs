﻿namespace Snoop.Infrastructure
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Input;
    using JetBrains.Annotations;

    public class LowLevelKeyboardHook
    {
#pragma warning disable SA1310 // Field names should not contain underscore
        private static readonly IntPtr WM_KEYDOWN = new IntPtr(0x0100);
        private static readonly IntPtr WM_KEYUP = new IntPtr(0x0101);
#pragma warning restore SA1310 // Field names should not contain underscore

        private IntPtr hookId = IntPtr.Zero;

        // We need to place this on a field/member.
        // Otherwise the delegate will be garbage collected and our hook crashes.
        private readonly NativeMethods.HookProc cachedProc;

        public LowLevelKeyboardHook()
        {
            this.cachedProc = this.HookCallback;
        }

        public class LowLevelKeyPressEventArgs : EventArgs
        {
            public LowLevelKeyPressEventArgs(ModifierKeys modifierKeys, Key key)
            {
                this.ModifierKeys = modifierKeys;
                this.Key = key;
            }

            public ModifierKeys ModifierKeys { get; }

            public Key Key { get; }
        }

        public event EventHandler<LowLevelKeyPressEventArgs> LowLevelKeyDown;

        public event EventHandler<LowLevelKeyPressEventArgs> LowLevelKeyUp;

        public bool IsRunning => this.hookId != IntPtr.Zero;

        public void Start()
        {
            if (this.hookId != IntPtr.Zero)
            {
                return;
            }

            this.hookId = CreateHook(this.cachedProc);
        }

        public void Stop()
        {
            if (this.hookId == IntPtr.Zero)
            {
                return;
            }

            NativeMethods.UnhookWindowsHookEx(this.hookId);
            this.hookId = IntPtr.Zero;
        }

        private static IntPtr CreateHook(NativeMethods.HookProc proc)
        {
            using var curProcess = Process.GetCurrentProcess();
            using var curModule = curProcess.MainModule;

            return NativeMethods.SetWindowsHookEx(NativeMethods.HookType.WH_KEYBOARD_LL, proc, NativeMethods.GetModuleHandle(curModule.ModuleName), 0);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            //you need to call CallNextHookEx without further processing
            //and return the value returned by CallNextHookEx
            if (nCode > 0)
            {
                var hookStruct = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));

                if (wParam == WM_KEYDOWN)
                {
                    this.LowLevelKeyDown?.Invoke(this, CreateEventArgs(hookStruct));
                }
                else if (wParam == WM_KEYUP)
                {
                    this.LowLevelKeyUp?.Invoke(this, CreateEventArgs(hookStruct));
                }
            }

            return NativeMethods.CallNextHookEx(this.hookId, nCode, wParam, lParam);
        }

        private static LowLevelKeyPressEventArgs CreateEventArgs(KBDLLHOOKSTRUCT hookStruct)
        {
            var key = KeyInterop.KeyFromVirtualKey(hookStruct.VKCode);
            return new LowLevelKeyPressEventArgs(Keyboard.Modifiers, key);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public int VKCode;
            public int ScanCode;
            public KBDLLHOOKSTRUCTFlags Flags;
            public uint Time;
            public UIntPtr ExtraInfo;
        }

        [Flags]
        private enum KBDLLHOOKSTRUCTFlags : uint
        {
            LLKHF_EXTENDED = 0x01,
            LLKHF_INJECTED = 0x10,
            LLKHF_ALTDOWN = 0x20,
            LLKHF_UP = 0x80,
        }
    }
}