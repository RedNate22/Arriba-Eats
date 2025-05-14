using System;

namespace UI;

/// <summary>
/// Define constants to be used across all Registration related menus.
/// </summary>
public static class RegistrationConstants
{
    public const string REGISTRATION_MENU_CHOICES_STR = """
        Which type of user would you like to register as?       
        1: Customer
        2: Deliverer
        3: Client
        """;
    public const string CUSTOMER_CHOICE = "CUSTOMER_CHOICE";
    public const string DELIVERER_CHOICE = "DELIVERER_CHOICE";
    public const string CLIENT_CHOICE = "CLIENT_CHOICE";
}
