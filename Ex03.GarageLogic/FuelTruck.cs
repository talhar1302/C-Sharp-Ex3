using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Ex03.GarageLogic
{
    public class FuelTruck : FuelVehicle
    {
        private const float k_WheelsMinAirPressure = 0f;
        private const float k_WheelsMaxAirPressure = 28f;
        private const float k_ValueOfMinFuelAmount = 0;
        private const float k_ValueOfMaxFuelAmount = 120f;
        private const int k_NumberOfWheels = 12;
        private const eFuelType k_ValueOfFuelType = eFuelType.Soler;
        private bool m_IsTransportsHazardousMaterials;
        private float m_CargoVolume;
        public bool IsTransportsHazardousMaterials { get=> m_IsTransportsHazardousMaterials; set=> m_IsTransportsHazardousMaterials=value; }
        public float CargoVolume { get=> m_CargoVolume; set=> m_CargoVolume=value; }

        public FuelTruck()
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
            fieldDescriptors.Add(new FieldDescriptor("IsTransportsHazardousMaterials", typeof(bool), InputValidator.ValidateBool));
            fieldDescriptors.Add(new FieldDescriptor("CargoVolume", typeof(float), InputValidator.ValidatePositiveFloat));
            // Add other fields as necessary
            return fieldDescriptors;
        }
        public override string GetDetails()
        {
            StringBuilder details = new StringBuilder();
            details.AppendLine("Vehicle Type: Fuel Truck");
            details.Append(base.GetDetails());
            details.AppendLine($"Is Carrying Hazardous Materials: {m_IsTransportsHazardousMaterials}\nCargo Volume: {m_CargoVolume}");
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
