using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        public string ManufacturerName { get; set; }
        public float CurrentAirPressure { get; set; }
        public float MaxAirPressure { get; set; }

        public Wheel(string manufacturerName, float currentAirPressure, float maxAirPressure)
        {
            ManufacturerName = manufacturerName;
            CurrentAirPressure = currentAirPressure;
            MaxAirPressure = maxAirPressure;
        }

        public void Inflate(float amount)
        {
            if (CurrentAirPressure + amount > MaxAirPressure)
            {
                throw new ValueOutOfRangeException(0, MaxAirPressure - CurrentAirPressure, "Air pressure exceeds the maximum limit.");
            }
            CurrentAirPressure += amount;
        }
    }

}
