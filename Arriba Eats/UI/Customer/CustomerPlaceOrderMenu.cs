using System;
using UIComponents;
using UINavigation;
using Entities;

namespace UI;

// TODO xml
public class CustomerPlaceOrderMenu : IMenu
{
    /// <summary>
    /// Displays the current total of the <see cref="Customer"/>'s order.
    /// </summary>
    private string _currentOrderTotalStr = "Current order total: ${0:F2}";

    /// <summary>
    /// The current total price amount of all items added to the <see cref="Customer"/>'s cart.
    /// </summary>
    private decimal _currentOrderTotalDec = 0M;

    // TODO xml
    public void DisplayMenu()
    {
        if (CustomerBrowseRestaurantsMenu.SelectedRestaurant != null)
        {
            Restaurant selectedRestaurant = CustomerBrowseRestaurantsMenu.SelectedRestaurant;

            selectedRestaurant.GetMenu(out List<decimal> restaurantMenuPrices, out List<string> restaurantMenuItemNames);

            IODisplay.DisplayMessage(string.Format(_currentOrderTotalStr, _currentOrderTotalDec));

            int index = 1;  // ! need a separate index int for the list itself, otherwise out of range
            foreach (string item in restaurantMenuItemNames)
            {
                IODisplay.DisplayMessage($"{index}:   ${restaurantMenuPrices[index]}  {restaurantMenuItemNames[index]}");
                index++;
            }

            int completeOrder = index;
            int cancelOrder = index + 1;
            IODisplay.DisplayMessage($"{completeOrder}: Complete order");
            IODisplay.DisplayMessage($"{cancelOrder}: Cancel order");

            int choice = IODisplay.GetChoice();

            if (choice == completeOrder)
            {

            }

            else if (choice == cancelOrder)
            {

            }

            else
            {
                decimal selectedMenuItemPrice = restaurantMenuPrices[choice - 1];
                string selectedMenuItemName = restaurantMenuItemNames[choice - 1];
                IODisplay.DisplayMessage("Please enter quantity (0 to cancel):");
                choice = IODisplay.GetChoice();

                bool isInvalidChoice = choice > restaurantMenuItemNames.Count
                    || choice < restaurantMenuItemNames.Count;

                if (isInvalidChoice)
                {
                    IODisplay.DisplayMessage("Invalid quantity.");
                }

                else if (choice == 0)
                {
                    // Go back to order menu
                }

                else
                {
                    //TODO SessionManager.CurrentUser.AddItemsToBasket(selectedMenuItemPrice, selectedMenuItemName);
                    _currentOrderTotalDec += choice * selectedMenuItemPrice;
                    IODisplay.DisplayMessage($"Added {choice} x {selectedMenuItemName} to order.");
                }
            }
        }

        else UIFlowController.ChangeMenu(MenuState.CustomerMainMenu);
    }
}
