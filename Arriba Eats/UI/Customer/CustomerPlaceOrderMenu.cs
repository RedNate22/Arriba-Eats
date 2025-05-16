using System;
using UIComponents;
using UINavigation;
using Entities;

namespace UI;

// TODO xml
public class CustomerPlaceOrderMenu : IMenu
{
    /*
    Current order total: $#.##
    1:   $*.**  MENU_ITEM
    Y: Complete order
    Z: Cancel order 
    */

    /// <summary>
    /// Displays the current total of the <see cref="Customer"/>'s order.
    /// </summary>
    private string currentOrderTotal = "Current order total: ${0}";

    // TODO xml
    public void DisplayMenu()
    {
        if (CustomerBrowseRestaurantsMenu.SelectedRestaurant != null)
        {
            CustomerBrowseRestaurantsMenu.SelectedRestaurant?.DisplayCurrentMenu();
            
        }

        else UIFlowController.ChangeMenu(MenuState.CustomerMainMenu);
    }
}
