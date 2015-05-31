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

        private const string k_MainMenuMessage = @"Hello and welcome to our garage!

Please select an option to proceed:
(1) Put a new vehicle in the garage
(2) See which vehicles are currently in
(3) Change a vehicle state
(4) Inflate tires
(5) Refuel a gas vehicle
(6) Recharge an electric vehicle
(7) Display vehicle info
(8) QUIT";

        private const string k_RefuelTitle = @"Refuel:
---------------------";

        private const string k_ChangeVehicleMessage = @"Change vehicle state:
---------------------";
        
        private const string k_DisplayLicenseMessage = @"Display Garage Licenses List:
---------------------

Do you wish to filter the results?
 
(1) Yes, Show just In Repair vehicles
(2) Yes, Show just Done vehicles
(3) Yes, Show just Paid vehicles
(4) No, Show me everything";

        private const string k_InsertVehicleOpenningMessage = @"Our garage supports several vehicles please select a type:
(1) MotorCycle
(2) Car
(3) Truck";

        

        private const string k_InsertVehicleHeaderMessage = @"Enter a new vehicle:
---------------------";

        private const string k_InsertVehicleFuelMessage = @"Select the vehicles' fuel type:
(1) Petrol
(2) Electric";

        private const string k_MotorCycleLicenseTypeMessage = @"Select the motorcycle license type: 
