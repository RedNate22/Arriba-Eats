using System;

namespace DisplayIO;

/// <summary>
/// Provides helper methods for managing user interface interactions.
/// <para> Contains utility methods for dynamically formatting UI messages. </para>
/// </summary>
public static class UIUtilities
{
    /// <summary>
    /// The template string to be used by <see cref="UIUtilities.EnterChoiceStr(int)">
    /// </summary>
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
}