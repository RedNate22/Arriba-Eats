using System;
using Menus;

namespace Navigation
{
    /// <summary>
    /// <para>Initialises menu instances and transitions between menus.<para>
    /// </summary>
    public static class MenuController
    {    
        /// <summary>
        /// Tracks the currently active menu state.
        /// </summary>
        private static MenuState _currentState;
        public static MenuState CurrentState 
        { 
            get { return _currentState; } 
            set { _currentState = value; } 
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static void Run()
        {
            CurrentState = MenuState.MainMenu;  // Default menu
            MainMenu.WelcomeMessage(); 

            while(CurrentState != MenuState.Exit)
            {
                if (MenuRegistry.menuMap.ContainsKey(CurrentState))
                {
                    MenuRegistry.menuMap[CurrentState].DisplayMenu();
                }
                else
                {
                    CurrentState = MenuState.MainMenu;
                }
                
                /*
                switch (currentState)
                {
                    case MenuState.MainMenu:
                        //mainMenu.DisplayMenu();
                        break;

                    case MenuState.RegistrationMenu:
                        registrationMenu.DisplayMenu();
                        break;

                    case MenuState.LoginMenu:
                        //loginMenu.DisplayMenu();
                        break;
                }
            */
            }
        }
    }
}

