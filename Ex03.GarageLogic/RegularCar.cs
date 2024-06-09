using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class RegularCar : FuelVehicle
    {
        public CarColor Color { get; set; }
        public int NumberOfDoors { get; set; }

        public RegularCar(string modelName, string licenseNumber, float energyPercentage, List<Wheel> wheels, string ownerName, string ownerPhone, eFuelType fuelType, float currentFuelAmount, float maxFuelAmount, CarColor color, int numberOfDoors)
            : base(modelName, licenseNumber, energyPercentage, wheels, ownerName, ownerPhone, fuelType, currentFuelAmount, maxFuelAmount)
        {
            Color = color;
            NumberOfDoors = numberOfDoors;
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
