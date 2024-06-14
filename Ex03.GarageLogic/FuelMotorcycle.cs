using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Ex03.GarageLogic
{
    public class FuelMotorcycle : FuelVehicle
    {
        private const float k_WheelsMinAirPressure = 0f;
        private const float k_WheelsMaxAirPressure = 33f;
        private const float k_ValueOfMinFuelAmount = 0;
        private const float k_ValueOfMaxFuelAmount = 5.5f;
        private const int k_NumberOfWheels = 2;
        private const eFuelType k_ValueOfFuelType = eFuelType.Octan98;
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;
        public eLicenseType LicenseType { get => m_LicenseType; set => m_LicenseType = value; }
        public int EngineVolume { get => m_EngineVolume; set => m_EngineVolume = value; }

        public FuelMotorcycle()
            : base(k_ValueOfFuelType, k_ValueOfMinFuelAmount, k_ValueOfMaxFuelAmount)
        {
            for (int i = 0; i < k_NumberOfWheels; i++)
            {
                Wheels.Add(new Wheel("", k_WheelsMinAirPressure, k_WheelsMaxAirPressure));
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
            fieldDescriptors.Add(new FieldDescriptor("CurrentFuelAmount", typeof(float), input => InputValidator.ValidatePositiveFloatInRange(input, k_ValueOfMaxFuelAmount)));
            fieldDescriptors.Add(new FieldDescriptor("EngineVolume", typeof(int), InputValidator.ValidatePositiveInt));
            fieldDescriptors.Add(new FieldDescriptor("LicenseType", typeof(eLicenseType), InputValidator.ValidateEnum<eLicenseType>));

            return fieldDescriptors;
        }
        public override string GetDetails()
        {
            StringBuilder details = new StringBuilder();

            details.AppendLine("Vehicle Type: Fuel Motorcycle");
            details.Append(base.GetDetails());
            details.AppendLine($"License Type: {m_LicenseType}\nEngine Volume: {m_EngineVolume} cm^3");      
            
            return details.ToString();
        }
        public override void InflateWheelsToMax()
        {
            foreach (Wheel wheel in Wheels)
            {
                wheel.Inflate(wheel.MaxAirPressure - wheel.CurrentAirPressure);
            }
        }
    }

}
