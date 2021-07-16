using System;
using Core_Interception.Helpers;
using Core_Interception.Lib;
using HighPrecisionTimer;
using Typewriter.AhkParser;

namespace Typewriter
{
    public class Typewriter : IDisposable
    {
        public int Interval { get; set; } = 10;

        private IntPtr context = ManagedWrapper.CreateContext();

        public void Run(Sequence sequence)
        {
            if (TryFindKeyboardDeviceId(out int device))
            {
                foreach (var action in sequence)
                {
                    if (ProcessSpecialAction(action))
                        continue;

                    var stroke = action.ToStroke();
                    ManagedWrapper.Send(context, device, ref stroke, 1);
                    Delay(Interval);
                }
            }
            else
            {
                throw new Exception("No keyboard found");
            }
        }

        private bool ProcessSpecialAction(KeyAction action)
        {
            if (action.Key < K.SpecialActionMin)
                return false;

            switch (action.Key)
            {
                case K.Interval:
                    Interval = (int) action.UpDown;
                    break;
                case K.Sleep:
                    Delay((int) action.UpDown);
                    break;
            }

            return true;
        }

        public void EchoInput()
        {
            Console.WriteLine("Echo mode activated. Press Ctrl-C to exit.");
            Console.WriteLine("code\tstate\tname");

            ManagedWrapper.SetFilter(context, ManagedWrapper.IsKeyboard, ManagedWrapper.Filter.All);
            try
            {
                while (true)
                {
                    int device;
                    var stroke = new ManagedWrapper.Stroke();
                    if (ManagedWrapper.Receive(context, device = ManagedWrapper.WaitWithTimeout(context, 5), ref stroke, 1) > 0)
                    {
                        if (ManagedWrapper.IsKeyboard(device) > 0)
                        {
                            int scancode = stroke.key.code;
                            if ((stroke.key.state & 2) != 0)
                                scancode += 256;

                            Console.WriteLine($"{stroke.key.code}\t"
                                            + $"{(ManagedWrapper.KeyState) stroke.key.state}\t"
                                            + $"{KeyNameHelper.GetNameFromScanCode(scancode)}");

                            ManagedWrapper.Send(context, device, ref stroke, 1);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }

        private bool TryFindKeyboardDeviceId(out int deviceId)
        {
            for (int i = 1; i <= 20; i++)
            {
                if (ManagedWrapper.IsKeyboard(i) <= 0)
                    continue;

                string name = ManagedWrapper.GetHardwareStr(context, i);
                if (!string.IsNullOrWhiteSpace(name))
                {
                    // Console.WriteLine($"Found device\nid = {i}\nname = {name}");
                    deviceId = i;
                    return true;
                }
            }

            deviceId = -1;
            return false;
        }

        private static void Delay(int ms)
        {
            MultimediaTimer.Delay(ms).Wait();
        }

        private void ReleaseUnmanagedResources()
        {
            ManagedWrapper.DestroyContext(context);
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~Typewriter()
        {
            ReleaseUnmanagedResources();
        }
    }
}