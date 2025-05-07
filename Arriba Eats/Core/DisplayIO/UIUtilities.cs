using System;

namespace DisplayIO;

/// <summary>
/// Provides helper methods for managing user interface interactions.
/// <para> Contains utility methods for dynamically formatting UI messages. </para>
/// </summary>
public static class UIUtilities
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
    /// Validates whether the provided string input consists of 
    /// at least one letter--and only letters,
    /// spaces, apostrophes and hyphens.
    /// </summary>
    /// <param name="input"> The string to validate. </param>
    /// <returns> 
    /// <c>true</c> if the input contains at least one letter and consists of only the
    /// allowed letters, otherwise <c>false</c>.
    /// </returns>
    public static bool IsValidName(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return false;
        
        else if (input.Length > 0)
        {
            bool containsValidCharacters = false;
            foreach (char character in input)
            {
                // Contains at least one letter 
                if (char.IsLetter(character)) containsValidCharacters = true;
                
                // Contains invalid characters
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
        return true;
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

        if (containsTarget && targetOccursOnce && isPositionValid) return true;
        else return false;
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

        if (isNumeric && startsWithZero && isValidLength) return true;
        else return false;
    }

    /// <summary>
    /// Validates whether the provided string input meets the following criteria:
    /// <para> - Be at least 8 characters long. </para>
    /// <para> - Contain a number </para>
    /// <para> - Contain a lowercase letter </para>
    /// <para> - Contain an uppercase letter </para>
    /// <para> 
    /// Use <see cref="UIUtilities.IsValidPasswordMatch()"/> to validate inputs match.
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
        
        if (isValidLength && containsUpperCase && containsLowerCase && containsNumber)
        {
            return true;
        }

        else return false;
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
        if (passwordsMatch) return true;
        else return false;
    }

    // TODO XML
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

        if (isCorrectFormat) return true;
        else return false;
    }
}