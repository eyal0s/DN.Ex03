using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class Car : Vehicle
    {
        private eColor m_ColorOfCar;
        private int m_AmountOfDoors;

        public Car()
            : base()
        { }

        public Car(string i_Manufacturer, string i_LicenseNumber, string i_WheelManufacturer, float i_CurrentAvailableHours, int i_NumberOfWheels, float i_MaxAirPressure, float i_CurrentAirPressure, eColor i_ColorOfCar, int i_NumOfDoors, FuelSource i_FuelSource) :
            base(i_Manufacturer, i_LicenseNumber, i_NumberOfWheels, i_MaxAirPressure, i_CurrentAirPressure, i_WheelManufacturer, i_FuelSource)
        {
          
            if (i_NumOfDoors > 5 || i_NumOfDoors < 2)
            {
                throw new ValueOutOfRangeException("Car can have between 2 and 5 doors");
            }

            m_ColorOfCar = i_ColorOfCar;
            m_AmountOfDoors = i_NumOfDoors;
            
        }

        public override List<string> getQuestionair()
        {
            List<string> carQuestions = base.getVehicleQuestionarir();
            StringBuilder carColorSelecition = new StringBuilder();
            int index = 1;

            foreach (eColor currentColorOfCar in Enum.GetValues(typeof(eColor)))
            {
                carColorSelecition.AppendLine(string.Format("{0} {1}", index++, currentColorOfCar));
            }

            carQuestions.Add(string.Format("Please enter the car's color:\n{0}", carColorSelecition.ToString()));
            carQuestions.Add("Please enter the amount of doors in the car");
         
            return carQuestions;
        }

        public override void InitVehicle(Vehicle.FuelSource i_FuelSource, List<string> i_Params)
        {
            base.InitVehicle(i_FuelSource, i_Params);
            int index = 5;
            int choiceOfColor;

            if (int.TryParse(i_Params[index++], out choiceOfColor))
            {
                throw new FormatException();
            }
            if (choiceOfColor < 0 || choiceOfColor > Enum.GetValues(typeof(eColor)).Length )
            {
                throw new ValueOutOfRangeException(0, Enum.GetValues(typeof(eColor)).Length);
            }

            eColor colorOfcar = (eColor)choiceOfColor;
            m_ColorOfCar = colorOfcar;

        }

        public override string ToString()
        {
            return string.Format(
@"{0}
Number of doors: {1}
Color of car: {2}",
            base.ToString(), m_AmountOfDoors, m_ColorOfCar);
        }

        public enum eColor
        {
            White = 1,
            Black,
            Green,
            Red
        }
    }
}
