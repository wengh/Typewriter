using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core_Interception.Helpers;
using Core_Interception.Lib;
using Typewriter.AhkParser;

namespace Typewriter
{
    internal class Program
    {
        private static void Main()
        {
            new Program().Run(GetRawArgsWithoutExe());
        }

        private IntPtr context = ManagedWrapper.CreateContext();
        private int interval = 10;

        private void Run(string args)
        {
            try
            {
                // EchoInput()
                var sequence = AhkParser.AhkParser.Parse(args);

                if (TryFindKeyboardDeviceId(out int device))
                {
                    foreach (var action in sequence)
                    {
                        if (ProcessSpecialAction(action))
                        {
                            continue;
                        }

                        var stroke = action.ToStroke();
                        ManagedWrapper.Send(context, device, ref stroke, 1);
                        Thread.Sleep(interval);
                    }
                }
                else
                {
                    throw new Exception("No keyboard found");
                }
            }
            finally
            {
                InterceptorFinish();
            }
        }

        private bool ProcessSpecialAction(KeyAction action)
        {
            if (action.Key < K.SpecialActionMin)
                return false;

            switch (action.Key)
            {
                case K.Interval:
                    interval = (int) action.UpDown;
                    break;
                case K.Sleep:
                    Thread.Sleep((int) action.UpDown);
                    break;
            }

            return true;
        }

        private void InterceptorInit()
        {
            ManagedWrapper.SetFilter(context, ManagedWrapper.IsKeyboard, ManagedWrapper.Filter.All);
        }

        private void EchoInput()
        {
            InterceptorInit();
            while (true)
            {
                try
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

                            Console.WriteLine($"{device}\t"
                                            + $"{stroke.key.code}\t"
                                            + $"{stroke.key.information}\t"
                                            + $"{(ManagedWrapper.KeyState) stroke.key.state}\t"
                                            + $"{KeyNameHelper.GetNameFromScanCode(scancode)}");

                            ManagedWrapper.Send(context, device, ref stroke, 1);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e);
                }
            }
        }

        private void InterceptorFinish()
        {
            ManagedWrapper.DestroyContext(context);
        }

        private bool TryFindKeyboardDeviceId(out int deviceId)
        {
            for (int i = 0; i < 100; i++)
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

        private static string GetRawArgsWithoutExe()
        {
            string exe = Environment.GetCommandLineArgs()[0];
            string rawCmd = Environment.CommandLine;
            string argsOnly = rawCmd.Remove(rawCmd.IndexOf(exe, StringComparison.Ordinal), exe.Length).TrimStart('"');

            if (argsOnly.StartsWith(" ", StringComparison.Ordinal))
                argsOnly = argsOnly.Substring(1);

            return argsOnly;
        }
    }
}