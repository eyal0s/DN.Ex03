using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class Truck : Vehicle
    {
        private bool m_isCarryingHazardousMaterial;
        private float m_CurrentCarryingWeight;

         public Truck(string i_Manufacturer, string i_LicenseNumber, string i_WheelManufacturer, float i_CurrentAvailabeHours,  bool i_isDangerous, float i_CurrentCarryingWeight) :
            base(i_Manufacturer, i_LicenseNumber, k_NumberOfWheels, k_MaxAirPressureTruck, i_WheelManufacturer)
        {
            m_CurrentCarryingWeight = i_CurrentCarryingWeight;
            m_isCarryingHazardousMaterial = i_isDangerous;
            Petrol m_FuelSrc = new Petrol(k_FuelType, i_CurrentAvailabeHours, k_MaxLiterOfTank);
        }

    }
}
