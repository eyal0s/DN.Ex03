using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class Car : Vehicle
    {
        private string m_ColorOfCar;
        private int m_AmountOfDoors;

        public Car(string i_Manufacturer, string i_LicenseNumber, int i_NumberOfWeels, float i_MaxAirPressure, string i_WheelManufacturer, string i_ColorOfCar, int i_NumOfDoors) :
            base(i_Manufacturer, i_LicenseNumber, i_NumberOfWeels, i_MaxAirPressure, i_WheelManufacturer)
        {
            m_ColorOfCar = i_ColorOfCar;
            m_AmountOfDoors = i_NumOfDoors;
        }

        public override bool Equals(object obj)
        {
            bool result = false;
            Vehicle toCompare = obj as Vehicle;  
            if (toCompare != null)
            {
                result = (toCompare.GetHashCode() == this.GetHashCode());
            }
            return result;
        }

        public override string ToString()
        {
            return string.Format("{0}, Number of doors: {1}, Color of car: {2}", base.ToString, m_AmountOfDoors, m_ColorOfCar);
        }


    }
}
