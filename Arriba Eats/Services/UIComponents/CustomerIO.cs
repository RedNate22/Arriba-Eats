using System;
using Entities;
using UINavigation;
using UI;

namespace UIComponents;

/// <summary>
/// Contains various static methods for handling I/O with <see cref="Customer"/> users. 
/// Uses <see cref="IOUtilities"/> for input and output formatting and validation. 
/// </summary>
public static class CustomerIO
{
    /// <summary>
    /// To be called by <see cref="DisplayRestaurantsList"/>.
    /// Extracts a list of the currently registered <see cref="Restaurant"/>s from
    /// <see cref="RestaurantRegistry"/> using <see cref="RestaurantRegistry.TryListRestaurants()"/>. 
    /// Based on the parsed <see cref="SortOption"/>, sorts the list appropriately.
    /// </summary>
    /// <param name="sortType"> The <see cref="SortOption"/> to be selected for ordering the list. </param>
    /// <returns> The sorted <see cref="Restaurant"/>s list, ordered by the given <see cref="SortOption"/>. </returns>
    private static List<Restaurant> SortRestaurants(SortOption sortType)
    {
        if (RestaurantRegistry.TryListRestaurants(out List<Restaurant> restaurantsList))
        {
            switch (sortType)
            {
                case (SortOption) 1:
                    static int SortAlphabetically(Restaurant a, Restaurant b)
                    {
                        return a.RestaurantName.CompareTo(b.RestaurantName);
                    }
                    restaurantsList.Sort(SortAlphabetically);
                    break;

                case (SortOption) 2:
                    static int SortByDistance(Restaurant a, Restaurant b)
                    {
                        int distanceA = IODisplay.GetDistance(SessionManager.ReturnCurrentUser(), a);
                        int distanceB = IODisplay.GetDistance(SessionManager.ReturnCurrentUser(), b);
                        int distanceComparison = distanceA.CompareTo(distanceB);

                        // * If distances are the same (returning 0), instead sort by name
                        return distanceComparison != 0 ? distanceComparison : a.RestaurantName.CompareTo(b.RestaurantName);
                    }
                    restaurantsList.Sort(SortByDistance);
                    break;

                case (SortOption) 3:
                    static int SortByStyle(Restaurant a, Restaurant b)
                    {
                        int styleComparison = a.RestaurantStyle.CompareTo(b.RestaurantStyle);

                        // * If styles are the same (returning 0), instead sort by name 
                        return styleComparison != 0 ? styleComparison : a.RestaurantName.CompareTo(b.RestaurantName);
                    }
                    restaurantsList.Sort(SortByStyle);
                    break;

                case (SortOption) 4:
                    // TODO average rating sort
                    break;
            }
            return restaurantsList;
        }
        return new List<Restaurant>();
    }

    /// <summary>
    /// Gets the list of registered <see cref="Restaurant"/>s after sorting them via <see cref="SortRestaurants"/>, 
    /// and then displays the restaurants and their details under their respective headings. 
    /// <para> As the restaurant list is suspectible to dynamically changing whenever a new <see cref="Restaurant"/>
    /// is registered, the index number for the listed options is therefore dynamic, 
    /// and is assigned in the <c>out</c> parameter to be referenced. </para>
    /// </summary>
    /// <param name="choiceIndex"> The index number for the listed options. </param>
    /// <returns> The list of currently registered <see cref="Restaurant"/>'s. </returns>
    public static List<Restaurant> DisplayRestaurantsList(out int choiceIndex)
    {
        List<Restaurant> restaurantsList = SortRestaurants(CustomerSortRestaurantsMenu.SortOption);

        int restaurantColumnWidth = CustomerConstants.RESTAURANT_NAME_HEADING_STR.Length + 7;
        int locationColumnWidth = CustomerConstants.LOCATION_HEADING_STR.Length + 4;
        int distanceColumnWidth = CustomerConstants.DISTANCE_HEADING_STR.Length + 2;
        int styleColumnWidth = CustomerConstants.STYLE_HEADING_STR.Length + 7;

        // Dynamically increase width of restaurant name column
        foreach (Restaurant restaurant in restaurantsList)
        {
            if (restaurant.RestaurantName.Length > restaurantColumnWidth)
            {
                restaurantColumnWidth = restaurant.RestaurantName.Length + 1;
            }
        }

        IODisplay.DisplayMessage("   "
            + CustomerConstants.RESTAURANT_NAME_HEADING_STR.PadRight(restaurantColumnWidth)
            + CustomerConstants.LOCATION_HEADING_STR.PadRight(locationColumnWidth)
            + CustomerConstants.DISTANCE_HEADING_STR.PadRight(distanceColumnWidth)
            + CustomerConstants.STYLE_HEADING_STR.PadRight(styleColumnWidth)
            + CustomerConstants.RATING_HEADING_STR);

        int restaurantChoiceIndex = 1;
        for (int i = 0; i < restaurantsList.Count(); i++)
        {
            IODisplay.DisplayMessage($"{restaurantChoiceIndex}: "
                + $"{restaurantsList[i].RestaurantName}".PadRight(restaurantColumnWidth)
                + $"{restaurantsList[i].Location}".PadRight(locationColumnWidth)
                + $"{IODisplay.GetDistance(SessionManager.ReturnCurrentUser(), restaurantsList[i])}".PadRight(distanceColumnWidth)
                + $"{restaurantsList[i].RestaurantStyle}".PadRight(styleColumnWidth)
                // TODO 
                + "-");
            restaurantChoiceIndex++;
        }
        choiceIndex = restaurantChoiceIndex;
        return restaurantsList;
    }

