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
    private string _logOut = UserIO.LogOutStr(5);
    private string _enterChoice = DisplayIO.EnterChoiceStr(5);

    /// <summary>
    /// Defines the <see cref="int"/> constants representing the menu options for use in a
    /// <see cref="switch"/> statement.
    /// </summary>
    private const int DISPLAY_USER_INFO_INT = 1, SELECT_RESTAURANTS_LIST_INT = 2, SEE_ORDERS_STATUS_INT = 3,
        RATE_RESTAURANT_INT = 4, LOG_OUT_INT = 5;
    
    /// <summary>
    /// A count to track if the <see cref="DisplayIO.WelcomeUser()"/>
    /// method has been run. This is to prevent the message from being displayed
    /// more than once.
    /// </summary>
    private int _welcomeCount = 0;  

    /// <summary>
    /// Displays the <see cref="CustomerMainMenu"/> options and prompts the <see cref="Entities.Customer"/>
    /// to choose an option.
    /// </summary>
    public void DisplayMenu()
    {
        if (_welcomeCount == 0)
        {
            UserIO.WelcomeUser();
            _welcomeCount++;
        }

        DisplayIO.DisplayMessage(MenuConstants.MAKE_CHOICE_STR);
        DisplayIO.DisplayMessage(MenuConstants.DISPLAY_USER_INFO_STR);
        DisplayIO.DisplayMessage(CustomerConstants.CUSTOMER_MAIN_MENU_CHOICES_STR);
        DisplayIO.DisplayMessage(_logOut);
        DisplayIO.DisplayMessage(_enterChoice);

        int choice = DisplayIO.GetChoice();

        switch (choice)
        {
            case DISPLAY_USER_INFO_INT:
                UserIO.DisplayUserInfo(SessionManager.ReturnCurrentUser());
                break;

            case SELECT_RESTAURANTS_LIST_INT:
                UIFlowController.ChangeMenu(MenuState.CustomerSortRestaurantsMenu);
                break;

            case SEE_ORDERS_STATUS_INT:
                UIFlowController.ChangeMenu(MenuState.CustomerOrderStatusMenu);
                break;

            case RATE_RESTAURANT_INT:
                UIFlowController.ChangeMenu(MenuState.CustomerRateRestaurantMenu);
                break;

            case LOG_OUT_INT:
                _welcomeCount = 0;
                SessionManager.Logout();
                UIFlowController.ChangeMenu(MenuState.MainMenu);
                break;
            
            default:  
                DisplayIO.InvalidChoice();
                break;
        }
    }
}
    
