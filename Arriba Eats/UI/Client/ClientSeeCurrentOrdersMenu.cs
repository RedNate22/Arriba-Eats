using System;
using UIComponents;
using UINavigation;

namespace UI;

/// <summary>
/// Represents the menu where a client can view all the currently active customer orders.
/// </summary>
public class ClientSeeCurrentOrdersMenu : IMenu
{
    /// <summary>
    /// Displays the order number, customer name, and status of the order.
    /// </summary>
    private string _orderStatusStr = "Order #{0} for {1}: {2}";

    /// <summary>
    /// Displays the <see cref="ClientSeeCurrentOrdersMenu"/>.
    /// Any orders made (and still yet to be cooked) are displayed.
    /// The client is then taken back to <see cref="ClientMainMenu"/> automatically.
    /// </summary>
    public void DisplayMenu()
    {
        var customerOrders = OrderIO.GetCustomerOrders();
        if (customerOrders.Count != 0)
        {
            foreach (var order in customerOrders)
            {
                if (!OrderIO.IsDelivered(order.OrderStatus))
                {
                    DisplayIO.DisplayMessage(String.Format(_orderStatusStr, order.OrderNumber,
                        order.Customer.Name, order.OrderStatus));

                    order.DisplayOrderedItems();

                    DisplayIO.DisplayEmptyLine();
                }
            }
            UIFlowController.ChangeMenu(MenuState.ClientMainMenu);
        }

        else
        {
            DisplayIO.DisplayMessage(MenuConstants.RESTAURANT_HAS_NO_ORDERS_STR);
            UIFlowController.ChangeMenu(MenuState.ClientMainMenu);
        }
    }
}