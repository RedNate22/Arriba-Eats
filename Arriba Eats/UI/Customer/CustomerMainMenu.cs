using System;
using UIComponents;
using UINavigation;

namespace UI;

/// <summary>
/// Represents the <see cref="Entities.Customer"/> main menu.
/// <para> 
/// From here, the <see cref="Entities.Customer"/> can view their <see cref="Entities.User"/> information,
/// select a list of <see cref="Entities.Restaurant"/>s to order from, see the status of their orders, and
/// rate a <see cref="Entities.Restaurant"/> they've ordered from.
/// </para>
/// </summary>
public class CustomerMainMenu : IMenu
{
    private string _logOut = IOUtilities.LogOutStr(5);
    private string _enterChoice = IOUtilities.EnterChoiceStr(5);
    private const int DISPLAY_USER_INFO_INT = 1, SELECT_RESTAURANTS_LIST_INT = 2, SEE_ORDERS_STATUS_INT = 3,
        RATE_RESTAURANT_INT = 4, LOG_OUT_INT = 5;
    
    /// <summary>
    /// A count to track if the <see cref="IODisplay.WelcomeUser()"/>
    /// method has been run. This is to prevent the message from being displayed
    /// more than once.
    /// </summary>
    private int _welcomeCount = 0;  // ? Turn this into a common IMenu field?

    // TODO xml
    public void DisplayMenu()
    {
        if (_welcomeCount == 0)
        {
            IODisplay.WelcomeUser();
            _welcomeCount++;
        }

        IODisplay.DisplayMessage(MenuConstants.MAKE_CHOICE_STR);
        IODisplay.DisplayMessage(MenuConstants.DISPLAY_USER_INFO_STR);
        IODisplay.DisplayMessage(CustomerConstants.CUSTOMER_MAIN_MENU_CHOICES_STR);
        IODisplay.DisplayMessage(_logOut);
        IODisplay.DisplayMessage(_enterChoice);

        int choice = IODisplay.GetChoice();

        switch (choice)
        {
            case DISPLAY_USER_INFO_INT:
                IODisplay.DisplayUserInfo(SessionManager.CurrentUser!);
                break;
            
            case SELECT_RESTAURANTS_LIST_INT:
                UIFlowController.ChangeMenu(MenuState.CustomerListRestaurantsMenu);
                break;
            
            case SEE_ORDERS_STATUS_INT:
                UIFlowController.ChangeMenu(MenuState.CustomerOrderStatusMenu);
                break;
            
            case RATE_RESTAURANT_INT:
                UIFlowController.ChangeMenu(MenuState.CustomerRateRestaurantMenu);
                break;
            
            case LOG_OUT_INT:
                SessionManager.Logout();
                break;
        }
    }
}
    
