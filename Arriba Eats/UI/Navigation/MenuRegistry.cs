using System;
using Menus;

namespace Navigation;

public static class MenuRegistry
{
    public static Dictionary<MenuState, IMenu> menuMap = new Dictionary<MenuState, IMenu>
    {
        { MenuState.MainMenu, new MainMenu() },
        { MenuState.RegistrationMenu, new RegistrationMenu() }
        //{ MenuState.LoginMenu, new LoginMenu},
    };
}