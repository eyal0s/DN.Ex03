using System;
using System.Collections.Generic;
using System.Text;

// This will be our API global interface

namespace Ex03.GarageLogic
{
    class Garage
    {
        private Dictionary<string, VehicleTicket> m_ListOfVehicleInGarage;


        public static InsertNewVehicleToGarage();

        public static DisplayAllLicenseNumber();

        public static ChangeStatusOfVehicle();

        public static WheelPump();

        public static FuelPump();

        public static RechargeBattery();

        public static DisplayFullSpecOfVehicle();




        protected class VehicleTicket
        {
            private string m_NameOfOwner;
            private Vehicle m_VehicleInGarage;
            private eVehicleStatus m_CurrentStatusOfVehicle;

            public VehicleTicket(string i_NameOfOwner, Vehicle i_NewVehicleInGarage)
            {
                m_NameOfOwner = i_NameOfOwner;
                m_CurrentStatusOfVehicle = eVehicleStatus.InRepair;
                m_VehicleInGarage = i_NewVehicleInGarage;
            }

            public string Owner 
            {
                get 
                {
                    return m_NameOfOwner;
                }

                set 
                {
                    m_NameOfOwner = value;
                }
            
            }

            public eVehicleStatus Status 
            {
                get 
                {
                    return m_CurrentStatusOfVehicle;
                }
            }

            
        
        }


        public enum eVehicleStatus
        {
            InRepair,
            Done,
            Paid
        }
    }
}
