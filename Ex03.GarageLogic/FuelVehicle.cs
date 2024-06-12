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

        protected FuelVehicle(string i_ModelName, string i_LicenseNumber, float i_EnergyPercentage, List<Wheel> i_Wheels, string i_OwnerName, string i_OwnerPhone, eFuelType i_FuelType, float i_CurrentFuelAmount, float i_MaxFuelAmount)
            : base(i_ModelName, i_LicenseNumber, i_EnergyPercentage, i_Wheels, i_OwnerName, i_OwnerPhone)
        {
            r_FuelType = i_FuelType;
            CurrentFuelAmount = i_CurrentFuelAmount;
            r_MaxFuelAmount = i_MaxFuelAmount;
        }
        public abstract void Refuel(float i_amount, eFuelType i_fuelType);  
    }
}
