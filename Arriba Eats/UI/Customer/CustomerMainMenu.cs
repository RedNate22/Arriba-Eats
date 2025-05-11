using System;
using DisplayIO;
using UINavigation;

namespace UI;

// TODO xml
public class CustomerMainMenu : IMenu
{
    private readonly string ENTER_CHOICE_STR = IOUtilities.EnterChoiceStr(5);
    private readonly string LOG_OUT_STR = IOUtilities.LogOutStr(5);

    private const int DISPLAY_USER_INFO_INT = 1, SELECT_RESTAURANTS_LIST_INT = 2, SEE_ORDERS_STATUS_INT = 3,
        RATE_RESTAURANT_INT = 4, LOG_OUT_INT = 5;

    // TODO xml
    public void DisplayMenu()
    {
        IODisplay.DisplayMessage(MenuConstants.MAKE_CHOICE_STR);
        IODisplay.DisplayMessage(MenuConstants.DISPLAY_USER_INFO_STR);
        IODisplay.DisplayMessage(MenuConstants.SELECT_RESTAURANTS_LIST_STR);
        IODisplay.DisplayMessage(MenuConstants.SEE_ORDERS_STATUS_STR);
        IODisplay.DisplayMessage(MenuConstants.RATE_RESTAURANT_STR);
        IODisplay.DisplayMessage(LOG_OUT_STR);
        IODisplay.DisplayMessage(ENTER_CHOICE_STR);

        int option = IODisplay.GetChoice();

        switch (option)
        {
            case DISPLAY_USER_INFO_INT:
                if (SessionManager.CurrentUser != null)
                {
                    IODisplay.DisplayUserInfo(SessionManager.CurrentUser);
                }
                else
                {
                    IODisplay.DisplayMessage("No user is currently logged in.");
                }
                break;
            case SELECT_RESTAURANTS_LIST_INT:
                break;
            case SEE_ORDERS_STATUS_INT:
                break;
            case RATE_RESTAURANT_INT:
                break;
            case LOG_OUT_INT:
                SessionManager.Logout();
                break;
        }
    }
}
    
