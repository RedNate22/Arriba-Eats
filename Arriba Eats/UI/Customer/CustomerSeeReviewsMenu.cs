using System;
using UIComponents;
using UINavigation;

namespace UI;

/// <summary>
/// Represents the menu where a <see cref="Entities.Customer"/> can view all the 
/// <see cref="Entities.RestaurantReview"/>s associated with the selected <see cref="Entities.Restaurant"/>
/// in <see cref="CustomerBrowseRestaurantsMenu"/>.
/// </summary>
public class CustomerSeeReviewsMenu : IMenu
{
    /// <summary>
    /// Displays the <see cref="CustomerSeeReviewsMenu"/>.
    /// <para> If the selected <see cref="Entities.Restaurant"/> from <see cref="CustomerBrowseRestaurantsMenu"/>
    /// currently has any <see cref="Entities.RestaurantReview"/>s, they are displayed. Otherwise,
    /// a prompt is displayed to the <see cref="Entities.Customer"/> of no reviews, and they are 
    /// returned to <see cref="CustomerBrowseRestaurantsMenu"/>. </para>
    /// </summary>
    public void DisplayMenu()
    {
        CustomerBrowseRestaurantsMenu.ReturningFromMenu = true;

        var customerOrders = IODisplay.GetCustomerOrders();
        if (CustomerIO.DisplayRestaurantReviews(customerOrders))
        {
            UIFlowController.ChangeMenu(MenuState.CustomerBrowseRestaurantsMenu);
        }

        else
        {
            IODisplay.DisplayMessage(CustomerConstants.NO_REVIEWS_STR);
            UIFlowController.ChangeMenu(MenuState.CustomerBrowseRestaurantsMenu);
        }
    }
}
