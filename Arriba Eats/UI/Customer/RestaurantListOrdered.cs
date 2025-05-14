using System;
using UIComponents;
using UINavigation;

namespace UI;

// TODO xml
public class RestaurantListOrdered : IMenu
{
    private string _returnPreviousMenu = IOUtilities.ReturnToPreviousMenuStr(5);
    private string _enterChoice = IOUtilities.EnterChoiceStr(5);
    private const int SORTED_ALPHABETICALLY_INT = 1, SORTED_DISTANCE_INT = 2, SORTED_STYLE_INT = 3,
        SORTED_AVERAGE_RATING_INT = 4, RETURN_PREVIOUS_MENU_INT = 5;

    // TODO xml
    public void DisplayMenu()
    {
        IODisplay.DisplayMessage(MenuConstants.RESTAURANT_LIST_ORDERED_STR);
        IODisplay.DisplayMessage(_returnPreviousMenu);
        IODisplay.DisplayMessage(_enterChoice);
        
        int choice = IODisplay.GetChoice();

        switch (choice)
        {
            case SORTED_ALPHABETICALLY_INT:
                // method
                break;
            
            case SORTED_DISTANCE_INT:
                // method
                break;

            case SORTED_STYLE_INT:
                // method
                break;
                
            case SORTED_AVERAGE_RATING_INT:
                // method
                break;

            case RETURN_PREVIOUS_MENU_INT:
                UIFlowController.ChangeMenu("CustomerMainMenu");
                break;

            default:
                IODisplay.DisplayMessage(MenuConstants.INVALID_CHOICE_STR);
                break;
        }
    }    
}
