using System;
using UIComponents;
using UINavigation;

namespace UI;

/// <summary>
/// Represents the menu where a <see cref="Entities.Client"/> can view all the currently
/// ordered, cooking, and cooked orders where the <see cref="Entities.Deliverer"/> has been assigned
/// and has arrived. They can then mark them as being delivered.
/// </summary>
public class ClientHandleDeliverersMenu : IMenu
{
    /// <summary>
    /// Displays the <see cref="ClientHandleDeliverersMenu"/>.
    /// <para> Any orders currently ordered, being cooked, or are cooked, and the deliverer has
    /// been assigned and has arrived, will be displayed.
    /// The <see cref="Entities.Client"/> can then choose an order, which marks it as 'being delivered',
    /// so long as the order is first marked as 'cooked', otherwise a prompt is displayed informing them
    /// of so.
    /// After marking an order as being delivered, they are returned to the <see cref="ClientMainMenu"/>. </para>
    /// </summary>
    public void DisplayMenu()
    {
        DisplayIO.DisplayMessage(ClientConstants.THESE_DELIVERERS_ARRIVED_STR);
        DisplayIO.DisplayMessage(ClientConstants.SELECT_COLLECTED_ORDERS_STR);

        var customerOrders = OrderIO.GetCustomerOrders();

        // * Check if any orders are ordered, cooking, or cooked, and the deliverer has arrived,
        // * and display them - updating the index
        int choiceIndex = OrderIO.DisplayOrdersReadyForCollection(customerOrders, out List<dynamic> ordersForCollection);

        DisplayIO.DisplayMessage(DisplayIO.ReturnToPreviousMenuStr(choiceIndex));
        DisplayIO.DisplayMessage(DisplayIO.EnterChoiceStr(choiceIndex));

        int choice = DisplayIO.GetChoice();

        if (choice == choiceIndex) UIFlowController.ChangeMenu(MenuState.ClientMainMenu);

        else if (IOUtilities.IsValueInIndexRange(ordersForCollection, choice - 1))  // Valid input
        {
            var selectedOrder = ordersForCollection[choice - 1];

            if (!OrderIO.IsCooked(selectedOrder.OrderStatus))
            {
                DisplayIO.DisplayMessage(ClientConstants.ORDER_NOT_COOKED_STR);
                UIFlowController.ChangeMenu(MenuState.ClientMainMenu);
            }

            else if (selectedOrder.DelivererArrivedAtRestaurant == true && OrderIO.UpdateOrder(selectedOrder))  // * Updates to 'Being Delivered'
            {
                DisplayIO.DisplayMessage(String.Format(ClientConstants.ORDER_NOW_BEING_DELIVERED, selectedOrder.OrderNumber));
                UIFlowController.ChangeMenu(MenuState.ClientMainMenu);
            }
        }
        else DisplayIO.InvalidChoice();
        
    }
}