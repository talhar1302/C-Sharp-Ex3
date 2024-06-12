using System.Linq;
using System;

public static class InputValidator
{
    public static bool ValidateLicenseNumber(string i_licenseNumber)
    {
        return !string.IsNullOrEmpty(i_licenseNumber) && i_licenseNumber.Length <= 8;
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

    public static bool ValidatePhoneNumber(string i_input)
    {
        return i_input.All(char.IsDigit) && i_input.Length == 10;
    }

    public static bool ValidateEnum<T>(string i_input) where T : struct, Enum
    {
        bool isValid = false;

        // Try parsing the input as an enum string value
        isValid = Enum.TryParse<T>(i_input, true, out _);

        if (!isValid)
        {
            // If parsing as a string failed, try parsing as an integer
            isValid = int.TryParse(i_input, out int intValue) && Enum.IsDefined(typeof(T), intValue);
        }

        return isValid;
    }

    public static bool ValidateYesNo(string i_input)
    {
        return i_input.ToLower() == "yes" || i_input.ToLower() == "no";
    }

    public static bool ValidateTrueFalse(string i_input)
    {
        return i_input.ToLower() == "true" || i_input.ToLower() == "false";
    }

    public static bool ValidatePositiveFloat(string i_input)
    {
        return float.TryParse(i_input, out float result) && result > 0;
    }
    public static bool ValidatePositiveFloatInRange(string i_input, float i_MaxValue)
    {
         return float.TryParse(i_input, out float result) && result > 0 && result <= i_MaxValue;
    }
}