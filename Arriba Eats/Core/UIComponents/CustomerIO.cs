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
    public static void DisplayRestaurantsList()
    {
        if (RestaurantRegistry.TryListRestaurants(out List<Restaurant> restaurantsList))
        {
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

            for (int i = 0; i < restaurantsList.Count(); i++)
            {
                IODisplay.DisplayMessage("   " +
                    RESTAURANT_NAME_HEADING.PadRight(maxRestaurantNameWidth) 
                    + LOCATION_HEADING.PadRight(locationColumnWidth) 
                    + DISTANCE_HEADING.PadRight(distanceColumnWidth)
                    + STYLE_HEADING.PadRight(styleColumnWidth) 
                    + RATING_HEADING);
                
                IODisplay.DisplayMessage($"{restaurantChoiceIndex}: " 
                    + $"{restaurantsList[i].RestaurantName}".PadRight(maxRestaurantNameWidth) 
                    + $"{restaurantsList[i].Location}".PadRight(locationColumnWidth) 
                    + "D".PadRight(distanceColumnWidth) 
                    + $"{restaurantsList[i].RestaurantStyle}".PadRight(styleColumnWidth) 
                    + "R");

                restaurantChoiceIndex++;
            }

            string _returnPreviousMenu = IOUtilities.ReturnToPreviousMenuStr(restaurantChoiceIndex + 1);
            string _enterChoice = IOUtilities.EnterChoiceStr(restaurantChoiceIndex + 1);
            
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
