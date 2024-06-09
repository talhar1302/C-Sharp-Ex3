using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : FuelVehicle
    {
        public bool IsTransportsHazardousMaterials { get; set; }
        public float CargoVolume { get; set; }

        public Truck(string modelName, string licenseNumber, float energyPercentage, List<Wheel> wheels, string ownerName, string ownerPhone, eFuelType fuelType, float currentFuelAmount, float maxFuelAmount, bool isHazardousMaterial, float cargoVolume)
            : base(modelName, licenseNumber, energyPercentage, wheels, ownerName, ownerPhone, fuelType, currentFuelAmount, maxFuelAmount)
        {
            IsTransportsHazardousMaterials = isHazardousMaterial;
            CargoVolume = cargoVolume;
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
