﻿using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageManagementConsoleUI;
using System.Text.RegularExpressions;

namespace Ex03.GarageManagementConsoleUI
{
    class Program
    {

        public static void Main(string[] args)
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("Hello and welcome to our garage. Please select the number of the action you wish to perform:");
                Console.WriteLine(
@"1. Put a new Vehicle in the garage
  2. Display a license plate list of vehicles that are in the garage
  3. Change a vehicle state
  4. Inflate tires
  5. Refuel a gas vehicle
  6. Recharge an electric vehicle
  7. Display vehicle info
  8. --QUIT--");

                char input = Console.ReadKey().KeyChar;
                eGarageAction selection = (eGarageAction) Convert.ToInt32(input);

                switch (selection)
                {
                    case eGarageAction.InsertVehicle:
                        insertVeihcle();
                        break;
                    case eGarageAction.DisplayLicenseList:
                        // create filter here
                        displayLicenseList();
                        break;
                    case eGarageAction.ChangeVehicleState:
                        changeVehicleState();
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
                        Console.WriteLine("Bye bye!");
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine(input + " is an invalid option. Options are between 1-8");
                        break;
                } 
            }
        }

        private static string getInputFromUser(string i_PromptMsg, string i_ErrorMsg)
        {
            string input;
            do
            {
                input = Console.ReadLine();
            } while (true);
 
        }

        private static void changeVehicleState()
        {
            Console.Clear();
            string licenseNumber = getLicenceNumberFromUser();
            
            // Display the possible states to switch the vehicle to 
            Console.Clear();
            Console.WriteLine(string.Format("Please select a new state for vehicle number {0}",licenseNumber));
            Console.WriteLine(string.Format(
@"1. In Repair
  2. Done
  3. Paid"));
            
            int selection = getAndAssertInputRangeFromUser('1','3');
             GarageLogic.Garage.ChangeStatusOfVehicle(licenseNumber, (GarageLogic.Garage.eVehicleStatus) selection);

             Console.WriteLine(string.Format("Vehicle with license"));
        }

        private static void displayVeihcleInfo()
        {
            // itex
            
            Console.Clear();
            Console.WriteLine("Display Vehicle");
            Console.WriteLine("Please enter the vehicle's license number:");
            string licenseNumber = Console.ReadLine();
            if (isValidLicenseNumber(licenseNumber))
            {
                try
                {
                    Console.WriteLine(GarageLogic.Garage.DisplayFullSpecOfVehicle(licenseNumber));
        }
                catch (ArgumentException)
                {
                    Console.WriteLine("The vehicle you wish to view is not in the garage");
                }
            }

        }

        private static void recharge()
        {
            // eyal
            throw new NotImplementedException();
        }

        private static void refuel()
        {
            // eyal
            throw new NotImplementedException();
        }

        private static void inflateWheels()
        {
            //eyal
            throw new NotImplementedException();
        }

        private static void displayLicenseList()
        {
            // filter by enum of state - eyal
            throw new NotImplementedException();
        }

        //string i_Manufacturer, string i_LicenseNumber, int i_NumberOfWeels, float i_MaxAirPressure, string i_WheelManufacturer, FuelSource i_FuelOfVehicle
        private static void insertVeihcle()
        {
            string manufacturer;
            string licenseNumber;
            string wheelManufacturer;

            Console.Clear();
            Console.WriteLine("New Vehicle Window");
            Console.WriteLine("Our garage supports several vehicles");
            foreach (GarageLogic.Garage. item in collection)
	{
		 
	}
            
            
            //itex
            throw new NotImplementedException();
            
        }

        private static int getAndAssertInputRangeFromUser(char i_MinVal, char i_MaxVal)
        {
            int selection;
            char input;
            while (true)
            {
                input = Console.ReadKey().KeyChar;
                if (input > i_MaxVal || input < i_MinVal)
                {
                    Console.WriteLine("Invalid option was entered. Please input a number in the range of 1-3.");
                    continue;
                }
                selection = Convert.ToInt32(input);
                break;
            }
            return selection;
        }

        private static string getLicenceNumberFromUser()
        {
            string input;
            Console.WriteLine("Please enter a veihcle license plate number:");
            while (true)
            {
                input = Console.ReadLine();
                if (Regex.IsMatch(input, @"^[0-9a-zA-Z]+$"))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid license number was entered. Please try again.");
                }
            }
            
            return input;
        }


        public enum eGarageAction
        {
            InsertVehicle = 1,
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
