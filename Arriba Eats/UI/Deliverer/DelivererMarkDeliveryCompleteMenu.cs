using System;
using UIComponents;
using UINavigation;

namespace UI;

/// <summary>
/// Represents the menu where a <see cref="Entities.Deliverer"/> can mark the delivery
/// as complete.
/// </summary>
public class DelivererMarkDeliveryCompleteMenu : IMenu
{
    /// <summary>
    /// Displays the <see cref="DelivererMarkDeliveryCompleteMenu"/>.
    /// <para> If the <see cref="Entities.Deliverer"/> has not accepted an order yet,
    /// or the order has not been picked up, a prompt is displayed and they are returned to the
    /// <see cref="DelivererMainMenu"/>. Otherwise, the <see cref="Entities.CustomerOrder"/>
    /// is marked as delivered. </para>
    /// </summary>
    public void DisplayMenu()
    {
        if (!DelivererIO.FindCurrentOrder(out var currentOrder1))
        {
            IODisplay.DisplayMessage(DelivererConstants.NOT_YET_ACCEPTED_ORDER_STR);
            UIFlowController.ChangeMenu(MenuState.DelivererMainMenu);
        }

        else if (DelivererIO.FindCurrentOrder(out var currentOrder2))
        {
            if (!IODisplay.IsOrderBeingDelivered(currentOrder2.OrderStatus))  // Not picked up
            {
                IODisplay.DisplayMessage(DelivererConstants.NOT_PICKED_UP_ORDER_STR);
                UIFlowController.ChangeMenu(MenuState.DelivererMainMenu);
            }

            else
            {
                currentOrder2.UpdateOrderStatus();
                IODisplay.DisplayMessage(DelivererConstants.THANK_YOU_FOR_DELIVERING_STR);
            }
        }
    }
}
