using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class FuelVehicle : Vehicle
    {
        public eFuelType FuelType { get; set; }
        public float CurrentFuelAmount { get; set; }
        public float MaxFuelAmount { get; set; }

        protected FuelVehicle(string modelName, string licenseNumber, float energyPercentage, List<Wheel> wheels, string ownerName, string ownerPhone, eFuelType fuelType, float currentFuelAmount, float maxFuelAmount)
            : base(modelName, licenseNumber, energyPercentage, wheels, ownerName, ownerPhone)
        {
            FuelType = fuelType;
            CurrentFuelAmount = currentFuelAmount;
            MaxFuelAmount = maxFuelAmount;
        }

        public void Refuel(float amount, eFuelType fuelType)
        {
            if (fuelType != FuelType)
            {
                throw new ArgumentException("Incorrect fuel type.");
            }
            if (CurrentFuelAmount + amount > MaxFuelAmount)
            {
                throw new ValueOutOfRangeException(0, MaxFuelAmount - CurrentFuelAmount, "Fuel amount exceeds the maximum limit.");
            }
            CurrentFuelAmount += amount;
        }
    }

}
