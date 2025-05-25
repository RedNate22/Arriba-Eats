using System;
using UIComponents;
using UINavigation;

namespace UI;

/// <summary>
/// Represents the menu where a <see cref="Entities.Deliverer"/> can mark themselves
/// as having arrived at the <see cref="Entities.Restaurant"/> to pick up the order.
/// </summary>
public class DelivererArrivedAtRestaurantMenu : IMenu
{
    /// <summary>
    /// Displays the <see cref="DelivererArrivedAtRestaurantMenu"/>.
    /// <para> If the <see cref="Entities.Deliverer"/> has been assigned to a 
    /// <see cref="Entities.CustomerOrder"/> and has not yet arrived at the <see cref="Entities.Restaurant"/>,
    /// the restaurant is notified of their arrival. If they have not yet been assigned to an order,
    /// or they have already arrived, or they have already collected the order, the appropriate
    /// prompt will be displayed, and the <see cref="Entities.Deliverer"/> is returned to the 
    /// <see cref="DelivererMainMenu"/>. </para>
    /// </summary>
    public void DisplayMenu()
    {
        if (!OrderIO.FindCurrentOrder(out var currentOrder))
        {
            DisplayIO.DisplayMessage(MenuConstants.NOT_YET_ACCEPTED_ORDER_STR);
            UIFlowController.ChangeMenu(MenuState.DelivererMainMenu);
        }

        else if (OrderIO.IsBeingDelivered(currentOrder.OrderStatus))
        {
            DisplayIO.DisplayMessage(MenuConstants.ALREADY_PICKED_UP_ORDER_STR);
            UIFlowController.ChangeMenu(MenuState.DelivererMainMenu);
        }

        else if (currentOrder.DelivererArrivedAtRestaurant == true)
        {
            DisplayIO.DisplayMessage(MenuConstants.ALREADY_AT_RESTAURANT_STR);
            UIFlowController.ChangeMenu(MenuState.DelivererMainMenu);
        }

        else
        {
            currentOrder.DelivererAtRestaurant();
            DisplayIO.DisplayMessage(String.Format(MenuConstants.ARRIVED_AT_RESTAURANT_STR,
                currentOrder.Restaurant.RestaurantName, currentOrder.OrderNumber));

            if (OrderIO.IsBeingPrepared(currentOrder.OrderStatus))
            {
                DisplayIO.DisplayMessage(MenuConstants.ORDER_STILL_BEING_PREPARED_STR);
            }

            DisplayIO.DisplayMessage(String.Format(MenuConstants.PLEASE_DELIVER_STR,
                currentOrder.Customer.Name, currentOrder.Customer.Location));
            UIFlowController.ChangeMenu(MenuState.DelivererMainMenu);
        }
    }
}
