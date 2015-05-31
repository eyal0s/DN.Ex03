using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class Motorcycle : Vehicle
    {
     
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        
        public Motorcycle(string i_Manufacturer, string i_LicenseNumber, int i_NumberOfWeels, float i_MaxAirPressure, float i_CurrentAirPressure, string i_WheelManufacturer, FuelSource i_TypeOfFuel, eLicenseType i_LicenseType, int i_EngineVolume) :
            base(i_Manufacturer, i_LicenseNumber, i_NumberOfWeels, i_MaxAirPressure,  i_CurrentAirPressure, i_WheelManufacturer, i_TypeOfFuel)
        {
            
            m_EngineVolume = i_EngineVolume;
            m_LicenseType = i_LicenseType;

        }

        public override string ToString()
        {
            return string.Format(
@"{0}
License Type: {1}
Engine Volume: {2}",
            base.ToString(), m_LicenseType, m_EngineVolume);
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
