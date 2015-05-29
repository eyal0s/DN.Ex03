using System;
using System.Collections.Generic;
using System.Text;

// This will be our API global interface
// BLA
namespace Ex03.GarageLogic
{
	public class Garage
	{

        static Vehicle temp = new Motorcycle("pointiac", "123", "yoko", 55F, new Vehicle.FuelSource(Vehicle.eFuelType.Electricity, 55F, 100F), Motorcycle.eLicenseType.A, 40);
        private static Dictionary<string, VehicleTicket> s_ListOfVehicleInGarage = new Dictionary<string, VehicleTicket>() {{"123", new VehicleTicket("Itay", "0542566789", temp)}};
        
        
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

        // cycle
		public static bool InsertNewVehicleToGarage(string i_Owner, string i_OwnerCellNumber, string i_Manufacturer, string i_LicenseNumber, string i_WheelManufacturer, float i_CurrentAvailableHours, bool i_isElectric, int i_licenseType, int i_EngineVolume)
		{      
            Factory.eSupportedVehicleType TypeOfVehicle = i_isElectric ? Factory.eSupportedVehicleType.ElectricCycle : Factory.eSupportedVehicleType.MotorCycle;
            Motorcycle.eLicenseType CycleLicenstType = (Motorcycle.eLicenseType) i_licenseType;
            Vehicle newVehicleForGarage = Factory.CreateMoto(i_Manufacturer, i_LicenseNumber, i_WheelManufacturer, i_CurrentAvailableHours, TypeOfVehicle, CycleLicenstType, i_EngineVolume);
			
            return tryToInsertVehicle(i_Owner, i_OwnerCellNumber, newVehicleForGarage);
		}
        
        // car
        public static bool InsertNewVehicleToGarage(string i_Owner, string i_OwnerCellNumber, string i_Manufacturer, string i_LicenseNumber, string i_WheelManufacturer, float i_CurrentAvailableHours, bool i_isElectric, string i_ColorOfCar, int i_AmountOfDoors)
        {
            Factory.eSupportedVehicleType TypeOfVehicle = i_isElectric ? Factory.eSupportedVehicleType.ElectricCar : Factory.eSupportedVehicleType.PetrolCar;        
            Car.eColor carColor = (Car.eColor) Enum.Parse(typeof(Car.eColor), i_ColorOfCar);
            Vehicle newVehicleForGarage = Factory.CreateCar(i_Manufacturer, i_LicenseNumber, i_WheelManufacturer, i_CurrentAvailableHours, TypeOfVehicle, carColor, i_AmountOfDoors);
			
            return tryToInsertVehicle(i_Owner, i_OwnerCellNumber, newVehicleForGarage);
        }
        
        // truck
        public static bool InsertNewVehicleToGarage(string i_Owner, string i_OwnerCellNumber, string i_Manufacturer, string i_LicenseNumber, string i_WheelManufacturer, float i_CurrentAvailableHours, bool i_IsCarryingDangerousMaterials, float i_CurrentCarryingWeight)
        {
            Factory.eSupportedVehicleType typeOfVehicle = Factory.eSupportedVehicleType.Truck;
            Vehicle newVehicleForGarage = Factory.CreateTruck(i_Manufacturer, i_LicenseNumber, i_WheelManufacturer, i_CurrentAvailableHours, typeOfVehicle, i_IsCarryingDangerousMaterials, i_CurrentCarryingWeight);
			
            return tryToInsertVehicle(i_Owner, i_OwnerCellNumber, newVehicleForGarage);
        }

        // general vehicle
        public static bool InsertNewVehicleToGarage(string i_Owner, string i_OwnerCellNumber, string i_Manufacturer, string i_LicenseNumber, string i_WheelManufacturer, float i_CurrentAvailableHours)
        {
            throw new NotImplementedException();
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
{3}" ,
     m_NameOfOwner, m_CellOfOwner, statusOfVehicle, m_VehicleInGarage.ToString());
            }

		}

		public static void CheckExistenceOfVehicle(string i_LicenseNumber)
		{
			if (!s_ListOfVehicleInGarage.ContainsKey(i_LicenseNumber))
				throw new ArgumentException();

		}

     
		public enum eVehicleStatus
		{
			InRepair = 1,
			Done = 2,
			Paid = 3
		}
	}
}
