using System;
using UIComponents;
using UINavigation;

namespace UI;

// TODO xml
public class CustomerOrderStatusMenu : IMenu
{
    //private int _numOfOrders = GetNumOfOrders();
    private int _numOfOrders = 0;
    
    // TODO xml
    public void DisplayMenu()
    {
        if (_numOfOrders < 1)
        {
            IODisplay.DisplayMessage(CustomerConstants.NOT_PLACED_ORDERS_STR);
            UIFlowController.ChangeMenu(MenuState.CustomerMainMenu);
        }
        
        else
        {
            // TODO
        }
    }
}