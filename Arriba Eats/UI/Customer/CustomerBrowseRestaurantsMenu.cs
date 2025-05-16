using System;
using UIComponents;
using UINavigation;

namespace UI;

// TODO xml
public class CustomerBrowseRestaurantsMenu : IMenu
{
    /// <summary>
    /// Gets the <see cref="UIComponents.SortOption"/> from <see cref="CustomerSortRestaurantsMenu"/>,
    /// based on <see cref="Entities.Customer"/> input.
    /// </summary>
    public static SortOption SortOption { get; set; }

    // TODO xml
    public void DisplayMenu()
    {
        IODisplay.DisplayMessage(CustomerConstants.YOU_CAN_ORDER_FROM_THE_FOLLOWING_STR);
        CustomerIO.DisplayRestaurantsList(SortOption);

        int choice = IODisplay.GetChoice();

        switch (choice)
        {
            case 1:
                UIFlowController.ChangeMenu(MenuState.CustomerSortRestaurantsMenu);
                break;
        }
    }
    
}
