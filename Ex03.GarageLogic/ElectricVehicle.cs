using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class ElectricVehicle : Vehicle
    {
        public float BatteryTimeRemaining { get; set; }
        public float MaxBatteryTime { get; set; }

        protected ElectricVehicle(string modelName, string licenseNumber, float energyPercentage, List<Wheel> wheels, string ownerName, string ownerPhone, float batteryTimeRemaining, float maxBatteryTime)
            : base(modelName, licenseNumber, energyPercentage, wheels, ownerName, ownerPhone)
        {
            BatteryTimeRemaining = batteryTimeRemaining;
            MaxBatteryTime = maxBatteryTime;
        }

        public void ChargeBattery(float hours)
        {
            if (BatteryTimeRemaining + hours > MaxBatteryTime)
            {
                throw new ValueOutOfRangeException(0, MaxBatteryTime - BatteryTimeRemaining, "Battery time exceeds the maximum limit.");
            }
            BatteryTimeRemaining += hours;
        }
    }

}
