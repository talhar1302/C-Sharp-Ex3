using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, Vehicle> m_vehicles;

        public Garage()
        {
            m_vehicles = new Dictionary<string, Vehicle>();
        }

        public void AddVehicle(Vehicle i_vehicle)
        {
            if (m_vehicles.ContainsKey(i_vehicle.LicenseNumber))
            {
                throw new ArgumentException("Vehicle with this license number already exists.");
            }
            m_vehicles.Add(i_vehicle.LicenseNumber, i_vehicle);
        }
        public bool IsGarageEmpty()
        {
            return m_vehicles.Count == 0;
        }
        public bool IsVehicleInGarage(string i_licenseNumber)
        {
            return m_vehicles.ContainsKey(i_licenseNumber);
        }
        public List<string> GetVehicleLicenseNumbers(eVehicleStatus? status = null)
        {
            if (status == null)
            {
                return m_vehicles.Keys.ToList();
            }
            return m_vehicles.Values.Where(v => v.Status == status).Select(v => v.LicenseNumber).ToList();
        }

        public void ChangeVehicleStatus(string i_licenseNumber, eVehicleStatus i_newStatus)
        {
            if (!m_vehicles.ContainsKey(i_licenseNumber))
            {
                throw new ArgumentException("Vehicle not found.");
            }
            m_vehicles[i_licenseNumber].Status = i_newStatus;
        }

        public void InflateWheelsToMax(string i_licenseNumber)
        {
            if (!m_vehicles.ContainsKey(i_licenseNumber))
            {
                throw new ArgumentException("Vehicle not found.");
            }
            m_vehicles[i_licenseNumber].InflateWheelsToMax();
        }

        public void RefuelVehicle(string i_licenseNumber, eFuelType i_fuelType, float i_amount)
        {
            if (!m_vehicles.ContainsKey(i_licenseNumber))
            {
                throw new ArgumentException("Vehicle not found.");
            }
            if (m_vehicles[i_licenseNumber] is FuelVehicle fuelVehicle)
            {
                fuelVehicle.Refuel(i_amount, i_fuelType);
            }
            else
            {
                throw new ArgumentException("Vehicle is not a fuel vehicle.");
            }
        }

        public void ChargeVehicle(string i_licenseNumber, float i_hours)
        {
            if (!m_vehicles.ContainsKey(i_licenseNumber))
            {
                throw new ArgumentException("Vehicle not found.");
            }
            if (m_vehicles[i_licenseNumber] is ElectricVehicle electricVehicle)
            {
                electricVehicle.ChargeBattery(i_hours);
            }
            else
            {
                throw new ArgumentException("Vehicle is not an electric vehicle.");
            }
        }

        public Vehicle GetVehicle(string i_licenseNumber)
        {
            if (!m_vehicles.ContainsKey(i_licenseNumber))
            {
                throw new ArgumentException("Vehicle not found.");
            }
            return m_vehicles[i_licenseNumber];
        }
    }
}
