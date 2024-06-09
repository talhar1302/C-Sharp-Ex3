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

            if (garage.GetVehicleLicenseNumbers().Contains(licenseNumber))
            {
                garage.ChangeVehicleStatus(licenseNumber, eVehicleStatus.UnderRepair);
                Console.WriteLine("Vehicle already exists. Status changed to 'Under Repair'.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Select vehicle type:");
            Console.WriteLine("1. Regular Motorcycle");
            Console.WriteLine("2. Electric Motorcycle");
            Console.WriteLine("3. Regular Car");
            Console.WriteLine("4. Electric Car");
            Console.WriteLine("5. Truck");
            string vehicleTypeChoice = Console.ReadLine();

            Console.WriteLine("Enter model name:");
            string modelName = Console.ReadLine();

            Console.WriteLine("Enter owner's name:");
            string ownerName = Console.ReadLine();

            Console.WriteLine("Enter owner's phone:");
            string ownerPhone = Console.ReadLine();

            Console.WriteLine("Enter energy percentage:");
            float energyPercentage = float.Parse(Console.ReadLine());

            List<Wheel> wheels = new List<Wheel>();
            Console.WriteLine("Enter wheel manufacturer name:");
            string wheelManufacturer = Console.ReadLine();

            Console.WriteLine("Enter current air pressure:");
            float currentAirPressure = float.Parse(Console.ReadLine());

            float maxAirPressure;
            int numberOfWheels;
            switch (vehicleTypeChoice)
            {
                case "1":
                    maxAirPressure = 33;
                    numberOfWheels = 2;
                    break;
                case "2":
                    maxAirPressure = 33;
                    numberOfWheels = 2;
                    break;
                case "3":
                    maxAirPressure = 31;
                    numberOfWheels = 5;
                    break;
                case "4":
                    maxAirPressure = 31;
                    numberOfWheels = 5;
                    break;
                case "5":
                    maxAirPressure = 28;
                    numberOfWheels = 12;
                    break;
                default:
                    throw new ArgumentException("Invalid vehicle type.");
            }

            for (int i = 0; i < numberOfWheels; i++)
            {
                wheels.Add(new Wheel(wheelManufacturer, currentAirPressure, maxAirPressure));
            }

            Vehicle vehicle;
            switch (vehicleTypeChoice)
            {
                case "1":
                    Console.WriteLine("Enter fuel type (Octan98):");
                    eFuelType fuelType = (eFuelType)Enum.Parse(typeof(eFuelType), Console.ReadLine(), true);

                    Console.WriteLine("Enter current fuel amount:");
                    float currentFuelAmount = float.Parse(Console.ReadLine());

                    Console.WriteLine("Enter max fuel amount:");
                    float maxFuelAmount = float.Parse(Console.ReadLine());
                    Console.WriteLine("Enter license type (A, A1, AA, B1):");
                    eLicenseType licenseType = (eLicenseType)Enum.Parse(typeof(eLicenseType), Console.ReadLine(), true);
                    Console.WriteLine("Enter engine volume:");
                    int engineVolume = int.Parse(Console.ReadLine());

                    vehicle = new RegularMotorcycle(modelName, licenseNumber, energyPercentage, wheels, ownerName, ownerPhone, fuelType, currentFuelAmount, maxFuelAmount, licenseType, engineVolume);
                    break;

                case "2":
                    Console.WriteLine("Enter battery time remaining:");
                    float batteryTimeRemaining = float.Parse(Console.ReadLine());

                    Console.WriteLine("Enter max battery time:");
                    float maxBatteryTime = float.Parse(Console.ReadLine());
                    Console.WriteLine("Enter license type (A, A1, AA, B1):");
                    licenseType = (eLicenseType)Enum.Parse(typeof(eLicenseType), Console.ReadLine(), true);

                    Console.WriteLine("Enter engine volume:");
                    engineVolume = int.Parse(Console.ReadLine());

                    vehicle = new ElectricMotorcycle(modelName, licenseNumber, energyPercentage, wheels, ownerName, ownerPhone, batteryTimeRemaining, maxBatteryTime, licenseType, engineVolume);
                    break;

                case "3":
                    Console.WriteLine("Enter fuel type (Octan95):");
                    fuelType = (eFuelType)Enum.Parse(typeof(eFuelType), Console.ReadLine(), true);

                    Console.WriteLine("Enter current fuel amount:");
                    currentFuelAmount = float.Parse(Console.ReadLine());

                    Console.WriteLine("Enter max fuel amount:");
                    maxFuelAmount = float.Parse(Console.ReadLine());

                    Console.WriteLine("Enter car color (Yellow, White, Red, Black):");
                    CarColor color = (CarColor)Enum.Parse(typeof(CarColor), Console.ReadLine(), true);

                    Console.WriteLine("Enter number of doors (2, 3, 4, 5):");
                    int numberOfDoors = int.Parse(Console.ReadLine());

                    vehicle = new RegularCar(modelName, licenseNumber, energyPercentage, wheels, ownerName, ownerPhone, fuelType, currentFuelAmount, maxFuelAmount, color, numberOfDoors);
                    break;

                case "4":
                    Console.WriteLine("Enter battery time remaining:");
                    batteryTimeRemaining = float.Parse(Console.ReadLine());

                    Console.WriteLine("Enter max battery time:");
                    maxBatteryTime = float.Parse(Console.ReadLine());

                    Console.WriteLine("Enter car color (Yellow, White, Red, Black):");
                    color = (CarColor)Enum.Parse(typeof(CarColor), Console.ReadLine(), true);

                    Console.WriteLine("Enter number of doors (2, 3, 4, 5):");
                    numberOfDoors = int.Parse(Console.ReadLine());

                    vehicle = new ElectricCar(modelName, licenseNumber, energyPercentage, wheels, ownerName, ownerPhone, batteryTimeRemaining, maxBatteryTime, color, numberOfDoors);
                    break;

                case "5":
                    Console.WriteLine("Enter fuel type (Soler):");
                    fuelType = (eFuelType)Enum.Parse(typeof(eFuelType), Console.ReadLine(), true);

                    Console.WriteLine("Enter current fuel amount:");
                    currentFuelAmount = float.Parse(Console.ReadLine());

                    Console.WriteLine("Enter max fuel amount:");
                    maxFuelAmount = float.Parse(Console.ReadLine());

                    Console.WriteLine("Does it transport hazardous materials? (true/false):");
                    bool transportsHazardousMaterials = bool.Parse(Console.ReadLine());

                    Console.WriteLine("Enter cargo volume:");
                    float cargoVolume = float.Parse(Console.ReadLine());

                    vehicle = new Truck(modelName, licenseNumber, energyPercentage, wheels, ownerName, ownerPhone, fuelType, currentFuelAmount, maxFuelAmount, transportsHazardousMaterials, cargoVolume);
                    break;

                default:
                    throw new ArgumentException("Invalid vehicle type.");
            }

            garage.AddVehicle(vehicle);
            Console.WriteLine("Vehicle added successfully. Press any key to continue...");
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
    }
}