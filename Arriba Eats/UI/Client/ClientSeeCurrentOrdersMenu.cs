using System;
using UIComponents;
using UINavigation;

namespace UI;

// TODO xml
public class ClientSeeCurrentOrdersMenu : IMenu
{
    // TODO xml
    public void DisplayMenu()
    {
        var customerOrders = IODisplay.GetCustomerOrders();

        if (customerOrders.Count != 0)
        {
            foreach (var order in customerOrders) ;
        }

        else
        {
            IODisplay.DisplayMessage(ClientConstants.RESTAURANT_HAS_NO_ORDERS_STR);
            UIFlowController.ChangeMenu(MenuState.ClientMainMenu); 
        }
    }
}
