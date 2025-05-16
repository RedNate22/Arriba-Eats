using System;
using UIComponents;
using UINavigation;
using Entities;

namespace UI;

// TODO xml
public class CustomerBrowseRestaurantsMenu : IMenu
{
    /// <summary>
    /// Gets the <see cref="UIComponents.SortOption"/> from <see cref="CustomerSortRestaurantsMenu"/>,
    /// based on <see cref="Entities.Customer"/> input.
    /// </summary>
    public static SortOption SortOption { private get; set; }

    // TODO xml
    public void DisplayMenu()
    {
        IODisplay.DisplayMessage(CustomerConstants.YOU_CAN_ORDER_FROM_THE_FOLLOWING_STR);
        List<Restaurant> restaurantsList = CustomerIO.GetRestaurantsList(SortOption);

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

        int returnPreviousMenu = restaurantChoiceIndex;
        int enterChoice = restaurantChoiceIndex;
        string _returnPreviousMenu = IOUtilities.ReturnToPreviousMenuStr(returnPreviousMenu);
        string _enterChoice = IOUtilities.EnterChoiceStr(enterChoice);

        IODisplay.DisplayMessage(_returnPreviousMenu);
        IODisplay.DisplayMessage(_enterChoice);

        int choice = IODisplay.GetChoice();

        if (choice == returnPreviousMenu) UIFlowController.ChangeMenu(MenuState.CustomerSortRestaurantsMenu);
        else
        {

        }

        /*
        switch (choice)
        {
            case returnPreviousMenu:
                UIFlowController.ChangeMenu(MenuState.CustomerSortRestaurantsMenu);
                break;
        }
        */
    }
    
}
