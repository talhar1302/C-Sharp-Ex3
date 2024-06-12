using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : ElectricVehicle
    {
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;
        public eLicenseType LicenseType { get=>m_LicenseType; set=>m_LicenseType=value; }
        public int EngineVolume { get => m_EngineVolume; set => m_EngineVolume = value; }

        public ElectricMotorcycle()
            : base(null, null, 0, new List<Wheel>(), null, null, 0, 2.5f)
        {
            for (int i = 0; i < 2; i++)
            {
                Wheels.Add(new Wheel("", 0, 33));
            }
        }
        public override void ChargeBattery(float i_Hours)
        {
            if (BatteryTimeRemaining + i_Hours > MaxBatteryTime)
            {
                throw new ValueOutOfRangeException(0, MaxBatteryTime - BatteryTimeRemaining, "Battery time exceeds the maximum limit.");
            }
            BatteryTimeRemaining += i_Hours;
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
