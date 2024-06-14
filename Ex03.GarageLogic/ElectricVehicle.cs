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
        protected ElectricVehicle(float i_BatteryTimeRemaining, float i_MaxBatteryTime)           
        {
            m_BatteryTimeRemaining = i_BatteryTimeRemaining;
            r_MaxBatteryTime = i_MaxBatteryTime;
        }
        public override List<FieldDescriptor> GetFieldDescriptors()
        {
            return base.GetFieldDescriptors();
        }
        public override string GetDetails()
        {
            StringBuilder details = new StringBuilder(base.GetDetails());
            details.AppendLine($"Battery Time Remaining: {m_BatteryTimeRemaining} hours, Max Battery Time: {r_MaxBatteryTime} hours");
            return details.ToString();
        }
        public abstract void ChargeBattery(float i_Hours);       
    }
}
