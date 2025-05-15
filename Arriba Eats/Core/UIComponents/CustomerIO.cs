using System;
using Entities;
using UINavigation;
using UI;

namespace UIComponents;

/// <summary>
/// Contains various static methods for handling I/O with <see cref="Customer"/> users.
/// <para> Uses <see cref="IOUtilities"/> for input & output formatting and validation. </para>
/// </summary>
public static class CustomerIO
{
    // TODO xml
    public static void DisplayRestaurantsList(SortOption sortType)  // Return list, use list[index] for customer choice
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

                    return distanceA.CompareTo(distanceB);
                }

                restaurantsList.Sort(SortByDistance);
            }

            else if (sortType == SortOption.ByStyle)
            {
                static int SortByStyle(Restaurant a, Restaurant b)
                {
                    int styleComparison = a.RestaurantStyle.CompareTo(b.RestaurantStyle);

                    // If styles are the same (returning 0), instead compare by name 
                    return styleComparison != 0 ? styleComparison : a.RestaurantName.CompareTo(b.RestaurantName);
                }

                restaurantsList.Sort(SortByStyle);
            }

            else if (sortType == SortOption.ByAverageRating)
            {
                // TODO
            }

            const string RESTAURANT_NAME_HEADING = "Restaurant Name";
            const string LOCATION_HEADING = "Loc";
            const string DISTANCE_HEADING = "Dist";
            const string STYLE_HEADING = "Style";
            const string RATING_HEADING = "Rating";

            int restaurantColumnWidth = 7;
            int locationColumnWidth = LOCATION_HEADING.Length + 4;
            int distanceColumnWidth = DISTANCE_HEADING.Length + 2;
            int styleColumnWidth = STYLE_HEADING.Length + 7;

            // Dynamically increase width of restaurant name column
            int maxRestaurantNameWidth = RESTAURANT_NAME_HEADING.Length + restaurantColumnWidth;

            foreach (Restaurant restaurant in restaurantsList)  
            {
                if (restaurant.RestaurantName.Length > maxRestaurantNameWidth)
                {
                    maxRestaurantNameWidth = restaurant.RestaurantName.Length + 1;
                }
            }

            int restaurantChoiceIndex = 1;

            IODisplay.DisplayMessage("   " +
                RESTAURANT_NAME_HEADING.PadRight(maxRestaurantNameWidth) 
                + LOCATION_HEADING.PadRight(locationColumnWidth) 
                + DISTANCE_HEADING.PadRight(distanceColumnWidth)
                + STYLE_HEADING.PadRight(styleColumnWidth) 
                + RATING_HEADING);
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

            string _returnPreviousMenu = IOUtilities.ReturnToPreviousMenuStr(restaurantChoiceIndex);
            string _enterChoice = IOUtilities.EnterChoiceStr(restaurantChoiceIndex);
            
            IODisplay.DisplayMessage(_returnPreviousMenu);
            IODisplay.DisplayMessage(_enterChoice);
        }
    }

    // TODO xml
    // TODO complete functionality
    public static void DisplayRestaurantsToRate(User user)
    {
        if (SessionManager.CurrentUser != null)
        {
            int n = 1; // TODO int needs to dynamically change
            string returnPreviousMenu = IOUtilities.ReturnToPreviousMenuStr(n);                  
            string enterChoice = IOUtilities.EnterChoiceStr(n);
            
            // TODO list restaurants ordered from
            IODisplay.DisplayMessage(returnPreviousMenu);
            IODisplay.DisplayMessage(enterChoice);
        }

        else IODisplay.DisplayMessage("No user is currently logged in.");
    }
}
