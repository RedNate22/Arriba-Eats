using System;

namespace UI;

/// <summary>
/// Define constants to be used across all <see cref="Entities.Customer"/> related menus.
/// </summary>
public static class CustomerConstants
{
    // TODO xml
    public const string CUSTOMER_MAIN_MENU_CHOICES_STR = """
        2: Select a list of restaurants to order from       
        3: See the status of your orders
        4: Rate a restaurant you've ordered from
        """;
    
    // CustomerSortRestaurantsMenu
    public const string RESTAURANT_LIST_ORDERED_STR = """
        How would you like the list of restaurants ordered?
        1: Sorted alphabetically by name
        2: Sorted by distance
        3: Sorted by style
        4: Sorted by average rating
        """;

    // CustomerBrowseRestaurantsMenu
    public const string YOU_CAN_ORDER_FROM_THE_FOLLOWING_STR = "You can order from the following restaurants:";
    public const string ORDER_DETAILS_FOR_REVIEW_STR = "{0}: Order #{1} from {2}";

    // CustomerSeeReviewsMenu
    public const string NO_REVIEWS_STR = "No reviews have been left for this restaurant.";
    public const string REVIEW_DETAILS_STR = """
        Reviewer: {0}
        Rating: {1}
        Comment: {2}
        """;

    // CustomerPlaceOrderMenu
    public const string CURRENT_ORDER_TOTAL_STR = "Current order total: ${0:F2}";
    public const string ORDER_PLACED_STR = "Your order has been placed. Your order number is #{0}.";
    public const string ORDER_NOT_CONFIRMED_STR = "Order could not be confirmed.";
    public const string RESTAURANT_HAS_NO_MENU_STR = "{0} currently has no items on the menu.";

    // CustomerOrderStatusMenu
    public const string NOT_PLACED_ORDERS_STR = "You have not placed any orders.";

    // CustomerRateRestaurantMenu
    public const string SELECT_PREVIOUS_ORDER_TO_RATE_STR = "Select a previous order to rate the restaurant it came from:";
    public const string YOU_ARE_RATING_STR = "You are rating order #{0} from {1}:";
    public const string PLEASE_ENTER_RATING_STR = "Please enter a rating for this restaurant (1-5, 0 to cancel):";
    public const string INVALID_RATING_STR = "Invalid rating.";
    public const string ENTER_COMMENT_STR = "Please enter a comment to accompany this rating:";
    public const string THANK_YOU_FOR_RATING_STR = "Thank you for rating {0}.";

    // 
    public const string RESTAURANT_NAME_HEADING_STR = "Restaurant Name";
    public const string LOCATION_HEADING_STR = "Loc";
    public const string DISTANCE_HEADING_STR = "Dist";
    public const string STYLE_HEADING_STR = "Style";
    public const string RATING_HEADING_STR = "Rating";
    public const string SEE_RESTAURANTS_MENU_STR = "1: See this restaurant's menu and place an order";
    public const string SEE_REVIEWS_STR = "2: See reviews for this restaurant";
    public const string RETURN_MAIN_MENU_STR = "3: Return to main menu";
    public const string INVALID_QUANTITY_STR = "Invalid quantity.";
}