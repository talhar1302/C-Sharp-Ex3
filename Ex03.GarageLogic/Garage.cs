using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, Vehicle> m_Vehicles;

        public Garage()
        {
            m_Vehicles = new Dictionary<string, Vehicle>();
        }

        public void AddVehicle(Vehicle i_Vehicle)
        {
            if (m_Vehicles.ContainsKey(i_Vehicle.LicenseNumber))
            {
                throw new ArgumentException("Vehicle with this license number already exists.");
            }

            m_Vehicles.Add(i_Vehicle.LicenseNumber, i_Vehicle);
        }
        public bool IsGarageEmpty()
        {
            return m_Vehicles.Count == 0;
        }
        public bool IsVehicleInGarage(string i_LicenseNumber)
        {
            return m_Vehicles.ContainsKey(i_LicenseNumber);
        }
        public List<string> GetVehicleLicenseNumbers(eVehicleStatus? i_Status = null)
        {
            if (i_Status == null)
            {
                return m_Vehicles.Keys.ToList();
            }

            return m_Vehicles.Values.Where(v => v.Status == i_Status).Select(v => v.LicenseNumber).ToList();
        }
        public void ChangeVehicleStatus(string i_LicenseNumber, eVehicleStatus i_NewStatus)
        {
            if (!m_Vehicles.ContainsKey(i_LicenseNumber))
            {
                throw new ArgumentException("Vehicle not found.");
            }

            m_Vehicles[i_LicenseNumber].Status = i_NewStatus;
        }
        public void InflateWheelsToMax(string i_LicenseNumber)
        {
            if (!m_Vehicles.ContainsKey(i_LicenseNumber))
            {
                throw new ArgumentException("Vehicle not found.");
            }

            m_Vehicles[i_LicenseNumber].InflateWheelsToMax();
        }
        public void RefuelVehicle(string i_LicenseNumber, eFuelType i_FuelType, float i_Amount)
        {
            if (!m_Vehicles.ContainsKey(i_LicenseNumber))
            {
                throw new ArgumentException("Vehicle not found.");
            }

            if (m_Vehicles[i_LicenseNumber] is FuelVehicle fuelVehicle)
            {
                fuelVehicle.Refuel(i_Amount, i_FuelType);
            }
            else
            {
                throw new ArgumentException("Vehicle is not a fuel vehicle.");
            }
        }
        public void ChargeVehicle(string i_LicenseNumber, float i_Hours)
        {
            if (!m_Vehicles.ContainsKey(i_LicenseNumber))
            {
                throw new ArgumentException("Vehicle not found.");
            }

            if (m_Vehicles[i_LicenseNumber] is ElectricVehicle electricVehicle)
            {
                electricVehicle.ChargeBattery(i_Hours);
            }
            else
            {
                throw new ArgumentException("Vehicle is not an electric vehicle.");
            }
        }
        public Vehicle GetVehicle(string i_LicenseNumber)
        {
            if (!m_Vehicles.ContainsKey(i_LicenseNumber))
            {
                throw new ArgumentException("Vehicle not found.");
            }

            return m_Vehicles[i_LicenseNumber];
        }
    }
}
