using System;

namespace Menus
{
    public class LoginMenu
    {
        private const string WELCOME = "Welcome to Arriba Eats!";
        private const string MAIN_MENU = """
            Please make a choice from the menu below:
            1: Login as a registered user
            2: Register as a new user
            3: Exit
            Please enter a choice between 1 and 3:
            """;
        
        /// <summary>
        /// Displays the welcome message.
        /// </summary>
        public void WelcomeMessage()
        {
            ConsoleDisplay.DisplayMessage(WELCOME);
        }

        public void MainMenu()
        {
            ConsoleDisplay.DisplayMessage(MAIN_MENU);
        }

        public void Run()
        {
            WelcomeMessage();
            MainMenu();
        }
    }
}