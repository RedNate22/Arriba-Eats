using System;
using Entities;
using UINavigation;
using UI;

namespace UIComponents;

/// <summary>
/// Contains various static methods for I/O associated with <see cref="CustomerOrder"/>s.
/// </summary>
public class OrderIO
{
    /// <summary>
    /// Updates the <see cref="OrderStatus"/> of the given <see cref="CustomerOrder"/>
    /// to the next stage.
    /// </summary>
    /// <param name="customerOrder"> The <see cref="CustomerOrder"/> to update the status of. </param>
    /// <returns> <c>true</c> if the status is updated successfully, otherwise, <c>false</c>. </returns>
    public static bool UpdateOrder(CustomerOrder customerOrder)
    {
        if (customerOrder is CustomerOrder)
        {
            customerOrder.UpdateOrderStatus();
            return true;
        }
        else return false;
    }

    /// <summary>
    /// Retrieves <see cref="CustomerOrder"/>s based on the current <see cref="User"/>'s
    /// <see cref="UserType"/>.
    /// </summary>
    /// <returns> A list of <see cref="CustomerOrder"/>s associated with the current 
    /// <see cref="User"/>. If no orders exist, returns an empty list. </returns>
    public static List<CustomerOrder> GetCustomerOrders()
    {
        List<CustomerOrder> customerOrders = new List<CustomerOrder>();

        User user = SessionManager.ReturnCurrentUser();
        UserType userType = SessionManager.ReturnUserType();
        if (userType == UserType.Customer)
        {
            if (OrderRegistry.TryGetOrders(out List<CustomerOrder> foundCustomerOrders, (Customer)user))
            {
                customerOrders = foundCustomerOrders;
                return customerOrders;
            }
            else return customerOrders;
        }

        else if (userType == UserType.Client)
        {
            if (OrderRegistry.TryGetOrders(out List<CustomerOrder> foundCustomerOrders, (Client)user))
            {
                customerOrders = foundCustomerOrders;
                return customerOrders;
            }
            else return customerOrders;
        }

        else if (userType == UserType.Deliverer)
        {
            if (OrderRegistry.TryGetOrders(out List<CustomerOrder> foundCustomerOrders, (Deliverer)user))
            {
                customerOrders = foundCustomerOrders;
                return customerOrders;
            }
            else return customerOrders;
        }

        else return customerOrders;
    }

