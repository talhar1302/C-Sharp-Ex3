namespace Ex03.ConsoleUI
{
    public static class InputValidator
    {
        public static bool ValidateLicenseNumber(string licenseNumber)
        {
            return (!string.IsNullOrEmpty(licenseNumber) && licenseNumber.Length <= 8);
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
        public static bool ValidateInRange(float i_Current, float i_MaxPossible)
        {
            return i_Current <= i_MaxPossible;
        }
        public static bool ValidateIntegerNumber(string engineVolume)
        {
            return int.TryParse(engineVolume, out _);
        }

        public static bool ValidateFloatNumber(string engineVolume)
        {
            return float.TryParse(engineVolume, out _);
        }

        public static bool ValidateYesNo(string input)
        {
            string lowerInput = input.ToLower();
            return lowerInput == "yes" || lowerInput == "no";
        }
    }
}
