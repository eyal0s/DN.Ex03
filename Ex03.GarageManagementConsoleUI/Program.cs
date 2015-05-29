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

                char input = Console.ReadKey().KeyChar;
                eGarageAction selection = (eGarageAction) Convert.ToInt32(input);

                switch (selection)
                {
                    case eGarageAction.InsertVeihcle:
                        insertVeihcle();
                        break;
                    case eGarageAction.DisplayLicenseList:
                        // create filter here
                        displayLicenseList();
                        break;
                    case eGarageAction.ChangeVehicleState:
                        insertVeihcle();
                        break;
                    case eGarageAction.InflateWheels:
                        inflateWheels();
                        break;
                    case eGarageAction.Refuel:
                        refuel();
                        break;
                    case eGarageAction.Recharge:
                        recharge();
                        break;
                    case eGarageAction.DisplayVehicleInfo:
                        displayVeihcleInfo();
                        break;
                    case eGarageAction.Quit:
                        exitGarage();
                        break;
                    default:
                        Console.WriteLine(input + " is an invalid option. Options are between 1-7");
                        break;
                } 
            }
        }

        private static void displayVeihcleInfo()
        {
            throw new NotImplementedException();
        }

        private static void recharge()
        {
            throw new NotImplementedException();
        }

        private static void refuel()
        {
            throw new NotImplementedException();
        }

        private static void inflateWheels()
        {
            throw new NotImplementedException();
        }

        private static void displayLicenseList()
        {
            throw new NotImplementedException();
        }

        private static void insertVeihcle()
        {
            throw new NotImplementedException();
        }

        private static void exitGarage()
        {
            Console.WriteLine("Bye bye!");
        }

        public enum eGarageAction
        {
            InsertVeihcle = 1,
            DisplayLicenseList = 2,
            ChangeVehicleState = 3,
            InflateWheels = 4,
            Refuel = 5,
            Recharge = 6,
            DisplayVehicleInfo = 7,
            Quit = 8
        }
    }
}
