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

        protected FuelVehicle(string i_modelName, string i_licenseNumber, float i_energyPercentage, List<Wheel> i_wheels, string i_ownerName, string i_ownerPhone, eFuelType i_fuelType, float i_currentFuelAmount, float i_maxFuelAmount)
            : base(i_modelName, i_licenseNumber, i_energyPercentage, i_wheels, i_ownerName, i_ownerPhone)
        {
            r_FuelType = i_fuelType;
            CurrentFuelAmount = i_currentFuelAmount;
            r_MaxFuelAmount = i_maxFuelAmount;
        }
        public abstract void Refuel(float i_amount, eFuelType i_fuelType);  
    }
}
