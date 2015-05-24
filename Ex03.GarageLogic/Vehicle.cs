using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
	// father object for all other types of veihcles
	protected class Vehicle
	{
		protected readonly string r_Manufacturer;
		protected readonly string r_LicenseNumber;
		protected float m_PercentageOfEnergyLeft;
		protected List<Wheel> m_Wheels;
        protected EnergyOfOperation m_FuelSrc;

		public Vehicle (string i_Manufacturer, string i_LicenseNumber, int i_NumberOfWeels, float i_MaxAirPressure, string i_WheelManufacturer)
		{
			r_Manufacturer = i_Manufacturer;
			r_LicenseNumber = i_LicenseNumber;

			for (int i = 0; i < i_NumberOfWeels; i++)
			{
				m_Wheels.Add(new Wheel(i_WheelManufacturer, i_MaxAirPressure));
			}

			//TODO: init the fuel source
            

		}

        public override bool Equals(object obj)
        {
            bool isEqual = false;
            Vehicle toCompare = obj as Vehicle;
            if (toCompare != null)
            {
                isEqual = (toCompare.r_LicenseNumber == this.r_LicenseNumber);
            }

            return base.Equals(obj);
        }

        public override string ToString()
        {            
            return string.Format(
@"License number: {0}, Model: {1}, Owner: {2}, Wheels Manufacturer: {3}, Current Wheels air pressure: {4}, Energy status: {5}, Type of energy: {6} "),
            r_LicenseNumber, r_Manufacturer, m_PercentageOfEnergyLeft, m_Wheels , m_FuelSrc. ); // need to complete for each vehicle
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

		protected class EnergyOfOperation
		{
			protected readonly string r_TypeEnergyOperatingCar;
			protected float m_HoursLeft;
			protected float m_MaxHours;
		   
			public EnergyOfOperation(string i_TypeOfFuel, float i_CurrentAvailabeHours, float i_MaximumHoursAvailabe)
			{
				
				r_TypeEnergyOperatingCar = i_TypeOfFuel;
				m_HoursLeft = i_CurrentAvailabeHours;
				m_MaxHours = i_MaximumHoursAvailabe;
		    }
		    

			public void Recharge(float i_Hours)
			{
                if(m_HoursLeft + i_Hours > m_MaxHours)
                {
                    throw new Exception(); /// exception for to much fuel
                }
                else
				{
					m_HoursLeft += i_Hours;
			    

			    }
			}

			public float HoursLeft
			{
				get
				{
					return m_HoursLeft;
				}

            }

            public float TimeLeft
            {
                get
                {
                    return m_HoursLeft;
                }
            }
		}

		public class Petrol : EnergyOfOperation
		{  
    
			private string m_TypeOfFuel;

			public Petrol (string i_TypeEnergyOperatingCar, float i_CurrentAvailabeHours, float i_MaximumHoursAvailabe , string i_TypeOfFuel) : base(i_TypeEnergyOperatingCar, i_CurrentAvailabeHours, i_MaximumHoursAvailabe)
			{
				if (true) //TODO:  check is a valid Enum
				{
					m_TypeOfFuel = i_TypeOfFuel;	 
				}
				else
				{
					throw new ValueOutOfRangeException();
				}
			}

			public string TypeOfFuel
			{
				get
				{
					return m_TypeOfFuel;
				}
			}

            public enum eFuelType
            { 
                Octan98,
                Octan96,
                Octan95,
                Soler
            
            }
		}
	}
}

