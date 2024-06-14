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
            throw new FormatException("Invalid license number. Must consist of 1-8 charcters.");
        }
        return value;
    }

    public static bool ValidateModelName(string i_Input)
    {
        bool valid = true;

        if (string.IsNullOrEmpty(i_Input))
        {
            valid = false;
            throw new FormatException("Model name cannot be empty.");
        }
        else
        {
            foreach (char c in i_Input)
            {
                if ((char.IsLetter(c) || char.IsDigit(c) || c == ' ') == false)
                {
                    valid = false;
                    throw new FormatException("Invalid model name. Must consist of only letters or digits.");
                }
            }
        }

        return valid;
    }
    public static bool ValidateLettersOnly(string i_Input)
    {
        bool valid = true;

        if (string.IsNullOrEmpty(i_Input))
        {
            valid = false;
            throw new FormatException("Invalid input. Cannot be empty.");
        }
        else
        {
            foreach (char c in i_Input)
            {
                if (!char.IsLetter(c))
                {
                    valid = false;
                    throw new FormatException("Invalid input. Must consist of only letters.");
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
            throw new FormatException("Invalid phone number. Must consist of only 10 digits.");
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

    public static bool ValidateBool(string i_Input)
    {
        bool value = bool.TryParse(i_Input, out bool result);

        if (!value)
        {
            throw new FormatException("Invalid input. Must be a boolean(true/false).");
        }

        return result;
    }

    public static bool ValidatePositiveInt(string i_Input)
    {
        bool value = int.TryParse(i_Input, out int result);

        if (!value)
        {
            throw new FormatException("Invalid input. Must be an integer number.");
        }

        value = value && (result > 0);
        if (!value)
        {
            throw new ArgumentException("The input must be a positive integer number.");
        }
        return value;
    }
    public static bool ValidatePositiveFloat(string i_Input)
    {
        bool value = float.TryParse(i_Input, out float result);

        if (!value)
        {
            throw new FormatException("Invalid input. Must be a float number.");
        }

        value = value && (result > 0);
        if (!value)
        {
            throw new ArgumentException("The input must be a positive float number.");
        }
        return value;
    }
    public static bool ValidatePositiveFloatInRange(string i_Input, float i_MaxValue)
    {
        bool value = float.TryParse(i_Input, out float result);

        if (!value)
        {
            throw new FormatException("Invalid input. Must be a float number.");
        }

        value = value && (result > 0 && result <= i_MaxValue);
        if (!value)
        {
            throw new ValueOutOfRangeException(0, i_MaxValue, $"The input must be between '{0}' and '{i_MaxValue}'.");
        }
        return value;
    }
}