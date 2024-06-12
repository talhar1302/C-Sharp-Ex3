using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class RegularMotorcycle : FuelVehicle
    {
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;
        public eLicenseType LicenseType { get=>m_LicenseType; set=>m_LicenseType=value; }
        public int EngineVolume { get=>m_EngineVolume; set=>m_EngineVolume=value; }

        public RegularMotorcycle()
            : base(null, null, 0, new List<Wheel>(), null, null, eFuelType.Octan98, 0, 5.5f)
        {
            for (int i = 0; i < 2; i++)
            {
                Wheels.Add(new Wheel("", 0, 33));
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
