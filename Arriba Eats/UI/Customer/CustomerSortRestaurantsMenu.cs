using System;
using UIComponents;
using UINavigation;

namespace UI;

/// <summary>
/// Represents the menu where a <see cref="Entities.Customer"/> chooses
/// how the displayed restaurants shall be ordered.
/// </summary>
public class CustomerSortRestaurantsMenu: IMenu
{
    private string _returnPreviousMenu = IOUtilities.ReturnToPreviousMenuStr(5);
    private string _enterChoice = IOUtilities.EnterChoiceStr(5);

    /// <summary>
    /// Gets the <see cref="UIComponents.SortOption"/> from the <see cref="Entities.Customer"/>.
    /// </summary>
    public static SortOption SortOption { get; private set; }

    /// <summary>
    /// Defines the <see cref="int"/> constants representing menu options for use in a
    /// <see cref="switch"/> statement.
    /// </summary>
    private const int SORTED_ALPHABETICALLY_INT = 1, SORTED_DISTANCE_INT = 2, SORTED_STYLE_INT = 3,
        SORTED_AVERAGE_RATING_INT = 4, RETURN_PREVIOUS_MENU_INT = 5;

    /// <summary>
    /// Displays the <see cref="CustomerSortRestaurantsMenu"/> options and prompts the <see cref="Entities.Customer"/>
    /// to choose an option.
    /// </summary>
    public void DisplayMenu()
    {
        IODisplay.DisplayMessage(CustomerConstants.RESTAURANT_LIST_ORDERED_STR);
        IODisplay.DisplayMessage(_returnPreviousMenu);
        IODisplay.DisplayMessage(_enterChoice);
        
        int choice = IODisplay.GetChoice();

        switch (choice)
        {
            case SORTED_ALPHABETICALLY_INT:
                SortOption = SortOption.Alphabetically;
                UIFlowController.ChangeMenu(MenuState.CustomerBrowseRestaurantsMenu);
                break;
            
            case SORTED_DISTANCE_INT:
                SortOption = SortOption.ByDistance; 
                UIFlowController.ChangeMenu(MenuState.CustomerBrowseRestaurantsMenu);
                break;

            case SORTED_STYLE_INT:
                SortOption = SortOption.ByStyle; 
                UIFlowController.ChangeMenu(MenuState.CustomerBrowseRestaurantsMenu);
                break;
                
            case SORTED_AVERAGE_RATING_INT:
                SortOption = SortOption.ByAverageRating; 
                UIFlowController.ChangeMenu(MenuState.CustomerBrowseRestaurantsMenu);
                break;

            case RETURN_PREVIOUS_MENU_INT:
                UIFlowController.ChangeMenu(MenuState.CustomerMainMenu);
                break;

            default:
                IODisplay.DisplayMessage(MenuConstants.INVALID_CHOICE_STR);
                break;
        }
    }    
}
