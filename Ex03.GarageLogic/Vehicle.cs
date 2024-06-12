using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private  string m_ModelName;
        private  string m_LicenseNumber;
        private float m_EnergyPercentage;
        private List<Wheel> m_Wheels;
        private   string m_OwnerName;
        public   string m_OwnerPhone;
        private eVehicleStatus m_status;
        public string ModelName { get => m_ModelName; set => m_ModelName = value; }
        public string LicenseNumber { get => m_LicenseNumber; set => m_LicenseNumber = value; }
        public float EnergyPercentage { get =>m_EnergyPercentage; set=>m_EnergyPercentage =value; }
        public List<Wheel> Wheels { get =>m_Wheels; set=>m_Wheels=value; }
        public string OwnerName { get => m_OwnerName; set => m_OwnerName = value; }
        public string OwnerPhone { get => m_OwnerPhone; set => m_OwnerPhone = value; }
        public eVehicleStatus Status { get=>m_status; set=>m_status=value; }

        protected Vehicle(string i_modelName, string i_licenseNumber, float i_energyPercentage, List<Wheel> i_wheels, string i_ownerName, string i_ownerPhone)
        {
            m_ModelName = i_modelName;
            m_LicenseNumber = i_licenseNumber;
            m_EnergyPercentage = i_energyPercentage;
            m_Wheels = i_wheels;
            m_OwnerName = i_ownerName;
            m_OwnerName = i_ownerPhone;
            m_status = eVehicleStatus.UnderRepair;
        }

        public abstract void InflateWheelsToMax();
    }
}
