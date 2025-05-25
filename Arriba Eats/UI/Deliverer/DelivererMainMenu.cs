using System;
using UIComponents;
using UINavigation;

namespace UI;

/// <summary>
/// Represents the <see cref="Entities.Deliverer"/> main menu.
/// <para>
/// From here, the <see cref="Entities.Deliverer"/> can view their <see cref="Entities.User"/> information,
/// view a list of orders available to deliver, mark their arrival at a <see cref="Entities.Restaurant"/> to pick up an order,
/// and mark their delivery as complete. 
/// </para>
/// </summary>
public class DelivererMainMenu : IMenu
{
    private string _logOut = IOUtilities.LogOutStr(5);
    private string _enterChoice = IOUtilities.EnterChoiceStr(5);

    /// <summary>
    /// Defines the <see cref="int"/> constants representing menu options for use in a
    /// <see cref="switch"/> statement.
    /// </summary>
    private const int DISPLAY_USER_INFO_INT = 1, LIST_ORDERS_AVAILABLE_INT = 2, ARRIVED_RESTAURANT_INT = 3,
        MARK_DELIVERY_COMPLETE_INT = 4, LOG_OUT_INT = 5;
    
    /// <summary>
    /// A count to track if the <see cref="DisplayIO.WelcomeUser()"/>
    /// method has been run. This is to prevent the message from being displayed
    /// more than once.
    /// </summary>
    private int _welcomeCount = 0;
    
    /// <summary>
    /// Displays the <see cref="DelivererMainMenu"/> options and prompts the <see cref="Entities.Deliverer"/>
    /// to choose an option.
    /// </summary>
    public void DisplayMenu()
    {
        if (_welcomeCount == 0)
        {
            DisplayIO.WelcomeUser();
            _welcomeCount++;
        }

        DisplayIO.DisplayMessage(MenuConstants.MAKE_CHOICE_STR);
        DisplayIO.DisplayMessage(MenuConstants.DISPLAY_USER_INFO_STR);
        DisplayIO.DisplayMessage(DelivererConstants.DELIVERER_MAIN_MENU_CHOICES_STR);
        DisplayIO.DisplayMessage(_logOut);
        DisplayIO.DisplayMessage(_enterChoice);

        int choice = DisplayIO.GetChoice();

        switch (choice)
        {
            case DISPLAY_USER_INFO_INT:
                DisplayIO.DisplayUserInfo(SessionManager.ReturnCurrentUser());
                break;

            case LIST_ORDERS_AVAILABLE_INT:
                UIFlowController.ChangeMenu(MenuState.DelivererListOrdersAvailableMenu);
                break;

            case ARRIVED_RESTAURANT_INT:
                UIFlowController.ChangeMenu(MenuState.DelivererArrivedAtRestaurantMenu);
                break;

            case MARK_DELIVERY_COMPLETE_INT:
                UIFlowController.ChangeMenu(MenuState.DelivererMarkDeliveryCompleteMenu);
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
