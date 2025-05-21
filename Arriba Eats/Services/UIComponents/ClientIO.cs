using System;
using Entities;

namespace UIComponents;

/// <summary>
/// Contains various static methods for handling I/O with <see cref="Client"/> users.
/// <para> Uses <see cref="IOUtilities"/> for input & output formatting and validation. </para>
/// </summary>
public static class ClientIO
{
    /// <summary>
    /// Allows the logged-in (<see cref="Client"/>) <see cref="User"/> to add a new item to their <see cref="Restaurant._restaurantMenu"/>.
    /// If the user owns a restaurant, displays the current menu and prompts for a new item.
    /// <para> If the user enters a non-blank input, this becomes the item name, and it then prompts for an 
    /// item price using <see cref="GetMenuItemPrice()"/>. </para>
    /// <para> 
    /// The item name and price are then registered into the <see cref="Restaurant._restaurantMenu"/>. 
    /// </para> 
    /// </summary>
    public static void AddItemsToRestaurant()
    {
        var currentUser = SessionManager.TryGetCurrentUser();
        if (currentUser != null)
        {
            if (RestaurantRegistry.TryFindClientsRestaurant(currentUser, out Restaurant? restaurant))
            {
                IODisplay.DisplayMessage("This is your restaurant's current menu:");
                restaurant?.DisplayCurrentlyRegisteredMenuItems();

                IODisplay.DisplayMessage("Please enter the name of the new item (blank to cancel):");
                
                string itemName = IODisplay.ReadInput();

                if (!string.IsNullOrWhiteSpace(itemName))
                {
                    decimal itemPrice = GetMenuItemPrice();
                    if (restaurant!.TryRegisterMenuItem(itemName, itemPrice))
                    {
                        IODisplay.DisplayMessage($"Successfully added {itemName} (${itemPrice:F2}) to menu.");
                    }
                    else IODisplay.DisplayMessage("This item is already added to the menu.");
                }
            }
            
            else IODisplay.DisplayMessage("You currently have no restaurants.");
        }

        else IODisplay.DisplayMessage("No user is currently logged in.");
    }

    /// <summary>
    /// To be called by <see cref="AddItemsToRestaurant()"/>, and continously
    /// reads a string input from the user via the console until it is successfully parsed
    /// into a decimal price. 
    /// <para> If the input is valid, parses the price to <see cref="IOUtilities.IsValidItemPrice()"/> 
    /// to verify if it is within the valid range. </para>
    /// </summary>
    /// <returns> The validated item price. This method loops until a valid input is provided. </returns>
    public static decimal GetMenuItemPrice()
    {
        while (true)
        {
            IODisplay.DisplayMessage("Please enter the price of the new item (without the $):");

            if (decimal.TryParse(IODisplay.ReadInput(), out decimal itemPriceInput))
            {
                decimal itemPrice = itemPriceInput;

                if (IOUtilities.IsValidItemPrice(itemPrice))
                {
                    return itemPrice;
                }

                else
                {
                    IODisplay.DisplayMessage("Invalid price.");
                    continue;
                }
            }

            else IODisplay.DisplayMessage("Invalid price.");
        }
    }
}
