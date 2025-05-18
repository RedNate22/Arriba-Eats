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

    /// <summary>
    /// Order number assigned to each order in the system. Each number shall be unique, and never reused.
    /// </summary>
    private static int _orderNumber = 1;
    
    /// <summary>
    /// Get the next order number.
    /// </summary>
    public int OrderNumber
    {
        get { return _orderNumber; }
        private set { _orderNumber = value; }
    }

    // TODO xml
    public void DisplayMenu()
    {
        if (CustomerBrowseRestaurantsMenu.SelectedRestaurant != null)
        {
            Restaurant selectedRestaurant = CustomerBrowseRestaurantsMenu.SelectedRestaurant;

            selectedRestaurant.GetMenu(out List<decimal> restaurantMenuPrices, out List<string> restaurantMenuItemNames);

            IODisplay.DisplayMessage(string.Format(_currentOrderTotalStr, _currentOrderTotalDec));

            int choiceIndex = 1;
            int menuIndex = 0;
            foreach (string item in restaurantMenuItemNames)
            {
                IODisplay.DisplayMessage($"{choiceIndex}:   ${restaurantMenuPrices[menuIndex]}  {restaurantMenuItemNames[menuIndex]}");
                choiceIndex++;
                menuIndex++;
            }

            int completeOrder = choiceIndex;
            int cancelOrder = choiceIndex + 1;
            IODisplay.DisplayMessage($"{completeOrder}: Complete order");
            IODisplay.DisplayMessage($"{cancelOrder}: Cancel order");

            // ? tied directly to Customer?
            Dictionary<string, decimal> customerOrder = new Dictionary<string, decimal>();

            int choice = IODisplay.GetChoice();

            if (choice == completeOrder)
            {
                // ? if (customerOrder is empty?)
                // (customerOrder)
            }

            else if (choice == cancelOrder)
            {
                customerOrder.Clear();  // Empties the order
            }

            else
            {
                decimal selectedMenuItemPrice = restaurantMenuPrices[choice - 1];
                string selectedMenuItemName = restaurantMenuItemNames[choice - 1];

                IODisplay.DisplayMessage("Please enter quantity (0 to cancel):");
                choice = IODisplay.GetChoice();

                bool isInvalidChoice = choice > restaurantMenuItemNames.Count
                    || choice < restaurantMenuItemNames.Count;

                if (choice == 0)
                {
                    // Go back to order menu
                }

                if (isInvalidChoice)
                {
                    IODisplay.DisplayMessage("Invalid quantity.");
                }


                else
                {
                    //TODO SessionManager.CurrentUser.AddItemsToBasket(selectedMenuItemPrice, selectedMenuItemName);
                    _currentOrderTotalDec += choice * selectedMenuItemPrice;
                    IODisplay.DisplayMessage($"Added {choice} x {selectedMenuItemName} to order.");
                }
            }
        }

        else
        {
            IODisplay.DisplayMessage("No restaurant currently selected.");
            UIFlowController.ChangeMenu(MenuState.CustomerMainMenu);
        }
    }
}
