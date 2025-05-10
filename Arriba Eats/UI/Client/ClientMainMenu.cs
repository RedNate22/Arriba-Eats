using System;
using DisplayIO;
using UINavigation;

namespace UI;

public class ClientMainMenu : IMenu
{  
    public void DisplayMenu()
    {
        IODisplay.DisplayMessage("Welcome to the client menu!");
    }
}
