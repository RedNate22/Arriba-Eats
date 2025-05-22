using System;
using Entities;

namespace UIComponents;

/// <summary>
/// Contains various static methods for handling I/O with <see cref="Client"/> users.
/// Uses <see cref="IOUtilities"/> for input and output formatting and validation.
/// </summary>
public static class ClientIO
{
    /// <summary>
    /// To be called by <see cref="AddItemsToRestaurant()"/>, and continously
    /// reads a string input from the user via the console until it is successfully parsed
    /// into a decimal price. 
    /// <para> If the input is valid, parses the price to <see cref="IOUtilities.IsValidItemPrice()"/> 
    /// to verify if it is within the valid range. </para>
    /// </summary>
    /// <returns> The validated item price. This method loops until a valid input is provided. </returns>
    private static decimal GetMenuItemPrice()
    {
        while (true)
        {
            IODisplay.DisplayMessage("Please enter the price of the new item (without the $):");

            if (decimal.TryParse(IODisplay.ReadInput(), out decimal itemPriceInput))
            {
                decimal itemPrice = itemPriceInput;

                if (IOUtilities.IsValidItemPrice(itemPrice)) return itemPrice;
                else
                {
                    IODisplay.DisplayMessage("Invalid price.");
                    continue;
                }
            }
            else IODisplay.DisplayMessage("Invalid price.");
        }
    }
    
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
        var currentUser = SessionManager.ReturnCurrentUser();
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
    /// Validates whether a list of <see cref="CustomerOrder"/>s contains any orders
    /// with the order status of <see cref="OrderStatus.Ordered"/>.
    /// </summary>
    /// <param name="customerOrders"> The list of customer orders to validate. </param>
    /// <returns> <c>true</c> if any of the orders are marked as ordered, otherwise,
    /// <c>false</c>. </returns>
    public static bool ContainsOrdered(List<CustomerOrder> customerOrders)
    {
        foreach (CustomerOrder order in customerOrders)
        {
            if (IODisplay.IsOrdered(order.OrderStatus)) return true;
        }
        return false;
    }

    /// <summary>
    /// Iterates through the given list of <see cref="CustomerOrder"/>s
    /// and displays them dynamically with an indexed number, the order number,
    /// and customer's name. Then returns the final value of the index.
    /// </summary>
    /// <param name="customerOrders"> The list of <see cref="CustomerOrder"/>s to iterate through. </param>
    /// <returns> The final value of the index. </returns>
    public static int DisplayOrderedOrders(List<CustomerOrder> customerOrders)
    {
        int choiceIndex = 1;
        string displayOrdersStr = "{0}: Order #{1} for {2}";

        foreach (var order in customerOrders)
        {
            if (IODisplay.IsOrdered(order.OrderStatus))
            {
                IODisplay.DisplayMessage(String.Format(displayOrdersStr, choiceIndex,
                    order.OrderNumber, order.Customer.Name));
                choiceIndex++;
            }
        }
        return choiceIndex;
    }
}
