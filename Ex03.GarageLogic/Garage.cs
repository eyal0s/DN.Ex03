using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{ 
	public class Garage
	{    
        public static Vehicle Temp = new Motorcycle("pointiac", "123", 2, 31, 20, "yoko", new Vehicle.FuelSource(Vehicle.eFuelType.Electricity, 55F, 100F), Motorcycle.eLicenseType.A, 40);
        private static Dictionary<string, VehicleTicket> s_ListOfVehicleInGarage = new Dictionary<string, VehicleTicket>() {{"123", new VehicleTicket("Itay", "0542566789", Temp)}};

        public static Dictionary<string, int> getQuestionForVehicle(string i_LicenseNumber)
        {
            return s_ListOfVehicleInGarage[i_LicenseNumber].Vehicle.getQuestionair();
        }

        public static void UpdateSpecs(string i_LicenseNumber, List<string> i_AnswersFromUser) 
        {
             s_ListOfVehicleInGarage[i_LicenseNumber].Vehicle.InitVehicle(i_AnswersFromUser);
        }

        private static bool tryToInsertVehicle(string i_Owner, string i_OwnerCellNumber, Vehicle i_VehicleToInsert)
        {   
            bool vehicleWasInsertedToGarage = false;

			if (s_ListOfVehicleInGarage.ContainsKey(i_VehicleToInsert.LicenseNumer))
			{
				s_ListOfVehicleInGarage[i_VehicleToInsert.LicenseNumer].Status = eVehicleStatus.InRepair;
			}
			else
			{
				VehicleTicket newTicket = new VehicleTicket(i_Owner, i_OwnerCellNumber, i_VehicleToInsert);
				s_ListOfVehicleInGarage.Add(i_VehicleToInsert.LicenseNumer, newTicket);
				vehicleWasInsertedToGarage = true;
			}

			return vehicleWasInsertedToGarage;
        }

        // general vehicle
        public static bool InsertNewVehicleToGarage(int i_TypeOfVehciel, string i_Owner, string i_OwnerCellNumber, string i_Manufacturer, string i_LicenseNumber, string i_WheelManufacturer, float i_CurrentAirPressureInWheel, float i_CurrentAvailableHours)
        {
            bool vehicleWasInsertedToGarage = false;
            Factory.eSupportedVehicleType typeOfVehicle = (Factory.eSupportedVehicleType) i_TypeOfVehciel;

            if (s_ListOfVehicleInGarage.ContainsKey(i_LicenseNumber))
            {               
                s_ListOfVehicleInGarage[i_LicenseNumber].Status = eVehicleStatus.InRepair;
            }
            else
            {
                Vehicle newlyVehicleToInsert = Factory.createVehicle(typeOfVehicle, i_Manufacturer, i_LicenseNumber, i_WheelManufacturer, i_CurrentAirPressureInWheel, i_CurrentAvailableHours);
                VehicleTicket newTicket = new VehicleTicket(i_Owner, i_OwnerCellNumber, newlyVehicleToInsert);
                s_ListOfVehicleInGarage.Add(newlyVehicleToInsert.LicenseNumer, newTicket);
                vehicleWasInsertedToGarage = true;           
            }

            return vehicleWasInsertedToGarage;       
        }

		public static string DisplayAllLicenseNumber(params eVehicleStatus[] i_Filter) 
		{
			StringBuilder listOfLicenses = new StringBuilder();
			bool addToList = false;
			
			foreach (string currentLicenseNumber in s_ListOfVehicleInGarage.Keys)
			{
				eVehicleStatus currentVehicleStatus = s_ListOfVehicleInGarage[currentLicenseNumber].Status;
				foreach (eVehicleStatus filterStatus in i_Filter)
				{
					if (filterStatus == currentVehicleStatus)
					{
						addToList = true;
						break;
					}
				}

				if (addToList)
				{                    
					listOfLicenses.AppendLine(currentLicenseNumber);
					addToList = false;
				}
			}

			return listOfLicenses.ToString();
		}

		public static void ChangeStatusOfVehicle(string i_LicenseNumber, eVehicleStatus i_NewStatus) 
		{
			CheckExistenceOfVehicle(i_LicenseNumber);
			s_ListOfVehicleInGarage[i_LicenseNumber].Status = i_NewStatus;
		}

		public static void WheelPump(string i_LicenseNumber)
		{
			CheckExistenceOfVehicle(i_LicenseNumber);
			List<Vehicle.Wheel> wheelsOfVehicle = s_ListOfVehicleInGarage[i_LicenseNumber].Vehicle.Wheels;

			foreach (Vehicle.Wheel wheelToPump in wheelsOfVehicle)
			{
				float amountOfAirNeeded = wheelToPump.MaxAirPressure - wheelToPump.CurrentAirPressure;
				wheelToPump.InflateTire(amountOfAirNeeded);
			}                
		}

		public static void Refuel(string i_LicenseNumber, int i_UserChoiceOfFuelType, string i_AmountToAdd)
		{
			CheckExistenceOfVehicle(i_LicenseNumber);
            Vehicle.eFuelType fuelTypeSelection;

            if (i_UserChoiceOfFuelType > 0 )
            {
                fuelTypeSelection = (Vehicle.eFuelType)i_UserChoiceOfFuelType;
            }
            else
            {
                fuelTypeSelection = Vehicle.eFuelType.Electricity;
            }

			Vehicle.FuelSource fuelSourceToRefuel = s_ListOfVehicleInGarage[i_LicenseNumber].Vehicle.FuelSrc;            
            fuelSourceToRefuel.Refuel(fuelTypeSelection, i_AmountToAdd);
		}

		public static string DisplayFullSpecOfVehicle(string i_LicenseNumber)
		{
			CheckExistenceOfVehicle(i_LicenseNumber);
			return s_ListOfVehicleInGarage[i_LicenseNumber].ToString();
		}

        public static List<string> GetVehicleStatusOptions()
        {
            List<string> possibleStatusList = new List<string>();

            foreach (eVehicleStatus possibleStatus in Enum.GetValues(typeof(eVehicleStatus)))
            {
                possibleStatusList.Add(possibleStatus.ToString());
            }

            return possibleStatusList;
        }

        public static List<string> GetFuelOptions()
        {
            List<string> possibleFuelList = new List<string>();

            foreach (Vehicle.eFuelType currentFuelType in Enum.GetValues(typeof(Vehicle.eFuelType)))
            {
                if (currentFuelType.Equals(Vehicle.eFuelType.Electricity))
                {
                    continue; 
                }

                possibleFuelList.Add(currentFuelType.ToString());
            }

            return possibleFuelList;
        }

        public static List<string> GetSupportedVehicles() 
        {
            List<string> possibleFuelList = new List<string>();

            foreach (Factory.eSupportedVehicleType supportedVehicle in Enum.GetValues(typeof(Factory.eSupportedVehicleType)))
            {
                possibleFuelList.Add(supportedVehicle.ToString());
            }

            return possibleFuelList;
        }

		public class VehicleTicket
		{
			private string m_NameOfOwner;
			private string m_CellOfOwner;
			private Vehicle m_VehicleInGarage;
			private eVehicleStatus m_CurrentStatusOfVehicle;

			public VehicleTicket(string i_NameOfOwner, string i_CellOfOwner, Vehicle i_NewVehicleInGarage)
			{
				m_NameOfOwner = i_NameOfOwner;
				m_CellOfOwner = i_CellOfOwner;
				m_CurrentStatusOfVehicle = eVehicleStatus.InRepair;
				m_VehicleInGarage = i_NewVehicleInGarage;                
			}

			public string Owner 
			{
				get 
				{
					return m_NameOfOwner;
				}

				set 
				{
					m_NameOfOwner = value;
				}
			}

			public eVehicleStatus Status 
			{
				get
				{
					return m_CurrentStatusOfVehicle;
				}

				set
				{
					m_CurrentStatusOfVehicle = value;
				}
			}

			public Vehicle Vehicle
			{ 
				get
				{
					return m_VehicleInGarage;
				}
			}

            public override string ToString()
            {
                string statusOfVehicle = m_CurrentStatusOfVehicle == eVehicleStatus.InRepair ? "In Repair" : m_CurrentStatusOfVehicle.ToString();

                return string.Format(
@"Owner of vehicle: {0}
Owner's number: {1}
Status of vehicle: {2}
{3}",
     m_NameOfOwner,
     m_CellOfOwner,
     statusOfVehicle,
     m_VehicleInGarage.ToString());
            }
		}

		public static void CheckExistenceOfVehicle(string i_LicenseNumber)
		{
            if (!s_ListOfVehicleInGarage.ContainsKey(i_LicenseNumber))
            { 
				throw new ArgumentException();
            }
		}
     
		public enum eVehicleStatus
		{
			InRepair = 1,
			Done,
			Paid 
		}
	}
}
