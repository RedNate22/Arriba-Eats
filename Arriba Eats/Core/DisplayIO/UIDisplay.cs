using System;

namespace DisplayIO
{
    /// <summary>
    /// Handles input and output for the menus using the console.
    /// </summary>
    public static class UIDisplay
    {
        /// <summary> Writes the specified string message to the screen. </summary>
        /// <param name="message">The string message to display to the screen.</param>
        public static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
        
        /// <summary>
        /// Reads a string input from the user via the console.
        /// <para> Attempts to convert the input into an integer. </para>
        /// </summary>
        /// <Returns> The integer value if valid, 
        /// or a default value (-1) if the input is invalid. </Returns>
        public static int GetChoice()
        {
            string? choice = Console.ReadLine();
            
            if (int.TryParse(choice, out int result))
            {
                return result;
            }
            else
            {
                return -1;
            }
        }

        public static string GetName()
        {
            return Console.ReadLine();
        }
    }
}
