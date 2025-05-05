using System;
using Menus;

namespace UINavigation;

/// <summary>
/// Registry to match Menu states to their respective menu instances.
/// </summary>
public static class MenuRegistry
{
    public static Dictionary<MenuState, IMenu> menuMap = new Dictionary<MenuState, IMenu>
    {
        { MenuState.MainMenu, new MainMenu() },
        { MenuState.RegistrationMenu, new RegistrationMenu() }
        //{ MenuState.LoginMenu, new LoginMenu},
    };
}