using System;
using DisplayIO;
using UINavigation;

namespace Menus
{
    /// <summary>
    /// Represents the main menu.
    /// <para> Allows users to login, register, or exit the application. </para>
    /// </summary>
    public class MainMenu : IMenu
    {
        private const string WELCOME_STR = "Welcome to Arriba Eats!";
        private const string LOGIN_STR = "1: Login as a registered user";
        private const string REGISTER_STR = "2: Register as a new user";
        private const string EXIT_STR = "3: Exit";
        private readonly string ENTER_CHOICE_STR = UIUtilities.EnterChoiceStr(3);
        private const string GOODBYE_STR = "Thank you for using Arriba Eats!";

        private const int LOGIN_INT = 1, REGISTER_INT = 2, EXIT_INT = 3;
        
        /// <summary> 
        /// Displays the welcome message.
        /// <para> To be called seperately from the menu transitions. </para>
        /// </summary>
        public static void WelcomeMessage()
        {
            UIDisplay.DisplayMessage(WELCOME_STR);
        }

        /// <summary> 
        /// Displays the main menu options and gets choice from user. 
        /// </summary>
        public void DisplayMenu()
        {
            UIDisplay.DisplayMessage(UIConstants.MAKE_CHOICE_STR);
            UIDisplay.DisplayMessage(LOGIN_STR);
            UIDisplay.DisplayMessage(REGISTER_STR);
            UIDisplay.DisplayMessage(EXIT_STR);
            UIDisplay.DisplayMessage(ENTER_CHOICE_STR);

            int option = UIDisplay.GetChoice();

            switch (option)
            {
                case LOGIN_INT:  // User chooses option 1: Go to login menu
                    UIDisplay.DisplayMessage(UIConstants.OPTION_1_SELECTED_STR);
                    break;
                case REGISTER_INT:  // User chooses option 2: Go to register menu
                    UIDisplay.DisplayMessage(UIConstants.OPTION_2_SELECTED_STR);
                    UIFlowController.CurrentState = MenuState.RegistrationMenu;
                    break;
                case EXIT_INT:  // User chooses option 3: Exit program
                    UIDisplay.DisplayMessage(GOODBYE_STR);
                    UIFlowController.CurrentState = MenuState.Exit;
                    break;
                default:  // User has entered an invalid option
                    UIDisplay.DisplayMessage(UIConstants.INVALID_CHOICE_STR);
                    break;
            }
        }
    }
}