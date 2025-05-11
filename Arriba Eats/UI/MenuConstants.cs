using System;
using DisplayIO;

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

    /// <summary> Used in <see cref="IOUtilities.ReturnToPreviousMenuStr()"/> to dynamically format the
    /// string for the correct position and choice number. </summary>
    public const string RETURN_PREVIOUS_MENU_TEMPLATE = "{0}: Return to the previous menu";
 
    // Main menu
    public const string MAIN_MENU_CHOICES_STR = """
        1: Login as a registered user
        2: Register as a new user
        3: Exit
        """;
    public const string GOODBYE_STR = "Thank you for using Arriba Eats!";

    // Registration Menu
    public const string REGISTRATION_MENU_CHOICES_STR = """
        Which type of user would you like to register as?       
        1: Customer
        2: Deliverer
        3: Client
        """;
    public const string CUSTOMER_CHOICE = "CUSTOMER_CHOICE";
    public const string DELIVERER_CHOICE = "DELIVERER_CHOICE";
    public const string CLIENT_CHOICE = "CLIENT_CHOICE";

    // CustomerMainMenu
    public const string CUSTOMER_MAIN_MENU_CHOICES_STR = """
        2: Select a list of restaurants to order from       
        3: See the status of your orders
        4: Rate a restaurant you've ordered from
        """;
}


