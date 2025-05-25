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
        if (OrderIO.FindCurrentOrder(out var orderNotFound) == false)
        {
            DisplayIO.DisplayMessage(DelivererConstants.NOT_YET_ACCEPTED_ORDER_STR);
            UIFlowController.ChangeMenu(MenuState.DelivererMainMenu);
        }

        else if (OrderIO.FindCurrentOrder(out var orderFound) == true)
        {
            if (OrderIO.IsBeingDelivered(orderFound.OrderStatus) && OrderIO.UpdateOrder(orderFound))  // * Updates to 'Delivered'
            {
                DisplayIO.DisplayMessage(DelivererConstants.THANK_YOU_FOR_DELIVERING_STR);
                UIFlowController.ChangeMenu(MenuState.DelivererMainMenu);
            }

            else
            {
                DisplayIO.DisplayMessage(DelivererConstants.NOT_PICKED_UP_ORDER_STR);
                UIFlowController.ChangeMenu(MenuState.DelivererMainMenu);
            }
        }
    }
}
