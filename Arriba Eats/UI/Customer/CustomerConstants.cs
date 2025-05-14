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

    public const string NOT_PLACED_ORDERS_STR = "You have not placed any orders.";
    public const string SELECT_PREVIOUS_ORDER_TO_RATE_STR = "Select a previous order to rate the restaurant it came from:";
}
