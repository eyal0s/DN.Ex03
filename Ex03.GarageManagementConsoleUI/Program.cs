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

            while (true)
            {

                Console.Clear();
                Console.WriteLine("Hello and welcome to our garage. Please pick an action from the list: ");
                Console.WriteLine("Display all the possibilities here...");
                Console.WriteLine("You can always hit 'q' if you want to exit.");

                char input = Console.ReadKey().KeyChar;
                if (input == 'q')
                {
                    exitGarage();
                    break;
                }

                int selection = Convert.ToInt32(input);
                if (selection < 1 || selection > 7)
                {
                    Console.WriteLine(input + " is an invalid option. Options are between 1-7");
                    continue;
                }

                switch (selection)
                {
                    case 1:
                        
                        break;
                    default:
                        
                        break;
                } 
            }



        }

        private static void exitGarage()
        {
            throw new NotImplementedException();
        }
    }
}