    /// <summary>
    /// To be called by <see cref="GetOrderFromCustomer"/>.
    /// Reads a string input from the user via the console.
    /// <para> Attempts to convert the input into an integer. </para>
    /// </summary>
    /// <returns> The valid integer quantity, otherwise <c>-1</c>. </returns>
    private static int GetItemQuantity()
    {
        string? quantity = IODisplay.ReadInput();

        if (int.TryParse(quantity, out int result)) return result;
        else
        {
            return -1;
        }
    }

    // TODO xml
    // ? split this up? or condense?
    public static int GetOrderFromCustomer(int orderNumber)
    {
        if (CustomerBrowseRestaurantsMenu.SelectedRestaurant == null) return orderNumber;
        Restaurant selectedRestaurant = CustomerBrowseRestaurantsMenu.SelectedRestaurant;

        string currentOrderTotalStr = "Current order total: ${0:F2}";
        decimal currentOrderTotalDec = 0.00M;

        if (selectedRestaurant.TryGetMenu(out List<decimal> restaurantMenuPrices, out List<string> restaurantMenuItems))
        {
            CustomerOrder customerOrder = new CustomerOrder
                ((Customer)SessionManager.ReturnCurrentUser(), orderNumber, selectedRestaurant);  // * Begin order

            string enterChoiceStr = IOUtilities.EnterChoiceStr(restaurantMenuItems.Count + 2);  // Adjust for confirm/cancel options

            while (true)
            {
                IODisplay.DisplayMessage(string.Format(currentOrderTotalStr, currentOrderTotalDec));

                int choiceIndex = 1;
                int menuIndex = 0;

                foreach (string item in restaurantMenuItems)
                {
                    IODisplay.DisplayMessage($"{choiceIndex}:   ${restaurantMenuPrices[menuIndex]}  {restaurantMenuItems[menuIndex]}");
                    choiceIndex++;
                    menuIndex++;
                }

                // * choiceIndex will always be the last menu item's index + 1
                // * e.g. if "3: Item" is the last item on the menu, then "Complete Order" will be 4, "4: Complete Order"
                int completeOrder = choiceIndex;
                int cancelOrder = choiceIndex + 1;

                IODisplay.DisplayMessage($"{completeOrder}: Complete order");
                IODisplay.DisplayMessage($"{cancelOrder}: Cancel order");
                IODisplay.DisplayMessage(enterChoiceStr);

                int choice = IODisplay.GetChoice();
                if (choice == completeOrder)
                {
                    if (customerOrder.IsOrderEmpty()) IODisplay.DisplayMessage("You have not added any items.");
                    else
                    {
                        if (OrderRegistry.TryAddOrder(customerOrder))
                        {
                            customerOrder.UpdateOrderStatus();
                            IODisplay.DisplayMessage($"Your order has been placed. Your order number is #{orderNumber}.");

                            orderNumber++;  // * Update order number for future (next) orders
                            return orderNumber;
                        }

                        else
                        {
                            IODisplay.DisplayMessage("Order could not be confirmed.");
                            return orderNumber;
                        }
                    }
                }

                else if (choice == cancelOrder)
                {
                    customerOrder = null!;  // Empties the cart
                    UIFlowController.ChangeMenu(MenuState.CustomerBrowseRestaurantsMenu);
                    return orderNumber;  // * Return original order number, to be reused until a confirmed order
                }

                else
                {
                    // * Adjust for index-based referencing
                    decimal selectedMenuItemPrice = restaurantMenuPrices[choice - 1];
                    string selectedMenuItemName = restaurantMenuItems[choice - 1];

                    while (choice != 0)
                    {
                        IODisplay.DisplayMessage($"Adding {selectedMenuItemName} to order.");
                        IODisplay.DisplayMessage("Please enter quantity (0 to cancel):");
                        choice = GetItemQuantity();

                        if (choice > 0)
                        {
                            customerOrder.AddItemToOrder(selectedMenuItemName, choice, selectedMenuItemPrice);
                            currentOrderTotalDec += choice * selectedMenuItemPrice;  // * Update cart total
                            IODisplay.DisplayMessage($"Added {choice} x {selectedMenuItemName} to order.");
                            break;
                        }

                        else if (choice != 0)
                        {
                            IODisplay.DisplayMessage(CustomerConstants.INVALID_QUANTITY_STR);
                        }
                    }
                }
            }
        }
        else
        {
            IODisplay.DisplayMessage($"{selectedRestaurant.RestaurantName} currently has no items on the menu.");
            return orderNumber;
        }
    }
}