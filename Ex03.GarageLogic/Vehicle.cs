using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        public string ModelName { get; set; }
        public string LicenseNumber { get; set; }
        public float EnergyPercentage { get; set; }
        public List<Wheel> Wheels { get; set; }
        public string OwnerName { get; set; }
        public string OwnerPhone { get; set; }
        public eVehicleStatus Status { get; set; }

        protected Vehicle(string modelName, string licenseNumber, float energyPercentage, List<Wheel> wheels, string ownerName, string ownerPhone)
        {
            ModelName = modelName;
            LicenseNumber = licenseNumber;
            EnergyPercentage = energyPercentage;
            Wheels = wheels;
            OwnerName = ownerName;
            OwnerPhone = ownerPhone;
            Status = eVehicleStatus.UnderRepair;
        }

        public abstract void InflateWheelsToMax();
    }
}
