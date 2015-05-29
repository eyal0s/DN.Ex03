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

                int userSelection = getAndAssertInputRangeFromUser('1', '8');
                eGarageAction selection = (eGarageAction) userSelection;

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
                        Console.WriteLine(string.Format("{0} is an invalid option. Options are between 1-8", userSelection));
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
            Console.Clear();
            Console.WriteLine("Display Vehicle");
            string licenseNumber = getLicenceNumberFromUser();

                try
                {
                    Console.WriteLine(GarageLogic.Garage.DisplayFullSpecOfVehicle(licenseNumber));
        }
                catch (ArgumentException)
                {
                    Console.WriteLine("The vehicle you wish to view is not in the garage");
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

        
        private static void insertVeihcle()
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
            Console.WriteLine("New Vehicle Window");
            licenseNumber = getLicenceNumberFromUser();
            Console.WriteLine(
@"Our garage supports several vehicles
    1. MotorCycle
    2. Car
    3. Truck");

            selectionOfUser = getAndAssertInputRangeFromUser('1', '3');
            if (selectionOfUser == 1 || selectionOfUser == 2)             
            {
                Console.WriteLine(
@"What is the vehicle fuel type?
    1. Petrol
    2. Electric");

                isElectric = (getAndAssertInputRangeFromUser('1', '2') == 2) ? true : false;
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
            }
            
	
            
            
            //itex
            throw new NotImplementedException();
            
        }

        private static bool isTruckCargoDangerous()
        {
            Console.WriteLine(
@"Is the truck carrying dangerous materials?
    1. Yes
    2. No");

            int userChoice = getAndAssertInputRangeFromUser('1', '2');

            return (userChoice == 1) ? true : false;

        }

        private static float getCurrentCargoWeight()
        {
            string inputFromUser;
            float currentCargoWeight;
            Console.WriteLine("How much does the cargo weight?");
            inputFromUser = Console.ReadLine();

            while (inputFromUser.Length == 0 || float.TryParse(inputFromUser, out currentCargoWeight))
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

            while (inputFromUser.Length == 0 || int.TryParse(inputFromUser, out amountOfDoorsInCar) || amountOfDoorsInCar > 5 || amountOfDoorsInCar < 2)
            {
                Console.WriteLine("The range for doors in car is 2-5 (inclusive), please try again");
                inputFromUser = Console.ReadLine();
            }

            return amountOfDoorsInCar;

        }

        

        private static string getColorOfCarFromUser()
        {   
            string[] colors = new string[4]{"White", "Black", "Green", "Read"};
            Console.WriteLine(string.Format(
@"Please enter one of the following colors
    1. {0}
    2. {1}
    3. {2}
    4. {3}", colors[0], colors[1], colors[2], colors[3]));

            int UserChoiceOfColor = getAndAssertInputRangeFromUser('1', '4');
            
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
            while (inputCellNumberFromUser.Length != 10 || int.TryParse(inputCellNumberFromUser, out ownerCellNumber))
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
            while (isValidName(ownerName)) ;
            {
                if (ownerName.Length == 0)
                {
                    Console.WriteLine("You must enter at least one letter");
                }
                else
                {
                    Console.WriteLine("Please enter your name using english letters only");
                }
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
                Console.WriteLine("How many liters are currently in the vehicle?");    
            }
            else
            {
                Console.WriteLine("How many percantage does the battery currently have?");
            }

            inputFromUser = Console.ReadLine();
            while (float.TryParse(inputFromUser, out amountOfEnergyLeft))
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
            while (int.TryParse(inputFromUser, out engineVolume))
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
    1. A
    2. A2
    3. AB
    4. B1");
            int userChoiceOfLicense = getAndAssertInputRangeFromUser('1', '4');

            return userChoiceOfLicense;
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
                    Console.WriteLine(string.Format("Invalid option was entered. Please input a number in the range of {0}-{2}.", i_MinVal, i_MaxVal);
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
            Console.WriteLine("Please enter the veihcle license plate number:");
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
