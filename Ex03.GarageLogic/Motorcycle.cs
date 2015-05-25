using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class Motorcycle : Vehicle
    {
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        public Motorcycle(string i_Manufacturer, string i_LicenseNumber, string i_WheelManufacturer, bool i_IsElectric, float i_CurrentAvailableHours, eLicenseType i_LicenseType, int i_EngineVolume) :
            base(i_Manufacturer, i_LicenseNumber, k_NumberOfWheels, k_MaxAirPressureMotorcycle, i_WheelManufacturer)
        {
            m_EngineVolume = i_EngineVolume;
            m_LicenseType = i_LicenseType;
            if (i_IsElectric)
            {
                Battery m_FuelSrc = new Battery(i_CurrentAvailableHours, k_MaxBatteryTime);
            }
            else
            {
                Petrol m_FuelSrc = new Petrol(k_FuelType, i_CurrentAvailableHours, k_MaxTankLiter);
            }

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
