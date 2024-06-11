using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, Vehicle> vehicles;

        public Garage()
        {
            vehicles = new Dictionary<string, Vehicle>();
        }

        public void AddVehicle(Vehicle vehicle)
        {
            if (vehicles.ContainsKey(vehicle.LicenseNumber))
            {
                throw new ArgumentException("Vehicle with this license number already exists.");
            }
            vehicles.Add(vehicle.LicenseNumber, vehicle);
        }
        public bool IsGarageEmpty()
        {
            return vehicles.Count == 0;
        }
        public bool IsVehicleInGarage(string licenseNumber)
        {
            return vehicles.ContainsKey(licenseNumber);
        }
        public List<string> GetVehicleLicenseNumbers(eVehicleStatus? status = null)
        {
            if (status == null)
            {
                return vehicles.Keys.ToList();
            }
            return vehicles.Values.Where(v => v.Status == status).Select(v => v.LicenseNumber).ToList();
        }

        public void ChangeVehicleStatus(string licenseNumber, eVehicleStatus newStatus)
        {
            if (!vehicles.ContainsKey(licenseNumber))
            {
                throw new ArgumentException("Vehicle not found.");
            }
            vehicles[licenseNumber].Status = newStatus;
        }

        public void InflateWheelsToMax(string licenseNumber)
        {
            if (!vehicles.ContainsKey(licenseNumber))
            {
                throw new ArgumentException("Vehicle not found.");
            }
            vehicles[licenseNumber].InflateWheelsToMax();
        }

        public void RefuelVehicle(string licenseNumber, eFuelType fuelType, float amount)
        {
            if (!vehicles.ContainsKey(licenseNumber))
            {
                throw new ArgumentException("Vehicle not found.");
            }
            if (vehicles[licenseNumber] is FuelVehicle fuelVehicle)
            {
                fuelVehicle.Refuel(amount, fuelType);
            }
            else
            {
                throw new ArgumentException("Vehicle is not a fuel vehicle.");
            }
        }

        public void ChargeVehicle(string licenseNumber, float hours)
        {
            if (!vehicles.ContainsKey(licenseNumber))
            {
                throw new ArgumentException("Vehicle not found.");
            }
            if (vehicles[licenseNumber] is ElectricVehicle electricVehicle)
            {
                electricVehicle.ChargeBattery(hours);
            }
            else
            {
                throw new ArgumentException("Vehicle is not an electric vehicle.");
            }
        }

        public Vehicle GetVehicle(string licenseNumber)
        {
            if (!vehicles.ContainsKey(licenseNumber))
            {
                throw new ArgumentException("Vehicle not found.");
            }
            return vehicles[licenseNumber];
        }
    }
}
