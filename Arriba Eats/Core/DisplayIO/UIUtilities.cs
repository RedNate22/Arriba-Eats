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
    /// specified range.
    /// </summary>
    /// <param name="input"> The int to validate. </param>
    /// <returns>
    /// <c>true</c> if the input is within range (18-100 inclusive),
    /// otherwise <c>false</c>
    /// </returns>
    public static bool IsValidAge(int input)
    {
        if (input < 18 || input > 100) return false;
        return true;
    }

    public static bool IsValidEmail(string input)
    {
        return false;
    }
}