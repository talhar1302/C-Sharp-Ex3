using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : FuelVehicle
    {
        public bool IsTransportsHazardousMaterials { get; set; }
        public float CargoVolume { get; set; }

        public Truck()
            : base(null, null, 0, new List<Wheel>(), null, null, eFuelType.Soler, 0, 120f)
        {
            for (int i = 0; i < 12; i++)
            {
                Wheels.Add(new Wheel("", 0, 28));
            }
        }
        public override void Refuel(float amount, eFuelType fuelType)
        {
            if (fuelType != FuelType)
            {
                throw new ArgumentException("Incorrect fuel type.");
            }
            if (CurrentFuelAmount + amount > MaxFuelAmount)
            {
                throw new ValueOutOfRangeException(0, MaxFuelAmount - CurrentFuelAmount, "Fuel amount exceeds the maximum limit.");
            }
            CurrentFuelAmount += amount;
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
