using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;

namespace Ex03.ConsoleUI
{
    public class UI
    {
        private Garage garage = new Garage();

        public void Run()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Garage Management System");
                Console.WriteLine("1. Add a new vehicle");
                Console.WriteLine("2. Show vehicle list");
                Console.WriteLine("3. Change vehicle status");
                Console.WriteLine("4. Inflate vehicle wheels to max");
                Console.WriteLine("5. Refuel a vehicle");
                Console.WriteLine("6. Charge a vehicle");
                Console.WriteLine("7. Display vehicle details");
                Console.WriteLine("8. Exit");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            AddVehicle();
                            break;
                        case "2":
                            ShowVehicleList();
                            break;
                        case "3":
                            ChangeVehicleStatus();
                            break;
                        case "4":
                            InflateWheelsToMax();
                            break;
                        case "5":
                            RefuelVehicle();
                            break;
                        case "6":
                            ChargeVehicle();
                            break;
                        case "7":
                            DisplayVehicleDetails();
                            break;
                        case "8":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Press any key to continue...");
                            Console.ReadKey();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }
        private void AddVehicle()
        {
            Console.WriteLine("Enter vehicle license number:");
            string licenseNumber = Console.ReadLine();

            if (garage.IsVehicleInGarage(licenseNumber))
            {
                Console.WriteLine("Vehicle is already in the garage. Status set to 'under repair'.");
                garage.ChangeVehicleStatus(licenseNumber, eVehicleStatus.UnderRepair);
            }
            else
            {
                Console.WriteLine("Select vehicle type:");
                var supportedTypes = VehicleBuilder.GetSupportedVehicleTypes();
                for (int i = 0; i < supportedTypes.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {supportedTypes[i]}");
                }

                int vehicleTypeIndex = int.Parse(Console.ReadLine()) - 1;
                eVehicleType selectedType = supportedTypes[vehicleTypeIndex];

                Vehicle newVehicle = VehicleBuilder.CreateVehicle(selectedType);
                newVehicle.LicenseNumber = licenseNumber;

                // Here you should collect the specific details for each vehicle type
                Console.WriteLine("Enter model name:");
                newVehicle.ModelName = Console.ReadLine();

                Console.WriteLine("Enter owner name:");
                newVehicle.OwnerName = Console.ReadLine();

                Console.WriteLine("Enter owner phone:");
                newVehicle.OwnerPhone = Console.ReadLine();

                // Initialize wheels
                Console.WriteLine("Enter manufacturer name for wheels:");
                string manufacturerName = Console.ReadLine();

                Console.WriteLine("Enter current air pressure for wheels:");
                float currentAirPressure = float.Parse(Console.ReadLine());

                foreach (var wheel in newVehicle.Wheels)
                {
                    wheel.ManufacturerName = manufacturerName;
                    wheel.CurrentAirPressure = currentAirPressure;
                }

                if (newVehicle is FuelVehicle fuelVehicle)
                {
                    Console.WriteLine("Enter fuel amount:");
                    fuelVehicle.CurrentFuelAmount = float.Parse(Console.ReadLine());
                }
                else if (newVehicle is ElectricVehicle electricVehicle)
                {
                    Console.WriteLine("Enter remaining battery time:");
                    electricVehicle.BatteryTimeRemaining = float.Parse(Console.ReadLine());
                }

                if (newVehicle is RegularMotorcycle regularMotorcycle)
                {
                    Console.WriteLine("Enter license type (A, A1, AA, B1):");
                    regularMotorcycle.LicenseType = (eLicenseType)Enum.Parse(typeof(eLicenseType), Console.ReadLine(), true);

                    Console.WriteLine("Enter engine volume:");
                    regularMotorcycle.EngineVolume = int.Parse(Console.ReadLine());
                }
                else if (newVehicle is ElectricMotorcycle electricMotorcycle)
                {
                    Console.WriteLine("Enter license type (A, A1, AA, B1):");
                    electricMotorcycle.LicenseType = (eLicenseType)Enum.Parse(typeof(eLicenseType), Console.ReadLine(), true);

                    Console.WriteLine("Enter engine volume:");
                    electricMotorcycle.EngineVolume = int.Parse(Console.ReadLine());
                }
                else if (newVehicle is RegularCar regularCar)
                {
                    Console.WriteLine("Enter car color (yellow, white, red, black):");
                    regularCar.Color = (eCarColor)Enum.Parse(typeof(eCarColor), Console.ReadLine(), true);

                    Console.WriteLine("Enter number of doors (2, 3, 4, 5):");
                    regularCar.NumberOfDoors = (eDoorsNumber)Enum.Parse(typeof(eCarColor), Console.ReadLine(), true);
                }
                else if (newVehicle is ElectricCar electricCar)
                {
                    Console.WriteLine("Enter car color (yellow, white, red, black):");
                    electricCar.Color = (eCarColor)Enum.Parse(typeof(eCarColor), Console.ReadLine(), true);

                    Console.WriteLine("Enter number of doors (2, 3, 4, 5):");
                    electricCar.NumberOfDoors = (eDoorsNumber)Enum.Parse(typeof(eCarColor), Console.ReadLine(), true);
                }
                else if (newVehicle is Truck truck)
                {
                    Console.WriteLine("Does the truck transport hazardous materials? (yes/no):");
                    truck.IsTransportsHazardousMaterials = Console.ReadLine().ToLower() == "yes";

                    Console.WriteLine("Enter cargo volume:");
                    truck.CargoVolume = float.Parse(Console.ReadLine());
                }

                garage.AddVehicle(newVehicle);
                Console.WriteLine("Vehicle added successfully.");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }


        private void ShowVehicleList()
        {
            Console.WriteLine("Show vehicles by status? (yes/no):");
            string showByStatus = Console.ReadLine().ToLower();

            List<string> licenseNumbers;
            if (showByStatus == "yes")
            {
                Console.WriteLine("Enter status (UnderRepair, Repaired, Paid):");
                eVehicleStatus status = (eVehicleStatus)Enum.Parse(typeof(eVehicleStatus), Console.ReadLine(), true);
                licenseNumbers = garage.GetVehicleLicenseNumbers(status);
            }
            else
            {
                licenseNumbers = garage.GetVehicleLicenseNumbers();
            }

            Console.WriteLine("Vehicle License Numbers:");
            foreach (var licenseNumber in licenseNumbers)
            {
                Console.WriteLine(licenseNumber);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void ChangeVehicleStatus()
        {
            Console.WriteLine("Enter vehicle license number:");
            string licenseNumber = Console.ReadLine();

            Console.WriteLine("Enter new status (UnderRepair, Repaired, Paid):");
            eVehicleStatus newStatus = (eVehicleStatus)Enum.Parse(typeof(eVehicleStatus), Console.ReadLine(), true);

            garage.ChangeVehicleStatus(licenseNumber, newStatus);
            Console.WriteLine("Vehicle status changed successfully. Press any key to continue...");
            Console.ReadKey();
        }

        private void InflateWheelsToMax()
        {
            Console.WriteLine("Enter vehicle license number:");
            string licenseNumber = Console.ReadLine();

            garage.InflateWheelsToMax(licenseNumber);
            Console.WriteLine("Wheels inflated to max successfully. Press any key to continue...");
            Console.ReadKey();
        }

        private void RefuelVehicle()
        {
            Console.WriteLine("Enter vehicle license number:");
            string licenseNumber = Console.ReadLine();

            Console.WriteLine("Enter fuel type (Octan95, Octan96, Octan98, Soler):");
            eFuelType fuelType = (eFuelType)Enum.Parse(typeof(eFuelType), Console.ReadLine(), true);
            Console.WriteLine("Enter amount to refuel:");
            float amount = float.Parse(Console.ReadLine());

            garage.RefuelVehicle(licenseNumber, fuelType, amount);
            Console.WriteLine("Vehicle refueled successfully. Press any key to continue...");
            Console.ReadKey();
        }

        private void ChargeVehicle()
        {
            Console.WriteLine("Enter vehicle license number:");
            string licenseNumber = Console.ReadLine();

            Console.WriteLine("Enter amount of hours to charge:");
            float hours = float.Parse(Console.ReadLine());

            garage.ChargeVehicle(licenseNumber, hours);
            Console.WriteLine("Vehicle charged successfully. Press any key to continue...");
            Console.ReadKey();
        }

        private void DisplayVehicleDetails()
        {
            Console.WriteLine("Enter vehicle license number:");
            string licenseNumber = Console.ReadLine();

            Vehicle vehicle = garage.GetVehicle(licenseNumber);

            Console.WriteLine("Vehicle Details:");
            Console.WriteLine($"License Number: {vehicle.LicenseNumber}");
            Console.WriteLine($"Model Name: {vehicle.ModelName}");
            Console.WriteLine($"Owner Name: {vehicle.OwnerName}");
            Console.WriteLine($"Owner Phone: {vehicle.OwnerPhone}");
            Console.WriteLine($"Status: {vehicle.Status}");
            Console.WriteLine($"Energy Percentage: {vehicle.EnergyPercentage}%");
            Console.WriteLine("Wheels:");
            foreach (var wheel in vehicle.Wheels)
            {
                Console.WriteLine($"Manufacturer: {wheel.ManufacturerName}, Current Pressure: {wheel.CurrentAirPressure}, Max Pressure: {wheel.MaxAirPressure}");
            }

            if (vehicle is FuelVehicle fuelVehicle)
            {
                Console.WriteLine($"Fuel Type: {fuelVehicle.FuelType}");
                Console.WriteLine($"Current Fuel Amount: {fuelVehicle.CurrentFuelAmount}L");
                Console.WriteLine($"Max Fuel Amount: {fuelVehicle.MaxFuelAmount}L");
            }
            else if (vehicle is ElectricVehicle electricVehicle)
            {
                Console.WriteLine($"Battery Time Remaining: {electricVehicle.BatteryTimeRemaining} hours");
                Console.WriteLine($"Max Battery Time: {electricVehicle.MaxBatteryTime} hours");
            }

            if (vehicle is RegularCar regularCar)
            {
                Console.WriteLine($"Color: {regularCar.Color}");
                Console.WriteLine($"Number of Doors: {regularCar.NumberOfDoors}");
            }
            else if (vehicle is ElectricCar electricCar)
            {
                Console.WriteLine($"Color: {electricCar.Color}");
                Console.WriteLine($"Number of Doors: {electricCar.NumberOfDoors}");
            }
            else if (vehicle is RegularMotorcycle regularMotorcycle)
            {
                Console.WriteLine($"License Type: {regularMotorcycle.LicenseType}");
                Console.WriteLine($"Engine Volume: {regularMotorcycle.EngineVolume}cc");
            }
            else if (vehicle is ElectricMotorcycle electricMotorcycle)
            {
                Console.WriteLine($"License Type: {electricMotorcycle.LicenseType}");
                Console.WriteLine($"Engine Volume: {electricMotorcycle.EngineVolume}cc");
            }
            else if (vehicle is Truck truck)
            {
                Console.WriteLine($"Transports Hazardous Materials: {truck.IsTransportsHazardousMaterials}");
                Console.WriteLine($"Cargo Volume: {truck.CargoVolume}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public static T GetEnumFromUser<T>(string i_EnumMessage)
            where T : struct
        {
            //Console.WriteLine($"Enter {i_EnumMessage}:");
            bool isValid = Enum.TryParse(Console.ReadLine(), true, out T enumResult);
            if (!isValid)
            {
                throw new FormatException($"Invalid {i_EnumMessage}!");
            }
            return enumResult;
        }
    }
}