using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
	// father object for all other types of veihcles
	class Vehicle
	{
		protected readonly string r_Manufacturer;
		protected readonly string r_LicenseNumber;
		protected float m_PercentageOfEnergyLeft;
		protected List<Wheel> m_Wheels;
        protected FuelSource m_FuelSrc;

		public Vehicle (string i_Manufacturer, string i_LicenseNumber, int i_NumberOfWeels, float i_MaxAirPressure, string i_WheelManufacturer)
		{
			r_Manufacturer = i_Manufacturer;
			r_LicenseNumber = i_LicenseNumber;
            //m_StatusOfVehicle = eVehicleStatus.InProgress;

			for (int i = 0; i < i_NumberOfWeels; i++)
			{
				m_Wheels.Add(new Wheel(i_WheelManufacturer, i_MaxAirPressure));
			}
                        
		}

        

        public override bool Equals(object i_obj)
        {
            bool isEqual = false;
            Vehicle toCompare = i_obj as Vehicle;
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

		public class Wheel
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

			public void InflateTire(float i_Volume)
			{
				if ((i_Volume + m_CurrentAirPressure) > m_MaxAirPressure)
				{
					throw new Exception();
				}
				else
				{
					m_CurrentAirPressure += i_Volume;
				}
			}

		}

		/*Fuel source logic. Has a class Fuel and battery and petrol as its subclasses*/

		protected class FuelSource
		{

			protected float m_EnergyLeft;
			protected float m_MaxCapacity;
		   
			public FuelSource(float i_EnergyLeft, float i_MaxCapacity)
			{
              
				m_EnergyLeft = i_EnergyLeft;
				m_MaxCapacity = i_MaxCapacity;
		    }
				
            //init energy left to 100%
            public FuelSource(float i_MaxCapacity)
            {
                m_EnergyLeft = i_MaxCapacity;
                m_MaxCapacity = i_MaxCapacity;
		    }
		    

			public virtual void Refuel(float i_Quantity)
			{
                if(m_EnergyLeft + i_Quantity > m_MaxCapacity)
                {
                    throw new ValueOutOfRangeException(0, m_MaxCapacity); /// exception for too much fuel
                }
                else
				{
					m_EnergyLeft += i_Quantity;
			    

			    }
			}

			public float EnergyLeft
			{
				get
				{
					return m_EnergyLeft;
				}

            }

            public float MaxCapacity
            {
                get
                {
                    return m_MaxCapacity;
                }
            }
                }

        protected class Battery : FuelSource
        {
            public Battery (float i_CurrentAvailableHours, float i_MaxHours)
                : base(i_CurrentAvailableHours, i_MaxHours)
            {

            }


		}

        protected class Petrol : FuelSource
        {

            private eFuelType m_TypeOfFuel;

            public Petrol(eFuelType i_TypeOfFuel, float i_CurrentAvailabeHours, float i_MaximumHoursAvailabe) :
                base(i_CurrentAvailabeHours, i_MaximumHoursAvailabe)
            {
                m_TypeOfFuel = i_TypeOfFuel;
            }


            public eFuelType TypeOfFuel
            {
                get
                {
                    return m_TypeOfFuel;
                }
            }

            public void Refuel(float i_Quantity, eFuelType i_FuelType)
            {
                if (i_FuelType != m_TypeOfFuel)
                {
                    throw new ValueOutOfRangeException(string.Format("{0} is not a known fuel type. Please choose a valid type.", i_FuelType.ToString()));
                }

                base.Refuel(i_Quantity);
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
	}
}

