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
        protected eVehicleStatus m_StatusOfVehicle;

		public Vehicle (string i_Manufacturer, string i_LicenseNumber, int i_NumberOfWeels, float i_MaxAirPressure, string i_WheelManufacturer)
		{
			r_Manufacturer = i_Manufacturer;
			r_LicenseNumber = i_LicenseNumber;
            m_StatusOfVehicle = eVehicleStatus.InProgress;

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
				isEqual = (toCompare.r_LicenseNumber.Equals(this.r_LicenseNumber));
			}

			return isEqual;
		}

//        public override string ToString()
//        {            
//            return string.Format(
//@"License number: {0}, Model: {1}, Owner: {2}, Wheels Manufacturer: {3}, Current Wheels air pressure: {4}, Energy status: {5}, Type of energy: {6} "),
//            r_LicenseNumber, r_Manufacturer, m_PercentageOfEnergyLeft, m_Wheels , m_FuelSrc. ); // need to complete for each vehicle
//        }

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
			protected readonly eVehicleFuelSource r_TypeEnergyOperatingCar;
			protected float m_HoursLeft;
			protected float m_MaxHours;
		   
			public EnergyOfOperation(eVehicleFuelSource i_TypeOfFuel, float i_CurrentAvailabeHours, float i_MaximumHoursAvailabe)
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

        protected class Battery : EnergyOfOperation
        {
            protected readonly string r_TypeEnergyOperatingCar;
            protected float m_HoursLeft;
            protected float m_MaxHours;

            public Battery(eVehicleFuelSource i_TypeOfFuel, float i_CurrentAvailabeHours, float i_MaximumHoursAvailabe) : base(i_TypeOfFuel, i_CurrentAvailabeHours, i_MaximumHoursAvailabe)
            {              
            }


            public void Recharge(float i_Hours)
            {
                if (m_HoursLeft + i_Hours > m_MaxHours)
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
	
			private eFuelType m_TypeOfFuel;

            public Petrol(eVehicleFuelSource i_TypeOfFuel, float i_CurrentAvailabeHours, float i_MaximumHoursAvailabe, eFuelType i_FuelType)
                : base(i_TypeOfFuel, i_CurrentAvailabeHours, i_MaximumHoursAvailabe)
			{
                m_TypeOfFuel = i_FuelType;
			}

			public eFuelType TypeOfFuel
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



        public enum eVehicleStatus
        {
            InProgress,
            Done,
            Paid
        }

        public enum eVehicleFuelSource
        {
            Electric,
            Petrol
        }
	}
}

