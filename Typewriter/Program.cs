using System;

namespace Typewriter
{
    internal static class Program
    {
        private static void Main()
        {
            using var tw = new Typewriter();
            string args = GetRawArgsWithoutExe();

            if (args == "")
            {
                HelpMessage.PrintHelpMessage();
                tw.EchoInput();
            }
            else
            {
                var sequence = AhkParser.AhkParser.Parse(args);
                tw.Run(sequence);
            }
        }

        private static string GetRawArgsWithoutExe()
        {
            string exe = Environment.GetCommandLineArgs()[0];
            string rawCmd = Environment.CommandLine;
            string argsOnly = rawCmd.Remove(rawCmd.IndexOf(exe, StringComparison.Ordinal), exe.Length).TrimStart('"').Trim(' ');
            if (argsOnly.StartsWith("\"") && argsOnly.EndsWith("\""))
            {
                argsOnly = argsOnly.Substring(1, argsOnly.Length - 2);
            }

            return argsOnly;
        }
    }
}