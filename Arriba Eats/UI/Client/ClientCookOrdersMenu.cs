using System;
using UIComponents;
using UINavigation;

namespace UI;

// TODO xml
public class ClientCookOrdersMenu : IMenu
{
    /// <summary>
    /// Dynamically displays the orders marked as Ordered. The index number, order number, and
    /// customer name are included.
    /// </summary>
    private string _displayOrdersStr = "{0}: Order #{1} for {2}";

    /// <summary>
    /// Displays a prompt to the <see cref="Entities.Client"/>, informing them that the
    /// <see cref="Entities.CustomerOrder"/> has been marked as cooking.
    /// </summary>
    private string _orderMarkedAsCookingStr
        = "Order #{0} is now marked as cooking. Please prepare the order, then mark it as finished cooking:";

    // TODO xml
    public void DisplayMenu()
    {
        IODisplay.DisplayMessage(ClientConstants.SELECT_ORDER_TO_COOK_STR);
        int choiceIndex = 1;
        int choice;

        var customerOrders = IODisplay.GetCustomerOrders();
        if (customerOrders.Count != 0 && ClientIO.ContainsOrdered(customerOrders))
        {
            foreach (var order in customerOrders)
            {
                // ? split into method
                if (IODisplay.IsOrdered(order.OrderStatus))
                {
                    IODisplay.DisplayMessage(String.Format(_displayOrdersStr, choiceIndex,
                        order.OrderNumber, order.Customer.Name));
                    choiceIndex++;
                }
            }

            int returnPreviousMenuChoiceInt = choiceIndex;
            string returnPreviousMenuChoiceStr = IOUtilities.ReturnToPreviousMenuStr(returnPreviousMenuChoiceInt);
            string enterChoiceStr = IOUtilities.EnterChoiceStr(choiceIndex);

            IODisplay.DisplayMessage(returnPreviousMenuChoiceStr);
            IODisplay.DisplayMessage(enterChoiceStr);

            choice = IODisplay.GetChoice();

            if (choice == returnPreviousMenuChoiceInt) UIFlowController.ChangeMenu(MenuState.ClientMainMenu);

            else if (IOUtilities.IsValueInIndexRange(customerOrders, choice - 1))
            {
                var selectedOrder = customerOrders[choice - 1];  // Adjust for index-based referencing
                selectedOrder.UpdateOrderStatus();
                IODisplay.DisplayMessage(String.Format(_orderMarkedAsCookingStr, selectedOrder.OrderNumber));
            }

            else IODisplay.InvalidChoice();
        }

        else  // No orders
        {
            string returnPreviousMenuStr = IOUtilities.ReturnToPreviousMenuStr(choiceIndex);
            string enterChoiceNoOrdersStr = IOUtilities.EnterChoiceStr(choiceIndex);

            IODisplay.DisplayMessage(returnPreviousMenuStr);
            IODisplay.DisplayMessage(enterChoiceNoOrdersStr);

            choice = IODisplay.GetChoice();
            if (choice == choiceIndex) UIFlowController.ChangeMenu(MenuState.ClientMainMenu);
            else IODisplay.InvalidChoice();
        }
    }
}
