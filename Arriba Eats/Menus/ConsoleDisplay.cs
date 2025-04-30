using System;

namespace Menus
{
    /// <summary>
    /// Handles I/O for the user to navigate the program.
    /// </summary>
    public static class ConsoleDisplay
    {
        /// <summary>
        /// Displays a message to the screen.
        /// </summary>
        /// <param name="message">A string</param>
        public static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
        
        public static int GetChoice()
        {
            string? choice = Console.ReadLine();
            
            if (int.TryParse(choice, out int result))
            {
                return result;
            }
            
            // Handle invalid input
            return 0;
        }
    }
}
