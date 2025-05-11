using System;
using DisplayIO;
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
    private readonly string ENTER_CHOICE_STR = IOUtilities.EnterChoiceStr(4);

    private const int CUSTOMER_INT = 1, DELIVERER_INT = 2, CLIENT_INT = 3, RETURN_INT = 4;

    /// <summary>
    /// Displays the registration menu options and prompts the user to select a user type to register as.
    /// <para> The appropriate user type is registered depending on the user's choice. </para>
    /// <para> Uses <see cref="IODisplay.DisplayMessage()"/> to display the options. </para>
    /// <para> Uses <see cref="IODisplay.GetChoice()"/> to get a choice from the user in the form
    /// of an integer. </para>
    /// <para> Matches the given integer with the associated menu option and passes the correct
    /// argument to <see cref="RegistrationProcess.Register"/> to begin the registration
    /// process. 
    /// </para>
    /// </summary>
    public void DisplayMenu()
    {
        IODisplay.DisplayMessage(MenuConstants.USER_TYPE_STR);
        IODisplay.DisplayMessage(MenuConstants.CUSTOMER_STR);
        IODisplay.DisplayMessage(MenuConstants.DELIVERER_STR);
        IODisplay.DisplayMessage(MenuConstants.CLIENT_STR);
        IODisplay.DisplayMessage(MenuConstants.RETURN_STR);
        IODisplay.DisplayMessage(ENTER_CHOICE_STR);

        int option = IODisplay.GetChoice();

        switch (option)
        {
            case CUSTOMER_INT:
                IODisplay.DisplayEmptyLine();
                RegistrationProcess.Register(MenuConstants.CUSTOMER_CHOICE);
                UIFlowController.ChangeMenu("MainMenu");
                break;
            
            case DELIVERER_INT:
                IODisplay.DisplayEmptyLine();
                RegistrationProcess.Register(MenuConstants.DELIVERER_CHOICE);
                UIFlowController.ChangeMenu("MainMenu"); 
                break;
            
            case CLIENT_INT:
                IODisplay.DisplayEmptyLine();
                RegistrationProcess.Register(MenuConstants.CLIENT_CHOICE);
                UIFlowController.ChangeMenu("MainMenu");
                break;
            
            case RETURN_INT:
                IODisplay.DisplayEmptyLine();
                UIFlowController.ChangeMenu("MainMenu");
                break;
            
            default:
                IODisplay.DisplayMessage(MenuConstants.INVALID_CHOICE_STR);
                break;
        }
    }
}