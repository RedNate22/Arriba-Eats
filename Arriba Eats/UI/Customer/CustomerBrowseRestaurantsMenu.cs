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
    /// Tracks whether the <see cref="Customer"/> has previously returned from the
    /// <see cref="CustomerPlaceOrderMenu"/>, either after cancelling their order, or completing one.
    /// </summary>
    private static bool _returningFromMenu = false;

    /// <summary>
    /// Gets or sets the value of <see cref="_returningFromMenu"/> to track whether the <see cref="Customer"/>
    /// has previously returned from <see cref="CustomerPlaceOrderMenu"/>, either after cancelling their order,
    /// or completing one.
    /// </summary>
    public static bool ReturningFromMenu
    {
        get { return _returningFromMenu; }
        set { _returningFromMenu = value; }
    }

    /// <summary>
    /// The method to be called in <see cref="DisplayMenu"/>. Depending on the state of
    /// <see cref="ReturningFromMenu"/>, this method is either called after prompting the <see cref="Customer"/>
    /// to choose a <see cref="Restaurant"/>, or by itself when the <see cref="Customer"/> is returning from
    /// <see cref="CustomerPlaceOrderMenu"/>.
    /// </summary>
    public static void DisplayOptions()
    {
        IODisplay.DisplayMessage($"Placing order from {SelectedRestaurant?.RestaurantName}.");
        IODisplay.DisplayMessage(CustomerConstants.SEE_RESTAURANTS_MENU_STR);
        IODisplay.DisplayMessage(CustomerConstants.SEE_REVIEWS_STR);
        IODisplay.DisplayMessage(CustomerConstants.RETURN_MAIN_MENU_STR);

        const int SEE_RESTAURANTS_MENU_INT = 1, SEE_REVIEWS_INT = 2, RETURN_MAIN_MENU_INT = 3;

        int choice = IODisplay.GetChoice();

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
                ReturningFromMenu = false;
                SelectedRestaurant = null;
                UIFlowController.ChangeMenu(MenuState.CustomerMainMenu);
                break;

            default:
                IODisplay.DisplayMessage(MenuConstants.INVALID_CHOICE_STR);
                break;
        }
    }

    /// <summary>
    /// Displays the <see cref="CustomerBrowseRestaurantsMenu"/> menu.
    /// <para> Gets the <see cref="Restaurant"/>s as a list from <see cref="CustomerIO.DisplayRestaurantsList"/>, 
    /// then prompts the <see cref="Customer"/> to either choose a <see cref="Restaurant"/> from the list or return to
    /// the previous menu, taking them back to <see cref="CustomerSortRestaurantsMenu"/>. </para>
    /// <para> After selecting a restaurant, the <see cref="Customer"/> can view its menu (taking them to
    /// <see cref="CustomerPlaceOrderMenu"/>) or check reviews. </para>
    /// <para> Based on the state of <see cref="ReturningFromMenu"/>, if <c>true</c>, the <see cref="Customer"/>
    /// is not prompted to choose a <see cref="Restaurant"/>, and instead is taken directly to the previously selected
    /// restaurant's menu. Otherwise, if <c>false</c>, the <see cref="DisplayOptions"/> method is called. The 
    /// <see cref="Customer"/> must select a <see cref="Restaurant"/>. The value of this field is set in 
    /// <see cref="CustomerPlaceOrderMenu"/> to track whether they are returning from 
    /// this menu or not. </para>
    /// </summary>
    public void DisplayMenu()
    {
        if (ReturningFromMenu == false)
        {
            IODisplay.DisplayMessage(CustomerConstants.YOU_CAN_ORDER_FROM_THE_FOLLOWING_STR);
            List<Restaurant> restaurantsList = CustomerIO.DisplayRestaurantsList(out int returnPreviousMenuChoice);

            int choice = IODisplay.GetChoice();

            if (choice == returnPreviousMenuChoice) UIFlowController.ChangeMenu(MenuState.CustomerMainMenu);
            else
            {
                SelectedRestaurant = restaurantsList[choice - 1];  // Adjust for index-based referencing
                DisplayOptions();
            }
        }
        else DisplayOptions();
    }
}