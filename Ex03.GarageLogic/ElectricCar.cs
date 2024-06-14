using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricCar : ElectricVehicle
    {
        private const float k_WheelsMinAirPressure = 0f;
        private const float k_WheelsMaxAirPressure = 31f;
        private const float k_ValueOfMinBatteryTimeRemaining = 0;
        private const float k_ValueOfMaxBatteryTimeRemaining = 3.5f;
        private const int k_NumberOfWheels = 5;
        private eCarColor m_color;
        private eDoorsNumber m_NumberOfDoors;
        public eCarColor Color { get => m_color; set => m_color = value; }
        public eDoorsNumber NumberOfDoors { get => m_NumberOfDoors; set => m_NumberOfDoors = value; }

        public ElectricCar()
            : base(k_ValueOfMinBatteryTimeRemaining, k_ValueOfMaxBatteryTimeRemaining)
        {
            for (int i = 0; i < k_NumberOfWheels; i++)
            {
                Wheels.Add(new Wheel("", k_WheelsMinAirPressure, k_WheelsMaxAirPressure));
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

        public override List<FieldDescriptor> GetFieldDescriptors()
        {
            List<FieldDescriptor> fieldDescriptors = base.GetFieldDescriptors();
            fieldDescriptors.Add(new FieldDescriptor("WheelsManufacturerName", typeof(string), InputValidator.ValidateLettersOnly, (vehicle, value) =>
            {
                vehicle.WheelsManufacturerName = value.ToString();
            }));
            fieldDescriptors.Add(new FieldDescriptor("WheelsAirPressure", typeof(float), input => InputValidator.ValidatePositiveFloatInRange(input, k_WheelsMaxAirPressure), (vehicle, value) =>
            {
                vehicle.WheelsAirPressure = float.Parse(value.ToString());
            }));
            fieldDescriptors.Add(new FieldDescriptor("BatteryTimeRemaining", typeof(float), input => InputValidator.ValidatePositiveFloatInRange(input, k_ValueOfMaxBatteryTimeRemaining)));
            fieldDescriptors.Add(new FieldDescriptor("Color", typeof(eCarColor), InputValidator.ValidateEnum<eCarColor>));
            fieldDescriptors.Add(new FieldDescriptor("NumberOfDoors", typeof(eDoorsNumber), InputValidator.ValidateEnum<eDoorsNumber>));
            // Add other fields as necessary
            return fieldDescriptors;
        }

        public override string GetDetails()
        {
            StringBuilder details = new StringBuilder();
            details.AppendLine("Vehicle Type: Electric Car");
            details.Append(base.GetDetails());
            details.AppendLine($"Car Color: {m_color}\nNumber of Doors: {m_NumberOfDoors}");
            return details.ToString();
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
