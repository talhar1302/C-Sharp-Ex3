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
            string userInput;
            string licenseNumber = GetValidInput("Enter vehicle license number:", InputValidator.ValidateLicenseNumber, "Invalid license number. Must consist of 1-8 characters");

            if (garage.IsVehicleInGarage(licenseNumber))
            {
                garage.ChangeVehicleStatus(licenseNumber, eVehicleStatus.UnderRepair);
                DisplayMessage("Vehicle is already in the garage. Status set to 'under repair'.");
                return;
            }

            eVehicleType selectedType = GetValidEnumInput<eVehicleType>("vehicle type");
            Vehicle newVehicle = VehicleBuilder.CreateVehicle(selectedType);
            newVehicle.LicenseNumber = licenseNumber;
            var fieldDescriptors = newVehicle.GetFieldDescriptors();
            foreach (var fieldDescriptor in fieldDescriptors)
            {
                if (fieldDescriptor.FieldType.IsEnum)
                {
                    userInput = GetValidEnumInput(fieldDescriptor.Name, fieldDescriptor.FieldType);
                    newVehicle.GetType().GetProperty(fieldDescriptor.Name).SetValue(newVehicle, Enum.Parse(fieldDescriptor.FieldType, userInput));
                }
                else
                {
                    userInput = GetValidInput($"Enter {fieldDescriptor.Name}:", fieldDescriptor.ValidationFunc, $"Invalid {fieldDescriptor.Name}. Please try again.");
                    newVehicle.GetType().GetProperty(fieldDescriptor.Name).SetValue(newVehicle, Convert.ChangeType(userInput, fieldDescriptor.FieldType));
                }
            }

            garage.AddVehicle(newVehicle);
            DisplayMessage("Vehicle added successfully.");
        }
        private void ShowVehicleList()
        {
            if (garage.IsGarageEmpty())
            {
                DisplayMessage("No vehicles in the garage.");
            }
            else
            {
                string showByStatus = GetValidInput("Show vehicles by status? (yes/no):", InputValidator.ValidateYesNo, "Invalid input.");
                List<string> licenseNumbers;

                if (showByStatus.ToLower() == "yes")
                {
                    eVehicleStatus status = GetValidEnumInput<eVehicleStatus>("status");
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
                eVehicleStatus newStatus = GetValidEnumInput<eVehicleStatus>("status");
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
            eFuelType fuelType = GetValidEnumInput<eFuelType>("fuel type");
            float amount = float.Parse(GetValidInput("Enter amount to refuel:", InputValidator.ValidatePositiveFloat, "Invalid amount. Must be a positive number."));

            try
            {
                garage.RefuelVehicle(licenseNumber, fuelType, amount);
                DisplayMessage("Vehicle refueled successfully.");
            }
            catch (ArgumentException ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        private void ChargeVehicle()
        {
            string licenseNumber = GetValidInput("Enter vehicle license number:", InputValidator.ValidateLicenseNumber, "Invalid license number. Must consist of 1-8 characters");
            float minutes = float.Parse(GetValidInput("Enter amount of minutes to charge:", InputValidator.ValidatePositiveFloat, "Invalid amount. Must be a positive number."));
            float hours = minutes / 60;

            try
            {
                garage.ChargeVehicle(licenseNumber, hours);
                DisplayMessage("Vehicle charged successfully.");
            }
            catch (ArgumentException ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        private void DisplayVehicleDetails()
        {
            string licenseNumber = GetValidInput("Enter vehicle license number:", InputValidator.ValidateLicenseNumber, "Invalid license number. Must consist of 1-8 characters");

            try
            {
                Vehicle vehicle = garage.GetVehicle(licenseNumber);
                DisplayMessage(vehicle.GetDetails());
            }
            catch (ArgumentException ex)
            {
                DisplayMessage(ex.Message);
            }
        }
        private string FormatFieldName(string i_FieldName)
        {
            StringBuilder formattedName = new StringBuilder();

            foreach (char c in i_FieldName)
            {
                if (char.IsUpper(c) && formattedName.Length > 0)
                {
                    formattedName.Append(' ');
                }
                formattedName.Append(c);
            }

            return formattedName.ToString();
        }

        private string GetInput(string i_Prompt)
        {
            Console.Write(i_Prompt + " ");
            return Console.ReadLine();
        }

        private string GetValidInput(string i_Prompt, Func<string, bool> i_ValidateFunc, string i_ErrorMessage)
        {
            string input = GetInput(i_Prompt);

            while (true)
            {
                try
                {
                    if (i_ValidateFunc(input))
                    {
                        break;
                    }
                    else
                    {
                        DisplayMessage(i_ErrorMessage);
                    }
                }
                catch (Exception ex)
                {
                    DisplayMessage(ex.Message);
                }

                input = GetInput(i_Prompt);
            }

            return input;
        }
        private T GetValidEnumInput<T>(string i_Prompt) where T : struct, Enum
        {
            string choice = GetValidEnumInput(i_Prompt, typeof(T));
            return (T)Enum.Parse(typeof(T), choice);
        }
        private string GetValidEnumInput(string i_Prompt, Type i_EnumType)
        {
            string result = string.Empty;
            bool isValid = false;

            do
            {
                Console.WriteLine("Select " + i_Prompt + ":");

                foreach (var value in Enum.GetValues(i_EnumType))
                {
                    Console.WriteLine($"{Convert.ToInt32(value)}. {value}");
                }

                string input = GetInput("Enter choice:");
                try
                {
                    if (int.TryParse(input, out int choice) && Enum.IsDefined(i_EnumType, choice))
                    {
                        result = choice.ToString();
                        isValid = true;
                    }
                    else
                    {
                        throw new FormatException("Invalid choice.");
                    }
                }
                catch (FormatException ex)
                {
                    DisplayMessage(ex.Message);
                }
                
            } while (!isValid);

            return result;
        }
    }
}
