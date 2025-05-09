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
        IODisplay.DisplayMessageSingleLine(MenuConstants.EMAIL_STR);
        string email = IODisplay.ReadInput();

        IODisplay.DisplayMessageSingleLine(MenuConstants.PASSWORD_STR);
        string password = IODisplay.ReadInput();

        User currentUser = IODisplay.Login(email, password);
        // TODO Call a new method(user) to login - switch case, call appropriate menu
        SessionManager.AuthenticateSession(currentUser);

        IODisplay.DisplayMessage($"You have logged in with {email}, {password}!");

        // ! Remove later
        UIFlowController.CurrentState = MenuState.MainMenu;
    }
}
