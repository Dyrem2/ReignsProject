using System;
using System.Collections.Generic;
using System.Text;

namespace ReignsBot.classes
{
    partial class ConsoleOutputs
    {
        public static void Output(OutputType arg, bool newLine = true)
        {
            switch (arg)
            {
                //GREEN
                case OutputType.Completed:
                    Console.ForegroundColor = ConsoleColor.Green;
                    if (newLine) Console.WriteLine("Completed!");
                    else Console.Write("Completed!");
                    Console.ForegroundColor = Reigns.console_default_color;
                    break;

                case OutputType.True:
                    Console.ForegroundColor = ConsoleColor.Green;
                    if (newLine) Console.WriteLine("True");
                    else Console.Write("True");
                    Console.ForegroundColor = Reigns.console_default_color;
                    break;
                
                //RED
                case OutputType.False:
                    Console.ForegroundColor = ConsoleColor.Red;
                    if (newLine) Console.WriteLine("False");
                    else Console.Write("False");
                    Console.ForegroundColor = Reigns.console_default_color;
                    break;

                case OutputType.Interrupted:
                    Console.ForegroundColor = ConsoleColor.Red;
                    if (newLine) Console.WriteLine("Interrupted");
                    else Console.Write("Interrupted");
                    Console.ForegroundColor = Reigns.console_default_color;
                    break;

                default:
                    break;
            }
        }
    }
}
