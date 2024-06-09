using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class RegularMotorcycle : FuelVehicle
    {
        public eLicenseType LicenseType { get; set; }
        public int EngineVolume { get; set; }

        public RegularMotorcycle(string modelName, string licenseNumber, float energyPercentage, List<Wheel> wheels, string ownerName, string ownerPhone, eFuelType fuelType, float currentFuelAmount, float maxFuelAmount, eLicenseType licenseType, int engineVolume)
            : base(modelName, licenseNumber, energyPercentage, wheels, ownerName, ownerPhone, fuelType, currentFuelAmount, maxFuelAmount)
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
