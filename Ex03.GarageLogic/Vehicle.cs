using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    // father object for all other types of veihcles
    protected abstract class Vehicle
    {
        private string m_Manufacturer;
        private string m_LicenseNumber;
        private float m_PercentageOfEnergyLeft;
        private List<Wheel> m_Wheels;

        
    }
}
