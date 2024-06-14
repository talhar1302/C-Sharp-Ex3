using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;
        private readonly float r_MaxAirPressure;
        public string ManufacturerName { get => m_ManufacturerName; set => m_ManufacturerName = value; }
        public float CurrentAirPressure { get => m_CurrentAirPressure; set => m_CurrentAirPressure = value; }
        public float MaxAirPressure { get => r_MaxAirPressure;}
        public Wheel(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            m_ManufacturerName = i_ManufacturerName;
            m_CurrentAirPressure = i_CurrentAirPressure;
            r_MaxAirPressure = i_MaxAirPressure;
        }
        public void Inflate(float i_Amount)
        {
            if (CurrentAirPressure + i_Amount > MaxAirPressure)
            {
                throw new ValueOutOfRangeException(0, MaxAirPressure - CurrentAirPressure, "Air pressure exceeds the maximum limit.");
            }

            CurrentAirPressure += i_Amount;
        }
        public override string ToString()
        {
            return ($"Wheel Manufacturer: {m_ManufacturerName}, Current Air Pressure: {m_CurrentAirPressure}, Max Air Pressure: {r_MaxAirPressure}");
        }
    }
}
