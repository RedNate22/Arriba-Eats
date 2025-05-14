using System;
using UI;

namespace UIComponents;

/// <summary>
/// Provides helper methods for managing user interface interactions.
/// <para> Also contains utility methods for dynamically formatting UI messages. </para>
/// </summary>
public static class IOUtilities
{
    /// <summary>
    /// Generates a string prompt asking for input within a specified range.
    /// <para> Formats a message based on the provided upper limit for user choices. </para>
    /// </summary>
    /// <param name="maxChoice"> The maximum valid choice a user can select. </param>
    /// <returns> The formatted string, prompting the user to enter a valid choice. </returns>
    public static string EnterChoiceStr(int maxChoice)
    {
        return string.Format(MenuConstants.ENTER_CHOICE_TEMPLATE, maxChoice);
    }

    /// <summary>
    /// Formats the <see cref="MenuConstants.LOG_OUT_TEMPLATE"/> string
    /// to dynamically display it in the correctly numbered position of the menu.
    /// </summary>
    /// <param name="menuChoiceNum"> The position number for the Log out option. </param>
    /// <returns> The formatted string, with the correctly numbered position. </returns>
    public static string LogOutStr(int menuChoiceNum)
    {
        return string.Format(MenuConstants.LOG_OUT_TEMPLATE, menuChoiceNum);
    }
    
    /// <summary>
    /// Formats the <see cref="MenuConstants.RETURN_PREVIOUS_MENU_TEMPLATE"/> string
    /// to dynamically display it in the correctly numbered position of the menu.
    /// </summary>
    /// <param name="menuChoiceNum"> The position number for the Return to previous menu option. </param>
    /// <returns> The formatted string, with the correctly numbered position. </returns>
    public static string ReturnToPreviousMenuStr(int menuChoiceNum)
    {
        return string.Format(MenuConstants.RETURN_PREVIOUS_MENU_TEMPLATE, menuChoiceNum);
    }

    /// <summary>
    /// Validates whether the provided string input meets the following criteria:
    /// - Consists of at least one letter.
    /// - Only letters, spaces, apostrophes, and hyphens.
    /// </summary>
    /// <param name="name"> The string to validate. </param>
    /// <returns> 
    /// <c>true</c> if the input meets the criteria, otherwise <c>false</c>.
    /// </returns>
    public static bool IsValidName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return false;
        
