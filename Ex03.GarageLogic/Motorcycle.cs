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
        //electric characteristics
        private const float k_MaxBatteryTime = 1.2f;
        //petrol characteristics
        private const float k_MaxTankLiter = 8f;
        private const Petrol.eFuelType k_FuelType = Petrol.eFuelType.Octan98;

        public Motorcycle(string i_Manufacturer, string i_LicenseNumber, string i_WheelManufacturer, eVehicleFuelSource i_FuelSource, float i_CurrentAvailableHours, eLicenseType i_LicenseType, int i_EngineVolume) :
            base(i_Manufacturer, i_LicenseNumber, k_NumberOfWheels, k_MaxAirPressureMotorcycle, i_WheelManufacturer)
        {
            m_EngineVolume = i_EngineVolume;
            m_LicenseType = i_LicenseType;

            // sets the fuel source according to car 
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
