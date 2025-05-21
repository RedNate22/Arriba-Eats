using System;

namespace UI;

/// <summary>
/// Define constants to be used across all <see cref="Entities.Customer"/> related menus.
/// </summary>
public static class CustomerConstants
{
    public const string CUSTOMER_MAIN_MENU_CHOICES_STR = """
        2: Select a list of restaurants to order from       
        3: See the status of your orders
        4: Rate a restaurant you've ordered from
        """;

    public const string RESTAURANT_LIST_ORDERED_STR = """
        How would you like the list of restaurants ordered?
        1: Sorted alphabetically by name
        2: Sorted by distance
        3: Sorted by style
        4: Sorted by average rating
        """;

    public const string YOU_CAN_ORDER_FROM_THE_FOLLOWING_STR = "You can order from the following restaurants:";
    public const string NOT_PLACED_ORDERS_STR = "You have not placed any orders.";
    public const string SELECT_PREVIOUS_ORDER_TO_RATE_STR = "Select a previous order to rate the restaurant it came from:";
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
