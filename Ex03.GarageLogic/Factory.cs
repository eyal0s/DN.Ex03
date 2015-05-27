using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{

	// put the init logic here. now "new" outside this class
	// put the list of supported vehicles here also



	class Factory
	{

		// car charetaristics
		private const float k_CarMaxBatteryTime = 2.2f;
		private const float k_CarMaxTankLiter = 35f;
		private const float k_CarMaxAirPressureCar = 31;
		private const int k_CarNumberOfWheels = 4;
		private const Vehicle.eFuelType k_CarFuelType = Vehicle.eFuelType.Octan96;
        

		// motorcycle charetaristics     
		private const float k_MotoMaxBatteryTime = 1.2f;
		private const float k_MotoMaxTankLiter = 8f;
		private const float k_MotoBatteryMaxAirPressureCar = 34;
		private const float k_MotoPetrolMaxAirPressureCar = 31;
		private const int k_MotoNumberOfWheels = 2;
		private const Vehicle.eFuelType k_MotoFuelType = Vehicle.eFuelType.Octan98;

		// Truck charetaristics
		private const float k_TruckMaxTankLiter = 170f;
		private const float k_TruckMaxAirPressureCar = 25;
		private const int k_TruckNumberOfWheels = 16;
		private const Vehicle.eFuelType k_TruckFuelType = Vehicle.eFuelType.Soler;

        //public static Vehicle.FuelSource CreateEngine(eSupportedVehicleType i_VehicleType, bool i_isElectric)
        //{
        //    Vehicle.FuelSource fuelSourceForVehicle = null;
        //    foreach (eSupportedVehicleType supportedVehicle in Enum.GetValues(typeof(eSupportedVehicleType)))
        //    {
        //        if (i_VehicleType.Equals(supportedVehicle))
        //        {
        //            if (i_iselectric)
        //            {
        //                fuelsourceforvehicle = new vehicle.fuelsource(vehicle.efueltype.electricity, 5.5f, supportedvehicle.gettype().)
        //            }
        //            else
        //            {

        //            }
        //        }
		 
        //    }
		
        //    return fuelSourceForVehicle;
        //}

        public static Vehicle.FuelSource InitFuelSource(eSupportedVehicleType i_VehicleType, float i_CurrentEnergyLevel) 
        {
            Vehicle.FuelSource currentlyCreatedFuelSourc;

            switch (i_VehicleType)
            {
                case eSupportedVehicleType.MotorCycle:
                    currentlyCreatedFuelSourc = new Vehicle.FuelSource(k_MotoFuelType, i_CurrentEnergyLevel, k_MotoMaxTankLiter);
                    break;
                case eSupportedVehicleType.ElectricCycle:
                    currentlyCreatedFuelSourc = new Vehicle.FuelSource(Vehicle.eFuelType.Electricity, i_CurrentEnergyLevel, k_MotoMaxBatteryTime);
                    break;
                case eSupportedVehicleType.PetrolCar:
                    currentlyCreatedFuelSourc = new Vehicle.FuelSource(k_CarFuelType, i_CurrentEnergyLevel, k_CarMaxTankLiter);
                    break;
                case eSupportedVehicleType.ElectricCar:
                    currentlyCreatedFuelSourc = new Vehicle.FuelSource(Vehicle.eFuelType.Electricity, i_CurrentEnergyLevel, k_CarMaxBatteryTime);
                    break;
                case eSupportedVehicleType.Truck:
                    currentlyCreatedFuelSourc = new Vehicle.FuelSource(k_TruckFuelType, i_CurrentEnergyLevel, k_TruckMaxTankLiter);
                    break;
                default:                   
                    currentlyCreatedFuelSourc = new Vehicle.FuelSource(Vehicle.eFuelType.Octan95, 0, 0);
                    break;
            }

            return currentlyCreatedFuelSourc;
        
        }

		public static Motorcycle CreateMoto(string i_Manufacturer, string i_LicenseNumber, string i_WheelManufacturer, float i_CurrentAvailableHours, bool i_IsElectric, Motorcycle.eLicenseType i_LicenseType, int i_EngineVolume) 
		{
			Motorcycle motoCreatedForGarage;
			Vehicle.FuelSource fuelSourceOfMoto;

			if (i_IsElectric)
			{
				fuelSourceOfMoto = new Vehicle.FuelSource(Vehicle.eFuelType.Electricity, i_CurrentAvailableHours, k_MotoMaxBatteryTime);
			}
			else
			{
				fuelSourceOfMoto = new Vehicle.FuelSource(k_MotoFuelType, i_CurrentAvailableHours, k_MotoMaxTankLiter);
			}

			motoCreatedForGarage = new Motorcycle(i_Manufacturer, i_LicenseNumber, i_WheelManufacturer, i_CurrentAvailableHours, fuelSourceOfMoto, i_LicenseType, i_EngineVolume);

			return motoCreatedForGarage;
		}

		public static Car CreateCar(string i_Manufacturer, string i_LicenseNumber, string i_WheelManufacturer, float i_CurrentAvailableHours, Car.eColor i_CarColor, int i_NumOfDoors, bool i_IsElectric){
			Car carCreatedForGarage;
			Vehicle.FuelSource fuelSourceOfCar;
			if (i_IsElectric)
			{
				fuelSourceOfCar = new Vehicle.FuelSource(Vehicle.eFuelType.Electricity, i_CurrentAvailableHours, k_CarMaxBatteryTime);		
			}
			else
			{
				fuelSourceOfCar = new Vehicle.FuelSource(k_CarFuelType, i_CurrentAvailableHours, k_CarMaxTankLiter);
			}

			carCreatedForGarage = new Car(i_Manufacturer, i_LicenseNumber, i_WheelManufacturer, i_CurrentAvailableHours, k_CarNumberOfWheels, k_CarMaxAirPressureCar, i_CarColor, i_NumOfDoors, fuelSourceOfCar);
			return carCreatedForGarage;
		}

		public static Truck CreateTruck(string i_Manufacturer, string i_LicenseNumber, string i_WheelManufacturer, float i_CurrentAvailableHours, bool i_IsCarryingDangerousMaterial, float i_CurrentCarryingWeight) 
		{
			return new Truck(i_Manufacturer, i_LicenseNumber, i_WheelManufacturer, i_CurrentAvailableHours, k_TruckNumberOfWheels, k_TruckMaxAirPressureCar, InitFuelSource(eSupportedVehicleType.Truck, i_CurrentAvailableHours), i_IsCarryingDangerousMaterial, i_CurrentCarryingWeight);		
		}

        //public Vehicle createVehicle(eSupportedVehicleType i_TypeOfVehicle, string i_Manufacturer, string i_LicenseNumber, string i_WheelManufacturer, float i_CurrentAvailableHours, Car.eColor i_CarColor, int i_NumOfDoors, bool i_IsElectric, bool i_IsCarryingDangerousMaterial, float i_CurrentCarryingWeight) 
        //{
        //    Vehicle vehicleCreated;
        //    switch (i_TypeOfVehicle)
        //    {
        //        case eSupportedVehicleType.MotorCycle:

        //            break;
        //        case eSupportedVehicleType.ElectricCycle:
        //            break;
        //        case eSupportedVehicleType.PetrolCar:
        //            break;
        //        case eSupportedVehicleType.ElectricCar:
        //            break;
        //        case eSupportedVehicleType.Truck:
        //            new Truck(i_Manufacturer, i_LicenseNumber, i_WheelManufacturer, i_CurrentAvailableHours, i_IsCarryingDangerousMaterial, i_CurrentCarryingWeight);
        //            break;

        //        default:
        //            vehicleCreated = new Vehicle(i_Manufacturer, i_LicenseNumber, 0, 0, i_WheelManufacturer, null);
        //            break;
        //    }

        //    return vehicleCreated;
	
        //}

		public enum eSupportedVehicleType 
		{
			MotorCycle,
			ElectricCycle,
			PetrolCar,
			ElectricCar,
			Truck
		}
	}
}
