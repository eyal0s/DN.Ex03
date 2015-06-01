using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
	public class Factory
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
				case eSupportedVehicleType.Motorcycle:

                        fuelSourceToCreate = new Vehicle.FuelSource(k_MotoFuelType, i_CurrentEnergyLevel, k_MotoMaxTankLiter);
                    break;
                case eSupportedVehicleType.ElectricCycle:
                        fuelSourceToCreate = new Vehicle.FuelSource(Vehicle.eFuelType.Electricity, i_CurrentEnergyLevel, k_MotoMaxBatteryTime);
                    					
					break;
				case eSupportedVehicleType.ElectricCar:
                  
                        fuelSourceToCreate = new Vehicle.FuelSource(Vehicle.eFuelType.Electricity, i_CurrentEnergyLevel, k_CarMaxBatteryTime);
                    break;
                case eSupportedVehicleType.PetrolCar:
                        fuelSourceToCreate = new Vehicle.FuelSource(k_CarFuelType, i_CurrentEnergyLevel, k_CarMaxTankLiter);
                    
					break;

				case eSupportedVehicleType.Truck:

					fuelSourceToCreate = new Vehicle.FuelSource(k_TruckFuelType, i_CurrentEnergyLevel, k_TruckMaxTankLiter);
					break;

                default:
                    fuelSourceToCreate = null;
                    break;			
			}

			return fuelSourceToCreate;
		}

        public static Vehicle createVehicle(eSupportedVehicleType i_TypeOfVehicle, string i_Manufacturer, string i_LicenseNumber, string i_WheelManufacturer, float i_CurrentAirPressure, float i_CurrentAvailableHours)
        {
            Vehicle vehicleToBeCreated = null;
            Vehicle.FuelSource fuelSourceForNewVehicle = InitFuelSource(i_TypeOfVehicle, i_CurrentAvailableHours);

            switch (i_TypeOfVehicle)
            {
                    // string i_Manufacturer, string i_LicenseNumber, int i_NumberOfWeels, float i_MaxAirPressureWheel, float i_CurrentAirPressureWheel, string i_WheelManufacturer, FuelSource i_FuelOfVehicle
                case eSupportedVehicleType.Motorcycle:                   
                case eSupportedVehicleType.ElectricCycle:
                    vehicleToBeCreated = new Motorcycle(i_Manufacturer, i_LicenseNumber, k_MotoNumberOfWheels, k_MotoPetrolMaxAirPressureCar, i_CurrentAirPressure, i_WheelManufacturer, fuelSourceForNewVehicle);                   
                    break;
                case eSupportedVehicleType.PetrolCar:
                case eSupportedVehicleType.ElectricCar:                   
                    vehicleToBeCreated = new Car(i_Manufacturer, i_LicenseNumber, k_CarNumberOfWheels, k_CarMaxAirPressureCar, i_CurrentAirPressure, i_WheelManufacturer, fuelSourceForNewVehicle);
                    break;
               
                case eSupportedVehicleType.Truck:
                    vehicleToBeCreated = new Truck(i_Manufacturer, i_LicenseNumber, k_TruckNumberOfWheels, k_TruckMaxAirPressureCar, i_CurrentAirPressure, i_WheelManufacturer, fuelSourceForNewVehicle);
                    break;
            }

            return vehicleToBeCreated;
        }

		public enum eSupportedVehicleType 
		{
			Motorcycle = 1,
            ElectricCycle,
			PetrolCar,
            ElectricCar,
			Truck           
		}
	}
}
