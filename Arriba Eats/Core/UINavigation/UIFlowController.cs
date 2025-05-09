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
        set { _currentState = value; } 
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