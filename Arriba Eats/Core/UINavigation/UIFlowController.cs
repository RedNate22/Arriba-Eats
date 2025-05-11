using System;

namespace UINavigation;
    
/// <summary>
/// Handles menu state transitions and controls the flow of navigation within the application.
/// </summary>
public static class UIFlowController
{    
    /// <summary>
    /// Tracks the currently active menu state.
    /// </summary>
    private static MenuState _currentState;
    
    /// <summary>
    /// <para> Gets or sets the current menu state. </para>
    /// <para> When updated, <see cref="RunMenuSystem()"/> transitions to the corresponding menu. </para>
    /// </summary>
    public static MenuState CurrentState 
    { 
        get { return _currentState; } 
        private set { _currentState = value; } 
    }

    /// <summary>
    /// Changes the current menu state based on the provided input.
    /// <para> Attempts to parse the given string into a <see cref="MenuState"/> enum value. </para>
    /// <para> 
    /// If the value exists, updates <see cref="CurrentState"/>, which then
    /// causes <see cref="RunMenuSystem()"/> to appropriately call the
    /// <see cref="IMenu.DisplayMenu()"/> method, displaying the menu to the user.
    /// </para>
    /// <para> If the menu does not exist, falls back to <see cref="MenuState.MainMenu"/>.</para>
    /// </summary>
    /// <param name="menu"> 
    /// The string representing the desired <see cref="MenuState"/>.
    /// Case-insensitive, meaning values such as <c>"MainMenu"</c> 
    /// and <c>"MAINMENU"</c> both work.
    /// </param>
    public static void ChangeMenu(string menu)
    {
        if (Enum.TryParse(menu, true, out MenuState parsedMenu))
        {
            CurrentState = parsedMenu;
        }
        
        else
        {
            CurrentState = MenuState.MainMenu;
        }
    }
    
    /// <summary>
    /// Starts the menu system and handles navigation based on the current state.
    /// <para> Initialises the default menu to display first. </para>
    /// <para> Continously monitors state transitions based on <see cref="CurrentState"/> and ensures the appropriate menu is displayed. </para>
    /// </summary>
    public static void RunMenuSystem()
    {
        CurrentState = MenuState.MainMenu;

        while (CurrentState != MenuState.Exit)
        {
            if (MenuRegistry.menuMap.ContainsKey(CurrentState))
            {
                MenuRegistry.menuMap[CurrentState].DisplayMenu();
            }
            else  // Fall back to main menu if the key doesn't exist
            {
                CurrentState = MenuState.MainMenu;
            }
        }
    }
}