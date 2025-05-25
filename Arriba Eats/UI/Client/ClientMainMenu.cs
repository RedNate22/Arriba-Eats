using System;
using UIComponents;
using UINavigation;

namespace UI;

/// <summary>
/// Represents the <see cref="Entities.Client"/> main menu.
/// <para>
/// From here, the <see cref="Entities.Client"/> can view their user information,
/// add items to their <see cref="Entities.Restaurant"/> menu, see current orders,
/// start cooking an order, finish cooking an order, and handle <see cref="Entities.Deliverer"/>s
/// who have arrived.
/// </para>
/// </summary>
public class ClientMainMenu : IMenu
{  
    private string _logOut = IOUtilities.LogOutStr(7);
    private string _enterChoice = IOUtilities.EnterChoiceStr(7);

    /// <summary>
    /// Defines the <see cref="int"/> constants representing menu options for use in a
    /// <see cref="switch"/> statement.
    /// </summary>
    private const int DISPLAY_USER_INFO_INT = 1, ADD_ITEM_RESTAURANT_INT = 2, SEE_CURRENT_ORDERS_INT = 3,
        START_COOKING_ORDER_INT = 4, FINISH_COOKING_ORDER_INT = 5, HANDLE_DELIVERERS_INT = 6, LOG_OUT_INT = 7;

    /// <summary>
    /// A count to track if the <see cref="IODisplay.WelcomeUser()"/>
    /// method has been run. This is to prevent the message from being displayed
    /// more than once.
    /// </summary>
    private int _welcomeCount = 0;

    /// <summary>
    /// Displays the <see cref="ClientMainMenu"/> options and prompts the <see cref="Entities.Client"/>
    /// to choose an option.
    /// </summary>
    public void DisplayMenu()
    {
        if (_welcomeCount == 0)
        {
            IODisplay.WelcomeUser();
            _welcomeCount++;
        }

        IODisplay.DisplayMessage(MenuConstants.MAKE_CHOICE_STR);
        IODisplay.DisplayMessage(MenuConstants.DISPLAY_USER_INFO_STR);
        IODisplay.DisplayMessage(ClientConstants.CLIENT_MAIN_MENU_CHOICES_STR);
        IODisplay.DisplayMessage(_logOut);
        IODisplay.DisplayMessage(_enterChoice);

        int choice = IODisplay.GetChoice();

        switch (choice)
        {
            case DISPLAY_USER_INFO_INT:
                IODisplay.DisplayUserInfo(SessionManager.ReturnCurrentUser());
                break;

            case ADD_ITEM_RESTAURANT_INT:
                RestaurantIO.AddItemsToRestaurant();
                break;

            case SEE_CURRENT_ORDERS_INT:
                UIFlowController.ChangeMenu(MenuState.ClientSeeCurrentOrdersMenu);
                break;

            case START_COOKING_ORDER_INT:
                UIFlowController.ChangeMenu(MenuState.ClientStartCookingMenu);
                break;

            case FINISH_COOKING_ORDER_INT:
                UIFlowController.ChangeMenu(MenuState.ClientFinishCookingMenu);
                break;

            case HANDLE_DELIVERERS_INT:
                UIFlowController.ChangeMenu(MenuState.ClientHandleDeliverersMenu);
                break;

            case LOG_OUT_INT:
                _welcomeCount = 0;
                SessionManager.Logout();
                UIFlowController.ChangeMenu(MenuState.MainMenu);
                break;
            
            default:  
                IODisplay.InvalidChoice();
                break;
        }
    }
}