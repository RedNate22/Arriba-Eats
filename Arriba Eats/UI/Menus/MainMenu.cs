using System;
using ConsoleIO;
using Navigation;

namespace Menus
{
    /// <summary>
    /// Displays the main menu where the user can login, register or exit.
    /// </summary>
    public class MainMenu : IMenu
    {
        // Strings to display options and messages for the main menu
        private const string WELCOME_STR = "Welcome to Arriba Eats!";
        private const string LOGIN_STR = "1: Login as a registered user";
        private const string REGISTER_STR = "2: Register as a new user";
        private const string EXIT_STR = "3: Exit";
        private const string ENTER_CHOICE_STR = "Please enter a choice between 1 and 3:";
        private const string GOODBYE_STR = "Thank you for using Arriba Eats!";

        private const int LOGIN_INT = 1, REGISTER_INT = 2, EXIT_INT = 3;
        
        /// <summary>
        /// Displays the welcome message.
        /// </summary>
        public void WelcomeMessage()
        {
            ConsoleDisplay.DisplayMessage(WELCOME_STR);
        }

        /// <summary>
        /// Display the main menu options and gets choice from user.
        /// </summary>
        public void DisplayMenu()
        {
            ConsoleDisplay.DisplayMessage(UIConstants.MAKE_CHOICE_STR);
            ConsoleDisplay.DisplayMessage(LOGIN_STR);
            ConsoleDisplay.DisplayMessage(REGISTER_STR);
            ConsoleDisplay.DisplayMessage(EXIT_STR);
            ConsoleDisplay.DisplayMessage(ENTER_CHOICE_STR);

            int option = ConsoleDisplay.GetChoice();

            switch (option)
            {
                case LOGIN_INT:  // User chooses option 1: Go to login menu
                    ConsoleDisplay.DisplayMessage(UIConstants.OPTION_1_SELECTED_STR);
                    break;
                case REGISTER_INT:  // User chooses option 2: Go to register menu
                    ConsoleDisplay.DisplayMessage(UIConstants.OPTION_2_SELECTED_STR);
                    break;
                case EXIT_INT:  // User chooses option 3: Exit program
                    ConsoleDisplay.DisplayMessage(GOODBYE_STR);
                    MenuController.currentState = MenuState.Exit;
                    break;
                default:  // User has entered an invalid option
                    ConsoleDisplay.DisplayMessage(UIConstants.INVALID_CHOICE_STR);
                    break;
            }
        }
    }
}