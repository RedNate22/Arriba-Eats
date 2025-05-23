using System;

namespace UI;

/// <summary>
/// Define constants to be used across all <see cref="Entities.Deliverer"/> related menus.
/// </summary>
public static class DelivererConstants
{
    // TODO xml

    // DelivererMainMenu constants
    public const string DELIVERER_MAIN_MENU_CHOICES_STR = """
        2: List orders available to deliver
        3: Arrived at restaurant to pick up order
        4: Mark this delivery as complete
        """;

    // DelivererListOrdersAvailableMenu
    public const string ALREADY_SELECTED_ORDER_STR = "You have already selected an order for delivery.";
    public const string ORDERS_AVAILABLE_TO_DELIVER_STR
        = "The following orders are available for delivery. Select an order to accept it:";
    public const string ORDER_HEADING_STR = "Order";
    public const string RESTAURANT_NAME_HEADING_STR = "Restaurant Name";
    public const string LOC_HEADING_STR = "Loc";
    public const string CUSTOMER_NAME_HEADING_STR = "Customer Name";
    public const string DISTANCE_HEADING_STR = "Dist";
    public const string THANKS_FOR_ACCEPTING_ORDER_STR = "Thanks for accepting the order. Please head to {0} at {1} to pick it up.";

    // DelivererArrivedAtRestaurantMenu
    public const string NOT_YET_ACCEPTED_ORDER_STR = "You have not yet accepted an order.";
    public const string ALREADY_PICKED_UP_ORDER_STR = "You have already picked up this order.";
    public const string ALREADY_AT_RESTAURANT_STR = "You already indicated that you have arrived at this restaurant.";
    public const string ARRIVED_AT_RESTAURANT_STR
        = """
        Thanks. We have informed {0} that you have arrived and are ready to pick up order #{1}.
        Please show the staff this screen as confirmation.
        """;
    public const string ORDER_STILL_BEING_PREPARED_STR = "The order is still being prepared, so please wait patiently until it is ready.";
    public const string PLEASE_DELIVER_STR = "When you have the order, please deliver it to {0} at {1}.";
}
