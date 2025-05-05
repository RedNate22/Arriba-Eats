using System;
using DisplayIO;
using UINavigation;

namespace Menus
{
    public class RegistrationMenu : IMenu
    {
        private const string USER_TYPE_STR = "Which type of user would you like to register as?";
        private const string CUSTOMER_STR = "1: Customer";
        private const string DELIVERER_STR = "2: Deliverer";
        private const string CLIENT_STR = "3: Client";
        private const string RETURN_STR = "4: Return to the previous menu";
        private readonly string ENTER_CHOICE_STR = UIUtilities.EnterChoiceStr(4);


        public void DisplayMenu()
        {

        }
    }
}

