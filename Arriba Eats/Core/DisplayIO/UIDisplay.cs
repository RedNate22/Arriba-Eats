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

        /// <summary>
        /// Reads a string input from the user via the console and ensures it meets
        /// the validation criteria.
        /// <para> 
        /// Uses the <see cref="UIUtilities.IsValidName"/> verify the input contains only
        /// valid characters and meets sanitisation requirements.
        /// </para>
        /// </summary>
        /// <returns> The valid and sanitised name as a string. </returns>
        public static string GetName()
        {
            while (true)
            {
                DisplayMessage("Please enter your name:");
                
                string input = Console.ReadLine()?.Trim() ?? "";

                if (UIUtilities.IsValidName(input))
                {
                    return input;
                }
                DisplayMessage("Invalid name.");
            }
        }
        
        public static int GetAge()
        {
            return 0;
        }
    }
}
