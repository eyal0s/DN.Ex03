﻿using System;using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
	// father object for all other types of veihcles
	public abstract class Vehicle
	{
		protected string m_Manufacturer;
		protected string m_LicenseNumber;
		protected float m_PercentageOfEnergyLeft;
		protected List<Wheel> m_Wheels;
        protected FuelSource m_FuelSrc;

        public Vehicle()
        {

        }
		public Vehicle (string i_Manufacturer, string i_LicenseNumber, int i_NumberOfWeels, float i_MaxAirPressure, float i_CurrentAirPressure, string i_WheelManufacturer, FuelSource i_FuelOfVehicle)
		{
			m_Manufacturer = i_Manufacturer;
			m_LicenseNumber = i_LicenseNumber;
            m_FuelSrc = i_FuelOfVehicle;
            m_Wheels = new List<Wheel>();

			for (int i = 0; i < i_NumberOfWeels; i++)
			{
				m_Wheels.Add(new Wheel(i_WheelManufacturer, i_MaxAirPressure, i_CurrentAirPressure));
			}

		}

        public virtual void InitVehicle(Vehicle.FuelSource i_FuelSource, List<string> i_Params)
        {
            int index = 0;
            this.m_Manufacturer = i_Params[index++];
            this.m_LicenseNumber = i_Params[index++];


            float maxPressure;
            float currentPressure;
            int numOfWheels;
            string wheelManufacturer = i_Params[index + 4];
            if (int.TryParse(i_Params[index], out numOfWheels) || float.TryParse(i_Params[index + 1], out currentPressure) || float.TryParse(i_Params[index + 2], out maxPressure))
            {
                throw new FormatException("Error invalid input for wheels, please enter numeric value");
            }
            else if(currentPressure < 0 || currentPressure > maxPressure)
            {
                throw new ValueOutOfRangeException(0, maxPressure);
            }

            for (int i = 0; i < numOfWheels; i++)
            {
                m_Wheels.Add(new Wheel(wheelManufacturer, maxPressure, currentPressure));
            }

        
        }

        
        public abstract List<string> getQuestionair();

        protected List<string> getVehicleQuestionarir() 
        {
            return new List<string>() { "Please enter manufacturer:", 
                                        "Please enter the vehicle's license number:",
                                        "Please enter the current air pressure",
                                        "Please enter the wheel manufacturer"};
        }

        public string LicenseNumer
        {
            get
            {
                return m_LicenseNumber;
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

       
        public override int GetHashCode()
        {
            return m_LicenseNumber.GetHashCode();
        }

        public override bool Equals(object i_obj)
        {
            bool isEqual = false;
            Vehicle toCompare = i_obj as Vehicle;
            if (toCompare != null)
            {
				isEqual = (toCompare.m_LicenseNumber.Equals(this.m_LicenseNumber));
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
            m_Manufacturer, m_LicenseNumber, m_Wheels.Count, wheelsOfVehicle.ToString() , m_FuelSrc.ToString()); 
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
					throw new ArgumentException();
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

		/*Fuel source logic. Has a class Fuel and battery and petrol as its subclasses*/

		public class FuelSource
		{

			private float m_EnergyLeft;
			private float m_MaxCapacity;
            private readonly eFuelType r_TypeOfFuel;
		   
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
				
            //init energy left to 100%
            public FuelSource(float i_MaxCapacity)
            {
                m_EnergyLeft = i_MaxCapacity;
                m_MaxCapacity = i_MaxCapacity;
		    }
		    

			public void Refuel(eFuelType i_TypeOfFuel  ,float i_AmountToAdd)
			{
                if (!i_TypeOfFuel.Equals(r_TypeOfFuel))
                {
                    throw new ValueOutOfRangeException(string.Format("{0} is not a known fuel type. Please choose a valid type.", i_TypeOfFuel.ToString()));
                }

                if(m_EnergyLeft + i_AmountToAdd > m_MaxCapacity)
                {
                    throw new ValueOutOfRangeException(0, m_MaxCapacity); /// exception for too much fuel
                }
                else
				{
					m_EnergyLeft += i_AmountToAdd;
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
Current battery status: {2}"
                        , r_TypeOfFuel, m_MaxCapacity, m_EnergyLeft);   
                }
                else
	            {
                    stringRepresenationOfFuelSource = string.Format(
@"The Vehicle is running on {0}
Maximum tank capacity: {1}
Current amount of liters: {2}"
                        , r_TypeOfFuel, m_MaxCapacity, m_EnergyLeft);
	            }


                return stringRepresenationOfFuelSource;

            }
        }
    

        public enum eFuelType
            {
                Octan98 = 1,
                Octan96 = 2,
                Octan95 = 3,
                Soler = 4,
                Electricity = 5
            }

       
	}
}

