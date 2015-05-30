using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageManagementConsoleUI;
using System.Text.RegularExpressions;
using Ex03.GarageLogic;

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
(2) See which vehicles are currently in
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
                        InflateTires();
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
                        Console.WriteLine("Thanks for coming to our garage. Bye bye!");
                        System.Threading.Thread.Sleep(3000);
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
            Console.WriteLine("Change vehicle state:");
            Console.WriteLine("---------------------");
            string licenseNumber = getLicenceNumberFromUser();

            if (!licenseExist(licenseNumber))
            {
                printGoingBackToMainMenuMsg();
                return;
            }

            // Display the possible states to switch the vehicle to 
            Console.Clear();
            Console.WriteLine("Change vehicle state:");
            Console.WriteLine("---------------------");
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
            Console.WriteLine("Display Vehicle Info:");
            Console.WriteLine("---------------------");

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
            Console.WriteLine("---------------------");

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
            Console.WriteLine("---------------------");

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
                Console.WriteLine("Invalid amount\fuel type was entered");
                return;
            }
        }

        private static void InflateTires()
        {
            Console.Clear();
            Console.WriteLine("Inflate Tires:");
            Console.WriteLine("---------------------");
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
            Console.WriteLine("Display Garage Licenses List:");
            Console.WriteLine("---------------------");

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
                licenseNumberList = Ex03.GarageLogic.Garage.DisplayAllLicenseNumber(Garage.eVehicleStatus.Done, Garage.eVehicleStatus.InRepair, Garage.eVehicleStatus.Paid);
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
            Console.ReadKey();
        }

        private static void printGoingBackToMainMenuMsg()
        {
            Console.WriteLine(k_GoingBackToMainMenu);
            System.Threading.Thread.Sleep(4000);
        }

        //string i_Manufacturer, string i_LicenseNumber, int i_NumberOfWeels, float i_MaxAirPressure, string i_WheelManufacturer, FuelSource i_FuelOfVehicle
        private static void insertVehicle()
        {
            string ownerName;
            string ownerCell;
            string manufacturer;
            string licenseNumber;
            string wheelManufacturer;
            float currentAvailableEnergyInVehicle;
            bool isElectric = false;
            int selectionOfUser;
            
            Console.Clear();
            Console.WriteLine("Enter a new vehicle:");
            Console.WriteLine("---------------------");
            Console.WriteLine(@"         
Our garage supports several vehicles please choose:
(1) MotorCycle
(2) Car
(3) Truck");

            selectionOfUser = getAndAssertInputRangeFromUser(1, 3);
Our garage supports several vehicles please choose (1-3)
    (1) MotorCycle
    (2) Car
    (3) Truck");

            selectionOfUser = getNumericValueFromUser(3);
            if (selectionOfUser == 1 || selectionOfUser == 2)
            {
                Console.WriteLine(
@"What is the vehicle fuel type?(1-2)
    (1) Petrol
    (2) Electric");

                isElectric = (getNumericValueFromUser(2) == 2) ? true : false;
            }

            initVehicleVarible(out ownerName, out ownerCell, out manufacturer, out licenseNumber, out wheelManufacturer, out currentAvailableEnergyInVehicle, isElectric);

            bool isVehicleAdded = false;
            switch (selectionOfUser)
            {
                // cycle
                case 1:

                    int licenseType = getMotorCycleLicenseType();
                    int enginVolume = getVolumeOfEngine();

                    isVehicleAdded = Garage.InsertNewVehicleToGarage(ownerName, ownerCell, manufacturer, licenseNumber, wheelManufacturer, currentAvailableEnergyInVehicle, isElectric, licenseType, enginVolume);

                    break;
                // car
                case 2:

                    string colorOfCar = getColorOfCarFromUser();
                    int amountOfDoors = getAmountOfDoorsFromUser();

                    isVehicleAdded = Garage.InsertNewVehicleToGarage(ownerName, ownerCell, manufacturer, licenseNumber, wheelManufacturer, currentAvailableEnergyInVehicle, isElectric, colorOfCar, amountOfDoors);

                    break;
                // truck
                case 3:

                    bool isCarryingDangerousMaterial = isTruckCargoDangerous();
                    float currentCarryingWeight = getCurrentCargoWeight();
                    isVehicleAdded = Garage.InsertNewVehicleToGarage(ownerName, ownerCell, manufacturer, licenseNumber, wheelManufacturer, currentAvailableEnergyInVehicle, isCarryingDangerousMaterial, currentCarryingWeight);
                    break;
                default:
                    break;
            }

            if (!isVehicleAdded)
            {
                Console.WriteLine(string.Format("Sorry, a vehicle with {0} license number already exist in the garage", licenseNumber));
                inputAnyKeytoReturnToMain();
            }

        }

        private static bool isTruckCargoDangerous()
        {
            Console.WriteLine(
@"Is the truck carrying dangerous materials?
    (1) Yes
    (2) No");

            int userChoice = getNumericValueFromUser(2);

            return (userChoice == 1) ? true : false;

        }

        private static float getCurrentCargoWeight()
        {
            string inputFromUser;
            float currentCargoWeight;
            Console.WriteLine("How much does the cargo weight?");
            inputFromUser = Console.ReadLine();

            while (inputFromUser.Length == 0 || !float.TryParse(inputFromUser, out currentCargoWeight))
            {
                Console.WriteLine("Please enter a valid weight in KG(digits only)");
                inputFromUser = Console.ReadLine();
            }

            return currentCargoWeight;
        }

        private static int getAmountOfDoorsFromUser()
        {
            string inputFromUser;
            int amountOfDoorsInCar;

            Console.WriteLine("Please enter the amount of doors in the car:");
            inputFromUser = Console.ReadLine();

            while (inputFromUser.Length == 0 || !int.TryParse(inputFromUser, out amountOfDoorsInCar) || amountOfDoorsInCar > 5 || amountOfDoorsInCar < 2)
            {
                Console.WriteLine("The range for doors in car is 2-5 (inclusive), please try again");
                inputFromUser = Console.ReadLine();
            }

            return amountOfDoorsInCar;

        }



        private static string getColorOfCarFromUser()
        {
            string[] colors = new string[4] { "White", "Black", "Green", "Red" };
            Console.WriteLine(string.Format(
@"Please enter one of the following colors
    (1) {0}
    (2) {1}
    (3) {2}
    (4) {3}", colors[0], colors[1], colors[2], colors[3]));

            int UserChoiceOfColor = getNumericValueFromUser(4);

            return colors[UserChoiceOfColor - 1];
        }

        private static void initVehicleVarible(out string io_OwnerName, out string io_OwnerCell, out string io_Manufacturer, out string io_LicenseNumber, out string io_WheelManufacturer, out float io_CurrentAvailableEnergyInVehicle, bool isElectric)
        {
            io_OwnerName = getOwnerNameFromUser();
            io_OwnerCell = getCellNumberFromUser();
            io_LicenseNumber = getLicenceNumberFromUser();
            io_Manufacturer = getManufacturers(out io_WheelManufacturer);
            io_CurrentAvailableEnergyInVehicle = getAmountOfEnergyLeftFromUser(isElectric);


        }

        private static string getManufacturers(out string io_wheelManufacturer)
        {
            string manufacturer;
            Console.WriteLine("Who is the vehicle's manufacturer?");
            manufacturer = Console.ReadLine();
            while (manufacturer.Length == 0)
            {
                Console.WriteLine("Please enter the manufacture (at least one letter)");
                manufacturer = Console.ReadLine();
            }


            Console.WriteLine("Who is the wheel manufacturer?");
            io_wheelManufacturer = Console.ReadLine();
            while (io_wheelManufacturer.Length == 0)
            {
                Console.WriteLine("Please enter the manufacturer");
                io_wheelManufacturer = Console.ReadLine();
            }


            return manufacturer;
        }

        private static string getCellNumberFromUser()
        {
            string inputCellNumberFromUser;
            int ownerCellNumber;
            Console.WriteLine("Please enter cell number of the owner:");
            inputCellNumberFromUser = Console.ReadLine();

            while (inputCellNumberFromUser.Length != 10 || !int.TryParse(inputCellNumberFromUser, out ownerCellNumber))
            {
                Console.WriteLine("The number should be 10 digits (ONLY digits 0-9)");
                inputCellNumberFromUser = Console.ReadLine();
            }

            return inputCellNumberFromUser;
        }

        private static string getOwnerNameFromUser()
        {
            string ownerName;
            Console.WriteLine("Please enter name of owner");
            ownerName = Console.ReadLine();
            while (!isValidName(ownerName))
            {
                if (ownerName.Length == 0)
                {
                    Console.WriteLine("You must enter at least one letter");
                }
                else
                {
                    Console.WriteLine("Please enter your name using english letters only");
                }

                ownerName = Console.ReadLine();
            }

            return ownerName;
        }

        private static bool isValidName(string i_inputName)
        {
            bool isValid = true;

            if (i_inputName.Length == 0)
            {
                isValid = false;
            }
            else
            {
                for (int i = 0; i < i_inputName.Length; i++)
                {
                    if (!('A' <= i_inputName[i] && i_inputName[i] <= 'Z') && !('a' <= i_inputName[i] && i_inputName[i] <= 'z'))
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            return isValid;
        }

        private static float getAmountOfEnergyLeftFromUser(bool i_IsElectricVehicle)
        {
            float amountOfEnergyLeft;
            string inputFromUser;
            if (i_IsElectricVehicle)
            {
                Console.WriteLine("What is the battery percantage currently?");
            }
            else
            {
                Console.WriteLine("How many liters are currently in the vehicle?");
            }

            inputFromUser = Console.ReadLine();
            while (!float.TryParse(inputFromUser, out amountOfEnergyLeft))
            {
                Console.WriteLine("Please enter a numeric value");
                inputFromUser = Console.ReadLine();
            }

            return amountOfEnergyLeft;
        }

        private static int getVolumeOfEngine()
        {
            int engineVolume;
            string inputFromUser;

            Console.WriteLine("What is the engine's volume?");
            inputFromUser = Console.ReadLine();

            while (!int.TryParse(inputFromUser, out engineVolume))
            {
                Console.WriteLine("please insert a number");
                inputFromUser = Console.ReadLine();
            }

            return engineVolume;

        }

        private static int getMotorCycleLicenseType()
        {
            Console.WriteLine(
@"What is the license type: 
    (1) A
    (2) A2
    (3) AB
    (4) B1");
            int userChoiceOfLicense = getNumericValueFromUser(4);

            return userChoiceOfLicense;
        }

        private static void inputAnyKeytoReturnToMain()
        {
            Console.WriteLine("Press any key to return to main window");
            Console.ReadLine();
        }

        private static int getNumericValueFromUser(int i_options)
        {
            int selection;
            char input;
            while (true)
            {
                input = Console.ReadKey().KeyChar;

                if (input > '1' + i_options || input < '1')
                {
                    Console.WriteLine(string.Format("Invalid option was entered. Please input a number in the range of 1-{0}", i_options));
                    continue;
                }

                selection = input - '0';
 
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
