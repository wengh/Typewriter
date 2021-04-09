using System;
using System.Collections.Generic;

namespace Typewriter.AhkParser.KeyMaps
{
    public static class EscapedMap
    {
        public static Sequence Get(string s)
        {
            if (k.TryGetValue(s, out var sequence))
                return sequence.Clone();

            if (s.Length == 1)
                return SingleCharMap.Get(s[0]);

            if (s.StartsWith("sc") && int.TryParse(s.Substring(2), out int code))
                return (K) code;

            if (s.StartsWith("s") && int.TryParse(s.Substring(1), out int sleepMs))
                return new KeyAction(K.Sleep, (UpDown) sleepMs);

            if (s.StartsWith("i") && int.TryParse(s.Substring(1), out int intervalMs))
                return new KeyAction(K.Interval, (UpDown) intervalMs);

            if (Enum.TryParse(s, out K key))
                return key;

            throw new ArgumentException($"Escaped key {{{s}}} is invalid");
        }

        public static Sequence GetDown(string s)
        {
            var sequence = Get(s);
            int n = sequence.Count;
            if (n == 1)
                throw new InvalidOperationException($"Action {sequence[0].Key} cannot be combined with 'down'");
            sequence.RemoveRange(n / 2, n / 2);
            return sequence;
        }

        public static Sequence GetUp(string s)
        {
            var sequence = Get(s);
            int n = sequence.Count;
            if (n == 1)
                throw new InvalidOperationException($"Action {sequence[0].Key} cannot be combined with 'up'");
            sequence.RemoveRange(0, n / 2);
            return sequence;
        }

        private static Dictionary<string, Sequence> k = new();

        static EscapedMap()
        {
            k["F1"] = K.F1;
            k["F2"] = K.F2;
            k["F3"] = K.F3;
            k["F4"] = K.F4;
            k["F5"] = K.F5;
            k["F6"] = K.F6;
            k["F7"] = K.F7;
            k["F8"] = K.F8;
            k["F9"] = K.F9;
            k["F10"] = K.F10;
            k["F11"] = K.F11;
            k["F12"] = K.F12;
            k["F13"] = K.F13;
            k["F14"] = K.F14;
            k["F15"] = K.F15;
            k["F16"] = K.F16;
            k["F17"] = K.F17;
            k["F18"] = K.F18;
            k["F19"] = K.F19;
            k["F20"] = K.F20;
            k["F21"] = K.F21;
            k["F22"] = K.F22;
            k["F23"] = K.F23;
            k["F24"] = K.F24;

            k["Enter"] = K.Enter;
            k["Escape"] = k["Esc"] = K.Esc;
            k["Space"] = K.Space;
            k["Tab"] = K.Tab;
            k["Backspace"] = k["BS"] = K.Backspace;
            k["Delete"] = k["Del"] = K.Delete;
            k["Insert"] = k["Ins"] = K.Insert;
            k["Up"] = K.Up;
            k["Down"] = K.Down;
            k["Left"] = K.Left;
            k["Right"] = K.Right;
            k["Home"] = K.Home;
            k["End"] = K.End;
            k["PgUp"] = K.PageUp;
            k["PgDn"] = K.PageDown;
            k["CapsLock"] = K.CapsLock;
            k["ScrollLock"] = K.ScrollLock;
            k["NumLock"] = K.NumLock;

            k["Control"] = k["Ctrl"] = K.Ctrl;
            k["LControl"] = k["LCtrl"] = K.Ctrl;
            k["RControl"] = k["RCtrl"] = K.RightCtrl;
            k["Alt"] = K.Alt;
            k["LAlt"] = K.Alt;
            k["RAlt"] = K.RightAlt;
            k["Shift"] = K.Shift;
            k["LShift"] = K.Shift;
            k["RShift"] = K.RightShift;
            k["LWin"] = K.LeftWin;
            k["RWin"] = K.RightWin;
            k["Menu"] = K.Menu;
            // k["Sleep"] = K.Sleep;

            k["Numpad0"] = K.Num0;
            k["Numpad1"] = K.Num1;
            k["Numpad2"] = K.Num2;
            k["Numpad3"] = K.Num3;
            k["Numpad4"] = K.Num4;
            k["Numpad5"] = K.Num5;
            k["Numpad6"] = K.Num6;
            k["Numpad7"] = K.Num7;
            k["Numpad8"] = K.Num8;
            k["Numpad9"] = K.Num9;

            k["NumpadDot"] = K.NumDot;
            k["NumpadEnter"] = K.NumEnter;
            k["NumpadMult"] = K.NumMult;
            k["NumpadDiv"] = K.NumDiv;
            k["NumpadAdd"] = K.NumPlus;
            k["NumpadSub"] = K.NumMinus;

            k["PrintScreen"] = K.PrintScreen;
            k["CtrlBreak"] = K.Break;
            k["Pause"] = K.Pause;

            // when NumLock is off
            k["NumpadDel"] = K.NumDot;
            k["NumpadIns"] = K.Num0;
            k["NumpadClear"] = K.Num5;
            k["NumpadUp"] = K.Num8;
            k["NumpadDown"] = K.Num2;
            k["NumpadLeft"] = K.Num4;
            k["NumpadRight"] = K.Num6;
            k["NumpadHome"] = K.Num7;
            k["NumpadEnd"] = K.Num1;
            k["NumpadPgUp"] = K.Num9;
            k["NumpadPgDn"] = K.Num3;
        }
    }
}