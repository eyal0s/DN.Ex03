using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private eColor m_ColorOfCar;
        private int m_AmountOfDoors;

        public Car(string i_Manufacturer, string i_LicenseNumber, int i_NumberOfWheels, float i_MaxAirPressure, float i_CurrentAirPressure, string i_WheelManufacturer, FuelSource i_FuelSource) :
            base(i_Manufacturer, i_LicenseNumber, i_NumberOfWheels, i_MaxAirPressure, i_CurrentAirPressure, i_WheelManufacturer, i_FuelSource)
        {       
        }

        public override Dictionary<string, int> getQuestionair()
        {
            Dictionary<string, int> carQuestions = new Dictionary<string, int>();
            StringBuilder carColorSelecition = new StringBuilder();
            int index = 1;

            foreach (eColor currentColorOfCar in Enum.GetValues(typeof(eColor)))
            {
                carColorSelecition.AppendLine(string.Format("{0} {1}", index++, currentColorOfCar));
            }

            carQuestions.Add(string.Format("Please enter the car's color:\n{0}", carColorSelecition.ToString()), Enum.GetValues(typeof(eColor)).Length);
            carQuestions.Add("Please enter the amount of doors in the car", 0);
         
            return carQuestions;
        }

        public override void InitVehicle(List<string> i_Params)
        {
            // color
            int colorChoiceFromUser;
            if (!int.TryParse(i_Params[0], out colorChoiceFromUser))
	        {
		        throw new FormatException("color choice must be a numeric value");
	        }
            
            // amount of doors
            int amountOfDoorsInCar;

            if (!int.TryParse(i_Params[1], out amountOfDoorsInCar))
	        {
		        throw new FormatException("amount of doors must be numeric value");
	        }
            
            if (amountOfDoorsInCar > 5 || amountOfDoorsInCar < 2)
            {
                throw new ValueOutOfRangeException("Car can have between 2 and 5 doors");
            }

            m_ColorOfCar = (eColor) colorChoiceFromUser;
            m_AmountOfDoors = amountOfDoorsInCar;     
        }    

        public override string ToString()
        {
            return string.Format(
@"{0}
Number of doors: {1}
Color of car: {2}",
            base.ToString(),
            m_AmountOfDoors,
            m_ColorOfCar);
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
