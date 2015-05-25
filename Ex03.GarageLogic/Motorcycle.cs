using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class Motorcycle : Vehicle
    {
        private const float k_MaxAirPressureMotorcycle = 34;
        private const int k_NumberOfWheels = 2;
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        public Motorcycle(string i_Manufacturer, string i_LicenseNumber, string i_WheelManufacturer, float i_CurrentAvailableHours, FuelSource i_TypeOfFuel, eLicenseType i_LicenseType, int i_EngineVolume) :
            base(i_Manufacturer, i_LicenseNumber, k_NumberOfWheels, k_MaxAirPressureMotorcycle, i_WheelManufacturer, i_TypeOfFuel)
        {
            FuelT
            m_EngineVolume = i_EngineVolume;
            m_LicenseType = i_LicenseType;

        }

        public override string ToString()
        {
            return string.Format("{0}, License Type: {1}, EngineVolume: {2}", base.ToString(), m_LicenseType, m_EngineVolume);
        }

        public enum eLicenseType
        {
            A,
            A2,
            AB,
            B1
        }
    }

}
