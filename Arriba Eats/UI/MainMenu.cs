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
    private readonly string ENTER_CHOICE_STR = IOUtilities.EnterChoiceStr(3);
    private const int LOGIN_INT = 1, REGISTER_INT = 2, EXIT_INT = 3;
    
    /// <summary> 
    /// <para> Displays the welcome message. </para>
    /// <para> To be called seperately from the menu transitions. </para>
    /// </summary>
    public static void WelcomeMessage()
    {
        IODisplay.DisplayMessage(MenuConstants.WELCOME_STR);
    }

    /// <summary> 
    /// Displays the main menu options and gets choice from user.
    /// </summary>
    public void DisplayMenu()
    {
        IODisplay.DisplayMessage(MenuConstants.MAKE_CHOICE_STR);
        IODisplay.DisplayMessage(MenuConstants.LOGIN_STR);
        IODisplay.DisplayMessage(MenuConstants.REGISTER_STR);
        IODisplay.DisplayMessage(MenuConstants.EXIT_STR);
        IODisplay.DisplayMessage(ENTER_CHOICE_STR);

        int option = IODisplay.GetChoice();

        switch (option)
        {
            case LOGIN_INT:  
                IODisplay.DisplayEmptyLine();
                UIFlowController.ChangeMenu("LoginMenu");
                break;
            case REGISTER_INT:  
                IODisplay.DisplayEmptyLine();
                UIFlowController.ChangeMenu("RegistrationMenu");
                break;
            case EXIT_INT:  
                IODisplay.DisplayMessage(MenuConstants.GOODBYE_STR);
                UIFlowController.ChangeMenu("Exit");
                break;
            default:  
                IODisplay.DisplayMessage(MenuConstants.INVALID_CHOICE_STR);
                break;
        }
    }
}