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

		public Car(string i_Manufacturer, string i_LicenseNumber, string i_WheelManufacturer, eVehicleFuelSource i_FuelSource, float i_CurrentAvailableHours, string i_ColorOfCar, int i_NumOfDoors) :
			base(i_Manufacturer, i_LicenseNumber, k_NumberOfWheels, k_MaxAirPressureCar, i_WheelManufacturer)
		{
			m_ColorOfCar = i_ColorOfCar;
			m_AmountOfDoors = i_NumOfDoors;


            if (i_FuelSource.Equals(eVehicleFuelSource.Electric))
            {
                Battery m_FuelSrc = new Battery(i_FuelSource, i_CurrentAvailableHours, k_MaxTankLiter);  
            }
            else
            {
                Petrol m_FuelSrc = new Petrol(i_FuelSource, i_CurrentAvailableHours, k_MaxTankLiter, k_FuelType);
            }
		
		}


		public override string ToString()
		{
			return string.Format("{0}, Number of doors: {1}, Color of car: {2}", base.ToString() , m_AmountOfDoors, m_ColorOfCar);
		}


	}
}
