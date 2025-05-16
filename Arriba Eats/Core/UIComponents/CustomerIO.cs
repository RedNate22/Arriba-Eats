using System;
using Entities;
using UINavigation;

namespace UIComponents;

/// <summary>
/// Contains various static methods for handling I/O with <see cref="Customer"/> users.
/// <para> Uses <see cref="IOUtilities"/> for input & output formatting and validation. </para>
/// </summary>
public static class CustomerIO
{
    // TODO xml
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
