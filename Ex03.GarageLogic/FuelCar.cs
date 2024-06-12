using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Ex03.GarageLogic
{
    public class FuelCar : FuelVehicle
    {
        private const float k_WheelsMinAirPressure = 0f;
        private const float k_WheelsMaxAirPressure = 31f;
        private const float k_ValueOfMinFuelAmount = 0;
        private const float k_ValueOfMaxFuelAmount = 45f;
        private const int k_NumberOfWheels = 5;
        private const eFuelType k_ValueOfFuelType = eFuelType.Octan95;
        private eCarColor m_Color;
        private eDoorsNumber m_NumberOfDoors;
        public eCarColor Color { get => m_Color; set => m_Color = value; }
        public eDoorsNumber NumberOfDoors { get => m_NumberOfDoors; set => m_NumberOfDoors = value; }

        public FuelCar()
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
            fieldDescriptors.Add(new FieldDescriptor("WheelManufacturerName", typeof(string), InputValidator.ValidateLettersOnly, (vehicle, value) =>
            {
                vehicle.WheelManufacturerName = value.ToString();
            }));
            fieldDescriptors.Add(new FieldDescriptor("WheelsAirPressure", typeof(float), input => InputValidator.ValidatePositiveFloatInRange(input, k_WheelsMaxAirPressure), (vehicle, value) =>
            {
                vehicle.WheelsAirPressure = float.Parse(value.ToString());
            }));           
            fieldDescriptors.Add(new FieldDescriptor("CurrentFuelAmount", typeof(float), input => InputValidator.ValidatePositiveFloatInRange(input, k_ValueOfMaxFuelAmount)));
            fieldDescriptors.Add(new FieldDescriptor("Color", typeof(eCarColor), InputValidator.ValidateEnum<eCarColor>));
            fieldDescriptors.Add(new FieldDescriptor("NumberOfDoors", typeof(eDoorsNumber), InputValidator.ValidateEnum<eDoorsNumber>));
            // Add other fields as necessary
            return fieldDescriptors;
        }

        public override string GetDetails()
        {
            StringBuilder details = new StringBuilder();
            details.AppendLine("Vehicle Type: Fuel Car");
            details.Append(base.GetDetails());;
            details.AppendLine($"Car Color: {m_Color}\nNumber of Doors: {m_NumberOfDoors}");
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
