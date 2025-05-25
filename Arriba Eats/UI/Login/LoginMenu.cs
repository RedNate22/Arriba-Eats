using System;
using UIComponents;
using UINavigation;

namespace UI;

/// <summary>
/// Represents the login menu.
/// <para> Allows users to login by providing their <see cref="User.Email"/>
/// and <see cref="User.Password"/>, using methods defined in <see cref="SessionManager"/>.</para>
/// </summary>
public class LoginMenu : IMenu
{
    /// <summary>
    /// Displays the <see cref="LoginMenu"/> to authenticate the <see cref="User"/>.
    /// <para> - Prompts the user for their <see cref="User.Email"/> and <see cref="User.Password"/>. </para>
    /// <para> - Authenticates the session using <see cref="SessionManager.AuthenticateSession()"/>,
    /// updating the tracked user to the current one. </para>
    /// <para> - Determines the user's <see cref="UserType"/> via <see cref="SessionManager.ReturnUserType()"/>. </para>
    /// <para> - Transitions to the appropriate menu based on the user's <see cref="UserType"/>. </para>
    /// </summary>
    public void DisplayMenu()
    {
        var currentUser = SessionManager.Login();

        if (currentUser == null)
        {
            UIFlowController.ChangeMenu(MenuState.MainMenu);
        }

        else
        {
            SessionManager.AuthenticateSession(currentUser);
            UserType userType = SessionManager.ReturnUserType();

            switch (userType)
            {
                case UserType.Customer:
                    UIFlowController.ChangeMenu(MenuState.CustomerMainMenu);
                    break;
                    
                case UserType.Deliverer:
                    UIFlowController.ChangeMenu(MenuState.DelivererMainMenu);
                    break;
                
                case UserType.Client:
                    UIFlowController.ChangeMenu(MenuState.ClientMainMenu);
                    break;
                
                default:
                    DisplayIO.InvalidChoice();
                    break;
            }
        }
    }
}
