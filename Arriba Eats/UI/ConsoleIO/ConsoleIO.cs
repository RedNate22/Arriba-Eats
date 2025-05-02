using System;

namespace ConsoleIO
{
    /// <summary>
    /// Handles input and output for the menus using the console.
    /// </summary>
    public static class ConsoleDisplay
    {
        /// <summary>
        /// Writes the specified string message to the screen.
        /// </summary>
        /// <param name="message">The string message to display to the screen.</param>
        public static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
        
        /// <summary>
        /// Reads a string input from the user via the console.
        /// <para>
        /// Attempts to convert the input into an integer.
        /// </para>
        /// Returns the integer value if valid, or a default value (-1) if the input
        /// is invalid.
        /// </summary>
        /// <returns>The user's input as an integer, or -1 if the input is invalid.</returns>
        public static int GetChoice()
        {
            string? choice = Console.ReadLine();
            
            if (int.TryParse(choice, out int result))
            {
                return result;
            }
            
            return -1;
        }
    }
}
