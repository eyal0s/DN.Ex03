using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class Truck : Vehicle
    {
        private const float k_MaxAirPressureTruck = 25;
        private const int k_NumberOfWheels = 16;
        private bool m_isCarryingHazardousMaterial;
        private float m_CurrentCarryingWeight;
        private const float k_MaxLiterOfTank = 170;
        private const Vehicle.eFuelType k_FuelType = Vehicle.eFuelType.Soler;

         public Truck(string i_Manufacturer, string i_LicenseNumber, string i_WheelManufacturer, float i_CurrentAvailabeHours, int i_NumberOfWheels, float i_MaxAirPressure, FuelSource i_FuelOfVehicle,  bool i_isDangerous, float i_CurrentCarryingWeight) :
            base(i_Manufacturer, i_LicenseNumber, i_NumberOfWheels, i_MaxAirPressure, i_WheelManufacturer, i_FuelOfVehicle)
        {
            m_CurrentCarryingWeight = i_CurrentCarryingWeight;
            m_isCarryingHazardousMaterial = i_isDangerous;
        
        }

         public override string ToString()
         {
             string dangerInCargo = m_isCarryingHazardousMaterial ? "The Truck's cargo is hazardous" : "The truck's cargo is safe";
             return string.Format(
@"{0}
{1}
The truck cargo weight is: {2}",
    base.ToString(), dangerInCargo, m_CurrentCarryingWeight);
         }

    }
}
