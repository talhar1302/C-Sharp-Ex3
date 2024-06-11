using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;

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
                Console.WriteLine(GetMenuOptions());

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
                            DisplayMessage("Invalid choice. Press any key to continue...");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    DisplayMessage($"Error: {ex.Message}");
                }
            }
        }

        private string GetMenuOptions()
        {
            StringBuilder menu = new StringBuilder();
            menu.AppendLine("Garage Management System");
            menu.AppendLine("1. Add a new vehicle");
            menu.AppendLine("2. Show vehicle list");
            menu.AppendLine("3. Change vehicle status");
            menu.AppendLine("4. Inflate vehicle wheels to max");
            menu.AppendLine("5. Refuel a vehicle");
            menu.AppendLine("6. Charge a vehicle");
            menu.AppendLine("7. Display vehicle details");
            menu.AppendLine("8. Exit");
            return menu.ToString();
        }
        private void DisplayMessage(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        private void AddVehicle()
        {
            string licenseNumber = GetInput("Enter vehicle license number:");

            if (!InputValidator.ValidateLicenseNumber(licenseNumber))
            {
                DisplayMessage("Invalid license number.");
                return;
            }

            if (garage.IsVehicleInGarage(licenseNumber))
            {
                garage.ChangeVehicleStatus(licenseNumber, eVehicleStatus.UnderRepair);
                DisplayMessage("Vehicle is already in the garage. Status set to 'under repair'.");
                return;
            }

            eVehicleType selectedType = SelectVehicleType();
            Vehicle newVehicle = VehicleBuilder.CreateVehicle(selectedType);
            newVehicle.LicenseNumber = licenseNumber;

            newVehicle.ModelName = GetValidInput("Enter model name:", InputValidator.ValidateModelName, "Invalid model name.");
            newVehicle.OwnerName = GetValidInput("Enter owner name:", InputValidator.ValidateOwnerName, "Invalid owner name.");
            newVehicle.OwnerPhone = GetValidInput("Enter owner phone:", InputValidator.ValidatePhoneNumber, "Invalid phone number.");

            SetWheelsDetails(newVehicle);
            SetSpecificVehicleDetails(newVehicle);

            garage.AddVehicle(newVehicle);
            DisplayMessage("Vehicle added successfully.");
        }

        private void ShowVehicleList()
        {
            if (garage.IsGarageEmpty() == true)
            {
                DisplayMessage("No vehicles in the garage.");
            }
            else
            {
                string showByStatus = GetValidInput("Show vehicles by status? (yes/no):", InputValidator.ValidateYesNo, "Invalid input.");
                List<string> licenseNumbers;

                if (showByStatus.ToLower() == "yes")
                {
                    eVehicleStatus status = (eVehicleStatus)Enum.Parse(typeof(eVehicleStatus), GetInput("Enter status (UnderRepair, Repaired, Paid):"), true);
                    licenseNumbers = garage.GetVehicleLicenseNumbers(status);
                }
                else
                {
                    licenseNumbers = garage.GetVehicleLicenseNumbers();
                }

                if (licenseNumbers.Count == 0)
                {
                    DisplayMessage("No vehicles found.");
                }
                else
                {
                    StringBuilder vehicleList = new StringBuilder("Vehicle License Numbers:");
                    foreach (string licenseNumber in licenseNumbers)
                    {
                        vehicleList.AppendLine(licenseNumber);
                    }
                    DisplayMessage(vehicleList.ToString());
                }
            }
        }

        private void ChangeVehicleStatus()
        {
            string licenseNumber = GetInput("Enter vehicle license number:");

            if (!garage.IsVehicleInGarage(licenseNumber))
            {
                DisplayMessage("Vehicle not found.");
                return;
            }

            eVehicleStatus newStatus = (eVehicleStatus)Enum.Parse(typeof(eVehicleStatus), GetInput("Enter new status (UnderRepair, Repaired, Paid):"), true);
            garage.ChangeVehicleStatus(licenseNumber, newStatus);
            DisplayMessage("Vehicle status changed successfully.");
        }

        private void InflateWheelsToMax()
        {
            string licenseNumber = GetInput("Enter vehicle license number:");

            if (!garage.IsVehicleInGarage(licenseNumber))
            {
                DisplayMessage("Vehicle not found.");
                return;
            }

            garage.InflateWheelsToMax(licenseNumber);
            DisplayMessage("Wheels inflated to max successfully.");
        }

        private void RefuelVehicle()
        {
            string licenseNumber = GetInput("Enter vehicle license number:");

            if (!garage.IsVehicleInGarage(licenseNumber))
            {
                DisplayMessage("Vehicle not found.");
                return;
            }

            Vehicle vehicle = garage.GetVehicle(licenseNumber);
            if (!(vehicle is FuelVehicle fuelVehicle))
            {
                DisplayMessage("Vehicle is not fuel-based.");
                return;
            }

            eFuelType fuelType = (eFuelType)Enum.Parse(typeof(eFuelType), GetInput("Enter fuel type (Octan95, Octan96, Octan98, Soler):"), true);

            if (fuelVehicle.FuelType != fuelType)
            {
                DisplayMessage("Incorrect fuel type.");
                return;
            }

            float amount = float.Parse(GetInput("Enter amount to refuel:"));

            if (!InputValidator.ValidateFuelAmount(amount, fuelVehicle.MaxFuelAmount - fuelVehicle.CurrentFuelAmount))
            {
                DisplayMessage("Invalid fuel amount.");
                return;
            }

            garage.RefuelVehicle(licenseNumber, fuelType, amount);
            DisplayMessage("Vehicle refueled successfully.");
        }

        private void ChargeVehicle()
        {
            string licenseNumber = GetInput("Enter vehicle license number:");

            if (!garage.IsVehicleInGarage(licenseNumber))
            {
                DisplayMessage("Vehicle not found.");
                return;
            }

            Vehicle vehicle = garage.GetVehicle(licenseNumber);
            if (!(vehicle is ElectricVehicle))
            {
                DisplayMessage("Vehicle is not electric.");
                return;
            }

            float hours = float.Parse(GetInput("Enter amount of minutes to charge:")) / 60;
            garage.ChargeVehicle(licenseNumber, hours);
            DisplayMessage("Vehicle charged successfully.");
        }

        private void DisplayVehicleDetails()
        {
            string licenseNumber = GetInput("Enter vehicle license number:");

            if (!garage.IsVehicleInGarage(licenseNumber))
            {
                DisplayMessage("Vehicle not found.");
                return;
            }

            Vehicle vehicle = garage.GetVehicle(licenseNumber);
            DisplayMessage(GetVehicleDetails(vehicle));
        }

        private string GetInput(string prompt)
        {
            Console.Write(prompt + " ");
            return Console.ReadLine();
        }

        private string GetValidInput(string prompt, Func<string, bool> validateFunc, string errorMessage)
        {
            string input;
            do
            {
                input = GetInput(prompt);
                if (validateFunc(input))
                {
                    break;
                }
                DisplayMessage(errorMessage);
            } while (true);

            return input;
        }

        private eVehicleType SelectVehicleType()
        {
            var supportedTypes = VehicleBuilder.GetSupportedVehicleTypes();
            for (int i = 0; i < supportedTypes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {supportedTypes[i]}");
            }

            int vehicleTypeIndex = int.Parse(Console.ReadLine()) - 1;
            return supportedTypes[vehicleTypeIndex];
        }

        private void SetWheelsDetails(Vehicle newVehicle)
        {
            string manufacturerName = GetValidInput("Enter manufacturer name for wheels:", InputValidator.ValidateManufacturerName, "Invalid manufacturer name.");
            float currentAirPressure = float.Parse(GetInput("Enter current air pressure for wheels:"));

            if (!InputValidator.ValidateAirPressure(currentAirPressure, newVehicle.Wheels[0].MaxAirPressure))
            {
                DisplayMessage("Invalid air pressure.");
                return;
            }

            foreach (var wheel in newVehicle.Wheels)
            {
                wheel.ManufacturerName = manufacturerName;
                wheel.CurrentAirPressure = currentAirPressure;
            }
        }

        private void SetSpecificVehicleDetails(Vehicle newVehicle)
        {
            switch (newVehicle)
            {
                case FuelVehicle fuelVehicle:
                    SetFuelVehicleDetails(fuelVehicle);
                    break;
                case ElectricVehicle electricVehicle:
                    SetElectricVehicleDetails(electricVehicle);
                    break;
            }
            switch (newVehicle)
            {
                case RegularMotorcycle regularMotorcycle:
                    SetRegularMotorcycleDetails(regularMotorcycle);
                    break;
                case ElectricMotorcycle electricMotorcycle:
                    SetElectricMotorcycleDetails(electricMotorcycle);
                    break;
                case RegularCar regularCar:
                    SetRegularCarDetails(regularCar);
                    break;
                case ElectricCar electricCar:
                    SetElectricCarDetails(electricCar);
                    break;
                case Truck truck:
                    SetTruckDetails(truck);
                    break;
            }
        }

        private void SetFuelVehicleDetails(FuelVehicle fuelVehicle)
        {
            float currentFuelAmount = float.Parse(GetInput("Enter fuel amount:"));

            if (!InputValidator.ValidateFuelAmount(currentFuelAmount, fuelVehicle.MaxFuelAmount))
            {
                DisplayMessage("Invalid fuel amount.");
                return;
            }

            fuelVehicle.CurrentFuelAmount = currentFuelAmount;
        }

        private void SetElectricVehicleDetails(ElectricVehicle electricVehicle)
        {
            electricVehicle.BatteryTimeRemaining = float.Parse(GetInput("Enter remaining battery time:"));
        }

        private void SetRegularMotorcycleDetails(RegularMotorcycle motorcycle)
        {
            motorcycle.EngineVolume = int.Parse(GetValidInput("Enter engine volume:", InputValidator.ValidateEngineVolume, "Invalid engine volume."));
            motorcycle.LicenseType = (eLicenseType)Enum.Parse(typeof(eLicenseType), GetInput("Enter license type (A, A1, AA, B1):"), true);
        }

        private void SetElectricMotorcycleDetails(ElectricMotorcycle motorcycle)
        {
            motorcycle.EngineVolume = int.Parse(GetValidInput("Enter engine volume:", InputValidator.ValidateEngineVolume, "Invalid engine volume."));
            motorcycle.LicenseType = (eLicenseType)Enum.Parse(typeof(eLicenseType), GetInput("Enter license type (A, A1, AA, B1):"), true);
        }

        private void SetRegularCarDetails(RegularCar car)
        {
            car.Color = (eCarColor)Enum.Parse(typeof(eCarColor), GetInput("Enter car color (Red, White, Black, Silver):"), true);
            car.NumberOfDoors = (eDoorsNumber)Enum.Parse(typeof(eDoorsNumber), GetInput("Enter number of doors (Two, Three, Four, Five):"), true);
        }

        private void SetElectricCarDetails(ElectricCar car)
        {
            car.Color = (eCarColor)Enum.Parse(typeof(eCarColor), GetInput("Enter car color (Red, White, Black, Silver):"), true);
            car.NumberOfDoors = (eDoorsNumber)Enum.Parse(typeof(eDoorsNumber), GetInput("Enter number of doors (Two, Three, Four, Five):"), true);
        }

        private void SetTruckDetails(Truck truck)
        {
            truck.IsTransportsHazardousMaterials = GetValidInput("Is carrying hazardous materials? (yes/no):", InputValidator.ValidateYesNo, "Invalid input.").ToLower() == "yes";
            truck.CargoVolume = float.Parse(GetInput("Enter cargo volume:"));
        }

        private string GetVehicleDetails(Vehicle vehicle)
        {
            StringBuilder details = new StringBuilder();

            details.AppendLine($"License Number: {vehicle.LicenseNumber}");
            details.AppendLine($"Model Name: {vehicle.ModelName}");
            details.AppendLine($"Owner Name: {vehicle.OwnerName}");
            details.AppendLine($"Owner Phone: {vehicle.OwnerPhone}");
            details.AppendLine($"Vehicle Status: {vehicle.Status}");

            foreach (var wheel in vehicle.Wheels)
            {
                details.AppendLine($"Wheel Manufacturer: {wheel.ManufacturerName}, Current Air Pressure: {wheel.CurrentAirPressure}, Max Air Pressure: {wheel.MaxAirPressure}");
            }

            switch (vehicle)
            {
                case FuelVehicle fuelVehicle:
                    details.AppendLine($"Fuel Type: {fuelVehicle.FuelType}, Current Fuel Amount: {fuelVehicle.CurrentFuelAmount}, Max Fuel Amount: {fuelVehicle.MaxFuelAmount}");
                    break;
                case ElectricVehicle electricVehicle:
                    details.AppendLine($"Battery Time Remaining: {electricVehicle.BatteryTimeRemaining}, Max Battery Time: {electricVehicle.MaxBatteryTime}");
                    break;
            }
            switch (vehicle)
            {
                case RegularMotorcycle regularMotorcycle:
                    details.AppendLine($"License Type: {regularMotorcycle.LicenseType}, Engine Volume: {regularMotorcycle.EngineVolume}");
                    break;
                case ElectricMotorcycle electricMotorcycle:
                    details.AppendLine($"License Type: {electricMotorcycle.LicenseType}, Engine Volume: {electricMotorcycle.EngineVolume}");
                    break;
                case RegularCar regularCar:
                    details.AppendLine($"Car Color: {regularCar.Color}, Number of Doors: {regularCar.NumberOfDoors}");
                    break;
                case ElectricCar electricCar:
                    details.AppendLine($"Car Color: {electricCar.Color}, Number of Doors: {electricCar.NumberOfDoors}");
                    break;
                case Truck truck:
                    details.AppendLine($"Is Carrying Hazardous Materials: {truck.IsTransportsHazardousMaterials}, Cargo Volume: {truck.CargoVolume}");
                    break;
            }

            return details.ToString();
        }
    }
}
