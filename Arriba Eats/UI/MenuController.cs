using System;

namespace UI;

public static class MenuController
{    
    public static MenuState currentState { get; set; }
    
    public static void Run()
    {
        MainMenu mainMenu = new MainMenu();
        RegistrationMenu registrationMenu = new RegistrationMenu();
        //LoginMenu loginMenu = new LoginMenu();

        currentState = MenuState.MainMenu;
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
