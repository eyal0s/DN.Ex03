using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageManagementConsoleUI;

namespace Ex03.GarageManagementConsoleUI
{
    class Program
    {

        public static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Option 1");
            Console.WriteLine("Option 2");
            Console.WriteLine("Option 3");
            Console.WriteLine();
            //Console.Write("input: ");

            var originalpos = Console.CursorTop;

            var k = Console.ReadKey();
            var i = 1;

            while (k.KeyChar != 'q')
            {

                if (k.Key == ConsoleKey.UpArrow)
                {
                    i = (i + 1) % 3;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(0, Console.CursorTop - i);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.WriteLine("Option " + (Console.CursorTop + 1));
                    Console.ResetColor();
                    

                }

                if (k.Key == ConsoleKey.DownArrow)
                {
                    i = (i - 1) % 3;
                    Console.SetCursorPosition(0, Console.CursorTop + i);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    //Console.WriteLine("Option " + (Console.CursorTop + 1));
                    Console.ResetColor();
                    

                }

                if (k.Key == ConsoleKey.Enter)
                {

                    Console.WriteLine(i);
                    Console.Clear();

                }

                Console.SetCursorPosition(8, originalpos);
                k = Console.ReadKey();
            }
        }
    }
}
