using System.Linq;
using System;
using Ex03.GarageLogic;

public static class InputValidator
{
    public static bool ValidateLicenseNumber(string i_LicenseNumber)
    {
        bool value= !string.IsNullOrEmpty(i_LicenseNumber) && i_LicenseNumber.Length <= 8;
        if(!value)
        {
            throw new FormatException("license number has to be between 1-8 charcters");
        }
        return value;
    }

    public static bool ValidateLettersOnly(string i_Input)
    {
        bool valid = true;

        if (string.IsNullOrEmpty(i_Input))
        {
            valid = false;
            throw new FormatException("invalid choice. field cannot be empty");
        }
        else
        {
            foreach (char c in i_Input)
            {
                if (!char.IsLetter(c))
                {
                    valid = false;
                    throw new FormatException("invalid choice. field can contain letters only");
                    //break;
                }
            }
        }

        return valid;
    }

    public static bool ValidatePhoneNumber(string i_Input)
    {
        bool value= i_Input.All(char.IsDigit) && i_Input.Length == 10;
        if (!value)
        {
            throw new FormatException("phone number invalid. phone number has to be 10 numbers long");
        }
        return value;
    }

    public static bool ValidateEnum<T>(string i_Input) where T : struct, Enum
    {
        bool isValid = false;

        // Try parsing the input as an enum string value
        isValid = Enum.TryParse<T>(i_Input, true, out _);

        if (!isValid)
        {
            // If parsing as a string failed, try parsing as an integer
            isValid = int.TryParse(i_Input, out int intValue) && Enum.IsDefined(typeof(T), intValue);
        }

        return isValid;
    }

    public static bool ValidateYesNo(string i_Input)
    {
        return i_Input.ToLower() == "yes" || i_Input.ToLower() == "no";
    }

    public static bool ValidateTrueFalse(string i_Input)
    {
        return i_Input.ToLower() == "true" || i_Input.ToLower() == "false";
    }

    public static bool ValidatePositiveFloat(string i_Input)
    {
        return float.TryParse(i_Input, out float result) && result > 0;
    }
    public static bool ValidatePositiveFloatInRange(string i_Input, float i_MaxValue)
    {
        bool value= float.TryParse(i_Input, out float result) && result > 0 && result <= i_MaxValue;
        if (!value)
        {
            throw new ValueOutOfRangeException(0, i_MaxValue, "Fuel amount exceeds the maximum limit.");
        }
        return value;
    }
}