using System;

namespace UI;

/// <summary>
/// Define constants to be used across all menus.
/// </summary>
public static class MenuConstants
{
    // MainMenu Constants
    public const string WELCOME_STR = "Welcome to Arriba Eats!";
    public const string MAKE_CHOICE_STR = "Please make a choice from the menu below:";
    public const string DISPLAY_USER_INFO_STR = "1: Display your user information";
    public const string MAIN_MENU_CHOICES_STR = """
        1: Login as a registered user
        2: Register as a new user
        3: Exit
        """;
    public const string GOODBYE_STR = "Thank you for using Arriba Eats!";

    // RegistrationMenu Constants
    public const string WHICH_TYPE_USER_STR = "Which type of user would you like to register as?";       
    public const string REGISTRATION_MENU_CHOICES_STR = """
        1: Customer
        2: Deliverer
        3: Client
        """;
    public const string CUSTOMER_CHOICE = "CUSTOMER_CHOICE";
    public const string DELIVERER_CHOICE = "DELIVERER_CHOICE";
    public const string CLIENT_CHOICE = "CLIENT_CHOICE";

    // CustomerMenus Constants
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
    public const string ORDER_DETAILS_FOR_REVIEW_STR = "{0}: Order #{1} from {2}";
    public const string NO_REVIEWS_STR = "No reviews have been left for this restaurant.";
    public const string REVIEW_DETAILS_STR = """
        Reviewer: {0}
        Rating: {1}
        Comment: {2}
        """;
    public const string NOT_PLACED_ORDERS_STR = "You have not placed any orders.";
    public const string SELECT_PREVIOUS_ORDER_TO_RATE_STR = "Select a previous order to rate the restaurant it came from:";
    public const string YOU_ARE_RATING_STR = "You are rating order #{0} from {1}:";
    public const string PLEASE_ENTER_RATING_STR = "Please enter a rating for this restaurant (1-5, 0 to cancel):";
    public const string INVALID_RATING_STR = "Invalid rating.";
    public const string ENTER_COMMENT_STR = "Please enter a comment to accompany this rating:";
    public const string THANK_YOU_FOR_RATING_STR = "Thank you for rating {0}.";
    public const string RESTAURANT_NAME_HEADING_STR = "Restaurant Name";
    public const string LOCATION_HEADING_STR = "Loc";
    public const string DISTANCE_HEADING_STR = "Dist";
    public const string STYLE_HEADING_STR = "Style";
    public const string RATING_HEADING_STR = "Rating";
    public const string SEE_RESTAURANTS_MENU_STR = "1: See this restaurant's menu and place an order";
    public const string SEE_REVIEWS_STR = "2: See reviews for this restaurant";
    public const string RETURN_MAIN_MENU_STR = "3: Return to main menu";
    public const string INVALID_QUANTITY_STR = "Invalid quantity.";

    // DelivererMenus Constants
    public const string DELIVERER_MAIN_MENU_CHOICES_STR = """
        2: List orders available to deliver
        3: Arrived at restaurant to pick up order
        4: Mark this delivery as complete
        """;
    public const string ALREADY_SELECTED_ORDER_STR = "You have already selected an order for delivery.";
    public const string ORDERS_AVAILABLE_TO_DELIVER_STR
        = "The following orders are available for delivery. Select an order to accept it:";
    public const string ORDER_HEADING_STR = "Order";
    public const string LOC_HEADING_STR = "Loc";
    public const string CUSTOMER_NAME_HEADING_STR = "Customer Name";
    public const string THANKS_FOR_ACCEPTING_ORDER_STR = "Thanks for accepting the order. Please head to {0} at {1} to pick it up.";
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
    public const string NOT_PICKED_UP_ORDER_STR = "You have not yet picked up this order.";
    public const string THANK_YOU_FOR_DELIVERING_STR = "Thank you for making the delivery.";

    // ClientMenus Constants
    public const string CLIENT_MAIN_MENU_CHOICES_STR = """
        2: Add item to restaurant menu
        3: See current orders
        4: Start cooking order
        5: Finish cooking order
        6: Handle deliverers who have arrived
        """;
    public const string RESTAURANT_HAS_NO_ORDERS_STR = "Your restaurant has no current orders.";
    public const string DISPLAY_ORDER_STR = "{0}: Order #{1} for {2}";  // Used in the next 3 menus
    public const string SELECT_ORDER_TO_COOK_STR = "Select an order once you are ready to start cooking:";
    public const string SELECT_ORDER_TO_FINISH_STR = "Select an order once you have finished preparing it:";
    public const string ORDER_READY_FOR_COLLECTION_STR = "Order #{0} is now ready for collection.";
    public const string NO_DELIVERER_ASSIGNED_STR = "No deliverer has been assigned yet.";
    public const string TAKE_TO_DELIVERER_STR
        = "Please take it to the deliverer with licence plate {0}, who is waiting to collect it.";
    public const string DELIVERER_ARRIVING_SOON_STR
        = "The deliverer with licence plate {0} will be arriving soon to collect it.";
    public const string THESE_DELIVERERS_ARRIVED_STR = "These deliverers have arrived and are waiting to collect orders.";
    public const string SELECT_COLLECTED_ORDERS_STR = "Select an order to indicate that the deliverer has collected it:";
    public const string ORDER_DETAILS_STR
        = "{0}: Order #{1} for {2} (Deliverer licence plate: {3}) (Order status: {4})";
    public const string ORDER_NOT_COOKED_STR = "This order has not yet been cooked.";
    public const string ORDER_NOW_BEING_DELIVERED = "Order #{0} is now marked as being delivered.";
}


