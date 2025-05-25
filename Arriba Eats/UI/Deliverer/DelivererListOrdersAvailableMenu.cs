using System;
using UIComponents;
using UINavigation;

namespace UI;

/// <summary>
/// Represents the menu where a <see cref="Entities.Deliverer"/> can view all the currently
/// active <see cref="Entities.CustomerOrder"/>s and accept them.
/// </summary>
public class DelivererListOrdersAvailableMenu : IMenu
{
    /// <summary>
    /// Displays the <see cref="DelivererListOrdersAvailableMenu"/>, which displays all the currently
    /// active <see cref="Entities.CustomerOrder"/>s, along with the relevant information a <see cref="Entities.Deliverer"/>
    /// may need. 
    /// <para> The deliverer can then accept one of the orders, and is then returned to the
    /// <see cref="DelivererMainMenu"/>. If the deliverer has already been assigned to an active order,
    /// they are returned back to the <see cref="DelivererMainMenu"/> automatically. </para>
    /// </summary>
    public void DisplayMenu()
    {
        if (OrderIO.FindCurrentOrder(out var currentOrder))
        {
            DisplayIO.DisplayMessage(DelivererConstants.ALREADY_SELECTED_ORDER_STR);
            UIFlowController.ChangeMenu(MenuState.DelivererMainMenu);
        }

        else
        {
            string delivererLocation = RegistrationIO.GetLocation();
            UserIO.DelivererChangeLocation(delivererLocation);

            // Display active orders
            DisplayIO.DisplayMessage(DelivererConstants.ORDERS_AVAILABLE_TO_DELIVER_STR);
            var customerOrdersList = OrderIO.DisplayOrdersList(out int choiceIndex);
            DisplayIO.DisplayMessage(IOUtilities.ReturnToPreviousMenuStr(choiceIndex));
            DisplayIO.DisplayMessage(IOUtilities.EnterChoiceStr(choiceIndex));

            int choice = DisplayIO.GetChoice();

            if (choice == choiceIndex) UIFlowController.ChangeMenu(MenuState.DelivererMainMenu);

            else if (IOUtilities.IsValueInIndexRange(customerOrdersList, choice - 1))
            {
                var selectedOrder = customerOrdersList[choice - 1];
                selectedOrder.AssignDeliverer(SessionManager.ReturnCurrentDeliverer());

                DisplayIO.DisplayMessage(String.Format(DelivererConstants.THANKS_FOR_ACCEPTING_ORDER_STR,
                    selectedOrder.Restaurant.RestaurantName, selectedOrder.Restaurant.Location));

                UIFlowController.ChangeMenu(MenuState.DelivererMainMenu);
            }

            else DisplayIO.InvalidChoice();
        }
    }
}