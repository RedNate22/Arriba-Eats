using System;
using DisplayIO;
using UINavigation;
using Entities;

namespace UI;

// TODO XML
public class LoginMenu : IMenu
{

    // TODO XML
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
            UserType userType = SessionManager.ReturnUserType(currentUser);
            IODisplay.DisplayMessage("User type is: " + userType);

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
