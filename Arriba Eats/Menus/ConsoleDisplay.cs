using System;

namespace Menus
{
    /// <summary>
    /// Handles output to the screen.
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
    }
}
