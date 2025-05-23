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
    /// Any orders currently ordered, being cooked, or are cooked, and the deliverer has
    /// been assigned and has arrived, will be displayed.
    /// The <see cref="Entities.Client"/> can then choose an order, which marks it as 'being delivered',
    /// so long as the order is first marked as 'cooked', otherwise a prompt is displayed informing them
    /// of so.
    /// After marking an order as being delivered, they are returned to the <see cref="ClientMainMenu"/>.
    /// </summary>
    public void DisplayMenu()
    {
        IODisplay.DisplayMessage(ClientConstants.THESE_DELIVERERS_ARRIVED_STR);
        IODisplay.DisplayMessage(ClientConstants.SELECT_COLLECTED_ORDERS_STR);

        var customerOrders = IODisplay.GetCustomerOrders();
        bool containsActiveOrders = customerOrders.Count != 0 && ClientIO.ContainsActiveOrders(customerOrders);
        
        // * Check if any orders are ordered, cooking, or cooked, and the deliverer has arrived,
        // * and display them - updating the index
        int choiceIndex = containsActiveOrders ? ClientIO.DisplayAllActiveOrders(customerOrders) : 1;
        int returnPreviousMenuInt = choiceIndex;

        IODisplay.DisplayMessage(IOUtilities.ReturnToPreviousMenuStr(choiceIndex));
        IODisplay.DisplayMessage(IOUtilities.EnterChoiceStr(choiceIndex));

        int choice = IODisplay.GetChoice();

        if (choice == returnPreviousMenuInt) UIFlowController.ChangeMenu(MenuState.ClientMainMenu);

        else if (IOUtilities.IsValueInIndexRange(customerOrders, choice - 1))  // Valid input
        {
            var selectedOrder = customerOrders[choice - 1];  // * Adjust for index-based referencing

            if (!ClientIO.IsCooked(selectedOrder.OrderStatus))
            {
                IODisplay.DisplayMessage(ClientConstants.ORDER_NOT_COOKED_STR);
            }

            else if (selectedOrder.DelivererArrivedAtRestaurant == true)
            {
                selectedOrder.UpdateOrderStatus();  // Updates to 'BeingDelivered'
                IODisplay.DisplayMessage(String.Format(ClientConstants.ORDER_NOW_BEING_DELIVERED, selectedOrder.OrderNumber));
                UIFlowController.ChangeMenu(MenuState.ClientMainMenu);
            }
        }
        else IODisplay.InvalidChoice();
        
    }
}
