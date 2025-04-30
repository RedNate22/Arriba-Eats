using System;

namespace Menus
{
    public class MainMenu
    {
        // Main menu strings
        private const string WELCOME_STR = "Welcome to Arriba Eats!";
        private const string LOGIN_STR = "1: Login as a registered user";
        private const string REGISTER_STR = "2: Register as a new user";
        private const string EXIT_STR = "3: Exit";
        private const string ENTER_CHOICE_STR = "Please enter a choice between 1 and 3:";
        private const string EXIT_MESSAGE_STR = "Thank you for using Arriba Eats!";

        // Int for each option
        private const int LOGIN_INT = 1, REGISTER_INT = 2, EXIT_INT = 3;

        bool isRunning = true;  // Control flag for main program loop
        
        /// <summary>
        /// Displays the welcome message.
        /// </summary>
        public void WelcomeMessage()
        {
            ConsoleDisplay.DisplayMessage(WELCOME_STR);
        }

        /// <summary>
        /// Display the main menu options and gets option choice from user.
        /// </summary>
        public void DisplayMenu()
        {
            // Display choices to the screen.
            ConsoleDisplay.DisplayMessage(MenusConstants.MAKE_CHOICE_STR);
            ConsoleDisplay.DisplayMessage(LOGIN_STR);
            ConsoleDisplay.DisplayMessage(REGISTER_STR);
            ConsoleDisplay.DisplayMessage(EXIT_STR);
            ConsoleDisplay.DisplayMessage(ENTER_CHOICE_STR);

            // Get choice from user.
            int option = ConsoleDisplay.GetChoice();

            switch (option)
            {
                case LOGIN_INT:  // User chooses option 1: Go to login menu
                    ConsoleDisplay.DisplayMessage(MenusConstants.OPTION_1_SELECTED_STR);
                    break;
                case REGISTER_INT:  // User chooses option 2: Go to register menu
                    ConsoleDisplay.DisplayMessage(MenusConstants.OPTION_2_SELECTED_STR);
                    break;
                case EXIT_INT:  // User chooses option 3: Exit program
                    ConsoleDisplay.DisplayMessage(EXIT_MESSAGE_STR);
                    isRunning = false;
                    break;
                default:  // User has entered an invalid option
                    ConsoleDisplay.DisplayMessage("Invalid option. Please choose again.");
                    break;
            }
        }

        
        /// <summary>
        /// Entry method to start the program.
        /// </summary>
        public void Run()
        {
            WelcomeMessage();  // Welcome the user
            
            while(isRunning)
            {
                DisplayMenu();
            }
        }
    }
}