        else if (name.Length > 0)
        {
            bool containsValidCharacters = false;
            foreach (char character in name)
            {
                if (char.IsLetter(character)) containsValidCharacters = true;
                
                else if (character != '-' && character != '\'' && character != ' ') return false;
            }          
            return containsValidCharacters;
        }
        return false;
    }

    /// <summary>
    /// Validates whether the provided int input is within the
    /// specified range (18-100 inclusive).
    /// </summary>
    /// <param name="age"> The int to validate. </param>
    /// <returns>
    /// <c>true</c> if the input meets the criteria, otherwise <c>false</c>.
    /// </returns>
    public static bool IsValidAge(int age)
    {
        if (age < 18 || age > 100) return false;
        else return true;
    }

    /// <summary>
    /// Validates whether the provided string input meets the following criteria:
    /// <para> - Contains exactly one instance of the target symbol (<c>@</c>). </para>
    /// <para> - Contains at least one character on both sides of the target symbol. </para>
    /// </summary>
    /// <param name="email"> The string to validate. </param>
    /// <returns> 
    /// <c>true</c> if input meets the criteria, otherwise <c>false</c>. 
    /// </returns>
    public static bool IsValidEmail(string email)
    {
        char targetSymbol = '@';
        int targetLocation = -1;
        int targetCount = 0;

        for (int i = 0; i < email.Length; i++)
        {
            if (email[i] == targetSymbol)
            {
                targetLocation = i;
                targetCount++;
            }
        }
        
        bool containsTarget = email.Contains(targetSymbol);
        bool targetOccursOnce = targetCount == 1;
        bool isPositionValid = targetLocation > 0 && targetLocation < email.Length - 1;
        bool isCorrectFormat = containsTarget && targetOccursOnce && isPositionValid;

        return isCorrectFormat;
    }

    /// <summary>
    /// Validates whether the provided string input meets the following criteria:
    /// <para> - Contains only digits. </para>
    /// <para> - Starts with a <c>0</c>. </para>
    /// <para> - Contains exactly 10 digits.</para>
    /// </summary>
    /// <param name="mobile"> The string to validate. </param>
    /// <returns>
    /// <c>true</c> if the input meets the criteria, otherwise <c>false</c>.
    /// </returns>
    public static bool IsValidMobile(string mobile)
    {
        bool isNumeric = true;
        foreach (char c in mobile)
        {
            if (!char.IsDigit(c))
            {
                isNumeric = false;
                break;
            }
        }

        bool startsWithZero = mobile.StartsWith('0');
        bool isValidLength = mobile.Length == 10;
        bool isCorrectFormat = isNumeric && startsWithZero && isValidLength;

        return isCorrectFormat;
    }

    /// <summary>
    /// Validates whether the provided string input meets the following criteria:
    /// <para> - Be at least 8 characters long. </para>
    /// <para> - Contain a number. </para>
    /// <para> - Contain a lowercase letter. </para>
    /// <para> - Contain an uppercase letter. </para>
    /// <para> 
    /// Use <see cref="IOUtilities.IsValidPasswordMatch()"/> to validate inputs match.
    /// </para>
    /// </summary>
    /// <param name="password"> The string to validate. </param>
    /// <returns>
    /// <c>true</c> if the input meets the criteria, otherwise <c>false</c>.
    /// </returns>
    public static bool IsValidPassword(string password)
    {
        bool isValidLength = password.Length >= 8;
        bool containsUpperCase = false;
        bool containsLowerCase = false;
        bool containsNumber = false;

        for (int i = 0; i < password.Length; i++)
        {
            if (char.IsUpper(password[i]))
            {
                containsUpperCase = true;
            }
            else if (char.IsLower(password[i]))
            {
                containsLowerCase = true;
            }
            else if (char.IsDigit(password[i]))
            {
                containsNumber = true;
            }
        }
        
        bool isCorrectFormat = isValidLength && containsUpperCase && containsLowerCase && containsNumber;
        return isCorrectFormat;
    }

    /// <summary>
    /// Validates whether both provided string inputs match.
    /// </summary>
    /// <param name="firstPasswordInput"> The first password string. </param>
    /// <param name="secondPasswordInput"> The second password string. </param>
    /// <returns>
    /// <c>true</c> if first password matches the second password, otherwise <c>false</c>.
    /// </returns>
    public static bool IsValidPasswordMatch(string firstPasswordInput, string secondPasswordInput)
    {
        bool passwordsMatch = firstPasswordInput == secondPasswordInput;
        return passwordsMatch;
    }

    /// <summary>
    /// Validates whether the provided string input meets the following criteria:
    /// <para> - Must be of the format <c>X,Y</c>. </para>
    /// <para> - <c>X</c> and <c>Y</c> must be integer values. </para>
    /// </summary>
    /// <param name="location"> The string to validate. </param>
    /// <returns> 
    /// <c>true</c> if the input meets the criteria, otherwise <c>false</c>.
    /// </returns>
    public static bool IsValidLocation(string location)
    {
        bool isValidLength = location.Length == 3;
        char targetSymbol = ',';
        int targetLocation = -1;
        int digitCount = 0;

        for (int i = 0; i < location.Length ; i++)
        {
            if (char.IsDigit(location[i]))
            {
                digitCount++;
            }
            
            else if (location[i] == targetSymbol)
            {
                targetLocation = i;
            }
        }

        bool isCorrectFormat = isValidLength && targetLocation == 1 && digitCount == 2;
        return isCorrectFormat;
    }

    /// <summary>
    /// Validates whether the provided string input meets the following criteria:
    /// <para> - Must be between 1 and 8 characters long. </para>
    /// <para> - May contain only uppercase letters, numbers and spaces. </para>
    /// <para> - May not consist entirely of spaces. </para>
    /// </summary>
    /// <param name="licencePlate"> The string to validate. </param>
    /// <returns>
    /// <c>true</c> if the input meets the criteria, otherwise <c>false</c>.
    /// </returns>
    public static bool IsValidLicencePlate(string licencePlate)
    {
        bool isValidLength = licencePlate.Length >= 1 && licencePlate.Length <= 8;
        bool isNotEmpty = !String.IsNullOrWhiteSpace(licencePlate);
        bool containsValidSymbols = true; 
        bool isOnlyUpperCase = true;

        for (int i = 0; i < licencePlate.Length; i++)
        {
            if (!char.IsLetterOrDigit(licencePlate[i]) && !char.IsWhiteSpace(licencePlate[i]))
            {
                containsValidSymbols = false;
                break;
            }
            
            if (char.IsLetter(licencePlate[i]) && char.IsLower(licencePlate[i]))
            {
                isOnlyUpperCase = false;
                break;
            }
        }
        
        bool isCorrectFormat = isValidLength && isNotEmpty && containsValidSymbols && isOnlyUpperCase;
        return isCorrectFormat;
    }

    /// <summary>
    /// Validates whether the provided string input meets the following criteria:
    /// <para> - Contains at least one non-whitespace character. </para>
    /// </summary>
    /// <param name="restaurantName"> The string to validate. </param>
    /// <returns>
    /// <c>true</c> if the input meets the criteria, otherwise <c>false</c>.
    /// </returns>
    public static bool IsValidRestaurantName(string restaurantName)
    {
        bool containsNonWhiteSpaceChar = false;

        for (int i = 0; i < restaurantName.Length; i++)
        {
            if (!char.IsWhiteSpace(restaurantName[i]))
            {
                containsNonWhiteSpaceChar = true;
                break;
            }
        }

        return containsNonWhiteSpaceChar;
    }

    /// <summary>
    /// Validates whether the provided int input meets the following criteria:
    /// <para> - Is a whole integer value between 1 and 6, inclusive. </para>
    /// </summary>
    /// <param name="restaurantStyleChoice"> The int to validate. </param>
    /// <returns>
    /// <c>true</c> if the input meets the criteria, otherwise <c>false</c>.
    /// </returns>
    public static bool IsValidRestaurantStyle(int restaurantStyleChoice)
    {
        List<int> validNumbers = new List<int> { 1, 2, 3, 4, 5, 6 };

        if (validNumbers.Contains(restaurantStyleChoice)) return true;
        else return false;
    }

    /// <summary>
    /// Validates whether the provided decimal input meets the following criteria:
    /// <para> - Is between $0.00 and $999.99 </para>
    /// </summary>
    /// <param name="itemPrice"> The decimal to validate. </param>
    /// <returns>
    /// <c>true</c> if the input meets the criteria, otherwise <c>false</c>.
    /// </returns>
    public static bool IsValidItemPrice(decimal itemPrice)
    {
        decimal lowerLimit = 0.00M;
        decimal upperLimit = 1000.00M;

        bool isValidRange = itemPrice > lowerLimit && itemPrice < upperLimit;
        return isValidRange;
    }
}