    /// <summary>
    /// Initiates the order placement process for a <see cref="Customer"/> at the currently selected <see cref="Restaurant"/>.
    /// </summary>
    /// <remarks>
    /// Displays the <see cref="Restaurant"/>'s menu and allows the <see cref="Customer"/> to select items, view the current order total, 
    /// and either confirm or cancel the order.
    /// The method loops until the user completes or cancels their order.
    /// Upon completion, a new order number is returned.
    /// </remarks>
    /// <param name="orderNumber"> The unique number assigned to the customer's order. </param>
    /// <returns>
    /// The updated order number if the order is successfully placed.
    /// If the order is canceled or unsuccessful, the original order number is returned.
    /// </returns>
    public static int GetOrderFromCustomer(int orderNumber)
    {
        if (SessionManager.SelectedRestaurant == null) return orderNumber;  // No restaurant selected
        Restaurant selectedRestaurant = SessionManager.SelectedRestaurant;

        decimal currentOrderTotalDec = 0.00M;
        if (selectedRestaurant.TryGetMenu(out List<decimal> restaurantMenuPrices, out List<string> restaurantMenuItems))
        {
            CustomerOrder customerOrder = new CustomerOrder
                ((Customer)SessionManager.ReturnCurrentUser(), orderNumber, selectedRestaurant);  // * Begin order

            string enterChoiceStr = DisplayIO.EnterChoiceStr(restaurantMenuItems.Count + 2);  // Adjust for confirm/cancel options

            while (true)
            {
                DisplayIO.DisplayMessage(string.Format("Current order total: ${0:F2}", currentOrderTotalDec));

                int choiceIndex = 1;  // The displayed number for the option
                int menuIndex = 0;
                foreach (string item in restaurantMenuItems)
                {
                    DisplayIO.DisplayMessage($"{choiceIndex}:   ${restaurantMenuPrices[menuIndex]}  {restaurantMenuItems[menuIndex]}");
                    choiceIndex++;
                    menuIndex++;
                }

                // * choiceIndex will always be the last menu item's index + 1
                // * e.g. if "3: Item" is the last item on the menu, then "Complete Order" will be 4, "4: Complete Order"
                int completeOrder = choiceIndex;
                int cancelOrder = choiceIndex + 1;

                DisplayIO.DisplayMessage($"{completeOrder}: Complete order");
                DisplayIO.DisplayMessage($"{cancelOrder}: Cancel order");
                DisplayIO.DisplayMessage(enterChoiceStr);

                int choice = DisplayIO.GetChoice();
                if (choice == completeOrder)
                {
                    CompleteOrder(customerOrder, orderNumber, out int newOrderNumber);
                    return newOrderNumber;
                }

                else if (choice == cancelOrder)
                {
                    UIFlowController.ChangeMenu(MenuState.CustomerBrowseRestaurantsMenu);
                    return orderNumber;  // * Return original order number, to be reused until a confirmed order
                }

                else
                {
                    AddItemToOrder(customerOrder, currentOrderTotalDec, choice, restaurantMenuPrices,
                        restaurantMenuItems, out decimal newOrderTotalDec);
                    currentOrderTotalDec = newOrderTotalDec;
                }
            }
        }
        else
        {
            DisplayIO.DisplayMessage(String.Format("{0} currently has no items on the menu.", selectedRestaurant.RestaurantName));
            return orderNumber;
        }
    }

    /// <summary>
    /// Attempts to finalise a <see cref="Customer"/>'s order by adding it to the <see cref="OrderRegistry"/>.
    /// </summary>
    /// <remarks>
    /// If the order contains no items, an error message is displayed, and the process is halted.
    /// If the order is successfully added, its status is updated to <see cref="OrderStatus.Ordered"/> and a new order 
    /// number is assigned.
    /// </remarks>
    /// <param name="customerOrder"> The <see cref="Customer"/>'s order to be finalised. </param>
    /// <param name="orderNumber"> The current order number before confirmation. </param>
    /// <param name="newOrderNumber">
    /// <c>Out</c> parameter that will contain the updated order number if the order is successfully placed.
    /// If unsuccessful, the original order number is retained.
    /// </param>
    private static void CompleteOrder(CustomerOrder customerOrder, int orderNumber, out int newOrderNumber)
    {
        if (customerOrder.IsOrderEmpty())
        {
            DisplayIO.DisplayMessage("You have not added any items.");
            newOrderNumber = orderNumber;
        }
        else
        {
            if (OrderRegistry.TryAddOrder(customerOrder) && UpdateOrder(customerOrder))  // * Updates status to 'Ordered'
            {
                DisplayIO.DisplayMessage(String.Format("Your order has been placed. Your order number is #{0}.", orderNumber));

                orderNumber++;  // * Update order number for future (next) orders
                newOrderNumber = orderNumber;
            }

            else
            {
                DisplayIO.DisplayMessage("Order could not be confirmed.");
                newOrderNumber = orderNumber;
            }
        }
    }

