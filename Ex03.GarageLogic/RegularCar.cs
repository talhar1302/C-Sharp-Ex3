using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class RegularCar : FuelVehicle
    {
        private eCarColor m_Color;
        private eDoorsNumber m_NumberOfDoors;
        public eCarColor Color { get=>m_Color; set=>m_Color=value; }
        public eDoorsNumber NumberOfDoors { get=>m_NumberOfDoors; set=>m_NumberOfDoors=value; }

        public RegularCar()
            : base(null, null, 0, new List<Wheel>(), null, null, eFuelType.Octan95, 0, 45f)
        {
            for (int i = 0; i < 5; i++)
            {
                Wheels.Add(new Wheel("", 0, 31));
            }
        }

        public override void Refuel(float i_Amount, eFuelType i_FuelType)
        {
            if (i_FuelType != FuelType)
            {
                throw new ArgumentException("Incorrect fuel type.");
            }
            if (CurrentFuelAmount + i_Amount > MaxFuelAmount)
            {
                throw new ValueOutOfRangeException(0, MaxFuelAmount - CurrentFuelAmount, "Fuel amount exceeds the maximum limit.");
            }
            CurrentFuelAmount += i_Amount;
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
