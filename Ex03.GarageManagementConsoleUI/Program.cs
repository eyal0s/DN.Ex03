﻿using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageManagementConsoleUI;


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
                Console.WriteLine("Hello and welcome to our garage. Please pick an action from the list: ");

                Console.WriteLine("Display all the possibilities here...");

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

        private static void changeVehicleState()
        {
            //eyal
            throw new NotImplementedException();
        }

        private static void displayVeihcleInfo()
        {
            // itex
            throw new NotImplementedException();
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

        private static void insertVeihcle()
        {
            //itex
            throw new NotImplementedException();
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
