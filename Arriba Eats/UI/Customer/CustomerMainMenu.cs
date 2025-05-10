using System;
using DisplayIO;
using UINavigation;

namespace UI;

public class CustomerMainMenu : IMenu
{
    public void DisplayMenu()
    {
        IODisplay.DisplayMessage("Welcome to the customer menu!");
    }
}
    
