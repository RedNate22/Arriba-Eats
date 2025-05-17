using System;
using UIComponents;
using UINavigation;
using Entities;

namespace UI;

/// <summary>
/// Represents the menu where a <see cref="Customer"/> can view all the currently registered
/// <see cref="Restaurant"/>s to order from, sorted by one of the options in <see cref="UIComponents.SortOption"/>s.
/// </summary>
public class CustomerBrowseRestaurantsMenu : IMenu
{
    /// <summary>
    /// Gets the <see cref="UIComponents.SortOption"/> from <see cref="CustomerSortRestaurantsMenu"/>,
    /// based on <see cref="Customer"/> input.
    /// </summary>
    public static SortOption SortOption { private get; set; }

    /// <summary>
    /// The currently selected <see cref="Restaurant"/> by the <see cref="Customer"/>.
    /// </summary>
    public static Restaurant? SelectedRestaurant { get; private set; }

    /// <summary>
    /// Displays a list of registered <see cref="Restaurant"/>s, sorted by the customer's selected <see cref="SortOption"/>.
    /// The customer can choose a restaurant or return to <see cref="CustomerSortRestaurantsMenu"/>.
    /// <para> After selecting a restaurant, the customer can view its menu, check ratings, or return to <see cref="CustomerMainMenu"/>. </para>
    /// </summary>
    public void DisplayMenu()
    {
        IODisplay.DisplayMessage(CustomerConstants.YOU_CAN_ORDER_FROM_THE_FOLLOWING_STR);
        List<Restaurant> restaurantsList = CustomerIO.GetRestaurantsList(SortOption);

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

        int returnPreviousMenuInt = restaurantChoiceIndex;
        int enterChoiceInt = restaurantChoiceIndex;
        IODisplay.DisplayMessage(IOUtilities.ReturnToPreviousMenuStr(returnPreviousMenuInt));
        IODisplay.DisplayMessage(IOUtilities.EnterChoiceStr(enterChoiceInt));

        int choice = IODisplay.GetChoice();
        if (choice == returnPreviousMenuInt) UIFlowController.ChangeMenu(MenuState.CustomerSortRestaurantsMenu);
        else
        {
            SelectedRestaurant = restaurantsList[choice - 1];  // Adjust for index-based referencing

            IODisplay.DisplayMessage($"Placing order from {SelectedRestaurant.RestaurantName}.");
            IODisplay.DisplayMessage(CustomerConstants.SEE_RESTAURANTS_MENU_STR);
            IODisplay.DisplayMessage(CustomerConstants.SEE_REVIEWS_STR);
            IODisplay.DisplayMessage(CustomerConstants.RETURN_MAIN_MENU_STR);

            const int SEE_RESTAURANTS_MENU_INT = 1, SEE_REVIEWS_INT = 2, RETURN_MAIN_MENU_INT = 3;

            choice = IODisplay.GetChoice();

            switch (choice)
            {
                case SEE_RESTAURANTS_MENU_INT:
                    UIFlowController.ChangeMenu(MenuState.CustomerPlaceOrderMenu);
                    break;

                case SEE_REVIEWS_INT:
                    // TODO
                    IODisplay.DisplayMessage("Placeholder!");
                    break;

                case RETURN_MAIN_MENU_INT:
                    UIFlowController.ChangeMenu(MenuState.CustomerMainMenu);
                    break;
                
                default:  
                    IODisplay.DisplayMessage(MenuConstants.INVALID_CHOICE_STR);
                    break;
            }
        }
    }
    
}