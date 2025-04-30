using System;

namespace Menus
{
    public class MainMenu
    {
        // Main menu strings
        private const string WELCOME = "Welcome to Arriba Eats!";
        private const string OPTION_1_STR = "1: Login as a registered user";
        private const string OPTION_2_STR = "2: Register as a new user";
        private const string OPTION_3_STR = "3: Exit";
        private const string CHOICE_STR = "Please enter a choice between 1 and 3:";

        // Int for each option
        private const int OPTION_1 = 1, OPTION_2 = 2, OPTION_3 = 3;

        /// <summary>
        /// Displays the welcome message.
        /// </summary>
        public void WelcomeMessage()
        {
            ConsoleDisplay.DisplayMessage(WELCOME);
        }

        /// <summary>
        /// Display the main menu options and gets option choice from user.
        /// </summary>
        public void DisplayMenu()
        {
            ConsoleDisplay.DisplayMessage(OPTION_1_STR);
            ConsoleDisplay.DisplayMessage(OPTION_2_STR);
            ConsoleDisplay.DisplayMessage(OPTION_3_STR);
            ConsoleDisplay.DisplayMessage(CHOICE_STR);

            // Get choice of user
            int option = ConsoleDisplay.GetChoice();

            switch (option)
            {
                case 1:
                    Console.WriteLine("Option 1 selected");
                    break;
                case 2:
                    Console.WriteLine("Option 2 selected");
                    break;
                case 3:
                    break;
            }
        }

        public void Run()
        {
            WelcomeMessage();
            DisplayMenu();
        }
    }
}