    /// <summary>
    /// Adds a selected menu item to the customer's order.
    /// </summary>
    /// <remarks>
    /// Displays the selected item, prompts the user for a quantity, and updates the order total.
    /// The method loops until a valid quantity is entered.
    /// </remarks>
    /// <param name="customerOrder"> The <see cref="Customer"/>'s order to which the item is being added. </param>
    /// <param name="currentOrderTotalDec"> The current total price of the order before adding the item. </param>
    /// <param name="choice"> The menu item selection index, corresponding to the <see cref="Customer"/>'s choice. </param>
    /// <param name="restaurantMenuPrices"> A list of item prices for the <see cref="Restaurant"/>'s menu. </param>
    /// <param name="restaurantMenuItems"> A list of item names for the <see cref="Restaurant"/>'s menu. </param>
    /// <param name="newOrderTotalDec">
    /// <c> Out</c> parameter that will contain the updated order total after the item is added.
    /// </param>
    private static void AddItemToOrder(CustomerOrder customerOrder, decimal currentOrderTotalDec, int choice,
        List<decimal> restaurantMenuPrices, List<string> restaurantMenuItems, out decimal newOrderTotalDec)
    {
        // * Adjust for index-based referencing
        decimal selectedMenuItemPrice = restaurantMenuPrices[choice - 1];
        string selectedMenuItemName = restaurantMenuItems[choice - 1];

        while (choice != 0)
        {
            DisplayIO.DisplayMessage($"Adding {selectedMenuItemName} to order.");
            DisplayIO.DisplayMessage("Please enter quantity (0 to cancel):");
            choice = GetItemQuantity();

            if (choice > 0)
            {
                customerOrder.AddItemToOrder(selectedMenuItemName, choice, selectedMenuItemPrice);
                currentOrderTotalDec += choice * selectedMenuItemPrice;  // * Update cart total
                newOrderTotalDec = currentOrderTotalDec;
                DisplayIO.DisplayMessage($"Added {choice} x {selectedMenuItemName} to order.");
                break;
            }

            else if (choice != 0)
            {
                DisplayIO.DisplayMessage(CustomerConstants.INVALID_QUANTITY_STR);
            }
        }
        newOrderTotalDec = currentOrderTotalDec;
    }

    /// <summary>
    /// Gets the quantity of the currently selected menu item from the <see cref="Customer"/>.
    /// </summary>
    /// <remarks>
    /// This method is intended to be called by <see cref="GetOrderFromCustomer"/>.
    /// If conversion fails, <c>-1</c> is returned to indicate invalid input.
    /// </remarks>
    /// <returns>
    /// The valid integer quantity entered by the user, or <c>-1</c> if the input is invalid.
    /// </returns>
    private static int GetItemQuantity()
    {
        string? quantity = DisplayIO.ReadInput();

        if (int.TryParse(quantity, out int result)) return result;
        else
        {
            return -1;
        }
    }

    /// <summary>
    /// Gets the list of registered <see cref="CustomerOrder"/>s ready to be assigned to a <see cref="Deliverer"/>, 
    /// and then displays the orers and their details under their respective headings. 
    /// <para> As the order list is suspectible to dynamically changing whenever a new <see cref="CustomerOrder"/>
    /// is registered, the index number for the listed options is therefore dynamic, 
    /// and is assigned in the <c>out</c> parameter to be referenced. </para>
    /// </summary>
    /// <param name="choiceIndex"> The index number for the listed options. </param>
    /// <returns> The list of currently registered <see cref="CustomerOrder"/>s ready to be
    /// assigned a <see cref="Deliverer"/>. </returns>
    public static List<CustomerOrder> DisplayOrdersList(out int choiceIndex)
    {
        List<CustomerOrder> customerOrdersList = GetCustomerOrders();

        int orderColumnWidth = DelivererConstants.ORDER_HEADING_STR.Length + 2;
        int restaurantColumnWidth = DelivererConstants.RESTAURANT_NAME_HEADING_STR.Length + 7;
        int locationColumnWidth = DelivererConstants.LOC_HEADING_STR.Length + 4;
        int customerColumnWidth = DelivererConstants.CUSTOMER_NAME_HEADING_STR.Length + 4;

        // Dynamically increase width of restaurant name column
        foreach (CustomerOrder order in customerOrdersList)
        {
            if (order.Restaurant.RestaurantName.Length > restaurantColumnWidth)
            {
                restaurantColumnWidth = order.Restaurant.RestaurantName.Length + 1;
            }
        }

        // Display the headings
        DisplayIO.DisplayMessage("   "
            + DelivererConstants.ORDER_HEADING_STR.PadRight(orderColumnWidth)
            + DelivererConstants.RESTAURANT_NAME_HEADING_STR.PadRight(restaurantColumnWidth)
            + DelivererConstants.LOC_HEADING_STR.PadRight(locationColumnWidth)
            + DelivererConstants.CUSTOMER_NAME_HEADING_STR.PadRight(customerColumnWidth)
            + DelivererConstants.LOC_HEADING_STR.PadRight(locationColumnWidth)
            + DelivererConstants.DISTANCE_HEADING_STR);

        // Display the currently active orders
        int orderChoiceIndex = 1;
        for (int i = 0; i < customerOrdersList.Count(); i++)
        {
            // * Calculate total taxi cab distance for current deliverer
            int totalDistance = UserIO.CalculateTotalTaxiCabDistance(customerOrdersList[i].Restaurant,
                customerOrdersList[i].Customer);
            customerOrdersList[i].UpdateTotalDistance(totalDistance);

            DisplayIO.DisplayMessage($"{orderChoiceIndex}: "
                + $"{customerOrdersList[i].OrderNumber}".PadRight(orderColumnWidth)
                + $"{customerOrdersList[i].Restaurant.RestaurantName}".PadRight(restaurantColumnWidth)
                + $"{customerOrdersList[i].Restaurant.Location}".PadRight(locationColumnWidth)
                + $"{customerOrdersList[i].Customer.Name}".PadRight(customerColumnWidth)
                + $"{customerOrdersList[i].Customer.Location}".PadRight(locationColumnWidth)
                + $"{customerOrdersList[i].TotalDistance}");
            orderChoiceIndex++;
        }
        choiceIndex = orderChoiceIndex;
        return customerOrdersList;
    }

