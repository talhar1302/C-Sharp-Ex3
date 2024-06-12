using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private  string m_ManufacturerName;
        private float m_CurrentAirPressure;
        private readonly float r_maxAirPressure;
        public string ManufacturerName { get => m_ManufacturerName; set => m_ManufacturerName = value; }
        public float CurrentAirPressure { get=> m_CurrentAirPressure; set=> m_CurrentAirPressure=value; }
        public float MaxAirPressure { get=> r_maxAirPressure;}

        public Wheel(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            m_ManufacturerName = i_ManufacturerName;
            m_CurrentAirPressure = i_CurrentAirPressure;
            r_maxAirPressure = i_MaxAirPressure;
        }

        public void Inflate(float i_Amount)
        {
            if (CurrentAirPressure + i_Amount > MaxAirPressure)
            {
                throw new ValueOutOfRangeException(0, MaxAirPressure - CurrentAirPressure, "Air pressure exceeds the maximum limit.");
            }
            CurrentAirPressure += i_Amount;
        }
    }

}
