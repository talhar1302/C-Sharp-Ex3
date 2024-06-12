namespace Ex03.ConsoleUI
{
    public static class InputValidator
    {
        public static bool ValidateLicenseNumber(string i_licenseNumber)
        {
            return (!string.IsNullOrEmpty(i_licenseNumber) && i_licenseNumber.Length <= 8);
        }   
        public static bool ValidateLettersOnly(string i_input)
        {
            bool valid = true;

            if (string.IsNullOrEmpty(i_input))
            {
                valid = false;
            }
            else
            {
                foreach (char c in i_input)
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

        public static bool ValidatePhoneNumber(string i_phoneNumber)
        {
            if (i_phoneNumber.Length != 10)
            {
                return false;
            }

            foreach (char c in i_phoneNumber)
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
        public static bool ValidateIntegerNumber(string i_engineVolume)
        {
            return int.TryParse(i_engineVolume, out _);
        }

        public static bool ValidateFloatNumber(string i_engineVolume)
        {
            return float.TryParse(i_engineVolume, out _);
        }

        public static bool ValidateYesNo(string i_input)
        {
            string lowerInput = i_input.ToLower();
            return lowerInput == "yes" || lowerInput == "no";
        }
    }
}
