using System;

namespace UI;

/// <summary>
/// Define constants to be used across all <see cref="Entities.Client"/> related menus.
/// </summary>
public static class ClientConstants
{
    // TODO xml
    // ClientMainMenu constants
    public const string CLIENT_MAIN_MENU_CHOICES_STR = """
        2: Add item to restaurant menu
        3: See current orders
        4: Start cooking order
        5: Finish cooking order
        6: Handle deliverers who have arrived
        """;

    // ClientSeeCurrentOrdersMenu constants
    public const string RESTAURANT_HAS_NO_ORDERS_STR = "Your restaurant has no current orders.";

    public const string DISPLAY_ORDER_STR = "{0}: Order #{1} for {2}";  // Used in the next 3 menus
    
    // ClientStartCookingMenu constants
    public const string SELECT_ORDER_TO_COOK_STR = "Select an order once you are ready to start cooking:";

    // ClientFinishCookingMenu constants
    public const string SELECT_ORDER_TO_FINISH_STR = "Select an order once you have finished preparing it:";
    public const string ORDER_READY_FOR_COLLECTION_STR = "Order #{0} is now ready for collection.";
    public const string NO_DELIVERER_ASSIGNED_STR = "No deliverer has been assigned yet.";
    public const string TAKE_TO_DELIVERER_STR
        = "Please take it to the deliverer with licence plate {0}, who is waiting to collect it.";
    public const string DELIVERER_ARRIVING_SOON_STR
        = "The deliverer with licence plate {0} will be arriving soon to collect it.";

    // ClientHandleDeliverer constants
    public const string THESE_DELIVERERS_ARRIVED_STR = "These deliverers have arrived and are waiting to collect orders.";
    public const string SELECT_COLLECTED_ORDERS_STR = "Select an order to indicate that the deliverer has collected it:";
    public const string ORDER_DETAILS_STR
        = "{0}: Order #{1} for {2} (Deliverer licence plate: {3}) (Order status: {4})";
    public const string ORDER_NOT_COOKED_STR = "This order has not yet been cooked.";
    public const string ORDER_NOW_BEING_DELIVERED = "Order #{0} is now marked as being delivered.";
}
