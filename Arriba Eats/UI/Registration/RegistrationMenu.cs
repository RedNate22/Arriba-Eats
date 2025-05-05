using System;
using DisplayIO;
using UINavigation;

namespace Menus
{
    /// <summary>
    /// Represents the registration menu.
    /// <para> Allows users to register as different types, or return to the main menu. </para>
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

        /// <summary>
        /// Displays the registration menu options and gets choice from using.
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
                    UIDisplay.DisplayMessage(UIConstants.OPTION_1_SELECTED_STR);
                    const string CUSTOMER = "CUSTOMER";
                    RegistrationProcess(CUSTOMER);
                    break;
                case DELIVERER_INT:  // User chooses option 2: Register as deliverer
                    UIDisplay.DisplayMessage(UIConstants.OPTION_2_SELECTED_STR);
                    break;
                case CLIENT_INT:  // User chooses option 3: Register as client
                    UIDisplay.DisplayMessage(UIConstants.OPTION_3_SELECTED_STR);
                    break;
                case RETURN_INT:  // User chooses option 4: Return to previous menu 
                    UIDisplay.DisplayMessage(UIConstants.OPTION_4_SELECTED_STR);
                    UIFlowController.CurrentState = MenuState.MainMenu;
                    break;
                default:  // User has entered an invalid option
                    UIDisplay.DisplayMessage(UIConstants.INVALID_CHOICE_STR);
                    break;
            }
        }

        public void RegistrationProcess(string userType)
        {
            string name = UIDisplay.GetName();
            int age = UIDisplay.GetAge();
            string email = UIDisplay.GetEmail();

            switch (userType)
            {
                case "CUSTOMER":
                    break;
                case "DELIVERER":
                    break;
                case "CLIENT":
                    break;
            }
        }
    }
}

