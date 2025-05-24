using System;
using Entities;
using UI;

namespace UIComponents;

/// <summary>
/// Contains various static methods for handling I/O specific to <see cref="Client"/> users.
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
    /// Validates whether a <see cref="Customer"/>'s <see cref="CustomerOrder"/> has been marked
    /// as <see cref="OrderStatus.Ordered"/>.
    /// </summary>
    /// <param name="orderStatus"> The status of the order to validate. </param>
    /// <returns> <c>true</c> if the status is marked as <see cref="OrderStatus.Ordered"/>,
    /// otherwise, <c>false</c>. </returns>
    public static bool IsOrdered(OrderStatus orderStatus)
    {
        if (orderStatus == OrderStatus.Ordered) return true;
        else return false;
    }

    /// <summary>
    /// Validates whether a <see cref="Customer"/>'s <see cref="CustomerOrder"/> has been marked
    /// as <see cref="OrderStatus.Cooking"/>.
    /// </summary>
    /// <param name="orderStatus"> The status of the order to validate. </param>
    /// <returns> <c>true</c> if the status is marked as <see cref="OrderStatus.Cooking"/>,
    /// otherwise, <c>false</c>. </returns>
    public static bool IsCooking(OrderStatus orderStatus)
    {
        if (orderStatus == OrderStatus.Cooking) return true;
        else return false;
    }

    /// <summary>
    /// Validates whether a <see cref="Customer"/>'s <see cref="CustomerOrder"/> has been marked
    /// as <see cref="OrderStatus.Cooked"/>.
    /// </summary>
    /// <param name="orderStatus"> The status of the order to validate. </param>
    /// <returns> <c>true</c> if the status is marked as <see cref="OrderStatus.Cooked"/>,
    /// otherwise, <c>false</c>. </returns>
    public static bool IsCooked(OrderStatus orderStatus)
    {
        if (orderStatus == OrderStatus.Cooked) return true;
        else return false;
    }

    /// <summary>
    /// Validates whether a <see cref="Customer"/>'s <see cref="CustomerOrder"/> has been marked
    /// as <see cref="OrderStatus.Delivered"/>.
    /// </summary>
    /// <param name="orderStatus"> The status of the order to validate. </param>
    /// <returns> <c>true</c> if the status is marked as <see cref="OrderStatus.Delivered"/>,
    /// otherwise, <c>false</c>. </returns>
    public static bool IsDelivered(OrderStatus orderStatus)
    {
        if (orderStatus == OrderStatus.Delivered) return true;
        else return false;
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
            if (IsOrdered(order.OrderStatus)) return true;
        }
        return false;
    }

    /// <summary>
    /// Validates whether a list of <see cref="CustomerOrder"/>s contains any orders
    /// with the order status of <see cref="OrderStatus.Cooking"/>.
    /// </summary>
    /// <param name="customerOrders"> The list of customer orders to validate. </param>
    /// <returns> <c>true</c> if any of the orders are marked as cooking, otherwise,
    /// <c>false</c>. </returns>
    public static bool ContainsCooking(List<CustomerOrder> customerOrders)
    {
        foreach (CustomerOrder order in customerOrders)
        {
            if (IsCooking(order.OrderStatus)) return true;
        }
        return false;
    }

    // ? Make these overloads?
    /// <summary>
    /// Iterates through the given list of <see cref="CustomerOrder"/>s,
    /// finding any orders marked as <see cref="OrderStatus.Ordered"/>, then
    /// displays them dynamically with an indexed number, the order number,
    /// and <see cref="Customer"/>'s name. Then returns the final value of the index.
    /// </summary>
    /// <param name="customerOrders"> The list of <see cref="CustomerOrder"/>s to iterate through. </param>
    /// <returns> The final value of the index. </returns>
    public static int DisplayOrdersReadyToCook(List<CustomerOrder> customerOrders)
    {
        int choiceIndex = 1;

        foreach (CustomerOrder order in customerOrders)
        {
            if (IsOrdered(order.OrderStatus))
            {
                IODisplay.DisplayMessage(String.Format(ClientConstants.DISPLAY_ORDER_STR, choiceIndex,
                    order.OrderNumber, order.Customer.Name));
                choiceIndex++;
            }
        }
        return choiceIndex;
    }

    /// <summary>
    /// Iterates through the given list of <see cref="CustomerOrder"/>s,
    /// finding any orders marked as <see cref="OrderStatus.Cooking"/>, then
    /// displays them dynamically with an indexed number, the order number,
    /// and <see cref="Customer"/>'s name. Then returns the final value of the index.
    /// </summary>
    /// <param name="customerOrders"> The list of <see cref="CustomerOrder"/>s to iterate through. </param>
    /// <returns> The final value of the index. </returns>
    public static int DisplayOrdersReadyToFinishCooking(List<CustomerOrder> customerOrders)
    {
        int choiceIndex = 1;

        foreach (CustomerOrder order in customerOrders)
        {
            if (IsCooking(order.OrderStatus))
            {
                IODisplay.DisplayMessage(String.Format(ClientConstants.DISPLAY_ORDER_STR, choiceIndex,
                    order.OrderNumber, order.Customer.Name));
                choiceIndex++;
            }
        }
        return choiceIndex;
    }

    /// <summary>
    /// Iterates through the given list of <see cref="CustomerOrder"/>s,
    /// finding any orders marked as <see cref="OrderStatus.Ordered"/>, 
    /// <see cref="OrderStatus.Cooking"/>, or <see cref="OrderStatus.Cooked"/>, and
    /// the <see cref="Deliverer"/> has arrived, then displays them dynamically with 
    /// an indexed number, the order number, the <see cref="Customer"/>'s name, 
    /// the <see cref="Deliverer"/>'s licence plate, and the <see cref="OrderStatus"/>. 
    /// Then returns the final value of the index.
    /// </summary>
    /// <param name="customerOrders"> The list of <see cref="CustomerOrder"/>s to iterate through. </param>
    /// <returns> The final value of the index. </returns>
    public static int DisplayAllActiveOrders(List<CustomerOrder> customerOrders)
    {
        int choiceIndex = 1;
        // bool trueValue = true;
        foreach (CustomerOrder order in customerOrders)
        {
            // if ((IsOrdered(order.OrderStatus) || IsCooking(order.OrderStatus) ||
            //     IsCooked(order.OrderStatus)) && order.DelivererArrivedAtRestaurant)
            // if (trueValue)
            if ((IsOrdered(order.OrderStatus) || IsCooking(order.OrderStatus) || IsCooked(order.OrderStatus)) && order.DelivererArrivedAtRestaurant == true)
            {
                IODisplay.DisplayMessage(String.Format(ClientConstants.ORDER_DETAILS_STR, choiceIndex,
                    order.OrderNumber, order.Customer.Name, order.Deliverer!.LicencePlate, order.OrderStatus));
                choiceIndex++;
            }
        }
        return choiceIndex;
    }
}
