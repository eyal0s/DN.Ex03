using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    protected class Wheel
    {

        private string m_Manufacturer;
        private float m_MaxAirPressure;
        private float m_CurrentAirPressure;

        public Wheel(string i_Manufacturer, float i_MaxAirPressure) 
        {
            m_MaxAirPressure = i_MaxAirPressure;
            m_Manufacturer = i_Manufacturer;
            m_CurrentAirPressure = 0;
        }

        public float MaxAirPressure
        {
            get
            {
                return m_MaxAirPressure;
            }

        }

        public float CurrentAirPressure 
        {
            get
            {
                return m_CurrentAirPressure;
            }
        }

        public string Manufacturer
        {
            get
            {
                return m_Manufacturer;
            }
        }

        public void InflateTire(float volume)
        {
            if ((volume + m_CurrentAirPressure) > m_MaxAirPressure)
            {
                throw new Exception();
            }
            else
            {
                m_CurrentAirPressure += volume;
            }
        }

    }
}
