using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : ElectricVehicle
    {
        public eLicenseType LicenseType { get; set; }
        public int EngineVolume { get; set; }

        public ElectricMotorcycle()
            : base(null, null, 0, new List<Wheel>(), null, null, 0, 2.5f)
        {
            for (int i = 0; i < 2; i++)
            {
                Wheels.Add(new Wheel("", 0, 33));
            }
        }
        public override void ChargeBattery(float hours)
        {
            if (BatteryTimeRemaining + hours > MaxBatteryTime)
            {
                throw new ValueOutOfRangeException(0, MaxBatteryTime - BatteryTimeRemaining, "Battery time exceeds the maximum limit.");
            }
            BatteryTimeRemaining += hours;
        }
        public override void InflateWheelsToMax()
        {
            foreach (var wheel in Wheels)
            {
                wheel.Inflate(wheel.MaxAirPressure - wheel.CurrentAirPressure);
            }
        }
    }

}
