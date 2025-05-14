using System;
using Entities;
using UINavigation;

namespace UIComponents;

/// <summary>
/// Contains various static methods for handling I/O with <see cref="Customer"/> users.
/// <para> Uses <see cref="IOUtilities"/> for input & output formatting and validation. </para>
/// </summary>
public static class CustomerIO
{
    // TODO xml
    // TODO complete functionality
    public static void DisplayRestaurantsToRate(User user)
    {
        if (SessionManager.CurrentUser != null)
        {
            int n = 2; // TODO int needs to dynamically change
            string returnPreviousMenu = IOUtilities.ReturnToPreviousMenuStr(n);                  
            string enterChoice = IOUtilities.EnterChoiceStr(n);
            
            // TODO list restaurants ordered from
            IODisplay.DisplayMessage(returnPreviousMenu);
            IODisplay.DisplayMessage(enterChoice);
        }

        else IODisplay.DisplayMessage("No user is currently logged in.");  // ? turn this into a const?
    }
}
