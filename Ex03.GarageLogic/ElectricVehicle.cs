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
        protected ElectricVehicle(string i_modelName, string i_licenseNumber, float i_energyPercentage, List<Wheel> i_wheels, string i_ownerName, string i_ownerPhone, float i_batteryTimeRemaining, float i_maxBatteryTime)
            : base(i_modelName, i_licenseNumber, i_energyPercentage, i_wheels, i_ownerName, i_ownerPhone)
        {
            m_BatteryTimeRemaining = i_batteryTimeRemaining;
            r_MaxBatteryTime = i_maxBatteryTime;
        }
        public abstract void ChargeBattery(float i_hours);       
    }
}
