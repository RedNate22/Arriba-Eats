using System;
using DisplayIO;
using UINavigation;
using Entities;

namespace UI;

/// <summary>
/// Represents the login menu.
/// <para> Allows users to login by providing their <see cref="User.Email"/>
/// and <see cref="User.Password"/>, using methods defined in <see cref="SessionManager"/>.</para>
/// </summary>
public class LoginMenu : IMenu
{
    /// <summary>
    /// Displays the login menu to authenticate the user.
    /// <para> - Prompts the user for their <see cref="User.Email"/> and <see cref="User.Password"/>. </para>
    /// <para> - Authenticates the session using <see cref="SessionManager.AuthenticateSession()"/>,
    /// updating <see cref="SessionManager.CurrentUser"/>. </para>
    /// <para> - Determines the user's <see cref="UserType"/> via <see cref="SessionManager.ReturnUserType()"/>. </para>
    /// <para> - Transitions to the appropriate menu based on the user's <see cref="UserType"/>. </para>
    /// </summary>
    public void DisplayMenu()
    {
        User? currentUser = SessionManager.Login();

        if (currentUser == null)
        {
            UIFlowController.ChangeMenu("MainMenu");
        }

        else
        {
            SessionManager.AuthenticateSession(currentUser);
            UserType userType = SessionManager.ReturnUserType();

            switch (userType)
            {
                case UserType.Customer:
                    UIFlowController.ChangeMenu("CustomerMainMenu");
                    break;
                case UserType.Deliverer:
                    UIFlowController.ChangeMenu("DelivererMainMenu");
                    break;
                case UserType.Client:
                    UIFlowController.ChangeMenu("CustomerMainMenu");
                    break;
                default:
                    UIFlowController.ChangeMenu("MainMenu");
                    break;
            }
        }
    }
}
