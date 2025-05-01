using System;

namespace Menus
{
    public class MainMenu
    {
        // Strings to display options and messages for the main menu
        private const string WELCOME_STR = "Welcome to Arriba Eats!";
        private const string LOGIN_STR = "1: Login as a registered user";
        private const string REGISTER_STR = "2: Register as a new user";
        private const string EXIT_STR = "3: Exit";
        private const string ENTER_CHOICE_STR = "Please enter a choice between 1 and 3:";
        private const string GOODBYE_STR = "Thank you for using Arriba Eats!";

        // Int for each option
        private const int LOGIN_INT = 1, REGISTER_INT = 2, EXIT_INT = 3;

        // Control flag for main program loop
        private bool _isRunning = true;  
        
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
            ConsoleDisplay.DisplayMessage(MenusConstants.MAKE_CHOICE_STR);
            ConsoleDisplay.DisplayMessage(LOGIN_STR);
            ConsoleDisplay.DisplayMessage(REGISTER_STR);
            ConsoleDisplay.DisplayMessage(EXIT_STR);
            ConsoleDisplay.DisplayMessage(ENTER_CHOICE_STR);

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
                    ConsoleDisplay.DisplayMessage(GOODBYE_STR);
                    _isRunning = false;
                    break;
                default:  // User has entered an invalid option
                    ConsoleDisplay.DisplayMessage(MenusConstants.INVALID_CHOICE_STR);
                    break;
            }
        }

        
        /// <summary>
        /// Entry point to start the program and begin the main input loop.
        /// </summary>
        public void Run()
        {
            WelcomeMessage(); 
            
            while(_isRunning)
            {
                DisplayMenu();
            }
        }
    }
}