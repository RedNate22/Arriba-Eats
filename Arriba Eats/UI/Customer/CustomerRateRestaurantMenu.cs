using System;
using UIComponents;
using UINavigation;

namespace UI;

/// <summary>
/// Represents the menu where a <see cref="Entities.Customer"/> can view all the previously ordered
/// <see cref="Entities.CustomerOrder"/>s they have not yet reviewed and rated, then select one to do so.
/// </summary>
public class CustomerRateRestaurantMenu : IMenu
{
    /// <summary>
    /// Displays the <see cref="CustomerRateRestaurantMenu"/>.
    /// <para> Any orders marked as delivered, and have no review, will be displayed
    /// for the <see cref="Entities.Customer"/> to select. </para>
    /// </summary>
    public void DisplayMenu()
    {
        DisplayIO.DisplayMessage(MenuConstants.SELECT_PREVIOUS_ORDER_TO_RATE_STR);

        var customerOrders = OrderIO.GetCustomerOrders();

        // * Check if any orders are marked delivered, and there are no reviews for them yet
        // * Then display these orders - updating the index
        int choiceIndex = ReviewIO.DisplayOrdersReadyToReview(out var ordersToReview, customerOrders);

        DisplayIO.DisplayMessage(DisplayIO.ReturnToPreviousMenuStr(choiceIndex));
        DisplayIO.DisplayMessage(DisplayIO.EnterChoiceStr(choiceIndex));

        int choice = DisplayIO.GetChoice();

        if (choice == choiceIndex) UIFlowController.ChangeMenu(MenuState.CustomerMainMenu);

        else if (IOUtilities.IsValueInIndexRange(ordersToReview, choice - 1))  // Valid input
        {
            var selectedOrder = ordersToReview[choice - 1];  // * Adjust for index-based referencing

            DisplayIO.DisplayMessage(String.Format(MenuConstants.YOU_ARE_RATING_STR,
                selectedOrder.OrderNumber, selectedOrder.Restaurant.RestaurantName));
            selectedOrder.DisplayOrderedItems();

            DisplayIO.DisplayMessage(MenuConstants.PLEASE_ENTER_RATING_STR);
            int orderRating = ReviewIO.GetRating();

            if (orderRating == 0) UIFlowController.ChangeMenu(MenuState.CustomerMainMenu);
            else
            {
                DisplayIO.DisplayMessage(MenuConstants.ENTER_COMMENT_STR);
                string orderComment = DisplayIO.ReadInput();
                ReviewIO.CreateReview(selectedOrder, orderRating, orderComment);

                DisplayIO.DisplayMessage(String.Format(MenuConstants.THANK_YOU_FOR_RATING_STR,
                    selectedOrder.Restaurant.RestaurantName));
                UIFlowController.ChangeMenu(MenuState.CustomerMainMenu);
            }
        }
        else DisplayIO.InvalidChoice();
    }
}
