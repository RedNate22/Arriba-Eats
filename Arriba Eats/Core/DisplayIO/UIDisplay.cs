using System;

namespace DisplayIO
{
    /// <summary>
    /// Handles input and output for the menus using the console.
    /// <para> 
    /// Uses <see cref="DisplayIO.UIUtilities"/> for input formatting and validation. 
    /// </para>
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
        /// Uses <see cref="Console.ReadLine"/> to read an input from the user and returns this as a string.
        /// </summary>
        /// <returns> The input as a string. </returns>
        public static string ReadInput()
        {
            string input;
            input = Console.ReadLine() ?? "";
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
                
                string input = ReadInput()?.Trim() ?? "";

                if (UIUtilities.IsValidName(input)) return input;
                DisplayMessage("Invalid name.");
            }
        }
        
        public static int GetAge()
        {
            while (true)
            {
                DisplayMessage("Please enter your age (18-100):");

                if (int.TryParse(ReadInput(), out int input))
                {
                    if (UIUtilities.IsValidAge(input)) return input;
                }
                DisplayMessage("Invalid age.");
            }
        }


        // TODO Also add check against existing emails
        public static string GetEmail()
        {
            while (true)
            {
                DisplayMessage("Please enter your email address:");

                string input = ReadInput()?.Trim() ?? "";
                
                if (UIUtilities.IsValidEmail(input))
                {
                    /*
                    if email already exists
                    {
                        DisplayMessage("This email address is already in use.");
                    }
                    */
                    return input;
                }
                
                else DisplayMessage("Invalid email address.");
            }
        }
    }
}