(1) A
(2) A2
(3) AB
(4) B1";



        private const string k_GoingBackToMainMenu = "Going back to main menu...";

        public static void Main(string[] args)
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine(k_MainMenuMessage);
                int input = getNumericValueFromUser(8);
                eGarageAction selection = (eGarageAction) input;
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
                        Console.WriteLine("\nThanks for coming to our garage. Bye bye!");
                        System.Threading.Thread.Sleep(3000);
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine(string.Format("{0} is an invalid option. Options are between 1-8", input));
                        break;
                } 
            }
        }

        private static void changeVehicleState()
        {

            Console.Clear();
            Console.WriteLine(k_ChangeVehicleMessage);
            string licenseNumber = getLicenceNumberFromUser();

            if (!licenseExist(licenseNumber))
            {
                printGoingBackToMainMenuMsg();
                return;
            }

            // Display the possible states to switch the vehicle to        
            Console.Clear();         
            Console.WriteLine(string.Format(@"{0}
Please select a new state for vehicle number {1}
{2}",
    k_ChangeVehicleMessage,
    licenseNumber,
    createQuestionaire(Garage.GetVehicleStatusOptions())));
            
            int selection = getNumericValueFromUser(Garage.GetVehicleStatusOptions().Count);
            GarageLogic.Garage.ChangeStatusOfVehicle(licenseNumber, (GarageLogic.Garage.eVehicleStatus) selection);
            printOperationSuccessMsg();
        }

        private static string createQuestionaire(List<string> i_Options) 
        {
            StringBuilder questionaireForUser = new StringBuilder();
            int index = 1;

            foreach (string currentOption in i_Options)
            {
                questionaireForUser.AppendLine(string.Format("({0}) {1}", index++, currentOption));
            }

            return questionaireForUser.ToString();
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
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Operation has ended successfuly!");
            Console.WriteLine("-----------------------------");
            promptAbort();
        }

        private static void insertVehicle()
        {
            // variables all vehicle must have
            string ownerName;
            string ownerCell;
            string manufacturer;
            string licenseNumber;
            string wheelManufacturer;
            float currentAvailableEnergyInVehicle;
            float currentAirPressure;
            bool isElectric = false;
            int selectionOfUserForVehicleType;

            showInsertHeader();
            Console.WriteLine(string.Format(@"{0}
Please choose one of our supported vehicle:
{1}", 
    k_InsertVehicleHeaderMessage,
    createQuestionaire(Garage.GetSupportedVehicles())));

            selectionOfUserForVehicleType = getNumericValueFromUser(Garage.GetSupportedVehicles().Count);

            if (selectionOfUserForVehicleType == 1 || selectionOfUserForVehicleType == 2)
            {
                showInsertHeader();
                Console.WriteLine(k_InsertVehicleFuelMessage);
                isElectric = (getNumericValueFromUser(2) == 2) ? true : false;
            }

            initVehicleVarible(out ownerName, out ownerCell, out manufacturer, out licenseNumber, out wheelManufacturer, out currentAirPressure, out currentAvailableEnergyInVehicle, isElectric);

            bool isVehicleAdded = false;
            
            switch (selectionOfUserForVehicleType)
            {
                // cycle
                case 1:

                    int licenseType = getMotorCycleLicenseType();
                    int enginVolume = getVolumeOfEngine();

                    isVehicleAdded = Garage.InsertNewVehicleToGarage(ownerName, ownerCell, manufacturer, licenseNumber, wheelManufacturer, currentAirPressure, currentAvailableEnergyInVehicle, isElectric, licenseType, enginVolume);

                    break;
                // car
                case 2:

                    string colorOfCar = getColorOfCarFromUser();
                    int amountOfDoors = getAmountOfDoorsFromUser();

                    isVehicleAdded = Garage.InsertNewVehicleToGarage(ownerName, ownerCell, manufacturer, licenseNumber, wheelManufacturer, currentAirPressure, currentAvailableEnergyInVehicle, isElectric, colorOfCar, amountOfDoors);

                    break;
                // truck
                case 3:

                    bool isCarryingDangerousMaterial = isTruckCargoDangerous();
                    float currentCarryingWeight = getCurrentCargoWeight();
                    isVehicleAdded = Garage.InsertNewVehicleToGarage(ownerName, ownerCell, manufacturer, licenseNumber, wheelManufacturer, currentAirPressure, currentAvailableEnergyInVehicle, isCarryingDangerousMaterial, currentCarryingWeight);
                    break;
                default:
                    break;
            }

            if (!isVehicleAdded)
            {
                Console.WriteLine(string.Format("Sorry, a vehicle with {0} license number already exist in the garage", licenseNumber));
                promptAbort();
            }
            else
            {
                printOperationSuccessMsg();
            }

        }

        private static void displayVeihcleInfo()
        {            
            Console.Clear();
            Console.WriteLine(
@"Display Vehicle Info:
---------------------");

            string licenseNumber = getLicenceNumberFromUser();
            if (!licenseExist(licenseNumber))
            {
                printGoingBackToMainMenuMsg();
                return;
            }

            Console.Clear();
            Console.WriteLine(string.Format("Displaying info for vehicle number {0}\n-----------------------------------", licenseNumber));
            string vehicleInfo = GarageLogic.Garage.DisplayFullSpecOfVehicle(licenseNumber);
            Console.WriteLine(vehicleInfo);
            promptAbort();
        }

        private static void recharge()
        {
            Console.Clear();
            Console.WriteLine(@"Recharge:
---------------------");

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

                // TODO: should catch a value out of range ex. we need to diff between ex that are caused by wrong format and others
            catch (Exception)
            {
                Console.WriteLine("Invalid amount was entered");
                return;
            }
            
          }

        private static void refuel()
        {
            
            Console.Clear();
            Console.WriteLine(k_RefuelTitle);

            string licenseNumber = getLicenceNumberFromUser();

            if (!licenseExist(licenseNumber))
            {
                printGoingBackToMainMenuMsg();
                return;
            }

            Console.Clear();
            Console.WriteLine(k_RefuelTitle);

            float parsedAmount;

            while (true)
            {
                Console.WriteLine("Enter the number or liters to refuel:");
                string amount = Console.ReadLine();
                
                try
                {
                    parsedAmount = float.Parse(amount);
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("invalid term was entered. Use numbers only dude");
                }
            }

            Console.Clear();

            Console.WriteLine(string.Format(@"{0}
Select a type of fuel to refuel with:
{1}",
    k_RefuelTitle, 
    createQuestionaire(Garage.GetFuelOptions())));

            int input = getNumericValueFromUser(4);
            GarageLogic.Vehicle.eFuelType fuelTypeSelection = (GarageLogic.Vehicle.eFuelType) (input - 1);
      
            try
            {
                Ex03.GarageLogic.Garage.RefuelPetrol(licenseNumber, (GarageLogic.Vehicle.eFuelType) fuelTypeSelection, parsedAmount);
                printOperationSuccessMsg();
            }
             
            catch (Exception)
            {
                Console.WriteLine("Invalid amount fuel type was entered");
                promptAbort();
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
            Console.WriteLine(k_DisplayLicenseMessage);

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

            Console.WriteLine("Display Garage Licenses List:\n---------------------");

            if (licenseNumberList.Length == 0)
            {
                Console.WriteLine("We haven't found results for this filter :(");
                printGoingBackToMainMenuMsg();
                return;
            }

            Console.WriteLine(licenseNumberList);
            promptAbort();
        }

        private static void promptAbort()
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Hit the return key to go back to main menu");
            Console.WriteLine("------------------------------------------");
            Console.ReadKey();
        }

        private static void printGoingBackToMainMenuMsg()
        {
            Console.WriteLine(k_GoingBackToMainMenu);
            System.Threading.Thread.Sleep(4000);
        }    

        private static bool isTruckCargoDangerous()
        {
            showInsertHeader();
            Console.WriteLine(@"Is the truck carrying dangerous materials?
(1) Yes
(2) No");

            int userChoice = getNumericValueFromUser(2);

            return (userChoice == 1) ? true : false;

        }

        private static float getCurrentCargoWeight()
        {
            Console.Clear();
            showInsertHeader();
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
            showInsertHeader();
            char inputFromUser;
            int amountOfDoorsInCar;

            Console.WriteLine("Please enter the amount of doors in the car, ranges between 2-5:");
            inputFromUser = Console.ReadKey().KeyChar;
            
            while (!int.TryParse(inputFromUser.ToString(), out amountOfDoorsInCar) || amountOfDoorsInCar > 5 || amountOfDoorsInCar < 2)
            {
                Console.WriteLine("The range for doors in car is 2-5 (inclusive), please try again");
                inputFromUser = Console.ReadKey().KeyChar;
            }

            return amountOfDoorsInCar;

        }

        private static string getColorOfCarFromUser()
        {
            showInsertHeader();
            string[] colors = new string[4] { "White", "Black", "Green", "Red" };
            Console.WriteLine(string.Format(
@"Select the vehicle color:
(1) {0}
(2) {1}
(3) {2}
(4) {3}", colors[0], colors[1], colors[2], colors[3]));

            int UserChoiceOfColor = getNumericValueFromUser(4);

            return colors[UserChoiceOfColor - 1];
        }

        private static void showInsertHeader()
        {
            Console.Clear();
            Console.WriteLine(k_InsertVehicleHeaderMessage);
        }
 

        private static void initVehicleVarible(out string io_OwnerName, out string io_OwnerCell, out string io_Manufacturer, out string io_LicenseNumber, out string io_WheelManufacturer, out float io_CurrentAirPressure, out float io_CurrentAvailableEnergyInVehicle, bool isElectric)
        {
            io_OwnerName = getOwnerNameFromUser();
            io_OwnerCell = getCellNumberFromUser();
            io_LicenseNumber = getLicenceNumberFromUser();
            io_CurrentAirPressure = getCurrentAirPressure();
            io_Manufacturer = getManufacturers(out io_WheelManufacturer);
            io_CurrentAvailableEnergyInVehicle = getAmountOfEnergyLeftFromUser(isElectric);


        }

        private static float getCurrentAirPressure()
        {
            float currentAirPressure;
            string inputFromUser;
            Console.Clear();
            Console.WriteLine("Please enter the current air pressure for the wheels");
            inputFromUser = Console.ReadLine();

            while (!float.TryParse(inputFromUser, out currentAirPressure) || currentAirPressure < 0 )
            {
                Console.WriteLine("invalid input");
                inputFromUser = Console.ReadLine();
            }

            return currentAirPressure;
        }

        private static string getManufacturers(out string io_wheelManufacturer)
        {
            showInsertHeader();
            string manufacturer;
            Console.WriteLine("Please enter the name of the vehicle manufacturer:");
            manufacturer = Console.ReadLine();
            while (manufacturer.Length == 0)
            {
                Console.WriteLine("Please enter the manufacturer name:)");
                manufacturer = Console.ReadLine();
            }

            showInsertHeader();
            Console.WriteLine("Please enter the name of the wheel manufacturer:");
            io_wheelManufacturer = Console.ReadLine();
            while (io_wheelManufacturer.Length == 0)
            {
                Console.WriteLine("Please enter the manufacturers name:");
                io_wheelManufacturer = Console.ReadLine();
            }

            return manufacturer;
        }

        private static string getCellNumberFromUser()
        {
            showInsertHeader();
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
            showInsertHeader();
            string ownerName;
            Console.WriteLine("Please enter the name of the owner:");
            ownerName = Console.ReadLine();
            while (!isValidName(ownerName))
            {
                if (ownerName.Length == 0)
                {
                    Console.WriteLine("You must enter at least one letter");
                }
                else
                {
                    Console.WriteLine("Please enter a name using english letters only");
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
            showInsertHeader();
            float amountOfEnergyLeft;
            string inputFromUser;
            if (i_IsElectricVehicle)
            {
                Console.WriteLine("With how many charged hours you wish your battery to start with?");
            }
            else
            {
                Console.WriteLine("With how many liters you wish to start the fuel tank?");
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
            showInsertHeader();
            int engineVolume;
            string inputFromUser;

            Console.WriteLine("Please enter the engines volume:");
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
            showInsertHeader();
            Console.WriteLine(k_MotorCycleLicenseTypeMessage);
            int userChoiceOfLicense = getNumericValueFromUser(4);

            return userChoiceOfLicense;
        }

        private static int getNumericValueFromUser(int i_options)
        {
            int selection;
            char input;
            while (true)
            {
                input = Console.ReadKey().KeyChar;

                if (input > '0' + i_options || input < '1')
                {
                    Console.WriteLine();
                    Console.WriteLine(string.Format("Invalid option was entered. Please input a number in the range of 1-{0}", i_options));
                    continue;
                }
                else
                {
                    selection = input - '0';
                    break;
                }

            }
            Console.WriteLine();
            return selection;
            
        }

        private static string getLicenceNumberFromUser()
        {
            //showInsertHeader();
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
