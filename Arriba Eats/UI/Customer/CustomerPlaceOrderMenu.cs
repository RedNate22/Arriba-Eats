using System;
using UIComponents;
using UINavigation;

namespace UI;

/// <summary>
/// Represents the menu where a <see cref="Entities.Customer"/> can place orders from 
/// a <see cref="Entities.Restaurant"/>.
/// </summary>
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

    /// <summary>
    /// Displays the <see cref="Entities.Restaurant"/>'s menu, the current order total amount, and option to
    /// either complete or cancel the order.
    /// <para> This method calls the <see cref="CustomerIO.GetOrderFromCustomer"/> method to assign an order
    /// number to the new <see cref="Entities.CustomerOrder"/>. If the order is successful, a new order
    /// number is returned. This new number can be compared to determine the order was confirmed. 
    /// The <see cref="Entities.Customer"/> is then taken back to the <see cref="CustomerBrowseRestaurantsMenu"/>
    /// where they can place another order, read reviews for the <see cref="Entities.Restaurant"/> or return
    /// to previous menus. </para>
    /// </summary>
    public void DisplayMenu()
    {
        // * Returns a new order number if order is confirmed
        int newOrderNumber = CustomerIO.GetOrderFromCustomer(OrderNumber);
        
        CustomerBrowseRestaurantsMenu.ReturningFromMenu = true;
        if (newOrderNumber != OrderNumber)
        {
            OrderNumber = newOrderNumber;   // Update order number for future orders
            UIFlowController.ChangeMenu(MenuState.CustomerBrowseRestaurantsMenu);
        }
        else
        {
            UIFlowController.ChangeMenu(MenuState.CustomerBrowseRestaurantsMenu);
        }
    }
}