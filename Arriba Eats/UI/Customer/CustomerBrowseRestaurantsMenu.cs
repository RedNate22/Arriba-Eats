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
    /// The currently selected <see cref="Restaurant"/> by the <see cref="Customer"/>.
    /// </summary>
    public static Restaurant? SelectedRestaurant { get; private set; }
    
    /// <summary>
    /// Displays the <see cref="CustomerBrowseRestaurantsMenu"/> menu.
    /// <para> Gets the <see cref="Restaurant"/>s as a list from <see cref="CustomerIO.DisplayRestaurantsList()"/>, then
    /// prompts the <see cref="Customer"/> to either choose a <see cref="Restaurant"/> from the list or return to
    /// the previous menu, taking them back to <see cref="CustomerSortRestaurantsMenu"/>. </para>
    /// <para> After selecting a restaurant, the <see cref="Customer"/> can view its menu (taking them to
    /// <see cref="CustomerPlaceOrderMenu"/>) or check reviews. </para>
    /// </summary>
    public void DisplayMenu()
    {
        IODisplay.DisplayMessage(CustomerConstants.YOU_CAN_ORDER_FROM_THE_FOLLOWING_STR);
        List<Restaurant> restaurantsList = CustomerIO.DisplayRestaurantsList(out int returnPreviousMenuChoice);

        int choice = IODisplay.GetChoice();

        if (choice == returnPreviousMenuChoice) UIFlowController.ChangeMenu(MenuState.CustomerSortRestaurantsMenu);
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
                    IODisplay.DisplayMessage("(Placeholder) See reviews option selected.");  // !
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