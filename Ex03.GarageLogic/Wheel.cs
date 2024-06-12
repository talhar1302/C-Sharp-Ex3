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

        public Wheel(string i_manufacturerName, float i_currentAirPressure, float i_maxAirPressure)
        {
            m_ManufacturerName = i_manufacturerName;
            m_CurrentAirPressure = i_currentAirPressure;
            r_maxAirPressure = i_maxAirPressure;
        }

        public void Inflate(float i_amount)
        {
            if (CurrentAirPressure + i_amount > MaxAirPressure)
            {
                throw new ValueOutOfRangeException(0, MaxAirPressure - CurrentAirPressure, "Air pressure exceeds the maximum limit.");
            }
            CurrentAirPressure += i_amount;
        }
    }

}
