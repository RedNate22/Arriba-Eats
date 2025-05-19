using System;
using Entities;
using UINavigation;
using UI;

namespace UIComponents;

/// <summary>
/// Contains various static methods for handling IO with <see cref="Customer"/> users. 
/// Uses <see cref="IOUtilities"/> for input and output formatting and validation. 
/// </summary>
public static class CustomerIO
{
    /// <summary>
    /// Extracts a list of the currently registered <see cref="Restaurant"/>s from
    /// <see cref="RestaurantRegistry"/> using <see cref="RestaurantRegistry.TryListRestaurants()"/>. 
    /// Based on the parsed <see cref="SortOption"/>, sorts the list appropriately.
    /// </summary>
    /// <param name="sortType"> The <see cref="SortOption"/> to be selected for ordering the list. </param>
    /// <returns> The sorted <see cref="Restaurant"/>s list, ordered by the given <see cref="SortOption"/>. </returns>
    public static List<Restaurant> GetRestaurantsList(SortOption sortType)
    {
        if (RestaurantRegistry.TryListRestaurants(out List<Restaurant> restaurantsList))
        {
            if (sortType == SortOption.Alphabetically)
            {
                static int SortAlphabetically(Restaurant a, Restaurant b)
                {
                    return a.RestaurantName.CompareTo(b.RestaurantName);
                }
                restaurantsList.Sort(SortAlphabetically);
            }

            else if (sortType == SortOption.ByDistance)
            {
                static int SortByDistance(Restaurant a, Restaurant b)
                {
                    int distanceA = IODisplay.GetDistance(SessionManager.CurrentUser!, a);
                    int distanceB = IODisplay.GetDistance(SessionManager.CurrentUser!, b);
                    int distanceComparison = distanceA.CompareTo(distanceB);

                    // If distances are the same (returning 0), instead sort by name
                    return distanceComparison != 0 ? distanceComparison : a.RestaurantName.CompareTo(b.RestaurantName);
                }
                restaurantsList.Sort(SortByDistance);
            }

            else if (sortType == SortOption.ByStyle)
            {
                static int SortByStyle(Restaurant a, Restaurant b)
                {
                    int styleComparison = a.RestaurantStyle.CompareTo(b.RestaurantStyle);

                    // If styles are the same (returning 0), instead sort by name 
                    return styleComparison != 0 ? styleComparison : a.RestaurantName.CompareTo(b.RestaurantName);
                }
                restaurantsList.Sort(SortByStyle);
            }

            else if (sortType == SortOption.ByAverageRating)
            {
                // TODO
            }
            return restaurantsList;
        }
        return new List<Restaurant>();
    }

