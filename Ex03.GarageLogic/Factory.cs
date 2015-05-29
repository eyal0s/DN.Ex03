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



		public static Vehicle.FuelSource InitFuelSource(eSupportedVehicleType i_VehicleType, float i_CurrentEnergyLevel) 
		{
			Vehicle.FuelSource fuelSourceToCreate;

			switch (i_VehicleType)
			{
				case eSupportedVehicleType.MotorCycle:
					fuelSourceToCreate = new Vehicle.FuelSource(k_MotoFuelType, i_CurrentEnergyLevel, k_MotoMaxTankLiter);
					break;
				case eSupportedVehicleType.ElectricCycle:
					fuelSourceToCreate = new Vehicle.FuelSource(Vehicle.eFuelType.Electricity, i_CurrentEnergyLevel, k_MotoMaxBatteryTime);
					break;
				case eSupportedVehicleType.PetrolCar:
					fuelSourceToCreate = new Vehicle.FuelSource(k_CarFuelType, i_CurrentEnergyLevel, k_CarMaxTankLiter);
					break;
				case eSupportedVehicleType.ElectricCar:
					fuelSourceToCreate = new Vehicle.FuelSource(Vehicle.eFuelType.Electricity, i_CurrentEnergyLevel, k_CarMaxBatteryTime);
					break;
				case eSupportedVehicleType.Truck:
					fuelSourceToCreate = new Vehicle.FuelSource(k_TruckFuelType, i_CurrentEnergyLevel, k_TruckMaxTankLiter);
					break;
				default:                   
					fuelSourceToCreate = new Vehicle.FuelSource(Vehicle.eFuelType.Octan95, 0, 0);
					break;
			}

			return fuelSourceToCreate;
		}

		public static Motorcycle CreateMoto(string i_Manufacturer, string i_LicenseNumber, string i_WheelManufacturer, float i_CurrentAvailableHours, eSupportedVehicleType i_TypeOfVehicle, Motorcycle.eLicenseType i_LicenseType, int i_EngineVolume) 
		{
			Motorcycle motoCreatedForGarage;
			Vehicle.FuelSource fuelSourceOfMoto = InitFuelSource(i_TypeOfVehicle, i_CurrentAvailableHours);
			motoCreatedForGarage = new Motorcycle(i_Manufacturer, i_LicenseNumber, i_WheelManufacturer, i_CurrentAvailableHours, fuelSourceOfMoto, i_LicenseType, i_EngineVolume);

			return motoCreatedForGarage;
		}

		public static Car CreateCar(string i_Manufacturer, string i_LicenseNumber, string i_WheelManufacturer, float i_CurrentAvailableHours, eSupportedVehicleType i_TypeOfVehicle,  Car.eColor i_CarColor, int i_NumOfDoors){
			Car carCreatedForGarage;
			Vehicle.FuelSource fuelSourceOfCar = InitFuelSource(i_TypeOfVehicle, i_CurrentAvailableHours);
			carCreatedForGarage = new Car(i_Manufacturer, i_LicenseNumber, i_WheelManufacturer, i_CurrentAvailableHours, k_CarNumberOfWheels, k_CarMaxAirPressureCar, i_CarColor, i_NumOfDoors, fuelSourceOfCar);
			return carCreatedForGarage;
		}

		public static Truck CreateTruck(string i_Manufacturer, string i_LicenseNumber, string i_WheelManufacturer, float i_CurrentAvailableHours, eSupportedVehicleType i_TypeOfVehicle, bool i_IsCarryingDangerousMaterial, float i_CurrentCarryingWeight) 
		{
			Vehicle.FuelSource truckFuelSourc = InitFuelSource(i_TypeOfVehicle, i_CurrentAvailableHours);
			return new Truck(i_Manufacturer, i_LicenseNumber, i_WheelManufacturer, i_CurrentAvailableHours, k_TruckNumberOfWheels, k_TruckMaxAirPressureCar, InitFuelSource(eSupportedVehicleType.Truck, i_CurrentAvailableHours), i_IsCarryingDangerousMaterial, i_CurrentCarryingWeight);		
		}

		//public Vehicle createVehicle(eSupportedVehicleType i_TypeOfVehicle, string i_Manufacturer, string i_LicenseNumber, string i_WheelManufacturer, float i_CurrentAvailableHours, params object[] i_SpecificationForVehciel)
		//{
		//    Vehicle vehicleToBeCreated;
		//    Vehicle.FuelSource fuelSourceForNewVehicle = InitFuelSource(i_TypeOfVehicle, i_CurrentAvailableHours);
			
		//    switch (i_TypeOfVehicle)
		//    {
		//        case eSupportedVehicleType.MotorCycle:
		//            vehicleToBeCreated = new Motorcycle(i_Manufacturer, i_LicenseNumber, i_WheelManufacturer, i_CurrentAvailableHours, fuelSourceForNewVehicle, (Motorcycle.eFuelType)i_SpecificationForVehciel[0], (int)i_SpecificationForVehciel[1]);
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
		//            vehicleToBeCreated = new Vehicle(i_Manufacturer, i_LicenseNumber, 0, 0, i_WheelManufacturer, null);
		//            break;
		//    }

		//    return vehicleToBeCreated;

		//}

		public enum eSupportedVehicleType 
		{
			MotorCycle,
			ElectricCycle,
			PetrolCar,
			ElectricCar,
			Truck,           
		}
	}
}
