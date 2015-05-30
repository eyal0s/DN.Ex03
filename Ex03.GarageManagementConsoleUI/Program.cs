﻿using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageManagementConsoleUI;
using System.Text.RegularExpressions;

namespace Ex03.GarageManagementConsoleUI
{
    class Program
    {
        private const bool v_CheckIfExists = true;
        private const string k_GoingBackToMainMenu = "Going back to main menu...";

        public static void Main(string[] args)
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("Hello and welcome to our garage! \nPlease select an option to proceed:");
                Console.WriteLine(@"
(1) Put a new Vehicle in the garage
(2) Display a license plate list of vehicles that are in the garage
(3) Change a vehicle state
(4) Inflate tires
(5) Refuel a gas vehicle
(6) Recharge an electric vehicle
(7) Display vehicle info
(8) QUIT");

                int input = getNumericValueFromUser(8);
                eGarageAction selection = (eGarageAction) Convert.ToInt32(input);
                switch (selection)
                {
                    case eGarageAction.InsertVehicle:
                        insertVehicle();
                        break;
                    case eGarageAction.DisplayLicenseList:
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


        private static void changeVehicleState()
        {


            Console.Clear();
            string licenseNumber = getLicenceNumberFromUser();

            if (!licenseExist(licenseNumber))
            {
                printGoingBackToMainMenuMsg();
                return;
            }

            // Display the possible states to switch the vehicle to 
            Console.Clear();
            Console.WriteLine(string.Format("Please select a new state for vehicle number {0}",licenseNumber));
            Console.WriteLine(string.Format(@"
(1) In Repair
(2) Done
(3) Paid"));
            
            int selection = getNumericValueFromUser(3);
            GarageLogic.Garage.ChangeStatusOfVehicle(licenseNumber, (GarageLogic.Garage.eVehicleStatus) selection);
            printOperationSuccessMsg();
        }

        private static bool licenseExist(string i_LicenseNumber)
        {
            bool licenseExist = true;
            try
            {
                Ex03.GarageLogic.Garage.CheckExistenceOfVehicle(i_LicenseNumber);
            }
            catch (ArgumentException)
            {
                Console.WriteLine(string.Format("Seems like vehicle with number {0} doens't exist in the garage", i_LicenseNumber));
                licenseExist = false;
            }

            return licenseExist; 
        }

        private static void printOperationSuccessMsg()
        {
            Console.Clear();
            Console.WriteLine("Operation Terminated successfuly!");
            System.Threading.Thread.Sleep(3000);
        }

        private static void displayVeihcleInfo()
        {
            // itex
            
            Console.Clear();
            Console.WriteLine("Display Vehicle");

            string licenseNumber = getLicenceNumberFromUser();
            if (!licenseExist(licenseNumber))
            {
                printGoingBackToMainMenuMsg();
                return;
            }

            string vehicleInfo = GarageLogic.Garage.DisplayFullSpecOfVehicle(licenseNumber);
            Console.WriteLine(vehicleInfo);
            inputAnythingToReturnToMain();
        }


        private static void recharge()
        {
            Console.Clear();
            Console.WriteLine("Recharge:");

            string licenseNumber = getLicenceNumberFromUser();
            if (!licenseExist(licenseNumber))
            {
                printGoingBackToMainMenuMsg();
                return;
            }
            
            Console.WriteLine("How many hours you wish to recharge?");
            string amount = Console.ReadLine();
            float parsedAmount;
            try
            {
                parsedAmount = float.Parse(amount);
            }
            catch (Exception)
            {
                Console.WriteLine("invalid term was entered");
                printGoingBackToMainMenuMsg();
                return;
            }

            try
            {
                Ex03.GarageLogic.Garage.RefuelBattery(licenseNumber, parsedAmount);
                printOperationSuccessMsg();
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid amount was entered");
                return;
            }
            
          }

        private static void refuel()
        {
            Console.Clear();
            Console.WriteLine("Refuel:");

            string licenseNumber = getLicenceNumberFromUser();
            if (!licenseExist(licenseNumber))
            {
                printGoingBackToMainMenuMsg();
                return;
            }

            Console.WriteLine("How many gas you wish to refuel?");
            string amount = Console.ReadLine();
            float parsedAmount;
            try
            {
                parsedAmount = float.Parse(amount);
            }
            catch (Exception)
            {
                Console.WriteLine("invalid term was entered");
                printGoingBackToMainMenuMsg();
                return;
            }

            Console.WriteLine(@"
Select a type of fuel:
(1) Put a new Vehicle in the garage
(2) Display a license plate list of vehicles that are in the garage
(3) Change a vehicle state");


            int input = getNumericValueFromUser(3);
            GarageLogic.Vehicle.eFuelType fuelTypeSelection = (GarageLogic.Vehicle.eFuelType) input;
            try
            {
                Ex03.GarageLogic.Garage.RefuelPetrol(licenseNumber, (GarageLogic.Vehicle.eFuelType) fuelTypeSelection, parsedAmount);
                printOperationSuccessMsg();
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid amount was entered");
                return;
            }
        }

        private static void inflateWheels()
        {
            string licensenNumber = getLicenceNumberFromUser();
            if (!licenseExist(licensenNumber))
            {
                printGoingBackToMainMenuMsg();
                return;
            }

            Ex03.GarageLogic.Garage.WheelPump(licensenNumber);
            printOperationSuccessMsg();
        }

        private static void displayLicenseList()
        {
            Console.Clear();
            Console.WriteLine("Do you wish to filter the results?");
            Console.WriteLine(string.Format(@"
(1) Yes, Show just In Repair vehicles
(2) Yes, Show just Done vehicles
(3) Yes, Show just Paid vehicles
(4) No, Show me everything"));
            int selection = getNumericValueFromUser(4);
            string licenseNumberList = string.Empty;
            if (selection == 4)
            {
                licenseNumberList = Ex03.GarageLogic.Garage.DisplayAllLicenseNumber();
            }
            else
            {
                licenseNumberList = Ex03.GarageLogic.Garage.DisplayAllLicenseNumber((GarageLogic.Garage.eVehicleStatus) selection);
            }

            Console.Clear();

            if (licenseNumberList.Length == 0)
            {
                Console.WriteLine("We haven't found results for this filter :(");
                printGoingBackToMainMenuMsg();
                return;
            }

            Console.WriteLine(licenseNumberList);
            inputAnythingToReturnToMain();
        }

        private static void inputAnythingToReturnToMain()
        {
            Console.WriteLine("Enter any key to return to main menu");
            Console.ReadLine();
        }

        private static void printGoingBackToMainMenuMsg()
        {
            Console.WriteLine(k_GoingBackToMainMenu);
            System.Threading.Thread.Sleep(4000);
        }

        //string i_Manufacturer, string i_LicenseNumber, int i_NumberOfWeels, float i_MaxAirPressure, string i_WheelManufacturer, FuelSource i_FuelOfVehicle
        private static void insertVehicle()
        {
            string manufacturer;
            string licenseNumber;
            string wheelManufacturer;
            
            Console.Clear();
            Console.WriteLine("New Vehicle Window");
            Console.WriteLine("Our garage supports several vehicles");
            
	
            
            
            //itex
            throw new NotImplementedException();
            
        }

        private static int getNumericValueFromUser(int i_options)
        {
            int selection;
            char input;
            while (true)
            {
                input = Console.ReadKey().KeyChar;
                Console.WriteLine(input);
                if (input > '1' + i_options || input < '1')
                {
                    Console.WriteLine(string.Format("Invalid option was entered. Please input a number in the range of 1-{0}", i_options));
                    continue;
                }
                selection = input - 48;
                Console.WriteLine(selection);
                return selection;
            }
            
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

        public class ConsoleSpinner
        {
            private int counter;

            public ConsoleSpinner()
            {
                counter = 0;
            }

            public void Spin()
            {
                counter++;
                switch (counter % 4)
                {
                    case 0: 
                        Console.Write("/"); 
                        break;
                    case 1: 
                        Console.Write("-"); 
                        break;
                    case 2: 
                        Console.Write("\\"); 
                        break;
                    case 3: 
                        Console.Write("-"); 
                        break;
                }

                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            }
        } 
    }
}
