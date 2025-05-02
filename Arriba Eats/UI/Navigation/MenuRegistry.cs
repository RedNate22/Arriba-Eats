using System;
using Menus;

namespace Navigation;

public class MenuRegistry
{
    Dictionary<MenuState, IMenu> menuRegistry = new Dictionary<MenuState, IMenu>
    {
        { MenuState.MainMenu, new MainMenu() },
        { MenuState.RegistrationMenu, new RegistrationMenu() }
        //{ MenuState.LoginMenu, new LoginMenu},
    };
}