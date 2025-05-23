using System;
using UI;

namespace UINavigation;

/// <summary>
/// Registry to match Menu states to their respective menu instances.
/// </summary>
public static class MenuRegistry
{
    /// <summary>
    /// Maps each <see cref="MenuState"/> to its corresponding <see cref="IMenu"/> instance.
    /// <para> Allows dynamic menu selection based on the current state of <see cref="UIFlowController.CurrentState"/>. </para>
    /// <para> E.g. <see cref="UIFlowController.ChangeMenu(MenuState)"/>.</para>
    /// </summary>
    public static Dictionary<MenuState, IMenu> menuMap = new Dictionary<MenuState, IMenu>
    {
        { MenuState.MainMenu, new MainMenu() },
        { MenuState.RegistrationMenu, new RegistrationMenu() },
        { MenuState.LoginMenu, new LoginMenu() },
        { MenuState.CustomerMainMenu, new CustomerMainMenu() },
        { MenuState.CustomerSortRestaurantsMenu, new CustomerSortRestaurantsMenu() },
        { MenuState.CustomerBrowseRestaurantsMenu, new CustomerBrowseRestaurantsMenu() },
        { MenuState.CustomerPlaceOrderMenu, new CustomerPlaceOrderMenu() },
        { MenuState.CustomerOrderStatusMenu, new CustomerOrderStatusMenu() },
        { MenuState.CustomerRateRestaurantMenu, new CustomerRateRestaurantMenu() },
        { MenuState.ClientMainMenu, new ClientMainMenu() },
        { MenuState.ClientSeeCurrentOrdersMenu, new ClientSeeCurrentOrdersMenu() },
        { MenuState.ClientStartCookingMenu, new ClientStartCookingMenu() },
        { MenuState.ClientFinishCookingMenu, new ClientFinishCookingMenu() },
        { MenuState.ClientHandleDeliverersMenu, new ClientHandleDeliverersMenu() },
        { MenuState.DelivererMainMenu, new DelivererMainMenu() },
        { MenuState.DelivererListOrdersAvailableMenu, new DelivererListOrdersAvailableMenu() },
        { MenuState.DelivererArrivedAtRestaurantMenu, new DelivererArrivedAtRestaurantMenu() },
        { MenuState.DelivererMarkDeliveryCompleteMenu, new DelivererMarkDeliveryCompleteMenu() }
    };
}