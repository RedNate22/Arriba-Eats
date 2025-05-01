using System;

namespace UI;

public static class MenuController
{    
    public static void Run()
    {
        MainMenu mainMenu = new MainMenu();
        RegistrationMenu registrationMenu = new RegistrationMenu();
        //LoginMenu loginMenu = new LoginMenu();

        
        MenuState currentState = MenuState.MainMenu;
        mainMenu.WelcomeMessage(); 
            
        while(currentState != MenuState.Exit)
        {
            switch (currentState)
            {
                case MenuState.MainMenu:
                mainMenu.DisplayMenu();
                break;

                case MenuState.RegistrationMenu:
                break;

                case MenuState.LoginMenu:
                break;
            }
        }
    }
}
