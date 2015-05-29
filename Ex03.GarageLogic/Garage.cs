using System;
using System.Collections.Generic;
using System.Text;

// This will be our API global interface
// BLA
namespace Ex03.GarageLogic
{
	public class Garage
	{
		private static Dictionary<string, VehicleTicket> s_ListOfVehicleInGarage = new Dictionary<string,VehicleTicket>();


		public static bool InsertNewVehicleToGarage(string i_Owner, string i_OwnerCellNumber, Vehicle i_VehicleToInsertGarage)
		{
			bool vehicleWasInsertedToGarage = false;

			if (s_ListOfVehicleInGarage.ContainsKey(i_VehicleToInsertGarage.LicenseNumer))
			{
				s_ListOfVehicleInGarage[i_VehicleToInsertGarage.LicenseNumer].Status = eVehicleStatus.InRepair;

			}
			else
			{
				VehicleTicket newTicket = new VehicleTicket(i_Owner, i_OwnerCellNumber, i_VehicleToInsertGarage);
				s_ListOfVehicleInGarage.Add(i_VehicleToInsertGarage.LicenseNumer, newTicket);
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

		public static void RefuelPetrol(string i_LicenseNumber, Vehicle.eFuelType i_FuelType, float i_AmountToAdd)
		{
			CheckExistenceOfVehicle(i_LicenseNumber);
			Vehicle.FuelSource fuelSourceToRefuel = s_ListOfVehicleInGarage[i_LicenseNumber].Vehicle.FuelSrc;
			fuelSourceToRefuel.Refuel(i_FuelType, i_AmountToAdd);
			
		}

		public static void RefuelBattery(string i_LicenseNumber, float i_AmountToAdd) 
		{
			CheckExistenceOfVehicle(i_LicenseNumber);
			Vehicle.FuelSource fuelSourceToRefuel = s_ListOfVehicleInGarage[i_LicenseNumber].Vehicle.FuelSrc;
			fuelSourceToRefuel.Refuel(Vehicle.eFuelType.Electricity, i_AmountToAdd);
		}

		public static string DisplayFullSpecOfVehicle(string i_LicenseNumber)
		{
			CheckExistenceOfVehicle(i_LicenseNumber);
			return s_ListOfVehicleInGarage[i_LicenseNumber].ToString();
		}

		protected class VehicleTicket
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
                return string.Format("Owner of vehicle: {0}, Owner's number: {1}, Status of vehicle: {3}\n{4}" ,
                    m_NameOfOwner, m_CellOfOwner, m_CurrentStatusOfVehicle, m_VehicleInGarage.ToString());
            }

		}

		public static void CheckExistenceOfVehicle(string i_LicenseNumber)
		{
			if (!s_ListOfVehicleInGarage.ContainsKey(i_LicenseNumber))
				throw new ArgumentException();

		}

        public static string getCurrentlySupportedVehicle()
        {
            foreach (var item in collection)
            {
                    
            }
            
            
        }

        public static getVehicleSupported()
        {
            Type sourceToAllVehicle = typeof(Vehicle);
            Type[] allSupportedVehicle = sourceToAllVehicle.Assembly.GetTypes();
        }
		public enum eVehicleStatus
		{
			InRepair = 1,
			Done = 2,
			Paid = 3
		}
	}
}
