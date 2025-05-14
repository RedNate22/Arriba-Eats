using System;
using UIComponents;
using UINavigation;

namespace UI;

// TODO xml
public class CustomerRateRestaurantMenu : IMenu
{
    // TODO xml
    public void DisplayMenu()
    {
        IODisplay.DisplayMessage(MenuConstants.SELECT_PREVIOUS_ORDER_TO_RATE_STR);
        CustomerIO.DisplayRestaurantsToRate(SessionManager.CurrentUser!);
               
        int choice = IODisplay.GetChoice();

        switch (choice)
        {
            case 1:
                UIFlowController.ChangeMenu(MenuState.CustomerMainMenu);
                break;
        }
    }
    
}
