﻿using System;
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
        private const Vehicle.Petrol.eFuelType k_CarFuelType = Vehicle.Petrol.eFuelType.Octan96;

        // motorcycle charetaristics
        private const float k_MotoMaxBatteryTime = 1.2f;
        private const float k_MotoMaxTankLiter = 8f;
        private const float k_MotoBatteryMaxAirPressureCar = 34;
        private const float k_MotoPetrolMaxAirPressureCar = 31;
        private const int k_MotoNumberOfWheels = 2;
        private const Vehicle.Petrol.eFuelType k_MotoFuelType = Vehicle.Petrol.eFuelType.Octan98;

        // Truck charetaristics
        private const float k_TruckMaxTankLiter = 170f;
        private const float k_TruckMaxAirPressureCar = 25;
        private const int k_TruckNumberOfWheels = 16;
        private const Vehicle.Petrol.eFuelType k_TruckFuelType = Vehicle.Petrol.eFuelType.Soler;



        public static Car CreateCar(string i_Manufacturer, string i_LicenseNumber, string i_WheelManufacturer, float i_CurrentAvailableHours, Car.eColor i_color, int i_NumOfDoors, bool i_IsElectric){
            Car car;
            if (i_IsElectric)
	        {
                car = new Car(i_Manufacturer, i_LicenseNumber, i_WheelManufacturer, i_CurrentAvailableHours, k_CarNumberOfWheels, k_CarMaxAirPressureCar, i_color, i_NumOfDoors, new Vehicle.Battery(i_CurrentAvailableHours, k_CarMaxBatteryTime));
	        }
            else
	        {
                car = new Car(i_Manufacturer, i_LicenseNumber, i_WheelManufacturer, i_CurrentAvailableHours, k_CarNumberOfWheels, k_CarMaxAirPressureCar, i_color, i_NumOfDoors, new Vehicle.Petrol(k_CarFuelType, i_CurrentAvailableHours, k_CarMaxTankLiter);
	        }
            return car;
        }
    }
}
