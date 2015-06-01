using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private const int k_maxEngineVolume = 2000;
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        public Motorcycle(string i_Manufacturer, string i_LicenseNumber, int i_NumberOfWeels, float i_MaxAirPressure, float i_CurrentAirPressure, string i_WheelManufacturer, FuelSource i_TypeOfFuel, eLicenseType i_TypeLicense, int i_EngineVol) :
            base(i_Manufacturer, i_LicenseNumber, i_NumberOfWeels, i_MaxAirPressure, i_CurrentAirPressure, i_WheelManufacturer, i_TypeOfFuel)
        {
            m_LicenseType = i_TypeLicense;
            m_EngineVolume = i_EngineVol;
        }

        public Motorcycle(string i_Manufacturer, string i_LicenseNumber, int i_NumberOfWeels, float i_MaxAirPressure, float i_CurrentAirPressure, string i_WheelManufacturer, FuelSource i_TypeOfFuel) :
            base(i_Manufacturer, i_LicenseNumber, i_NumberOfWeels, i_MaxAirPressure,  i_CurrentAirPressure, i_WheelManufacturer, i_TypeOfFuel)
        {           
        }

        public override string ToString()
        {
            return string.Format(
@"{0}
License Type: {1}
Engine Volume: {2}",
            base.ToString(),
            m_LicenseType,
            m_EngineVolume);
        }

        public override Dictionary<string, int> getQuestionair()
        {
            Dictionary<string, int> motorCycleQuestions = new Dictionary<string, int>();
            StringBuilder licenseTypeList = new StringBuilder();
            int index = 1;

            foreach (eLicenseType currentLicense in Enum.GetValues(typeof(eLicenseType)))
	        {
		        licenseTypeList.AppendLine(string.Format("({0}) {1}", index++, currentLicense));
	        }

            motorCycleQuestions.Add(string.Format("Please enter the license type:\n{0}", licenseTypeList), Enum.GetValues(typeof(eLicenseType)).Length);
            motorCycleQuestions.Add("Please enter the engine's volume:", 0);

            return motorCycleQuestions;
        }

        public override void InitVehicle(List<string> i_Params)
        {
            int index = 0;

            // license
            int licenseChoice;

            if (!int.TryParse(i_Params[index++], out licenseChoice))
            {
                throw new FormatException("license choice must be a numeric value");
            }

            int maxValueForLicenseChoice = Enum.GetValues(typeof(eLicenseType)).Length;
            
            if (licenseChoice < 1 || licenseChoice > maxValueForLicenseChoice)
            {
                throw new ValueOutOfRangeException(1, maxValueForLicenseChoice);
            }

            // engine volume
            int enginVolume;

            if (!int.TryParse(i_Params[index++], out enginVolume))
            {
                throw new FormatException("engine volume must be a numeric value");
            }

            if (enginVolume > k_maxEngineVolume)
            {
                throw new ValueOutOfRangeException(1, k_maxEngineVolume);
            }

            m_EngineVolume = enginVolume;
            m_LicenseType = (eLicenseType) licenseChoice;
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
