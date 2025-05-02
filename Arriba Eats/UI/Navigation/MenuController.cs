using System;
using Menus;

namespace Navigation;

/// <summary>
/// <para>Initialises menu instances and starts main input loop.<para>
/// <para>Tracks the current menu state and transitions based on user input.</para>
/// </summary>
public static class MenuController
{    
    /// <summary>
    /// Tracks the currently active menu state.
    /// </summary>
    public static MenuState currentState { get; set; }
    
    /// <summary>
    /// Initialises all menus, tracks the active menu state,
    /// and runs the main input loop.
    /// </summary>
    public static void Run()
    {
        MainMenu mainMenu = new MainMenu();
        RegistrationMenu registrationMenu = new RegistrationMenu();
        //LoginMenu loginMenu = new LoginMenu();

        currentState = MenuState.MainMenu;  // Default menu
        mainMenu.WelcomeMessage(); 

        while(currentState != MenuState.Exit)
        {
            switch (currentState)
            {
                case MenuState.MainMenu:
                mainMenu.DisplayMenu();
                break;

                case MenuState.RegistrationMenu:
                registrationMenu.DisplayMenu();
                break;

                case MenuState.LoginMenu:
                //loginMenu.DisplayMenu();
                break;
            }
        }
    }
}
