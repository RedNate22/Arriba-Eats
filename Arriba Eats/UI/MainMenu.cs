using System;
using DisplayIO;
using UINavigation;

namespace UI;

/// <summary>
/// Represents the main menu.
/// <para> Allows users to login, register, or exit the application. </para>
/// <para> Handles user input and updates the application state accordingly. </para>
/// </summary>
public class MainMenu : IMenu
{
    private const string WELCOME_STR = "Welcome to Arriba Eats!";
    private const string LOGIN_STR = "1: Login as a registered user";
    private const string REGISTER_STR = "2: Register as a new user";
    private const string EXIT_STR = "3: Exit";
    private readonly string ENTER_CHOICE_STR = IOUtilities.EnterChoiceStr(3);
    private const string GOODBYE_STR = "Thank you for using Arriba Eats!";

    private const int LOGIN_INT = 1, REGISTER_INT = 2, EXIT_INT = 3;
    
    /// <summary> 
    /// <para> Displays the welcome message. </para>
    /// <para> To be called seperately from the menu transitions. </para>
    /// </summary>
    public static void WelcomeMessage()
    {
        IODisplay.DisplayMessage(WELCOME_STR);
    }

    /// <summary> 
    /// Displays the main menu options and gets choice from user.
    /// </summary>
    public void DisplayMenu()
    {
        IODisplay.DisplayMessage(MenuConstants.MAKE_CHOICE_STR);
        IODisplay.DisplayMessage(LOGIN_STR);
        IODisplay.DisplayMessage(REGISTER_STR);
        IODisplay.DisplayMessage(EXIT_STR);
        IODisplay.DisplayMessage(ENTER_CHOICE_STR);

        int option = IODisplay.GetChoice();

        switch (option)
        {
            case LOGIN_INT:  
                IODisplay.DisplayEmptyLine();
                // TryGetValue(?)
                break;
            case REGISTER_INT:  
                IODisplay.DisplayEmptyLine();
                UIFlowController.CurrentState = MenuState.RegistrationMenu;
                break;
            case EXIT_INT:  
                IODisplay.DisplayMessage(GOODBYE_STR);
                UIFlowController.CurrentState = MenuState.Exit;
                break;
            default:  
                IODisplay.DisplayMessage(MenuConstants.INVALID_CHOICE_STR);
                break;
        }
    }
}