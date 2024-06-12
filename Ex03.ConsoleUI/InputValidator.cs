namespace Ex03.ConsoleUI
{
    public static class InputValidator
    {
        public static bool ValidateLicenseNumber(string i_LicenseNumber)
        {
            return (!string.IsNullOrEmpty(i_LicenseNumber) && i_LicenseNumber.Length <= 8);
        }   
        public static bool ValidateLettersOnly(string i_Input)
        {
            bool valid = true;

            if (string.IsNullOrEmpty(i_Input))
            {
                valid = false;
            }
            else
            {
                foreach (char c in i_Input)
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

        public static bool ValidatePhoneNumber(string i_PhoneNumber)
        {
            if (i_PhoneNumber.Length != 10)
            {
                return false;
            }

            foreach (char c in i_PhoneNumber)
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
        public static bool ValidateIntegerNumber(string i_EngineVolume)
        {
            return int.TryParse(i_EngineVolume, out _);
        }

        public static bool ValidateFloatNumber(string i_EngineVolume)
        {
            return float.TryParse(i_EngineVolume, out _);
        }

        public static bool ValidateYesNo(string i_Input)
        {
            string lowerInput = i_Input.ToLower();
            return lowerInput == "yes" || lowerInput == "no";
        }
    }
}
