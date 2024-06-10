using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class RegularMotorcycle : FuelVehicle
    {
        public eLicenseType LicenseType { get; set; }
        public int EngineVolume { get; set; }

        public RegularMotorcycle()
            : base(null, null, 0, new List<Wheel>(), null, null, eFuelType.Octan98, 0, 5.5f)
        {
            for (int i = 0; i < 2; i++)
            {
                Wheels.Add(new Wheel("", 0, 33));
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
