using System;

namespace UIComponents;

/// <summary>
/// Contains various static methods for handling general I/O with any users, regardless of type.
/// </summary>
public static class DisplayIO
{
    /// <summary>
    /// Writes an empty line to the screen, to be used as a line break between selections and menus.
    /// </summary>
    public static void DisplayEmptyLine()
    {
        Console.WriteLine();
    }

    /// <summary> Writes the specified string message to the screen on a new line. </summary>
    /// <param name="message"> The string message to display to the screen. </param>
    public static void DisplayMessage(string message)
    {
        Console.WriteLine(message);
    }

    /// <summary>
    /// Reads a line from the user and returns this as a string.
    /// <para> Trims any leading and trailing whitespace from the string. </para>
    /// <para> Assigns an empty value if the original input is empty. </para>
    /// </summary>
    /// <returns> The input as a string. </returns>
    public static string ReadInput()
    {
        string input = Console.ReadLine()?.Trim() ?? "";
        return input;
    }

    /// <summary>
    /// Reads a string input from the user via the console.
    /// <para> Attempts to convert the input into an integer. </para>
    /// </summary>
    /// <Returns> The integer value if valid, 
    /// or a default value (-1) if the input is invalid. </Returns>
    public static int GetChoice()
    {
        string? choice = ReadInput();

        if (int.TryParse(choice, out int result)) return result;
        else return -1;
    }

    /// <summary>
    /// Displays a message informing the user of an invalid choice.
    /// </summary>
    public static void InvalidChoice()
    {
        DisplayMessage("Invalid choice.");
    }

    /// <summary>
    /// Generates a string prompt asking for a numbered input within a specified range.
    /// <para> Formats a message based on the provided upper limit for user choices. </para>
    /// </summary>
    /// <param name="maxChoice"> The maximum valid choice a user can select. </param>
    /// <returns> The formatted string, prompting the user to enter a valid choice. </returns>
    public static string EnterChoiceStr(int maxChoice)
    {
        string enterChoiceStr = "Please enter a choice between 1 and {0}:";
        return string.Format(enterChoiceStr, maxChoice);
    }

    /// <summary>
    /// Generates a string to represent the 'return to previous menu' option,
    /// with the given integer interpolated as the index. 
    /// </summary>
    /// <param name="menuChoiceNum"> The position number for the Return to previous menu option. </param>
    /// <returns> The formatted string, with the correctly numbered position. </returns>
    public static string ReturnToPreviousMenuStr(int menuChoiceNum)
    {
        string returnPreviousMenuStr = "{0}: Return to the previous menu";
        return string.Format(returnPreviousMenuStr, menuChoiceNum);
    }
}