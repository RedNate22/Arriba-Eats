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

    // TODO xml
    public static int GetOrderFromCustomer(int orderNumber)
    {
        string currentOrderTotalStr = "Current order total: ${0:F2}";
        decimal currentOrderTotalDec = 0.00M;
        
        Restaurant selectedRestaurant = CustomerBrowseRestaurantsMenu.SelectedRestaurant;
        if (selectedRestaurant!.TryGetMenu(out List<decimal> foundRestaurantMenuPrices, out List<string> foundRestaurantMenuItems))
        {
            CustomerOrder customerOrder = new CustomerOrder(orderNumber);

            while (true)
            {
                int choiceIndex = 1;
                int menuIndex = 0;

                foreach (string item in restaurantMenuItems)
                {
                    IODisplay.DisplayMessage($"{choiceIndex}:   ${foundRestaurantMenuPrices[menuIndex]}  {foundRestaurantMenuItems[menuIndex]}");
                    choiceIndex++;
                    menuIndex++;
                }

                // choiceIndex will always be the last menu item's index + 1
                // e.g. if "3: Item" is the last item on the menu, then "Complete Order" will be 4, "4: Complete Order"
                int completeOrder = choiceIndex;
                int cancelOrder = choiceIndex + 1;  // Comes after "Complete Order"

                IODisplay.DisplayMessage(string.Format(currentOrderTotalStr, currentOrderTotalDec));
                
                IODisplay.DisplayMessage($"{completeOrder}: Complete order");
                IODisplay.DisplayMessage($"{cancelOrder}: Cancel order");

                int choice = IODisplay.GetChoice();

                if (choice == completeOrder)
                {
                    if (customerOrder.IsOrderEmpty())
                    {
                        IODisplay.DisplayMessage("You have not added any items.");
                    }

                    else
                    {
                        ((Customer)SessionManager.CurrentUser!).TryAddCurrentOrder(customerOrder);
                        return orderNumber++;  // Advance to next (future) order number    
                    }
                }

                else if (choice == cancelOrder)
                {
                    customerOrder = null!;  // Empties the cart
                    UIFlowController.ChangeMenu(MenuState.CustomerBrowseRestaurantsMenu);
                    return orderNumber;
                }

                else
                {
                    decimal selectedMenuItemPrice = restaurantMenuPrices[choice - 1];
                    string selectedMenuItemName = restaurantMenuItems[choice - 1];

                    IODisplay.DisplayMessage("Please enter quantity (0 to cancel):");
                    choice = IODisplay.GetChoice();

                    bool isInvalidChoice = choice > restaurantMenuItems.Count
                        || choice < restaurantMenuItems.Count;

                    if (choice == 0)
                    {
                        // Go back to order menu
                    }

                    else if (isInvalidChoice)
                    {
                        IODisplay.DisplayMessage("Invalid quantity.");
                    }


                    else
                    {
                        currentOrderTotalDec += choice * selectedMenuItemPrice;
                        IODisplay.DisplayMessage($"Added {choice} x {selectedMenuItemName} to order.");
                    }
                }
            }
        }
        else return orderNumber;
    }
}
