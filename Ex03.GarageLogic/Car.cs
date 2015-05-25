using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
	class Car : Vehicle
	{
		private const float k_MaxAirPressureCar = 31;
		private const int k_NumberOfWheels = 4;
		private string m_ColorOfCar;
		private int m_AmountOfDoors;
		 //electric characteristics
		private const float k_MaxBatteryTime = 2.2f;
		//petrol characteristics
		private const float k_MaxTankLiter = 35f;
        private const Petrol.eFuelType k_FuelType = Petrol.eFuelType.Octan96;

		public Car(string i_Manufacturer, string i_LicenseNumber, string i_WheelManufacturer, float i_CurrentAvailableHours, string i_ColorOfCar, int i_NumOfDoors, FuelSource i_FuelSource) :
			base(i_Manufacturer, i_LicenseNumber, k_NumberOfWheels, k_MaxAirPressureCar, i_WheelManufacturer , i_FuelSource)
		{         
			m_ColorOfCar = i_ColorOfCar;
			m_AmountOfDoors = i_NumOfDoors;		
		}


		public override string ToString()
		{
			return string.Format("{0}, Number of doors: {1}, Color of car: {2}", base.ToString() , m_AmountOfDoors, m_ColorOfCar);
		}


	}
}
