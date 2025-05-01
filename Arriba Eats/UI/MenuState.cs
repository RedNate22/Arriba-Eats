using System;

namespace UI;

/// <summary>
/// <para>Represents the different states of the menu system.</para>
/// <para>Used by MenuController to determine which menu should be displayed.</para>
/// </summary>
public enum MenuState
{
    Exit,
    MainMenu,
    RegistrationMenu,
    LoginMenu
}
