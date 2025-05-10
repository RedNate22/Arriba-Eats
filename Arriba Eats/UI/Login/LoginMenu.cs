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
            UIFlowController.CurrentState = MenuState.MainMenu;
        }

        else
        {
            SessionManager.AuthenticateSession(currentUser);
            UserType userType = SessionManager.GetUserType(currentUser);

            switch (userType)
            {
                case UserType.Customer:
                    UIFlowController.CurrentState = MenuState.CustomerMainMenu;
                    break;
                case UserType.Deliverer:
                    UIFlowController.CurrentState = MenuState.DelivererMainMenu;
                    break;
                case UserType.Client:
                    UIFlowController.CurrentState = MenuState.CustomerMainMenu;
                    break;
                default:
                    UIFlowController.CurrentState = MenuState.MainMenu;
                    return;
            }
        }
    }
}
