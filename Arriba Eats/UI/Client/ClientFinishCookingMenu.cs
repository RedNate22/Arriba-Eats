using System;
using UIComponents;
using UINavigation;

namespace UI;

/// <summary>
/// Represents the menu where a <see cref="Entities.Client"/> can view all the currently
/// cooking orders, and choose to mark them as cooked.
/// </summary>
public class ClientFinishCookingMenu : IMenu
{
    /// <summary>
    /// Displays the <see cref="ClientFinishCookingMenu"/>.
    /// Any orders currently being cooked (marked as 'cooking') are displayed.
    /// The <see cref="Entities.Client"/> can then choose an order, which marks it as 'cooked', and is then
    /// returned to <see cref="ClientMainMenu"/>.
    /// </summary>
    public void DisplayMenu()
    {
        IODisplay.DisplayMessage(ClientConstants.SELECT_ORDER_TO_FINISH_STR);

        var customerOrders = IODisplay.GetCustomerOrders();

        // * Check if any orders are cooking and display them - updating the index
        int choiceIndex = ClientIO.DisplayOrdersReadyToFinishCooking(customerOrders, out List<dynamic> ordersToFinish);

        IODisplay.DisplayMessage(IOUtilities.ReturnToPreviousMenuStr(choiceIndex));
        IODisplay.DisplayMessage(IOUtilities.EnterChoiceStr(choiceIndex));

        int choice = IODisplay.GetChoice();

        if (choice == choiceIndex) UIFlowController.ChangeMenu(MenuState.ClientMainMenu);

        else if (IOUtilities.IsValueInIndexRange(ordersToFinish, choice - 1))  // Valid input
        {
            var selectedOrder = ordersToFinish[choice - 1];

            selectedOrder.UpdateOrderStatus();  // Updates to 'Cooked'
            IODisplay.DisplayMessage(String.Format(ClientConstants.ORDER_READY_FOR_COLLECTION_STR, selectedOrder.OrderNumber));

            if (selectedOrder.Deliverer == null)
            {
                IODisplay.DisplayMessage(ClientConstants.NO_DELIVERER_ASSIGNED_STR);
            }

            else if (selectedOrder.DelivererArrivedAtRestaurant == true)
            {
                IODisplay.DisplayMessage(String.Format(ClientConstants.TAKE_TO_DELIVERER_STR, selectedOrder.Deliverer.LicencePlate));
            }

            else IODisplay.DisplayMessage(String.Format(ClientConstants.DELIVERER_ARRIVING_SOON_STR, selectedOrder.Deliverer.LicencePlate));

            UIFlowController.ChangeMenu(MenuState.ClientMainMenu);
        }
        else IODisplay.InvalidChoice();
    }
}
