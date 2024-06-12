using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public static class VehicleBuilder
    {
        private static readonly Dictionary<eVehicleType, Func<Vehicle>> VehicleCreators = new Dictionary<eVehicleType, Func<Vehicle>>
        {
            { eVehicleType.FuelMotorcycle, () => new FuelMotorcycle() },
            { eVehicleType.ElectricMotorcycle, () => new ElectricMotorcycle() },
            { eVehicleType.FuelCar, () => new FuelCar() },
            { eVehicleType.ElectricCar, () => new ElectricCar() },
            { eVehicleType.FuelTruck, () => new FuelTruck() }
        };

        public static Vehicle CreateVehicle(eVehicleType i_VehicleType)
        {
            if (VehicleCreators.TryGetValue(i_VehicleType, out Func<Vehicle> creator))
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
