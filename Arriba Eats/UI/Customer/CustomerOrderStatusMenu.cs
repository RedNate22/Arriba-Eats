using System;
using UIComponents;
using UINavigation;

namespace UI;

// TODO xml
public class CustomerOrderStatusMenu : IMenu
{

    // TODO xml
    public void DisplayMenu()
    {
        IODisplay.DisplayMessage(MenuConstants.NOT_PLACED_ORDERS_STR);
        UIFlowController.ChangeMenu(MenuState.CustomerMainMenu);
    }
}