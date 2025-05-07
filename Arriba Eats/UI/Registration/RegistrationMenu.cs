using System;
using DisplayIO;
using UINavigation; 

namespace UI
{
    /// <summary>
    /// Represents the registration menu.
    /// <para> Allows users to register as different types, or return to the main menu. </para>
    /// <para> 
    /// The types are selected by calling <see cref="RegistrationProcess.Register()"/> and parsing
    /// a string parameter, named after the user type.
    /// </para>
    /// </summary>
    public class RegistrationMenu : IMenu
    {
        private const string USER_TYPE_STR = "Which type of user would you like to register as?";
        private const string CUSTOMER_STR = "1: Customer";
        private const string DELIVERER_STR = "2: Deliverer";
        private const string CLIENT_STR = "3: Client";
        private const string RETURN_STR = "4: Return to the previous menu";
        private readonly string ENTER_CHOICE_STR = UIUtilities.EnterChoiceStr(4);

        private const int CUSTOMER_INT = 1, DELIVERER_INT = 2, CLIENT_INT = 3, RETURN_INT = 4;
        const string CUSTOMER = "CUSTOMER";
        const string DELIVERER = "DELIVERER";
        const string CLIENT = "CLIENT";

        // TODO finish this xml
        /// <summary>
        /// Displays the registration menu options and gets choice of which option 
        /// to choose from the user. Calls th.
        /// <para> Uses <see cref="UIDisplay.DisplayMessage()"/> to display the options. </para>
        /// <para> Uses <see cref="UIDisplay.GetChoice()"/> to get a choice from the user in the form
        /// of an integer.
        /// <para> Matches the integer with the associated menu option and calls </para>
        /// </summary>
        public void DisplayMenu()
        {
            UIDisplay.DisplayMessage(USER_TYPE_STR);
            UIDisplay.DisplayMessage(CUSTOMER_STR);
            UIDisplay.DisplayMessage(DELIVERER_STR);
            UIDisplay.DisplayMessage(CLIENT_STR);
            UIDisplay.DisplayMessage(RETURN_STR);
            UIDisplay.DisplayMessage(ENTER_CHOICE_STR);

            int option = UIDisplay.GetChoice();

            switch (option)
            {
                case CUSTOMER_INT:  // User chooses option 1: Register as customer
                    UIDisplay.DisplayEmptyLine();
                    RegistrationProcess.Register(CUSTOMER);
                    UIFlowController.CurrentState = MenuState.MainMenu;
                    break;
                
                case DELIVERER_INT:  // User chooses option 2: Register as deliverer
                    UIDisplay.DisplayEmptyLine();
                    RegistrationProcess.Register(DELIVERER);
                    break;
                
                case CLIENT_INT:  // User chooses option 3: Register as client
                    UIDisplay.DisplayEmptyLine();
                    RegistrationProcess.Register(CLIENT);
                    break;
                
                case RETURN_INT:  // User chooses option 4: Return to previous menu 
                    UIDisplay.DisplayEmptyLine();
                    UIFlowController.CurrentState = MenuState.MainMenu;
                    break;
                
                default:  // User has entered an invalid option
                    UIDisplay.DisplayMessage(UIConstants.INVALID_CHOICE_STR);
                    break;
            }
        }
    }
}

