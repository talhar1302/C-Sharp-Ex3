using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class RegularCar : FuelVehicle
    {

        public eCarColor Color { get; set; }
        public eDoorsNumber NumberOfDoors { get; set; }

        public RegularCar()
            : base(null, null, 0, new List<Wheel>(), null, null, eFuelType.Octan95, 0, 45f)
        {
            for (int i = 0; i < 5; i++)
            {
                Wheels.Add(new Wheel("", 0, 31));
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
