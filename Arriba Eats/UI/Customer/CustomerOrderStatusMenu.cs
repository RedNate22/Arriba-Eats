using System;
using UIComponents;
using UINavigation;
using Entities;

namespace UI;

// TODO xml
public class CustomerOrderStatusMenu : IMenu
{
    /// <summary>
    /// Displays the <see cref="CustomerOrder.OrderNumber"/>, <see cref="Restaurant"/> name, and 
    /// <see cref="OrderStatus"/> of the order.
    /// </summary>
    private string _orderStatusStr = "Order #{0} from {1}: {2}";

    /// <summary>
    /// If the order is marked as <see cref="OrderStatus.Delivered"/>, displays 
    /// the <see cref="Deliverer"/>'s name and <see cref="Deliverer.LicencePlate"/>
    /// of the <see cref="Deliverer"/> who delivered the order.
    /// </summary>
    private string _orderDeliveredByStr = "This order was delivered by {0} (licence plate: {1})";

    // TODO xml
    public void DisplayMenu()
    {
        if (OrderRegistry.TryGetOrders(out List<CustomerOrder> customerOrders, (Customer)SessionManager.CurrentUser!))
        {
            foreach (CustomerOrder order in customerOrders)
            {
                IODisplay.DisplayMessage(String.Format(_orderStatusStr, order.OrderNumber,
                    order.Restaurant.RestaurantName, order.OrderStatus));

                if (order.OrderStatus == OrderStatus.Delivered)
                {
                    IODisplay.DisplayMessage(String.Format(_orderDeliveredByStr, order.Deliverer?.Name,
                        order.Deliverer?.LicencePlate));
                }
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