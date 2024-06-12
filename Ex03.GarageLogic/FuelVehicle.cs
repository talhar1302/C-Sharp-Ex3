using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class FuelVehicle : Vehicle
    {
        private  readonly eFuelType r_FuelType;
        private float m_CurrentFuelAmount;
        private readonly float r_MaxFuelAmount;
        public eFuelType FuelType { get=>r_FuelType; }
        public float CurrentFuelAmount { get=>m_CurrentFuelAmount; set=>m_CurrentFuelAmount=value; }
        public float MaxFuelAmount { get=>r_MaxFuelAmount; }

        protected FuelVehicle(eFuelType i_FuelType, float i_CurrentFuelAmount, float i_MaxFuelAmount)
        {
            r_FuelType = i_FuelType;
            m_CurrentFuelAmount = i_CurrentFuelAmount;
            r_MaxFuelAmount = i_MaxFuelAmount;
        }

        public override List<FieldDescriptor> GetFieldDescriptors()
        {
            return base.GetFieldDescriptors();
        }
        public override string GetDetails()
        {
            StringBuilder details = new StringBuilder(base.GetDetails());
            details.AppendLine($"Fuel Type: {r_FuelType}, Current Fuel Amount: {CurrentFuelAmount}, Max Fuel Amount: {r_MaxFuelAmount}");
            return details.ToString();
        }
        public abstract void Refuel(float i_amount, eFuelType i_fuelType);  
    }
}
