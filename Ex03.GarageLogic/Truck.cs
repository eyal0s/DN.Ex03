using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class Truck : Vehicle
    {
        private const int k_MaxCargoWeight = 25000;
        private int m_cargoWeight;
        private bool m_isDangerousCargo;

        public Truck(string i_Manufacturer, string i_LicenseNumber, int i_NumberOfWheels, float i_MaxAirPressure, float i_CurrentAirPressure, string i_WheelManufacturer, FuelSource i_FuelSource) :
            base(i_Manufacturer, i_LicenseNumber, i_NumberOfWheels, i_MaxAirPressure, i_CurrentAirPressure, i_WheelManufacturer, i_FuelSource)
        {
        
        }

        public override Dictionary<string, int> getQuestionair()
        {

            Dictionary<string, int> truckQuestions = new Dictionary<string, int>();

            string dangerMaterialQuestion = @"Is the truck carrying dangerous materials?
{1} No
{2} Yes";
            truckQuestions.Add(dangerMaterialQuestion , 2);
            truckQuestions.Add("How much does the cargo weight?", 0);

            return truckQuestions;

        }

        public override void InitVehicle(List<string> i_Params)
        {
            int index = 0;
            // is cargo dangerous
            int isCargoDangerousAnswer;

            if (int.TryParse(i_Params[index++], out isCargoDangerousAnswer))
            {
                throw new FormatException("answer must be a numeric value");
            }

            if (isCargoDangerousAnswer > 2 || isCargoDangerousAnswer < 0 )
            {
                throw new ValueOutOfRangeException(0, 2);
            }

            //weight of cargo

            int weightOfCargo;

            if (int.TryParse(i_Params[index++], out weightOfCargo))
            {
                throw new FormatException("cargo weight must be a numeric value");   
            }

            if (weightOfCargo < 0)
            {
                throw new ValueOutOfRangeException(1, k_MaxCargoWeight);
            }

            m_cargoWeight = weightOfCargo;
            m_isDangerousCargo = (isCargoDangerousAnswer == 1) ? true : false;

        }

         public override string ToString()
         {
             string dangerInCargo = m_isDangerousCargo ? "The Truck's cargo is hazardous" : "The truck's cargo is safe";
             return string.Format(
@"{0}
{1}
The truck cargo weight is: {2}",
    base.ToString(), dangerInCargo, m_cargoWeight);
         }

    }
}
