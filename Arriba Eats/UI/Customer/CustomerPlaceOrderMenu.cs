using System;
using UIComponents;
using UINavigation;
using Entities;

namespace UI;

// TODO xml
public class CustomerPlaceOrderMenu : IMenu
{
    /// <summary>
    /// Order number assigned to each order in the system. Each number shall be unique, and never reused.
    /// </summary>
    private static int _orderNumber = 1;
    
    /// <summary>
    /// Get and set <see cref="_orderNumber"/>.
    /// </summary>
    public int OrderNumber
    {
        get { return _orderNumber; }
        private set { _orderNumber = value; }
    }

    // TODO xml
    public void DisplayMenu()
    {
        OrderNumber = CustomerIO.GetOrderFromCustomer(OrderNumber);
    }
}
