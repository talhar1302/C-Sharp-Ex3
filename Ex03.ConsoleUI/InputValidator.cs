namespace Ex03.ConsoleUI
{
    public static class InputValidator
    {
        public static bool ValidateLicenseNumber(string licenseNumber)
        {
            return (!string.IsNullOrEmpty(licenseNumber) && licenseNumber.Length <= 8);
        }

        public static bool ValidateModelName(string modelName)
        {
            return ValidateLettersOnly(modelName);
        }

        public static bool ValidateOwnerName(string ownerName)
        {
            return ValidateLettersOnly(ownerName);
        }

        public static bool ValidateLettersOnly(string input)
        {
            bool valid = true;

            if (string.IsNullOrEmpty(input))
            {
                valid = false;
            }
            else
            {
                foreach (char c in input)
                {
                    if (!char.IsLetter(c))
                    {
                        valid = false;
                        break;
                    }
                }
            }

            return valid;
        }

        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length != 10)
            {
                return false;
            }

            foreach (char c in phoneNumber)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool ValidateManufacturerName(string manufacturerName)
        {
            if (string.IsNullOrEmpty(manufacturerName))
            {
                return false;
            }

            foreach (char c in manufacturerName)
            {
                if (!char.IsLetter(c))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool ValidateAirPressure(float currentAirPressure, float maxAirPressure)
        {
            return currentAirPressure <= maxAirPressure;
        }

        public static bool ValidateFuelAmount(float currentFuelAmount, float maxFuelAmount)
        {
            return currentFuelAmount <= maxFuelAmount;
        }

        public static bool ValidateEngineVolume(string engineVolume)
        {
            return int.TryParse(engineVolume, out _);
        }

        public static bool ValidateYesNo(string input)
        {
            string lowerInput = input.ToLower();
            return lowerInput == "yes" || lowerInput == "no";
        }
    }
}
