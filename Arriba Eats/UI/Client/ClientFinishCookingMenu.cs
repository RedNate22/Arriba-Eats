using System;
using UIComponents;
using UINavigation;

namespace UI;

// TODO xml
public class ClientFinishCookingMenu : IMenu
{
    /// <summary>
    /// Displays a prompt to the client, informing them that the
    /// customer order has been marked as ready for collection (cooked).
    /// </summary>
    private string _orderMarkedAsCookedStr = "Order #{0} is now ready for collection.";

    /// <summary>
    /// Displays a prompt to the client, informing them that the customer order has
    /// not been assigned a deliverer yet. 
    /// </summary>
    private string _noDelivererAssignedStr = "No deliverer has been assigned yet.";

    /// <summary>
    /// Displays a prompt to the client, informing them to take the order to the deliverer,
    /// and displays their licence plate.
    /// </summary>
    private string _takeItToDelivererStr
        = "Please take it to the deliverer with licence plate {0}, who is waiting to collect it.";

    /// <summary>
    /// Displays a prompt to the client, informing them that the deliverer is on their way,
    /// and displays their licence plate.
    /// </summary>
    private string _delivererArrivingSoonStr
        = "The deliverer with licence plate {0} will be arriving soon to collect it.";

    // TODO xml
    public void DisplayMenu()
    {
        IODisplay.DisplayMessage(ClientConstants.SELECT_ORDER_TO_FINISH_STR);
        var customerOrders = IODisplay.GetCustomerOrders();
        bool containsReadyOrders = customerOrders.Count != 0 && ClientIO.ContainsCooking(customerOrders);

        // * Check if any orders are cooking and display them - updating the index
        int choiceIndex = containsReadyOrders ? ClientIO.DisplayOrdersReadyToFinish(customerOrders) : 1;
        int returnPreviousMenuInt = choiceIndex;

        IODisplay.DisplayMessage(IOUtilities.ReturnToPreviousMenuStr(choiceIndex));
        IODisplay.DisplayMessage(IOUtilities.EnterChoiceStr(choiceIndex));

        int choice = IODisplay.GetChoice();

        if (choice == returnPreviousMenuInt) UIFlowController.ChangeMenu(MenuState.ClientMainMenu);

        else if (IOUtilities.IsValueInIndexRange(customerOrders, choice - 1))  // Valid input
        {
            var selectedOrder = customerOrders[choice - 1];  // * Adjust for index-based referencing

            selectedOrder.UpdateOrderStatus();  // Updates to 'Cooked'
            IODisplay.DisplayMessage(String.Format(_orderMarkedAsCookedStr, selectedOrder.OrderNumber));

            if (selectedOrder.Deliverer == null)
            {
                IODisplay.DisplayMessage(_noDelivererAssignedStr);
            }

            else if (selectedOrder.DelivererArrived == true)
            {
                IODisplay.DisplayMessage(String.Format(_takeItToDelivererStr, selectedOrder.Deliverer.LicencePlate));
            }

            else IODisplay.DisplayMessage(String.Format(_delivererArrivingSoonStr, selectedOrder.Deliverer.LicencePlate));

            UIFlowController.ChangeMenu(MenuState.ClientMainMenu);
        }

        else IODisplay.InvalidChoice();
    }
}
