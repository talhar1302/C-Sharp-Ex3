using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class ElectricVehicle : Vehicle
    {
        private float m_BatteryTimeRemaining;
        private readonly float r_MaxBatteryTime;
        public float BatteryTimeRemaining { get=>m_BatteryTimeRemaining; set=>m_BatteryTimeRemaining=value; }
        public float MaxBatteryTime { get=>r_MaxBatteryTime;}
        protected ElectricVehicle(string i_ModelName, string i_LicenseNumber, float i_EnergyPercentage, List<Wheel> i_Wheels, string i_OwnerName, string i_OwnerPhone, float i_BatteryTimeRemaining, float i_MaxBatteryTime)
            : base(i_ModelName, i_LicenseNumber, i_EnergyPercentage, i_Wheels, i_OwnerName, i_OwnerPhone)
        {
            m_BatteryTimeRemaining = i_BatteryTimeRemaining;
            r_MaxBatteryTime = i_MaxBatteryTime;
        }
        public abstract void ChargeBattery(float i_Hours);       
    }
}
