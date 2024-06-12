using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public static class VehicleBuilder
    {
        private static readonly Dictionary<eVehicleType, Func<Vehicle>> VehicleCreators = new Dictionary<eVehicleType, Func<Vehicle>>
        {
            { eVehicleType.RegularMotorcycle, () => new RegularMotorcycle() },
            { eVehicleType.ElectricMotorcycle, () => new ElectricMotorcycle() },
            { eVehicleType.RegularCar, () => new RegularCar() },
            { eVehicleType.ElectricCar, () => new ElectricCar() },
            { eVehicleType.Truck, () => new Truck() }
        };

        public static Vehicle CreateVehicle(eVehicleType i_vehicleType)
        {
            if (VehicleCreators.TryGetValue(i_vehicleType, out Func<Vehicle> creator))
            {
                return creator();
            }
            else
            {
                throw new ArgumentException("Unsupported vehicle type.");
            }
        }

        public static List<eVehicleType> GetSupportedVehicleTypes()
        {
            return new List<eVehicleType>(VehicleCreators.Keys);
        }
    }
}