    /// <summary>
    /// Attempts to find the current <see cref="CustomerOrder"/> assigned to the
    /// currently authenticated <see cref="Deliverer"/>, then assigns this to the <c>out</c> parameter.
    /// </summary>
    /// <param name="customerOrder"> The found <see cref="CustomerOrder"/> assigned to the <see cref="Deliverer"/>. </param>
    /// <returns> <c>true</c> if the order is found and assigned, otherwise, <c>false</c>. </returns>
    public static bool FindCurrentOrder(out CustomerOrder customerOrder)
    {
        Deliverer deliverer = SessionManager.ReturnCurrentDeliverer();
        if (OrderRegistry.TryGetCurrentlyAssignedOrder(deliverer, out CustomerOrder foundOrder))
        {
            customerOrder = foundOrder;
            return true;
        }
        customerOrder = null!;
        return false;
    }

    /// <summary>
    /// Gets the currently authenticated <see cref="Deliverer"/>, then
    /// determines whether they have already arrived at the <see cref="Restaurant"/>
    /// for their currently assigned order.
    /// </summary>
    /// <returns> <c>true</c> if the <see cref="Deliverer"/> has already arrived at
    /// the <see cref="Restaurant"/>, otherwise, <c>false</c>. </returns>
    public static bool DelivererArrivedAlready()
    {
        Deliverer deliverer = SessionManager.ReturnCurrentDeliverer();
        return OrderRegistry.TryFindDelivererAlreadyArrived(deliverer);
    }

    /// <summary>
    /// Gets the currently authenticated <see cref="Deliverer"/>, then
    /// determines whether they are already in the process of delivering
    /// an active order.
    /// </summary>
    /// <returns> <c>true</c> if the <see cref="Deliverer"/> is currently
    /// delivering an active order, otherwise, <c>false</c>. </returns>
    public static bool DelivererAlreadyPickedUpOrder()
    {
        Deliverer deliverer = SessionManager.ReturnCurrentDeliverer();
        return OrderRegistry.TryFindPickedUpOrder(deliverer);
    }

