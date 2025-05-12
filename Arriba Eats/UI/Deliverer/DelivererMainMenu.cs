using System;
using DisplayIO;
using UINavigation;

namespace UI;

// TODO xml
public class DelivererMainMenu : IMenu
{
    private string _logOut = IOUtilities.LogOutStr(5);
    private string _enterChoice = IOUtilities.EnterChoiceStr(5);
    private const int DISPLAY_USER_INFO_INT = 1, LIST_ORDERS_AVAILABLE_INT = 2, ARRIVED_RESTAURANT_INT = 3,
        MARK_DELIVERY_COMPLETE_INT = 4, LOG_OUT_INT = 5;
    
    // TODO xml
    public void DisplayMenu()
    {
        IODisplay.DisplayMessage(MenuConstants.MAKE_CHOICE_STR);
        IODisplay.DisplayMessage(MenuConstants.DELIVERER_MAIN_MENU_CHOICES_STR);
        IODisplay.DisplayMessage(_logOut);
        IODisplay.DisplayMessage(_enterChoice);

        int choice = IODisplay.GetChoice();

        switch (choice)
        {
            case DISPLAY_USER_INFO_INT:
                IODisplay.DisplayUserInfo(SessionManager.CurrentUser!);
                break;

            case LIST_ORDERS_AVAILABLE_INT:
                // TODO
                break;

            case ARRIVED_RESTAURANT_INT:
                // TODO
                break;

            case MARK_DELIVERY_COMPLETE_INT:
                // TODO
                break;

            case LOG_OUT_INT:
                SessionManager.Logout();
                break;
        }
    }    
}
