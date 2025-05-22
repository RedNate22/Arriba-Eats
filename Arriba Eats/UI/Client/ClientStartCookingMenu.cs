using System;
using UIComponents;
using UINavigation;

namespace UI;

/// <summary>
/// Represents the menu where a <see cref="Entities.Client"/> can view all the currently ordered orders (ready to cook),
/// and choose to start cooking them.
/// </summary>
public class ClientStartCookingMenu : IMenu
{
    private string _orderMarkedAsCookingStr
        = "Order #{0} is now marked as cooking. Please prepare the order, then mark it as finished cooking:";

    /// <summary>
    /// Displays the <see cref="ClientStartCookingMenu"/>.
    /// Any orders ready to cook (marked as 'ordered') are displayed.
    /// The <see cref="Entities.Client"/> can then choose an order, which marks it as 'cooking', and is then
    /// returned to <see cref="ClientMainMenu"/>.
    /// </summary>
    public void DisplayMenu()
    {
        IODisplay.DisplayMessage(ClientConstants.SELECT_ORDER_TO_COOK_STR);
        var customerOrders = IODisplay.GetCustomerOrders();
        bool containsOrdersReady = customerOrders.Count != 0 && ClientIO.ContainsOrdered(customerOrders);

        // * Check if any orders are ready to cook and display them - updating the index
        int choiceIndex = containsOrdersReady ? ClientIO.DisplayOrdersReadyToCook(customerOrders) : 1;
        int returnPreviousMenuInt = choiceIndex;

        IODisplay.DisplayMessage(IOUtilities.ReturnToPreviousMenuStr(choiceIndex));
        IODisplay.DisplayMessage(IOUtilities.EnterChoiceStr(choiceIndex));

        int choice = IODisplay.GetChoice();
        
        if (choice == returnPreviousMenuInt) UIFlowController.ChangeMenu(MenuState.ClientMainMenu);

        else if (IOUtilities.IsValueInIndexRange(customerOrders, choice - 1))  // Valid input
        {
            var selectedOrder = customerOrders[choice - 1];  // * Adjust for index-based referencing

            selectedOrder.UpdateOrderStatus();  // Updates to 'Cooking'
            IODisplay.DisplayMessage(String.Format(_orderMarkedAsCookingStr, selectedOrder.OrderNumber));
            selectedOrder.DisplayOrderedItems();

            UIFlowController.ChangeMenu(MenuState.ClientMainMenu);
        }
        else IODisplay.InvalidChoice();
    }
}
