using System;
using UIComponents;
using UINavigation;

namespace UI;

// TODO xml
public class CustomerPlaceOrderMenu : IMenu
{
    /// <summary>
    /// Order number assigned to each order in the system. Each number shall be unique, and never reused.
    /// </summary>
    private static int _orderNumber = 1;
    
    /// <summary>
    /// Get and set <see cref="_orderNumber"/>. A new number must be returned from 
    /// <see cref="CustomerIO.GetOrderFromCustomer"/> and assigned to keep every order no. unique.
    /// </summary>
    public int OrderNumber
    {
        get { return _orderNumber; }
        private set { _orderNumber = value; }
    }

    // TODO xml
    public void DisplayMenu()
    {
        int newOrderNumber = CustomerIO.GetOrderFromCustomer(OrderNumber);
        if (newOrderNumber != OrderNumber)
        {
            OrderNumber = newOrderNumber;   // Update order number for future orders
            UIFlowController.ChangeMenu(MenuState.CustomerMainMenu);
        }
        else
        {
            UIFlowController.ChangeMenu(MenuState.CustomerMainMenu);
        }
    }
}