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
        IODisplay.DisplayMessage(CustomerConstants.SELECT_PREVIOUS_ORDER_TO_RATE_STR);
        //TODO CustomerIO.DisplayRestaurantsToRate(SessionManager.CurrentUser!);
               
        int choice = IODisplay.GetChoice();

        switch (choice)
        {
            case 1:
                UIFlowController.ChangeMenu(MenuState.CustomerMainMenu);
                break;

            default:  
                IODisplay.DisplayMessage(MenuConstants.INVALID_CHOICE_STR);
                break;
        }
    }
    
}
