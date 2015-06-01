using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class Motorcycle : Vehicle
    {
     
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        public Motorcycle() :base()
        {

        }
        
        public Motorcycle(string i_Manufacturer, string i_LicenseNumber, int i_NumberOfWeels, float i_MaxAirPressure, float i_CurrentAirPressure, string i_WheelManufacturer, FuelSource i_TypeOfFuel, eLicenseType i_LicenseType, int i_EngineVolume) :
            base(i_Manufacturer, i_LicenseNumber, i_NumberOfWeels, i_MaxAirPressure,  i_CurrentAirPressure, i_WheelManufacturer, i_TypeOfFuel)
        {
            
            m_EngineVolume = i_EngineVolume;
            m_LicenseType = i_LicenseType;

        }

        public override string ToString()
        {
            return string.Format(
@"{0}
License Type: {1}
Engine Volume: {2}",
            base.ToString(), m_LicenseType, m_EngineVolume);
        }

        public override List<string> getQuestionair()
        {
            List<string> motorCycleQuestions = base.getVehicleQuestionarir();
            StringBuilder licensetype = new StringBuilder();
            int index = 1;

            foreach (eLicenseType currentLicense in Enum.GetValues(typeof(eLicenseType)))
	        {
		        licensetype.AppendLine(string.Format("{0} {1}", index++, currentLicense));
	        }

            motorCycleQuestions.Add(string.Format("Please enter the license type:\n{0}", licensetype));
            motorCycleQuestions.Add("Please enter the engine's volume:");

            return motorCycleQuestions;
        }

        public enum eLicenseType
        {
            A = 1,
            A2,
            AB,
            B1
        }
    }

}
