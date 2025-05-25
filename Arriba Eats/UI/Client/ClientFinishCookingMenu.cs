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
        DisplayIO.DisplayMessage(ClientConstants.SELECT_ORDER_TO_FINISH_STR);

        var customerOrders = OrderIO.GetCustomerOrders();

        // * Check if any orders are cooking and display them - updating the index
        int choiceIndex = OrderIO.DisplayOrdersReadyToFinishCooking(customerOrders, out List<dynamic> ordersToFinish);

        DisplayIO.DisplayMessage(DisplayIO.ReturnToPreviousMenuStr(choiceIndex));
        DisplayIO.DisplayMessage(DisplayIO.EnterChoiceStr(choiceIndex));

        int choice = DisplayIO.GetChoice();

        if (choice == choiceIndex) UIFlowController.ChangeMenu(MenuState.ClientMainMenu);

        else if (IOUtilities.IsValueInIndexRange(ordersToFinish, choice - 1))  // Valid input
        {
            var selectedOrder = ordersToFinish[choice - 1];

            if (OrderIO.UpdateOrder(selectedOrder))  // * Updates to 'Cooked'
            {
                DisplayIO.DisplayMessage(String.Format(ClientConstants.ORDER_READY_FOR_COLLECTION_STR, selectedOrder.OrderNumber));

                if (selectedOrder.Deliverer == null)
                {
                    DisplayIO.DisplayMessage(ClientConstants.NO_DELIVERER_ASSIGNED_STR);
                }

                else if (selectedOrder.DelivererArrivedAtRestaurant == true)
                {
                    DisplayIO.DisplayMessage(String.Format(ClientConstants.TAKE_TO_DELIVERER_STR, selectedOrder.Deliverer.LicencePlate));
                }

                else DisplayIO.DisplayMessage(String.Format(ClientConstants.DELIVERER_ARRIVING_SOON_STR, selectedOrder.Deliverer.LicencePlate));

                UIFlowController.ChangeMenu(MenuState.ClientMainMenu);
            }
        }
        else DisplayIO.InvalidChoice();
    }
}
