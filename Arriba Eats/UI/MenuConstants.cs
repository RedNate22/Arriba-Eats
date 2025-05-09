using System;

namespace UI;

/// <summary>
/// Define constants to be used across all menus.
/// </summary>
public static class MenuConstants
{
    public const string WELCOME_STR = "Welcome to Arriba Eats!";
    public const string MAKE_CHOICE_STR = "Please make a choice from the menu below:";
    public const string INVALID_CHOICE_STR = "Invalid choice.";
 
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

    // Login Menu
    public const string EMAIL_STR = "Email: ";
    public const string PASSWORD_STR = "Password: ";
}


