using System;
using UIComponents;
using UINavigation;

namespace UI;

// TODO xml
public class ClientSeeCurrentOrdersMenu : IMenu
{
    // TODO xml
    public void DisplayMenu()
    {
        IODisplay.DisplayMessage(ClientConstants.RESTAURANT_HAS_NO_ORDERS_STR);
        UIFlowController.ChangeMenu(MenuState.ClientMainMenu);
    }
}
