using System;
using UIComponents;
using UINavigation;

namespace UI;

/// <summary>
/// Represents the menu where a customer can view the status of all their orders,
/// both active and past.
/// </summary>
public class CustomerOrderStatusMenu : IMenu
{
    /// <summary>
    /// Displays the order number, restaurant name, and status of the order.
    /// </summary>
    private string _orderStatusStr = "Order #{0} from {1}: {2}";

    /// <summary>
    /// If the order is marked as delivered, displays the deliverer's name and 
    /// <see cref="Deliverer.LicencePlate"/>
    /// of the <see cref="Deliverer"/> who delivered the order.
    /// </summary>
    private string _orderDeliveredByStr = "This order was delivered by {0} (licence plate: {1})";

    /// <summary>
    /// Displays the <see cref="CustomerOrderStatusMenu"/>, then returns back to
    /// <see cref="CustomerMainMenu"/>.
    /// </summary>
    public void DisplayMenu()
    {
        var customerOrders = IODisplay.GetCustomerOrders();
        if (customerOrders.Count != 0)
        {
            foreach (var order in customerOrders)
            {
                IODisplay.DisplayMessage(String.Format(_orderStatusStr, order.OrderNumber,
                    order.Restaurant.RestaurantName, order.OrderStatus));

                if (IODisplay.IsOrderDelivered(order.OrderStatus))
                {
                    IODisplay.DisplayMessage(String.Format(_orderDeliveredByStr, order.Deliverer?.Name,
                        order.Deliverer?.LicencePlate));
                }
                order.DisplayOrderedItems();
                IODisplay.DisplayEmptyLine();
            }
            UIFlowController.ChangeMenu(MenuState.CustomerMainMenu);
        }

        else
        {
            IODisplay.DisplayMessage(CustomerConstants.NOT_PLACED_ORDERS_STR);
            UIFlowController.ChangeMenu(MenuState.CustomerMainMenu);
        }
    }
}