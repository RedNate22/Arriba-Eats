using System;
using UIComponents;
using UINavigation; 

namespace UI;

/// <summary>
/// Represents the registration menu.
/// <para> Allows users to register as different types, or return to the main menu. </para>
/// <para> 
/// The types are selected by calling <see cref="RegistrationProcess.Register()"/> and parsing
/// a string parameter, named after the user type.
/// </para>
/// </summary>
public class RegistrationMenu : IMenu
{
    private string _returnPreviousMenu = IOUtilities.ReturnToPreviousMenuStr(4);
    private string _enterChoice = IOUtilities.EnterChoiceStr(4);

    /// <summary>
    /// Defines the <see cref="int"/> constants representing menu options for use in a
    /// <see cref="switch"/> statement.
    /// </summary>
    private const int CUSTOMER_INT = 1, DELIVERER_INT = 2, CLIENT_INT = 3, RETURN_INT = 4;

    /// <summary>
    /// Displays the <see cref="RegistrationMenu"/> options and prompts the <see cref="Entities.User"/> to select a user type to register as.
    /// <para> The appropriate user type is registered depending on the user's choice. </para>
    /// <para> Uses <see cref="DisplayIO.DisplayMessage()"/> to display the options. </para>
    /// <para> Uses <see cref="DisplayIO.GetChoice()"/> to get a choice from the user in the form
    /// of an integer. </para>
    /// <para> Matches the given integer with the associated menu option and passes the correct
    /// argument to <see cref="RegistrationProcess.Register"/> to begin the registration
    /// process. 
    /// </para>
    /// </summary>
    public void DisplayMenu()
    {
        DisplayIO.DisplayMessage(RegistrationConstants.WHICH_TYPE_USER_STR);
        DisplayIO.DisplayMessage(RegistrationConstants.REGISTRATION_MENU_CHOICES_STR);
        DisplayIO.DisplayMessage(_returnPreviousMenu);
        DisplayIO.DisplayMessage(_enterChoice);

        int choice = DisplayIO.GetChoice();

        switch (choice)
        {
            case CUSTOMER_INT:
                RegistrationProcess.Register(RegistrationConstants.CUSTOMER_CHOICE);
                UIFlowController.ChangeMenu(MenuState.MainMenu);
                break;
            
            case DELIVERER_INT:
                RegistrationProcess.Register(RegistrationConstants.DELIVERER_CHOICE);
                UIFlowController.ChangeMenu(MenuState.MainMenu); 
                break;
            
            case CLIENT_INT:
                RegistrationProcess.Register(RegistrationConstants.CLIENT_CHOICE);
                UIFlowController.ChangeMenu(MenuState.MainMenu);
                break;
            
            case RETURN_INT:
                UIFlowController.ChangeMenu(MenuState.MainMenu);
                break;
            
            default:
                DisplayIO.InvalidChoice();
                break;
        }
    }
}