    /// <summary>
    /// Validates whether a <see cref="Customer"/>'s <see cref="CustomerOrder"/> has been marked
    /// as <see cref="OrderStatus.Ordered"/> or <see cref="OrderStatus.Cooking"/>.
    /// </summary>
    /// <param name="orderStatus"> The status of the order to validate. </param>
    /// <returns> <c>true</c> if the status is marked as <see cref="OrderStatus.Ordered"/>
    /// or <see cref="OrderStatus.Cooking"/>, otherwise, <c>false</c>. </returns>
    public static bool IsBeingPrepared(OrderStatus orderStatus)
    {
        if (orderStatus == OrderStatus.Ordered || orderStatus == OrderStatus.Cooking) return true;
        else return false;
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
    /// as <see cref="OrderStatus.BeingDelivered"/>.
    /// </summary>
    /// <param name="orderStatus"> The status of the order to validate. </param>
    /// <returns> <c>true</c> if the status is marked as <see cref="OrderStatus.BeingDelivered"/>,
    /// otherwise, <c>false</c>. </returns>
    public static bool IsBeingDelivered(OrderStatus orderStatus)
    {
        if (orderStatus == OrderStatus.BeingDelivered) return true;
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
    /// Iterates through the given list of <see cref="CustomerOrder"/>s,
    /// finding any orders marked as <see cref="OrderStatus.Ordered"/>, then
    /// displays them dynamically with an indexed number, the order number,
    /// and <see cref="Customer"/>'s name. Then returns the final value of the index.
    /// </summary>
    /// <param name="customerOrders"> The list of <see cref="CustomerOrder"/>s to iterate through. </param>
    /// <param name="ordersToCook"> The list of <see cref="CustomerOrder"/>s marked as <see cref="OrderStatus.Ordered"/>. </param>
    /// <returns> The final value of the index. </returns>
    public static int DisplayOrdersReadyToCook(List<CustomerOrder> customerOrders, out List<dynamic> ordersToCook)
    {
        ordersToCook = new List<dynamic> ();

        int choiceIndex = 1;

        foreach (CustomerOrder order in customerOrders)
        {
            if (IsOrdered(order.OrderStatus))
            {
                DisplayIO.DisplayMessage(String.Format(ClientConstants.DISPLAY_ORDER_STR, choiceIndex,
                    order.OrderNumber, order.Customer.Name));

                ordersToCook.Add(order);
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
    /// <param name="ordersToFinish"> The list of <see cref="CustomerOrder"/>s marked as <see cref="OrderStatus.Cooking"/>. </param>
    /// <returns> The final value of the index. </returns>
    public static int DisplayOrdersReadyToFinishCooking(List<CustomerOrder> customerOrders, out List<dynamic> ordersToFinish)
    {
        ordersToFinish = new List<dynamic>();
        int choiceIndex = 1;

        foreach (CustomerOrder order in customerOrders)
        {
            if (IsCooking(order.OrderStatus))
            {
                DisplayIO.DisplayMessage(String.Format(ClientConstants.DISPLAY_ORDER_STR, choiceIndex,
                    order.OrderNumber, order.Customer.Name));

                ordersToFinish.Add(order);
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
    /// <param name="ordersForCollection"> The list of <see cref="CustomerOrder"/>s marked as <see cref="OrderStatus.Ordered"/>,
    /// <see cref="OrderStatus.Cooking"/>, and <see cref="OrderStatus.Cooked"/>.</param>
    /// <returns> The final value of the index. </returns>
    public static int DisplayOrdersReadyForCollection(List<CustomerOrder> customerOrders, out List<dynamic> ordersForCollection)
    {
        ordersForCollection = new List<dynamic>();
        int choiceIndex = 1;
        
        foreach (CustomerOrder order in customerOrders)
        {
            if (order.Deliverer != null)
            {
                if ((IsOrdered(order.OrderStatus) || IsCooking(order.OrderStatus) || IsCooked(order.OrderStatus)) && order.DelivererArrivedAtRestaurant == true)
                {
                    DisplayIO.DisplayMessage(String.Format(ClientConstants.ORDER_DETAILS_STR, choiceIndex,
                        order.OrderNumber, order.Customer.Name, order.Deliverer!.LicencePlate, order.OrderStatus));

                    ordersForCollection.Add(order);
                    choiceIndex++;
                }
            }
        }
        return choiceIndex;
    }
}