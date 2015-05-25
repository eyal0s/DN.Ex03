using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{

	// put the init logic here. now "new" outside this class
	// put the list of supported vehicles here also

  

	class Factory
	{

		private const float k_MaxAirPressureCar = 31;
		private const int k_NumberOfWheels = 4;
		private string m_ColorOfCar;
		private int m_AmountOfDoors;
		//electric characteristics
		private const float k_CarMaxBatteryTime = 2.2f;
		//petrol characteristics
		private const float k_MaxTankLiter = 35f;
		private const Vehicle.Petrol.eFuelType k_FuelType = Petrol.eFuelType.Octan96;

		public static Car CreateCar(string i_Manufacturer, string i_LicenseNumber, string i_WheelManufacturer, float i_CurrentAvailableHours, string i_color, int i_NumOfDoors ){
			Car toReturn;
			if (i_IsElectric)
			{
				toReturn = new Car(i_Manufacturer, i_LicenseNumber, i_WheelManufacturer, i_CurrentAvailableHours, i_color, i_NumOfDoors, new Vehicle.Battery(i_CurrentAvailableHours, k_CarMaxBatteryTime));
				
			}
			else
			{

			}
			return toReturn;
		}
	}
}
