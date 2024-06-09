using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : ElectricVehicle
    {
        public eLicenseType LicenseType { get; set; }
        public int EngineVolume { get; set; }

        public ElectricMotorcycle(string modelName, string licenseNumber, float energyPercentage, List<Wheel> wheels, string ownerName, string ownerPhone, float batteryTimeRemaining, float maxBatteryTime, eLicenseType licenseType, int engineVolume)
            : base(modelName, licenseNumber, energyPercentage, wheels, ownerName, ownerPhone, batteryTimeRemaining, maxBatteryTime)
        {
            LicenseType = licenseType;
            EngineVolume = engineVolume;
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
