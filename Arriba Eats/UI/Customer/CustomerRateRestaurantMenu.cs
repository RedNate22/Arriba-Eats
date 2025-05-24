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
        IODisplay.DisplayMessage(CustomerConstants.SELECT_PREVIOUS_ORDER_TO_RATE_STR);

        var customerOrders = IODisplay.GetCustomerOrders();

        // * Check if any orders are marked delivered, and there are no reviews for them yet
        // * Then display these orders - updating the index
        int choiceIndex = CustomerIO.DisplayOrdersReadyToReview(out var ordersToReview, customerOrders);

        IODisplay.DisplayMessage(IOUtilities.ReturnToPreviousMenuStr(choiceIndex));
        IODisplay.DisplayMessage(IOUtilities.EnterChoiceStr(choiceIndex));

        int choice = IODisplay.GetChoice();

        if (choice == choiceIndex) UIFlowController.ChangeMenu(MenuState.CustomerMainMenu);

        else if (IOUtilities.IsValueInIndexRange(ordersToReview, choice - 1))  // Valid input
        {
            var selectedOrder = ordersToReview[choice - 1];  // * Adjust for index-based referencing

            IODisplay.DisplayMessage(String.Format(CustomerConstants.YOU_ARE_RATING_STR,
                selectedOrder.OrderNumber, selectedOrder.Restaurant.RestaurantName));
            selectedOrder.DisplayOrderedItems();

            IODisplay.DisplayMessage(CustomerConstants.PLEASE_ENTER_RATING_STR);
            int orderRating = CustomerIO.GetRating();

            if (orderRating == 0) UIFlowController.ChangeMenu(MenuState.CustomerMainMenu);
            else
            {
                IODisplay.DisplayMessage(CustomerConstants.ENTER_COMMENT_STR);
                string orderComment = IODisplay.ReadInput();
                CustomerIO.GetReview(selectedOrder, orderRating, orderComment);

                IODisplay.DisplayMessage(String.Format(CustomerConstants.THANK_YOU_FOR_RATING_STR,
                    selectedOrder.Restaurant.RestaurantName));
                UIFlowController.ChangeMenu(MenuState.CustomerMainMenu);
            }
        }
        else IODisplay.InvalidChoice();
    }
}
