using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class Truck : Vehicle
    {
        private bool m_isCarryingHazardousMaterial;
        private float m_CurrentCarryingWeight;
         public Truck(string i_Manufacturer, string i_LicenseNumber, int i_NumberOfWeels, float i_MaxAirPressure, string i_WheelManufacturer, bool i_isDangerous, float i_CurrentCarryingWeight) :
            base(i_Manufacturer, i_LicenseNumber, i_NumberOfWeels, i_MaxAirPressure, i_WheelManufacturer)
        {
            m_CurrentCarryingWeight = i_CurrentCarryingWeight;
            m_isCarryingHazardousMaterial = i_isDangerous;
        }

    }
}
