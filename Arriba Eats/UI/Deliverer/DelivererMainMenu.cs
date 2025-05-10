using System;
using DisplayIO;
using UINavigation;

namespace UI;

public class DelivererMainMenu : IMenu
{
    public void DisplayMenu()
    {
        IODisplay.DisplayMessage("Welcome to the deliverer menu!");
    }    
}
