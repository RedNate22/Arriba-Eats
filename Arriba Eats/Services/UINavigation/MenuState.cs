using System;

namespace UINavigation;

/// <summary>
/// Defines the various states of the menu system. All states are stored in <see cref="MenuRegistry"/>
/// with their associated <see cref="UI"/> menus instances.
/// <para> To be called by <see cref="UIFlowController.ChangeMenu()"/> to determine which menu 
/// should be displayed. </para>
/// </summary>
public enum MenuState
{
    /// <summary>
    /// Exits the application.
    /// </summary>
    Exit,

    /// <summary>
    /// Represents the <see cref="UI.MainMenu"/>.
    /// </summary>
    MainMenu,

    /// <summary>
    /// Represents the <see cref="UI.RegistrationMenu"/>.
    /// </summary>
    RegistrationMenu,

    /// <summary>
    /// Represents the <see cref="UI.LoginMenu"/>.
    /// </summary>
    LoginMenu,

    /// <summary>
    /// Represents the <see cref="UI.CustomerMainMenu"/>.
    /// </summary>
    CustomerMainMenu,

    /// <summary>
    /// Represents the <see cref="UI.CustomerSortRestaurantsMenu"/>.
    /// </summary>
    CustomerSortRestaurantsMenu,

    /// <summary>
    /// Represents the <see cref="UI.CustomerBrowseRestaurantsMenu"/>.
    /// </summary>
    CustomerBrowseRestaurantsMenu,
    
    /// <summary>
    /// Represents the <see cref="UI.CustomerPlaceOrderMenu"/>.
    /// </summary>
    CustomerPlaceOrderMenu,

    /// <summary>
    /// Represents the <see cref="UI.CustomerOrderStatusMenu"/>.
    /// </summary>
    CustomerOrderStatusMenu,

    /// <summary>
    /// Represents the <see cref="UI.CustomerRateRestaurantMenu"/>.
    /// </summary>
    CustomerRateRestaurantMenu,

    /// <summary>
    /// Represents the <see cref="UI.DelivererMainMenu"/>.
    /// </summary>
    DelivererMainMenu,

    /// <summary>
    /// Represents the <see cref="UI.ClientMainMenu"/>.
    /// </summary>
    ClientMainMenu,
}