    /// <summary>
    /// Gets the list of registered <see cref="Restaurant"/>s via <see cref="GetRestaurantsList()"/>, 
    /// and then displays the restaurants and their details under the respective headings. 
    /// The <see cref="Restaurant"/>s are sorted by the <see cref="Customer"/>'s selected <see cref="SortOption"/>
    /// previously set in <see cref="CustomerSortRestaurantsMenu"/> and the list is then returned.
    /// <para> As the restaurant list is suspectible to dynamically changing whenever a new <see cref="Restaurant"/>
    /// is registered, the number for selecting the option to return to the previous menu is therefore dynamic, 
    /// and must be assigned in the <c>out</c> parameter. </para>
    /// </summary>
    /// <param name="returnPreviousMenuChoice"> The index number for the option to return to the previous menu. </param>
    /// <returns> The list of currently registered <see cref="Restaurant"/>'s. </returns>
    public static List<Restaurant> DisplayRestaurantsList(out int returnPreviousMenuChoice)
    {
        List<Restaurant> restaurantsList = GetRestaurantsList(CustomerSortRestaurantsMenu.SortOption);

        int restaurantColumnWidth = 7;
        int locationColumnWidth = CustomerConstants.LOCATION_HEADING_STR.Length + 4;
        int distanceColumnWidth = CustomerConstants.DISTANCE_HEADING_STR.Length + 2;
        int styleColumnWidth = CustomerConstants.STYLE_HEADING_STR.Length + 7;

        // Dynamically increase width of restaurant name column
        int maxRestaurantNameWidth = CustomerConstants.RESTAURANT_NAME_HEADING_STR.Length + restaurantColumnWidth;
        foreach (Restaurant restaurant in restaurantsList)
        {
            if (restaurant.RestaurantName.Length > maxRestaurantNameWidth)
            {
                maxRestaurantNameWidth = restaurant.RestaurantName.Length + 1;
            }
        }

        IODisplay.DisplayMessage("   " +
            CustomerConstants.RESTAURANT_NAME_HEADING_STR.PadRight(maxRestaurantNameWidth)
            + CustomerConstants.LOCATION_HEADING_STR.PadRight(locationColumnWidth)
            + CustomerConstants.DISTANCE_HEADING_STR.PadRight(distanceColumnWidth)
            + CustomerConstants.STYLE_HEADING_STR.PadRight(styleColumnWidth)
            + CustomerConstants.RATING_HEADING_STR);

        int restaurantChoiceIndex = 1;
        for (int i = 0; i < restaurantsList.Count(); i++)
        {
            IODisplay.DisplayMessage($"{restaurantChoiceIndex}: "
                + $"{restaurantsList[i].RestaurantName}".PadRight(maxRestaurantNameWidth)
                + $"{restaurantsList[i].Location}".PadRight(locationColumnWidth)
                + $"{IODisplay.GetDistance(SessionManager.CurrentUser!, restaurantsList[i])}".PadRight(distanceColumnWidth)
                + $"{restaurantsList[i].RestaurantStyle}".PadRight(styleColumnWidth)
                + "-");

            restaurantChoiceIndex++;
        }

        returnPreviousMenuChoice = restaurantChoiceIndex;
        int enterChoiceInt = restaurantChoiceIndex;
        IODisplay.DisplayMessage(IOUtilities.ReturnToPreviousMenuStr(returnPreviousMenuChoice));
        IODisplay.DisplayMessage(IOUtilities.EnterChoiceStr(enterChoiceInt));

        return restaurantsList;
    }

    /// <summary>
    /// Reads a string input from the user via the console.
    /// <para> Attempts to convert the input into an integer. </para>
    /// </summary>
    /// <returns> The valid integer quantity, otherwise <c>-1</c>. </returns>
    public static int GetItemQuantity()
    {
        string? quantity = IODisplay.ReadInput();

        if (int.TryParse(quantity, out int result)) return result;
        else
        {
            return -1;
        }
    }

    // TODO xml
    public static int GetOrderFromCustomer(int orderNumber)
    {
        if (CustomerBrowseRestaurantsMenu.SelectedRestaurant == null) return orderNumber;
        Restaurant selectedRestaurant = CustomerBrowseRestaurantsMenu.SelectedRestaurant;

        string currentOrderTotalStr = "Current order total: ${0:F2}";
        decimal currentOrderTotalDec = 0.00M;

        if (selectedRestaurant.TryGetMenu(out List<decimal> restaurantMenuPrices, out List<string> restaurantMenuItems))
        {
            CustomerOrder customerOrder = new CustomerOrder(orderNumber);  // * Begin order

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

                int choice = IODisplay.GetChoice();
                if (choice == completeOrder)
                {
                    if (customerOrder.IsOrderEmpty()) IODisplay.DisplayMessage("You have not added any items.");
                    else
                    {
                        // ! Doesn't complete order properly
                        // TODO
                        //((Customer)SessionManager.CurrentUser!).TryAddCurrentOrder(customerOrder);  // !
                        orderNumber = orderNumber + 1;
                        return orderNumber;  // * Update for future (next) orders    
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