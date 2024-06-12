using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricCar : ElectricVehicle
    {
        private  eCarColor m_color;
        private  eDoorsNumber m_NumberOfDoors;
        public eCarColor Color { get => m_color; set => m_color = value; }
        public eDoorsNumber NumberOfDoors { get => m_NumberOfDoors; set => m_NumberOfDoors = value; }

        public ElectricCar()
            : base(null, null, 0, new List<Wheel>(), null, null, 0, 3.5f)
        {
            for (int i = 0; i < 5; i++)
            {
                Wheels.Add(new Wheel("", 0, 31));
            }
        }
        public override void ChargeBattery(float i_Hours)
        {
            if (BatteryTimeRemaining + i_Hours > MaxBatteryTime)
            {
                throw new ValueOutOfRangeException(0, MaxBatteryTime - BatteryTimeRemaining, "Battery time exceeds the maximum limit.");
            }
            BatteryTimeRemaining += i_Hours;
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
