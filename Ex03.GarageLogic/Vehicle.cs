using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    // father object for all other types of veihcles
    protected class Vehicle
    {
        private string m_Manufacturer;
        private string m_LicenseNumber;
        private float m_PercentageOfEnergyLeft;
        private List<Wheel> m_Wheels;
        private Fuel m_FuelSrc;

        public Vehicle (string i_Manufacturer, string i_LicenseNumber, int i_NumberOfWeels, float i_MaxAirPressure, string i_WheelManufacturer)
        {
            m_Manufacturer = i_Manufacturer;
            m_LicenseNumber = i_LicenseNumber;
            for (int i = 0; i < i_NumberOfWeels; i++)
            {
                m_Wheels.Add(new Wheel(i_WheelManufacturer, i_MaxAirPressure));
            }

            //TODO: init the fuel source
            m_FuelSrc = new Fuel();
        }

        protected class Wheel
        {

            private string m_Manufacturer;
            private float m_MaxAirPressure;
            private float m_CurrentAirPressure;

            public Wheel(string i_Manufacturer, float i_MaxAirPressure)
            {
                m_MaxAirPressure = i_MaxAirPressure;
                m_Manufacturer = i_Manufacturer;
                m_CurrentAirPressure = i_MaxAirPressure;
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

        /*Fuel source logic. Has a class Fuel and battery and petrol as its subclasses*/

        protected class Fuel
        {
           
        }

        public class Battery : Fuel
        {
            private float m_HoursLeft;
            private float m_MaxHours;
            
            public void Recharge(float i_Hours)
            {
                if (i_Hours + m_HoursLeft > m_MaxHours)
                {
                    throw new ValueOutOfRangeException(0, m_MaxHours);
                }
            }


        }

        public class Petrol : Fuel
        {

        }



    }
}
