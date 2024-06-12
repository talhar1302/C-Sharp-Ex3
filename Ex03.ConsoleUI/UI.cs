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
                            DisplayMessage("Invalid choice.");
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
        private void DisplayMessage(string i_Message)
        {
            Console.WriteLine(i_Message);
            Console.WriteLine("Press any key to continue...\n");
            Console.ReadKey(true);
        }
        private void AddVehicle()
        {
            string licenseNumber = GetValidInput("Enter vehicle license number:", InputValidator.ValidateLicenseNumber, "Invalid license number. Must consist of 1-8 characters");          

            if (garage.IsVehicleInGarage(licenseNumber))
            {
                garage.ChangeVehicleStatus(licenseNumber, eVehicleStatus.UnderRepair);
                DisplayMessage("Vehicle is already in the garage. Status set to 'under repair'.");
                return;
            }

            eVehicleType selectedType = SelectVehicleType();
            Vehicle newVehicle = VehicleBuilder.CreateVehicle(selectedType);
            newVehicle.LicenseNumber = licenseNumber;

            newVehicle.ModelName = GetValidInput("Enter model name:", InputValidator.ValidateLettersOnly, "Invalid model name. Must consist of only letters");
            newVehicle.OwnerName = GetValidInput("Enter owner name:", InputValidator.ValidateLettersOnly, "Invalid owner name. Must consist of only letters");
            newVehicle.OwnerPhone = GetValidInput("Enter owner phone:", InputValidator.ValidatePhoneNumber, "Invalid phone number. Must consist of only 10 digits");

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
                    eVehicleStatus status = GetValidEnumInput<eVehicleStatus>("Enter status:");
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
                    StringBuilder vehicleList = new StringBuilder("Vehicle License Numbers:\n");
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
            string licenseNumber = GetValidInput("Enter vehicle license number:", InputValidator.ValidateLicenseNumber, "Invalid license number. Must consist of 1-8 characters");

            try
            {
                eVehicleStatus newStatus = GetValidEnumInput<eVehicleStatus>("Enter status:");           
                garage.ChangeVehicleStatus(licenseNumber, newStatus);
                DisplayMessage("Vehicle status changed successfully.");
            }
            catch (ArgumentException ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        private void InflateWheelsToMax()
        {
            string licenseNumber = GetValidInput("Enter vehicle license number:", InputValidator.ValidateLicenseNumber, "Invalid license number. Must consist of 1-8 characters");

            try
            {
                garage.InflateWheelsToMax(licenseNumber);
                DisplayMessage("Wheels inflated to max successfully.");
            }
            catch (ArgumentException ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        private void RefuelVehicle()
        {
            string licenseNumber = GetValidInput("Enter vehicle license number:", InputValidator.ValidateLicenseNumber, "Invalid license number. Must consist of 1-8 characters");
            eFuelType fuelType = GetValidEnumInput<eFuelType>("Enter fuel type:");          
            float amount = float.Parse(GetInput("Enter amount to refuel:"));

            try
            {
                garage.RefuelVehicle(licenseNumber, fuelType, amount);
                DisplayMessage("Vehicle refueled successfully.");
            }
            catch (ArgumentException ex)
            {
                DisplayMessage(ex.Message);
            }
            return;
        }

        private void ChargeVehicle()
        {
            string licenseNumber = GetValidInput("Enter vehicle license number:", InputValidator.ValidateLicenseNumber, "Invalid license number. Must consist of 1-8 characters");
            float hours = float.Parse(GetInput("Enter amount of minutes to charge:")) / 60;

            try
            {
                garage.ChargeVehicle(licenseNumber, hours);
                DisplayMessage("Vehicle charged successfully.");
            }
            catch (ArgumentException ex)
            {
                DisplayMessage(ex.Message);
            }
            return;
        }

        private void DisplayVehicleDetails()
        {
            string licenseNumber = GetValidInput("Enter vehicle license number:", InputValidator.ValidateLicenseNumber, "Invalid license number. Must consist of 1-8 characters");

            try
            {
                Vehicle vehicle = garage.GetVehicle(licenseNumber);
                DisplayMessage(GetVehicleDetails(vehicle));
            }
            catch (ArgumentException ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        private string GetInput(string i_Prompt)
        {
            Console.Write(i_Prompt + " ");
            return Console.ReadLine();
        }

        private string GetValidInput(string i_Prompt, Func<string, bool> i_ValidateFunc, string i_ErrorMessage)
        {
            string input;
            do
            {
                input = GetInput(i_Prompt);
                if (i_ValidateFunc(input))
                {
                    break;
                }
                DisplayMessage(i_ErrorMessage);
            } while (true);

            return input;
        }

        private T GetValidEnumInput<T>(string i_Prompt) where T : struct, Enum
        {
            Console.WriteLine(i_Prompt);
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                Console.WriteLine($"{(int)value}. {value}");
            }

            T enumValue;
            while (true)
            {
                string input = GetInput("Enter your choice:");
                if (Enum.TryParse(input, out enumValue) && Enum.IsDefined(typeof(T), enumValue))
                {
                    break;
                }
                DisplayMessage("Invalid choice. Please try again.");
            }

            return enumValue;
        }

        private eVehicleType SelectVehicleType()
        {
            return GetValidEnumInput<eVehicleType>("Select vehicle type:");
        }

        private void SetWheelsDetails(Vehicle i_NewVehicle)
        {
            string manufacturerName = GetValidInput("Enter manufacturer name for wheels:", InputValidator.ValidateLettersOnly, "Invalid manufacturer name. Must consist of only letters");
            float currentAirPressure = float.Parse(GetInput("Enter current air pressure for wheels:"));
            float maxAirPressure = i_NewVehicle.Wheels[0].MaxAirPressure;

            while (!InputValidator.ValidateInRange(currentAirPressure, maxAirPressure))
            {
                DisplayMessage($"Invalid air pressure. Has to be between '0' and '{maxAirPressure}'");
                currentAirPressure = float.Parse(GetInput("Enter current air pressure for wheels:"));
            }

            foreach (var wheel in i_NewVehicle.Wheels)
            {
                wheel.ManufacturerName = manufacturerName;
                wheel.CurrentAirPressure = currentAirPressure;
            }
        }

        private void SetSpecificVehicleDetails(Vehicle i_NewVehicle)
        {
            switch (i_NewVehicle)
            {
                case FuelVehicle fuelVehicle:
                    SetFuelVehicleDetails(fuelVehicle);
                    break;
                case ElectricVehicle electricVehicle:
                    SetElectricVehicleDetails(electricVehicle);
                    break;
            }
            switch (i_NewVehicle)
            {
                case RegularMotorcycle motorcycle:
                    SetRegularMotorcycleDetails(motorcycle);
                    break;
                case ElectricMotorcycle motorcycle:
                    SetElectricMotorcycleDetails(motorcycle);
                    break;
                case RegularCar car:
                    SetRegularCarDetails(car);
                    break;
                case ElectricCar car:
                    SetElectricCarDetails(car);
                    break;
                case Truck truck:
                    SetTruckDetails(truck);
                    break;
            }
        }

        private void SetFuelVehicleDetails(FuelVehicle i_FuelVehicle)
        {
            float currentFuelAmount = float.Parse(GetInput("Enter fuel amount:"));
            float maxFuelAmount = i_FuelVehicle.MaxFuelAmount;

            while (!InputValidator.ValidateInRange(currentFuelAmount, maxFuelAmount))
            {
                DisplayMessage($"Invalid fuel amount. Has to be between '0' and '{maxFuelAmount}'");
                currentFuelAmount = float.Parse(GetInput("Enter fuel amount:"));
            }

            i_FuelVehicle.CurrentFuelAmount = currentFuelAmount;
        }

        private void SetElectricVehicleDetails(ElectricVehicle i_ElectricVehicle)
        {
            float batteryTimeRemaining = float.Parse(GetValidInput("Enter remaining battery time(in hours):", InputValidator.ValidateFloatNumber, "Invalid remaining battery time. Must be a float number."));
            float maxBatteryTime = i_ElectricVehicle.MaxBatteryTime;

            while (!InputValidator.ValidateInRange(batteryTimeRemaining, maxBatteryTime))
            {
                DisplayMessage($"Invalid remaining battery time. Has to be between '0' and '{maxBatteryTime}'");               
                batteryTimeRemaining = float.Parse(GetInput("Enter remaining battery time:"));
            }

            i_ElectricVehicle.BatteryTimeRemaining = batteryTimeRemaining;
        }

        private void SetRegularMotorcycleDetails(RegularMotorcycle i_Motorcycle)
        {
            i_Motorcycle.EngineVolume = int.Parse(GetValidInput("Enter engine volume:", InputValidator.ValidateIntegerNumber, "Invalid engine volume. Must be an integer number."));
            i_Motorcycle.LicenseType = GetValidEnumInput<eLicenseType>("Select license type:");
        }

        private void SetElectricMotorcycleDetails(ElectricMotorcycle i_Motorcycle)
        {
            i_Motorcycle.EngineVolume = int.Parse(GetValidInput("Enter engine volume:", InputValidator.ValidateIntegerNumber, "Invalid engine volume. Must be an integer number."));
            i_Motorcycle.LicenseType = GetValidEnumInput<eLicenseType>("Select license type:");
        }

        private void SetRegularCarDetails(RegularCar i_Car)
        {
            i_Car.Color = GetValidEnumInput<eCarColor>("Select car color:");
            i_Car.NumberOfDoors = GetValidEnumInput<eDoorsNumber>("Select number of doors:");
        }

        private void SetElectricCarDetails(ElectricCar i_Car)
        {
            i_Car.Color = GetValidEnumInput<eCarColor>("Select car color:");
            i_Car.NumberOfDoors = GetValidEnumInput<eDoorsNumber>("Select number of doors:");
        }

        private void SetTruckDetails(Truck i_Truck)
        {
            i_Truck.IsTransportsHazardousMaterials = GetValidInput("Is carrying hazardous materials? (yes/no):", InputValidator.ValidateYesNo, "Invalid input.").ToLower() == "yes";
            i_Truck.CargoVolume = float.Parse(GetValidInput("Enter cargo volume:", InputValidator.ValidateFloatNumber, "Invalid cargo volume. Must be a float number."));
        }

        private string GetVehicleDetails(Vehicle i_Vehicle)
        {
            StringBuilder details = new StringBuilder();

            details.AppendLine($"License Number: {i_Vehicle.LicenseNumber}");
            details.AppendLine($"Model Name: {i_Vehicle.ModelName}");
            details.AppendLine($"Owner Name: {i_Vehicle.OwnerName}");
            details.AppendLine($"Owner Phone: {i_Vehicle.OwnerPhone}");
            details.AppendLine($"Vehicle Status: {i_Vehicle.Status}");

            foreach (var wheel in i_Vehicle.Wheels)
            {
                details.AppendLine($"Wheel Manufacturer: {wheel.ManufacturerName}, Current Air Pressure: {wheel.CurrentAirPressure}, Max Air Pressure: {wheel.MaxAirPressure}");
            }

            switch (i_Vehicle)
            {
                case FuelVehicle fuelVehicle:
                    details.AppendLine($"Fuel Type: {fuelVehicle.FuelType}, Current Fuel Amount: {fuelVehicle.CurrentFuelAmount}, Max Fuel Amount: {fuelVehicle.MaxFuelAmount}");
                    break;
                case ElectricVehicle electricVehicle:
                    details.AppendLine($"Battery Time Remaining: {electricVehicle.BatteryTimeRemaining}, Max Battery Time: {electricVehicle.MaxBatteryTime}");
                    break;
            }
            switch (i_Vehicle)
            {
                case RegularMotorcycle regularMotorcycle:
                    details.AppendLine($"License Type: {regularMotorcycle.LicenseType}\nEngine Volume: {regularMotorcycle.EngineVolume}");
                    break;
                case ElectricMotorcycle electricMotorcycle:
                    details.AppendLine($"License Type: {electricMotorcycle.LicenseType}\nEngine Volume: {electricMotorcycle.EngineVolume}");
                    break;
                case RegularCar regularCar:
                    details.AppendLine($"Car Color: {regularCar.Color}\nNumber of Doors: {regularCar.NumberOfDoors}");
                    break;
                case ElectricCar electricCar:
                    details.AppendLine($"Car Color: {electricCar.Color}\nNumber of Doors: {electricCar.NumberOfDoors}");
                    break;
                case Truck truck:
                    details.AppendLine($"Is Carrying Hazardous Materials: {truck.IsTransportsHazardousMaterials}\nCargo Volume: {truck.CargoVolume}");
                    break;
            }

            return details.ToString();
        }
    }
}
