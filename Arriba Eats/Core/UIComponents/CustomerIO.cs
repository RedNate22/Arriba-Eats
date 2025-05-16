using System;
using Entities;
using UINavigation;

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
}
