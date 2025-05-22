using System;
using UIComponents;
using UINavigation;

namespace UI;

// TODO xml
public class DelivererListOrdersAvailableMenu : IMenu
{
    // TODO xml
    public void DisplayMenu()
    {
        if (DelivererIO.DelivererAlreadyAssignedToOrder())
        {
            IODisplay.DisplayMessage(DelivererConstants.ALREADY_SELECTED_ORDER_STR);
            UIFlowController.ChangeMenu(MenuState.DelivererMainMenu);
        }

        else
        {
            IODisplay.DisplayMessage(DelivererConstants.ORDERS_AVAILABLE_TO_DELIVER_STR);
            var customerOrdersList = DelivererIO.DisplayOrdersList(out int choiceIndex);
            IODisplay.DisplayMessage(IOUtilities.ReturnToPreviousMenuStr(choiceIndex));
            IODisplay.DisplayMessage(IOUtilities.EnterChoiceStr(choiceIndex));

            int choice = IODisplay.GetChoice();

            if (choice == choiceIndex) UIFlowController.ChangeMenu(MenuState.DelivererMainMenu);

            else if (IOUtilities.IsValueInIndexRange(customerOrdersList, choice - 1))
            {
                var selectedOrder = customerOrdersList[choice - 1];
                selectedOrder.AssignDeliverer(SessionManager.ReturnCurrentDeliverer());

                IODisplay.DisplayMessage(String.Format(DelivererConstants.THANKS_FOR_ACCEPTING_ORDER_STR,
                    selectedOrder.Restaurant.RestaurantName, selectedOrder.Restaurant.Location));

                UIFlowController.ChangeMenu(MenuState.DelivererMainMenu);
            }

            else IODisplay.InvalidChoice();
        }
    }
}