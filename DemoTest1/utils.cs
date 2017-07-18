// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace DemoTest1
{
    using System;

    class utils
    {
        internal static void WriteInformation(double percent, int items, TimeSpan time)
        {
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            Console.CursorTop = Console.WindowTop + Console.WindowHeight - 2;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Green;

            Console.Write("Percentage Read: {0:N2}%  ", percent);
            //Continue line in black where we finished the blue square.
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(new string(' ', Console.WindowWidth - Console.CursorLeft));

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write("Items: {0} Time: {1:00}:{2:00}:{3:00}   ", items, time.Minutes, time.Seconds, time.Milliseconds);
            //Continue line in black where we finished the blue square.
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(new string(' ', Console.WindowWidth - Console.CursorLeft-1));

            // Restore previous position and colors
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(x, y);
        }

        internal static bool ValidateVerbose(string input)
        {
            input = input.Trim();
            input = input.ToUpper();

            if (input[0] == 'Y')
            {
                return true;
            }
            return false;
        }

        internal static void ClearInformation()
        {
            int currentY = Console.CursorTop;
            Console.SetCursorPosition(0, Console.WindowHeight + Console.WindowTop - 2);
            string emptyLine = new string(' ', Console.WindowWidth - 1);
            Console.Write(emptyLine);
            Console.Write(emptyLine);
            Console.SetCursorPosition(0, currentY);
        }

        internal static Tuple<double, string> ConvertBytesToSize(double bytes)
        {
            int timesDivided = 0;
            double _bytes = bytes;
            while (bytes > 1024)
            {
                timesDivided++;
                bytes /= 1024;
            }

            string name = null;

            switch (timesDivided)
            {
                case 1:
                    name = "Kb";
                    break;
                case 2:
                    name = "Mb";
                    break;
                case 3:
                    name = "Gb";
                    break;
                case 4:
                    name = "Tb";
                    break;
                default:
                    name = "Bytes";
                    bytes = _bytes;
                    break;
            }

            Tuple<double, string> result = new Tuple<double, string>(bytes, name);
            return result;
        }
    }
}
