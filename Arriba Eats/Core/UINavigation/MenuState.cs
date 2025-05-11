using System;

namespace UINavigation;

/// <summary>
/// Defines the various states of the menu system.
/// <para> Used to determine which menu should be displayed. </para>
/// </summary>
public enum MenuState
{
    // TODO xml for these...
    Exit,
    MainMenu,
    RegistrationMenu,
    LoginMenu,
    CustomerMainMenu,
    DelivererMainMenu,
    ClientMainMenu,
    RestaurantListOrdered
}