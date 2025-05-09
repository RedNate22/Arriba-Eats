using System;

namespace DisplayIO;

/// <summary>
/// Provides helper methods for managing user interface interactions.
/// <para> Also contains utility methods for dynamically formatting UI messages. </para>
/// </summary>
public static class IOUtilities
{
    private const string ENTER_CHOICE_TEMPLATE = "Please enter a choice between 1 and {0}:";
    
    /// <summary>
    /// Generates a string prompt asking for input within a specified range.
    /// <para> Formats a message based on the provided upper limit for user choices. </para>
    /// </summary>
    /// <param name="maxChoice"> The maximum valid choice a user can select. </param>
    /// <returns> A formatted string, prompting the user to enter a valid choice. </returns>
    public static string EnterChoiceStr(int maxChoice)
    {
        return string.Format(ENTER_CHOICE_TEMPLATE, maxChoice);
    }

    /// <summary>
    /// Validates whether the provided string input meets the following criteria:
    /// - Consists of at least one letter.
    /// - Only letters, spaces, apostrophes, and hyphens.
    /// </summary>
    /// <param name="input"> The string to validate. </param>
    /// <returns> 
    /// <c>true</c> if the input meets the criteria, otherwise <c>false</c>.
    /// </returns>
    public static bool IsValidName(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return false;
        
        else if (input.Length > 0)
        {
            bool containsValidCharacters = false;
            foreach (char character in input)
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
    /// <param name="input"> The int to validate. </param>
    /// <returns>
    /// <c>true</c> if the input meets the criteria, otherwise <c>false</c>.
    /// </returns>
    public static bool IsValidAge(int input)
    {
        if (input < 18 || input > 100) return false;
        else return true;
    }

    /// <summary>
    /// Validates whether the provided string input meets the following criteria:
    /// <para> - Contains exactly one instance of the target symbol (<c>@</c>). </para>
    /// <para> - Contains at least one character on both sides of the target symbol. </para>
    /// </summary>
    /// <param name="input"> The string to validate. </param>
    /// <returns> 
    /// <c>true</c> if input meets the criteria, otherwise <c>false</c>. 
    /// </returns>
    public static bool IsValidEmail(string input)
    {
        char targetSymbol = '@';
        int targetLocation = -1;
        int targetCount = 0;

        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == targetSymbol)
            {
                targetLocation = i;
                targetCount++;
            }
        }
        
        bool containsTarget = input.Contains(targetSymbol);
        bool targetOccursOnce = targetCount == 1;
        bool isPositionValid = targetLocation > 0 && targetLocation < input.Length - 1;
        bool isCorrectFormat = containsTarget && targetOccursOnce && isPositionValid;

        return isCorrectFormat;
    }

    /// <summary>
    /// Validates whether the provided string input meets the following criteria:
    /// <para> - Contains only digits. </para>
    /// <para> - Starts with a <c>0</c>. </para>
    /// <para> - Contains exactly 10 digits.</para>
    /// </summary>
    /// <param name="input"> The string to validate. </param>
    /// <returns>
    /// <c>true</c> if the input meets the criteria, otherwise <c>false</c>.
    /// </returns>
    public static bool IsValidMobile(string input)
    {
        bool isNumeric = true;
        foreach (char c in input)
        {
            if (!char.IsDigit(c))
            {
                isNumeric = false;
                break;
            }
        }

        bool startsWithZero = input.StartsWith('0');
        bool isValidLength = input.Length == 10;
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
    /// <param name="input"> The string to validate. </param>
    /// <returns>
    /// <c>true</c> if the input meets the criteria, otherwise <c>false</c>.
    /// </returns>
    public static bool IsValidPassword(string input)
    {
        bool isValidLength = input.Length >= 8;
        bool containsUpperCase = false;
        bool containsLowerCase = false;
        bool containsNumber = false;

        for (int i = 0; i < input.Length; i++)
        {
            if (char.IsUpper(input[i]))
            {
                containsUpperCase = true;
            }
            else if (char.IsLower(input[i]))
            {
                containsLowerCase = true;
            }
            else if (char.IsDigit(input[i]))
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
    /// <param name="firstInput"> The first string. </param>
    /// <param name="secondInput"> The second string. </param>
    /// <returns>
    /// <c>true</c> if first input matches the second input, otherwise <c>false</c>.
    /// </returns>
    public static bool IsValidPasswordMatch(string firstInput, string secondInput)
    {
        bool passwordsMatch = firstInput == secondInput;
        return passwordsMatch;
    }

    /// <summary>
    /// Validates whether the provided string input meets the following criteria:
    /// <para> - Must be of the format <c>X,Y</c>. </para>
    /// <para> - <c>X</c> and <c>Y</c> must be integer values. </para>
    /// </summary>
    /// <param name="input"> The string to validate. </param>
    /// <returns> 
    /// <c>true</c> if the input meets the criteria, otherwise <c>false</c>.
    /// </returns>
    public static bool IsValidLocation(string input)
    {
        bool isValidLength = input.Length == 3;
        char targetSymbol = ',';
        int targetLocation = -1;
        int digitCount = 0;

        for (int i = 0; i < input.Length ; i++)
        {
            if (char.IsDigit(input[i]))
            {
                digitCount++;
            }
            
            else if (input[i] == targetSymbol)
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
    /// <param name="input"> The string to validate. </param>
    /// <returns>
    /// <c>true</c> if the input meets the criteria, otherwise <c>false</c>.
    /// </returns>
    public static bool IsValidLicencePlate(string input)
    {
        bool isValidLength = input.Length >= 1 && input.Length <= 8;
        bool isNotEmpty = !String.IsNullOrWhiteSpace(input);
        bool containsValidSymbols = true; 
        bool isOnlyUpperCase = true;

        for (int i = 0; i < input.Length; i++)
        {
            if (!char.IsLetterOrDigit(input[i]) && !char.IsWhiteSpace(input[i]))
            {
                containsValidSymbols = false;
                break;
            }
            
            if (char.IsLetter(input[i]) && char.IsLower(input[i]))
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
    /// <param name="input"> The string to validate. </param>
    /// <returns>
    /// <c>true</c> if the input meets the criteria, otherwise <c>false</c>.
    /// </returns>
    public static bool IsValidRestaurantName(string input)
    {
        bool containsNonWhiteSpaceChar = false;

        for (int i = 0; i < input.Length; i++)
        {
            if (!char.IsWhiteSpace(input[i]))
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
    /// <param name="choice"> The int to validate. </param>
    /// <returns>
    /// <c>true</c> if the input meets the criteria, otherwise <c>false</c>.
    /// </returns>
    public static bool IsValidRestaurantStyle(int choice)
    {
        List<int> validNumbers = new List<int> { 1, 2, 3, 4, 5, 6 };

        if (validNumbers.Contains(choice)) return true;
        else return false;
    }
}