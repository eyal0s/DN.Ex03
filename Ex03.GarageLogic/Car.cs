using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class Car : Vehicle
    {
        private eColor m_ColorOfCar;
        private int m_AmountOfDoors;

        public Car(string i_Manufacturer, string i_LicenseNumber, string i_WheelManufacturer, float i_CurrentAvailableHours, int i_NumberOfWheels, float i_MaxAirPressure, eColor i_ColorOfCar, int i_NumOfDoors, FuelSource i_FuelSource) :
            base(i_Manufacturer, i_LicenseNumber, i_NumberOfWheels, i_MaxAirPressure, i_WheelManufacturer, i_FuelSource)
        {
          
            if (i_NumOfDoors > 5 || i_NumOfDoors < 2)
            {
                throw new ValueOutOfRangeException("Car can have between 2 and 5 doors");
            }

            m_ColorOfCar = i_ColorOfCar;
            m_AmountOfDoors = i_NumOfDoors;
        }


        public override string ToString()
        {
            return string.Format("{0}, Number of doors: {1}, Color of car: {2}", base.ToString(), m_AmountOfDoors, m_ColorOfCar);
        }

        public enum eColor
        {
            White,
            Black,
            Green,
            Red
        }
    }
}
