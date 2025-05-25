using System;
using Entities;
using UI;

namespace UIComponents;

/// <summary>
/// Contains various static methods for I/O associated with a <see cref="Restaurant"/>.
/// </summary>
public static class RestaurantIO
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
                case (SortOption)1:
                    static int SortByName(Restaurant a, Restaurant b)
                    {
                        return a.RestaurantName.CompareTo(b.RestaurantName);
                    }
                    restaurantsList.Sort(SortByName);
                    break;

                case (SortOption)2:
                    static int SortByDistance(Restaurant a, Restaurant b)
                    {
                        int distanceA = GetDistance(SessionManager.ReturnCurrentUser(), a);
                        int distanceB = GetDistance(SessionManager.ReturnCurrentUser(), b);
                        int distanceComparison = distanceA.CompareTo(distanceB);

                        // * If distances are the same (returning 0), instead sort by name
                        return distanceComparison != 0 ? distanceComparison : a.RestaurantName.CompareTo(b.RestaurantName);
                    }
                    restaurantsList.Sort(SortByDistance);
                    break;

                case (SortOption)3:
                    static int SortByStyle(Restaurant a, Restaurant b)
                    {
                        int styleComparison = a.RestaurantStyle.CompareTo(b.RestaurantStyle);
                        // * If styles are the same (returning 0), instead sort by name 
                        return styleComparison != 0 ? styleComparison : a.RestaurantName.CompareTo(b.RestaurantName);
                    }
                    restaurantsList.Sort(SortByStyle);
                    break;

                case (SortOption)4:
                    static int SortByRating(Restaurant a, Restaurant b)
                    {
                        double averageRatingA = RestaurantIO.GetAverageRestaurantRating(a);
                        double averageRatingB = RestaurantIO.GetAverageRestaurantRating(b);

                        int ratingComparison = averageRatingB.CompareTo(averageRatingA);
                        // * If ratings are the same (returning 0), instead sort by name 
                        return ratingComparison != 0 ? ratingComparison : a.RestaurantName.CompareTo(b.RestaurantName);
                    }
                    restaurantsList.Sort(SortByRating);
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

        // Display the headings
        DisplayIO.DisplayMessage("   "
            + CustomerConstants.RESTAURANT_NAME_HEADING_STR.PadRight(restaurantColumnWidth)
            + CustomerConstants.LOCATION_HEADING_STR.PadRight(locationColumnWidth)
            + CustomerConstants.DISTANCE_HEADING_STR.PadRight(distanceColumnWidth)
            + CustomerConstants.STYLE_HEADING_STR.PadRight(styleColumnWidth)
            + CustomerConstants.RATING_HEADING_STR);

        // Display the restaurants
        int restaurantChoiceIndex = 1;
        for (int i = 0; i < restaurantsList.Count(); i++)
        {
            // * Get average rating, if 0, then use "-"
            double averageRating = RestaurantIO.GetAverageRestaurantRating(restaurantsList[i]);
            string rating = averageRating != 0 ? $"{averageRating:F1}" : "-";

            DisplayIO.DisplayMessage($"{restaurantChoiceIndex}: "
                + $"{restaurantsList[i].RestaurantName}".PadRight(restaurantColumnWidth)
                + $"{restaurantsList[i].Location}".PadRight(locationColumnWidth)
                + $"{GetDistance(SessionManager.ReturnCurrentUser(), restaurantsList[i])}".PadRight(distanceColumnWidth)
                + $"{restaurantsList[i].RestaurantStyle}".PadRight(styleColumnWidth)
                + $"{rating}".PadRight(3));
            restaurantChoiceIndex++;
        }
        choiceIndex = restaurantChoiceIndex;
        return restaurantsList;
    }
    
    /// <summary>
    /// Allows the logged-in (<see cref="Client"/>) <see cref="User"/> to add a new item to their <see cref="Restaurant._restaurantMenu"/>.
    /// If the user owns a restaurant, displays the current menu and prompts for a new item.
    /// <para> If the user enters a non-blank input, this becomes the item name, and it then prompts for an 
    /// item price using <see cref="GetMenuItemPrice()"/>. </para>
    /// <para> 
    /// The item name and price are then registered into the <see cref="Restaurant._restaurantMenu"/>. 
    /// </para> 
    /// </summary>
    public static void AddItemsToRestaurant()
    {
        var currentUser = SessionManager.ReturnCurrentUser();
        if (currentUser != null)
        {
            if (RestaurantRegistry.TryFindClientsRestaurant(currentUser, out Restaurant? restaurant))
            {
                DisplayIO.DisplayMessage("This is your restaurant's current menu:");
                restaurant?.DisplayCurrentlyRegisteredMenuItems();

                DisplayIO.DisplayMessage("Please enter the name of the new item (blank to cancel):");

                string itemName = DisplayIO.ReadInput();

                if (!string.IsNullOrWhiteSpace(itemName))
                {
                    decimal itemPrice = GetMenuItemPrice();
                    if (restaurant!.TryRegisterMenuItem(itemName, itemPrice))
                    {
                        DisplayIO.DisplayMessage($"Successfully added {itemName} (${itemPrice:F2}) to menu.");
                    }
                    else DisplayIO.DisplayMessage("This item is already added to the menu.");
                }
            }

            else DisplayIO.DisplayMessage("You currently have no restaurants.");
        }

        else DisplayIO.DisplayMessage("No user is currently logged in.");
    }

    /// <summary>
    /// Calculates the average rating for a given <see cref="Restaurant"/> based on
    /// all <see cref="RestaurantReview.Rating"/>s for the <see cref="Restaurant"/>.
    /// </summary>
    /// <param name="restaurant"> The <see cref="Restaurant"/> to find the average rating for. </param>
    /// <returns> The average rating of a <see cref="Restaurant"/>, based on <see cref="RestaurantReview"/>s.
    /// If no reviews exist, returns <c>0</c>. </returns>
    public static double GetAverageRestaurantRating(Restaurant restaurant)
    {
        int totalRating = 0;
        int reviewsCount = 0;

        if (OrderRegistry.TryGetOrders(out List<CustomerOrder> customerOrders, restaurant))
        {
            foreach (CustomerOrder order in customerOrders)
            {
                if (order.Restaurant == restaurant && order.RestaurantReview != null)
                {
                    totalRating += order.RestaurantReview.Rating;
                    reviewsCount++;
                }
            }
        }
        return reviewsCount > 0 ? Math.Round((double)totalRating / reviewsCount, 1) : 0;
    }

    /// <summary>
    /// To be called by <see cref="AddItemsToRestaurant()"/>, and continously
    /// reads a string input from the user via the console until it is successfully parsed
    /// into a decimal price. 
    /// <para> If the input is valid, parses the price to <see cref="IOUtilities.IsValidItemPrice()"/> 
    /// to verify if it is within the valid range. </para>
    /// </summary>
    /// <returns> The validated item price. This method loops until a valid input is provided. </returns>
    private static decimal GetMenuItemPrice()
    {
        while (true)
        {
            DisplayIO.DisplayMessage("Please enter the price of the new item (without the $):");

            if (decimal.TryParse(DisplayIO.ReadInput(), out decimal itemPriceInput))
            {
                decimal itemPrice = itemPriceInput;

                if (IOUtilities.IsValidItemPrice(itemPrice)) return itemPrice;
                else
                {
                    DisplayIO.DisplayMessage("Invalid price.");
                    continue;
                }
            }
            else DisplayIO.DisplayMessage("Invalid price.");
        }
    }

    /// <summary>
    /// Calculates the taxicab distance between a <see cref="User"/> and
    /// a <see cref="Restaurant"/>.
    /// </summary>
    /// <param name="user"> The user whose location will be compared. </param>
    /// <param name="restaurant"> The restaurant whose location will be compared. </param>
    /// <returns> The taxicab distance between the user and the restaurant. </returns>
    public static int GetDistance(User user, Restaurant restaurant)
    {
        // user u1, u2
        // restaurant r1, r2
        // Distance = (u1 - r1) + (u2 - r2)   

        string[] userCoords = user.Location.Split(',');
        string[] restaurantCoords = restaurant.Location.Split(',');

        int u1 = int.Parse(userCoords[0]);
        int u2 = int.Parse(userCoords[1]);
        int r1 = int.Parse(restaurantCoords[0]);
        int r2 = int.Parse(restaurantCoords[1]);

        int distance = Math.Abs(u1 - r1) + Math.Abs(u2 - r2);
        return distance;
    }

}
