using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : FuelVehicle
    {
        private bool m_IsTransportsHazardousMaterials;
        private float m_CargoVolume;
        public bool IsTransportsHazardousMaterials { get=> m_IsTransportsHazardousMaterials; set=> m_IsTransportsHazardousMaterials=value; }
        public float CargoVolume { get=> m_CargoVolume; set=> m_CargoVolume=value; }

        public Truck()
            : base(null, null, 0, new List<Wheel>(), null, null, eFuelType.Soler, 0, 120f)
        {
            for (int i = 0; i < 12; i++)
            {
                Wheels.Add(new Wheel("", 0, 28));
            }
        }
        public override void Refuel(float i_amount, eFuelType i_fuelType)
        {
            if (i_fuelType != FuelType)
            {
                throw new ArgumentException("Incorrect fuel type.");
            }
            if (CurrentFuelAmount + i_amount > MaxFuelAmount)
            {
                throw new ValueOutOfRangeException(0, MaxFuelAmount - CurrentFuelAmount, "Fuel amount exceeds the maximum limit.");
            }
            CurrentFuelAmount += i_amount;
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
