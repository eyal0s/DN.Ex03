using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageManagementConsoleUI;
using Ex03.GarageLogic;

namespace Ex03.GarageManagementConsoleUI
{
    public class Program
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

        private const string k_PromptBackMessage = @"------------------------------------------
Hit the return key to go back to main menu
------------------------------------------";

        private const string k_InsertVehicleHeaderMessage = @"Enter a new vehicle:
---------------------";

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
            Console.WriteLine(string.Format(
@"{0}
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
            
            int selectionOfUserForVehicleType;

            showInsertHeader();
            Console.WriteLine(string.Format(
@"{0}
Please choose one of our supported vehicle:
{1}", 
    k_InsertVehicleHeaderMessage,
    createQuestionaire(Garage.GetSupportedVehicles())));

            selectionOfUserForVehicleType = getNumericValueFromUser(Garage.GetSupportedVehicles().Count);      

            initVehicleVarible(out ownerName, out ownerCell, out licenseNumber, out manufacturer, out wheelManufacturer, out currentAirPressure, out currentAvailableEnergyInVehicle);
            bool wasCarInserted = false;
            try
            {
                wasCarInserted = Garage.InsertNewVehicleToGarage(selectionOfUserForVehicleType, ownerName, ownerCell, manufacturer, licenseNumber, wheelManufacturer, currentAirPressure, currentAvailableEnergyInVehicle);
                if (wasCarInserted)
                {
                    Dictionary<string, int> questionSpecification = Garage.getQuestionForVehicle(licenseNumber);
                    List<string> answersFromUser = getAnswerForVehicleSpece(questionSpecification);
                    Garage.UpdateSpecs(licenseNumber, answersFromUser);
                    printOperationSuccessMsg();    
                }
                else
                {
                    Console.WriteLine(string.Format("Sorry, a vehicle with {0} license number already exist in the garage", licenseNumber));
                }
            }
            catch (ArgumentException)
            {

            }
            catch (FormatException )
            {
                
            }
            catch (ValueOutOfRangeException)
            { 
                
            }
            finally
            {
                if (!wasCarInserted)
                {
                    Console.WriteLine("Could not insert new vehicle");
                    promptAbort();
                }
            }
        }

        private static List<string> getAnswerForVehicleSpece(Dictionary<string, int> questionSpecification)
        {
            Console.Clear();
            List<string> answerFromUser = new List<string>();
            string currentAnswerFromUser;

            foreach (string currentQuestion in questionSpecification.Keys)
            {
                Console.WriteLine(string.Format("{0}\n{1}", k_InsertVehicleHeaderMessage, currentQuestion));

                if (questionSpecification[currentQuestion] > 0)
                {
                    answerFromUser.Add(getNumericValueFromUser(questionSpecification[currentQuestion]).ToString());
                }
                else
                {
                    currentAnswerFromUser = Console.ReadLine();
                    answerFromUser.Add(currentAnswerFromUser);
                }

                Console.Clear();
            }

            return answerFromUser;
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
            Console.WriteLine(k_RefuelTitle);

            string licenseNumber = getLicenceNumberFromUser();
            if (!licenseExist(licenseNumber))
            {
                printGoingBackToMainMenuMsg();              
            }
            else
            {
                Console.WriteLine("How many hours you wish to recharge?");
                string amountToAdd = Console.ReadLine();
                refueilingAction(0, licenseNumber, amountToAdd);         
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
            }
            else
            {
                Console.Clear();
                Console.WriteLine(k_RefuelTitle);
                Console.WriteLine("Enter the number or liters to refuel:");
                string amountToAdd = Console.ReadLine();

                Console.Clear();
                Console.WriteLine(string.Format(
@"{0}
Select a type of fuel to refuel with:
{1}",
        k_RefuelTitle,
        createQuestionaire(Garage.GetFuelOptions())));

                int choiceOfFuelType = getNumericValueFromUser(4);
                refueilingAction(choiceOfFuelType, licenseNumber, amountToAdd);
            }         
        }

        private static void refueilingAction(int i_ChoiceOfFuelType, string i_LicenseNumber, string i_AmountToAdd)
        {
            bool refuelSucceeded = false;

            try
            {
                Ex03.GarageLogic.Garage.Refuel(i_LicenseNumber, i_ChoiceOfFuelType, i_AmountToAdd);
                refuelSucceeded = true;
            }
            catch (ValueOutOfRangeException outOfRangeException)
            {
                Console.WriteLine(string.Format("error, cant choose values who are not in range {0}-{1}", outOfRangeException.m_MinValue, outOfRangeException.m_MaxValue));
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Incompatible fuel type!");
            }
            catch (FormatException)
            {
                Console.WriteLine("value has to be numeric");                
            }
            catch (Exception)
            {
                Console.WriteLine("There has been an error, please enter data carefuly");                
            }
            finally
            {
                if (refuelSucceeded)
                {
                    printOperationSuccessMsg();
                }
                else
                {
                    promptAbort();
                }
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
            }
            else
            {
                Ex03.GarageLogic.Garage.WheelPump(licensenNumber);
                printOperationSuccessMsg();
            }   
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
            Console.WriteLine(k_PromptBackMessage);
            Console.ReadKey();
        }

        private static void printGoingBackToMainMenuMsg()
        {
            Console.WriteLine(k_GoingBackToMainMenu);
            System.Threading.Thread.Sleep(4000);
        }    

        private static void showInsertHeader()
        {
            Console.Clear();
            Console.WriteLine(k_InsertVehicleHeaderMessage);
        }

        private static void initVehicleVarible(out string io_OwnerName, out string io_OwnerCell, out string io_LicenseNumber, out string io_Manufacturer, out string io_WheelManufacturer, out float io_CurrentAirPressure, out float io_CurrentAvailableEnergyInVehicle)
        {
            io_OwnerName = getOwnerNameFromUser();
            io_OwnerCell = getCellNumberFromUser();

            Console.Clear();
            Console.WriteLine(k_InsertVehicleHeaderMessage);

            io_LicenseNumber = getLicenceNumberFromUser();
            io_CurrentAirPressure = getCurrentAirPressure();
            io_Manufacturer = getManufacturers(out io_WheelManufacturer);
            io_CurrentAvailableEnergyInVehicle = getAmountOfEnergyLeftFromUser();
        }

        private static float getCurrentAirPressure()
        {
            float currentAirPressure;
            string inputFromUser;
            Console.Clear();
            Console.WriteLine(string.Format("{0}\nPlease enter the current air pressure for the wheels", k_InsertVehicleHeaderMessage));
            inputFromUser = Console.ReadLine();

            while (!float.TryParse(inputFromUser, out currentAirPressure) || currentAirPressure < 0)
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

        private static float getAmountOfEnergyLeftFromUser()
        {
            showInsertHeader();
            float amountOfEnergyLeft;
            string inputFromUser;
            
            Console.WriteLine("What is the current energy level?");

            inputFromUser = Console.ReadLine();
            while (!float.TryParse(inputFromUser, out amountOfEnergyLeft))
            {
                Console.WriteLine("Please enter a numeric value");
                inputFromUser = Console.ReadLine();
            }

            return amountOfEnergyLeft;
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
            string input;
            Console.WriteLine("Please enter a vehicle license plate number:");
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
