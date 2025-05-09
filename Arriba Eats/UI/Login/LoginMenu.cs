using System;
using DisplayIO;
using UINavigation;

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

        // TODO method(email, password) check if email and password match to an existing user, 
        // TODO then return that user here,
        // TODO Call a new method(user) to login - switch case, call appropriate menu
    }
}
