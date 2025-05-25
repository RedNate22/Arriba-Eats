using System;
using UIComponents;
using UINavigation;

namespace UI;

/// <summary>
/// Represents the main menu.
/// <para> Allows users to login, register, or exit the application. </para>
/// <para> Handles user input and updates the application state accordingly. </para>
/// </summary>
public class MainMenu : IMenu
{
    private string _enterChoice = DisplayIO.EnterChoiceStr(3);
    private const int LOGIN_INT = 1, REGISTER_INT = 2, EXIT_INT = 3;

    /// <summary> 
    /// <para> Displays the welcome message. </para>
    /// <para> To be called seperately from the menu transitions. </para>
    /// </summary>
    public static void WelcomeMessage()
    {
        DisplayIO.DisplayMessage(MenuConstants.WELCOME_STR);
    }

    /// <summary> 
    /// Displays the <see cref="MainMenu"/> options and prompts the user to choose an option.
    /// This menu serves as the main entry point for the <see cref="Entities.User"/>.
    /// </summary>
    public void DisplayMenu()
    {
        DisplayIO.DisplayMessage(MenuConstants.MAKE_CHOICE_STR);
        DisplayIO.DisplayMessage(MenuConstants.MAIN_MENU_CHOICES_STR);
        DisplayIO.DisplayMessage(_enterChoice);

        int choice = DisplayIO.GetChoice();

        switch (choice)
        {
            case LOGIN_INT:  
                UIFlowController.ChangeMenu(MenuState.LoginMenu);
                break;
            
            case REGISTER_INT:  
                UIFlowController.ChangeMenu(MenuState.RegistrationMenu);
                break;
            
            case EXIT_INT:  
                DisplayIO.DisplayMessage(MenuConstants.GOODBYE_STR);
                UIFlowController.ChangeMenu(MenuState.Exit);
                break;
            
            default:  
                DisplayIO.InvalidChoice();
                break;
        }
    }
}