using System;
using Entities;
using UINavigation;

namespace UIComponents;

// TODO xml
public static class CustomerIO
{
    // TODO xml
    // TODO complete functionality
    public static void DisplayRestaurantsToRate(User user)
    {
        if (SessionManager.CurrentUser != null)
        {
            int n = 2; // TODO int needs to dynamically change
            string _returnPreviousMenu = IOUtilities.ReturnToPreviousMenuStr(n);                  
            string _enterChoice = IOUtilities.EnterChoiceStr(n);
            
            // TODO list restaurants ordered from
            IODisplay.DisplayMessage(_returnPreviousMenu);
            IODisplay.DisplayMessage(_enterChoice);
        }

        else IODisplay.DisplayMessage("No user is currently logged in.");  // ? turn this into a const?
    }
}
