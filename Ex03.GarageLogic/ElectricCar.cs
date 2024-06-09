using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricCar : ElectricVehicle
    {
        public CarColor Color { get; set; }
        public int NumberOfDoors { get; set; }

        public ElectricCar(string modelName, string licenseNumber, float energyPercentage, List<Wheel> wheels, string ownerName, string ownerPhone, float batteryTimeRemaining, float maxBatteryTime, CarColor color, int numberOfDoors)
            : base(modelName, licenseNumber, energyPercentage, wheels, ownerName, ownerPhone, batteryTimeRemaining, maxBatteryTime)
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
