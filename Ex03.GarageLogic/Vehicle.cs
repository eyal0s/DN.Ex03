using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
	// father object for all other types of veihcles
	public abstract class Vehicle
	{
		protected readonly string r_Manufacturer;
        protected readonly string r_LicenseNumber;
		protected List<Wheel> m_Wheels;
        protected FuelSource m_FuelSrc;

        public Vehicle()
        {
        }

		public Vehicle(string i_Manufacturer, string i_LicenseNumber, int i_NumberOfWeels, float i_MaxAirPressureWheel, float i_CurrentAirPressureWheel, string i_WheelManufacturer, FuelSource i_FuelOfVehicle)
		{
			r_Manufacturer = i_Manufacturer;
			r_LicenseNumber = i_LicenseNumber;
            m_FuelSrc = i_FuelOfVehicle;
            m_Wheels = new List<Wheel>();

			for (int i = 0; i < i_NumberOfWeels; i++)
			{
				m_Wheels.Add(new Wheel(i_WheelManufacturer, i_MaxAirPressureWheel, i_CurrentAirPressureWheel));
			}
		}

        public abstract void InitVehicle(List<string> i_Params);
     
        public abstract Dictionary<string, int> getQuestionair();

        protected List<string> getVehicleQuestionarir() 
        {
            return new List<string>() 
            { 
                "Please enter manufacturer:", 
                "Please enter the vehicle's license number:",
                "Please enter the current air pressure",
                "Please enter the wheel manufacturer"
            };
        }

        public string LicenseNumer
        {
            get
            {
                return r_LicenseNumber;
            }
        }

        public FuelSource FuelSrc
        {
            get
            {
                return m_FuelSrc;
            }
        }

        public List<Wheel> Wheels 
        {
            get
            {
                return m_Wheels;
            }
        }

		public class Wheel
		{
			private string m_Manufacturer;
			private float m_MaxAirPressure;
			private float m_CurrentAirPressure;

			public Wheel(string i_Manufacturer, float i_MaxAirPressure, float i_CurrentAirPressure)
			{
				m_MaxAirPressure = i_MaxAirPressure;
				m_Manufacturer = i_Manufacturer;
				m_CurrentAirPressure = i_CurrentAirPressure;
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
					throw new ArgumentException("cannot exceed the maximum air pressure for the wheel");
				}
				else
				{
					m_CurrentAirPressure += i_Volume;
				}
			}

            public override string ToString()
            {
                return string.Format("Wheel manufacturer: {0}, Maximal Pressure: {1}, current pressure: {2}", m_Manufacturer, m_MaxAirPressure, m_CurrentAirPressure);
            }
		}	

		public class FuelSource
		{
            private readonly eFuelType r_TypeOfFuel;
			private float m_EnergyLeft;
			private float m_MaxCapacity;
		   
			public FuelSource(eFuelType i_TypeOfFuel, float i_EnergyLeft, float i_MaxCapacity)
			{
                if (i_EnergyLeft > i_MaxCapacity || i_EnergyLeft < 0)
                {
                    throw new ValueOutOfRangeException(0, i_MaxCapacity);
                }

				m_EnergyLeft = i_EnergyLeft;
				m_MaxCapacity = i_MaxCapacity;
                r_TypeOfFuel = i_TypeOfFuel;
		    }
			         
			public void Refuel(eFuelType i_TypeOfFuel, string i_AmountToAdd)
			{
                float amountToAdd;
                if (!float.TryParse(i_AmountToAdd, out amountToAdd))
                {
                    throw new FormatException("the value has to be numeric");                
                }

                if (!i_TypeOfFuel.Equals(r_TypeOfFuel))
                {
                    throw new ArgumentException(string.Format("{0} is not a compatible fuel type. Please choose a valid type.", i_TypeOfFuel.ToString()));
                }

                if (m_EnergyLeft + amountToAdd > m_MaxCapacity)
                {
                    throw new ValueOutOfRangeException(0, MaxCapacity - m_EnergyLeft); /// exception for too much fuel
                }
                else
				{
                    m_EnergyLeft += amountToAdd;
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

            public override string ToString()
            {
                string stringRepresenationOfFuelSource;

                if (r_TypeOfFuel.Equals(eFuelType.Electricity))
                {
                    stringRepresenationOfFuelSource = string.Format(
@"The Vehicle is running on {0}
Maximum Battery Time: {1}
Current battery status: {2}",
                            r_TypeOfFuel,
                            m_MaxCapacity,
                            m_EnergyLeft);   
                }
                else
	            {
                    stringRepresenationOfFuelSource = string.Format(
@"The Vehicle is running on {0}
Maximum tank capacity: {1}
Current amount of liters: {2}",
                              r_TypeOfFuel,
                            m_MaxCapacity,
                            m_EnergyLeft);
	            }

                return stringRepresenationOfFuelSource;
            }
        }

        public enum eFuelType
            {
                Octan98 = 1,
                Octan96,
                Octan95,
                Soler,
                Electricity 
            }

        public override bool Equals(object i_obj)
        {
            bool isEqual = false;
            Vehicle toCompare = i_obj as Vehicle;
            if (toCompare != null)
            {
				isEqual = toCompare.r_LicenseNumber.Equals(this.r_LicenseNumber);
            }

			return isEqual;
        }

        public override string ToString()
        {
            StringBuilder wheelsOfVehicle = new StringBuilder();

            for (int i = 0; i < m_Wheels.Count - 1; i++)
            {
                wheelsOfVehicle.AppendLine(m_Wheels[i].ToString());
            }

            wheelsOfVehicle.Append(m_Wheels[m_Wheels.Count - 1]);

            return string.Format(
@"Manufacturer: {0}
License number: {1}
Number of wheels: {2}
{3}
{4}",
            r_Manufacturer,
            r_LicenseNumber,
            m_Wheels.Count,
            wheelsOfVehicle.ToString(),
            m_FuelSrc.ToString()); 
        }

        public override int GetHashCode()
        {
            return r_LicenseNumber.GetHashCode();
        }
	}
}
