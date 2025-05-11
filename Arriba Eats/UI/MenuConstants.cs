using System;

namespace UI;

/// <summary>
/// Define constants to be used across all menus.
/// </summary>
public static class MenuConstants
{
    // Global
    public const string WELCOME_STR = "Welcome to Arriba Eats!";
    public const string MAKE_CHOICE_STR = "Please make a choice from the menu below:";

    /// <summary> Used in <see cref="IOUtilities.EnterChoiceStr()"/> to dynmically format the
    /// string to display the correct range of choices. </summary>
    public const string ENTER_CHOICE_TEMPLATE = "Please enter a choice between 1 and {0}:";
    public const string INVALID_CHOICE_STR = "Invalid choice.";
    public const string DISPLAY_USER_INFO_STR = "1: Display your user information";
    
    
    /// <summary> Used in <see cref="IOUtilities.LogOutStr()"/> to dynamically format the
    /// string for the correct position and choice number. </summary>
    public const string LOG_OUT_TEMPLATE = "{0}: Log out";
 
    // Main menu
    public const string LOGIN_STR = "1: Login as a registered user";
    public const string REGISTER_STR = "2: Register as a new user";
    public const string EXIT_STR = "3: Exit";
    public const string GOODBYE_STR = "Thank you for using Arriba Eats!";

    // Registration Menu
    public const string USER_TYPE_STR = "Which type of user would you like to register as?";
    public const string CUSTOMER_STR = "1: Customer";
    public const string DELIVERER_STR = "2: Deliverer";
    public const string CLIENT_STR = "3: Client";
    public const string RETURN_STR = "4: Return to the previous menu";
    public const string CUSTOMER_CHOICE = "CUSTOMER_CHOICE";
    public const string DELIVERER_CHOICE = "DELIVERER_CHOICE";
    public const string CLIENT_CHOICE = "CLIENT_CHOICE";

    // CustomerMainMenu
    public const string SELECT_RESTAURANTS_LIST_STR = "2: Select a list of restaurants to order from";
    public const string SEE_ORDERS_STATUS_STR = "3: See the status of your orders";
    public const string RATE_RESTAURANT_STR = "4: Rate a restaurant you've ordered from";

}


