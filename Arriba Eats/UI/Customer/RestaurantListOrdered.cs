using System;
using DisplayIO;
using UINavigation;

namespace UI;

public class RestaurantListOrdered : IMenu
{
    private string _enterChoice = IOUtilities.EnterChoiceStr(5);
    public void DisplayMenu()
    {
        IODisplay.DisplayMessage(MenuConstants.RESTAURANT_LIST_ORDERED_STR);
        IODisplay.DisplayMessage(_enterChoice);
    }    
}
