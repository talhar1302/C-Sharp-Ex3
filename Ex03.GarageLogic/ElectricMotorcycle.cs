using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : ElectricVehicle
    {
        private const float k_WheelsMinAirPressure = 0f;
        private const float k_WheelsMaxAirPressure = 33f;
        private const float k_ValueOfMinBatteryTimeRemaining = 0;
        private const float k_ValueOfMaxBatteryTimeRemaining = 2.5f;
        private const int k_NumberOfWheels = 2;
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;
        public eLicenseType LicenseType { get => m_LicenseType; set => m_LicenseType=value; }
        public int EngineVolume { get => m_EngineVolume; set => m_EngineVolume = value; }

        public ElectricMotorcycle()
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
            fieldDescriptors.Add(new FieldDescriptor("EngineVolume", typeof(int), InputValidator.ValidatePositiveInt));
            fieldDescriptors.Add(new FieldDescriptor("LicenseType", typeof(eLicenseType), InputValidator.ValidateEnum<eLicenseType>));
            // Add other fields as necessary
            return fieldDescriptors;
        }
        public override string GetDetails()
        {
            StringBuilder details = new StringBuilder();
            details.AppendLine("Vehicle Type: Electric Motorcycle");
            details.Append(base.GetDetails());
            details.AppendLine($"License Type: {m_LicenseType}\nEngine Volume: {m_EngineVolume} cm^3");
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
