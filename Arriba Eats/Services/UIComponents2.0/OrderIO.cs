using System;
using Entities;
using UINavigation;
using UI;

namespace UIComponents;

// TODO xml
public class OrderIO
{
    // TODO xml
    // ? split this up? or condense?
    public static int GetOrderFromCustomer(int orderNumber)
    {
        if (SessionManager.SelectedRestaurant == null) return orderNumber;
        Restaurant selectedRestaurant = SessionManager.SelectedRestaurant;

        decimal currentOrderTotalDec = 0.00M;

        if (selectedRestaurant.TryGetMenu(out List<decimal> restaurantMenuPrices, out List<string> restaurantMenuItems))
        {
            CustomerOrder customerOrder = new CustomerOrder
                ((Customer)SessionManager.ReturnCurrentUser(), orderNumber, selectedRestaurant);  // * Begin order

            string enterChoiceStr = IOUtilities.EnterChoiceStr(restaurantMenuItems.Count + 2);  // Adjust for confirm/cancel options

            while (true)
            {
                DisplayIO.DisplayMessage(string.Format(CustomerConstants.CURRENT_ORDER_TOTAL_STR, currentOrderTotalDec));

                int choiceIndex = 1;
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
                    if (customerOrder.IsOrderEmpty()) DisplayIO.DisplayMessage("You have not added any items.");
                    else
                    {
                        if (OrderRegistry.TryAddOrder(customerOrder) && DisplayIO.UpdateOrder(customerOrder))  // * Updates status to 'Ordered'
                        {
                            DisplayIO.DisplayMessage(String.Format(CustomerConstants.ORDER_PLACED_STR, orderNumber));

                            orderNumber++;  // * Update order number for future (next) orders
                            return orderNumber;
                        }
                        else
                        {
                            DisplayIO.DisplayMessage(CustomerConstants.ORDER_NOT_CONFIRMED_STR);
                            return orderNumber;
                        }
                    }
                }

                else if (choice == cancelOrder)
                {
                    customerOrder = null!;  // Empties the cart
                    UIFlowController.ChangeMenu(MenuState.CustomerBrowseRestaurantsMenu);
                    return orderNumber;  // * Return original order number, to be reused until a confirmed order
                }

                else
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
                            DisplayIO.DisplayMessage($"Added {choice} x {selectedMenuItemName} to order.");
                            break;
                        }

                        else if (choice != 0)
                        {
                            DisplayIO.DisplayMessage(CustomerConstants.INVALID_QUANTITY_STR);
                        }
                    }
                }
            }
        }
        else
        {
            DisplayIO.DisplayMessage(String.Format(CustomerConstants.RESTAURANT_HAS_NO_MENU_STR, selectedRestaurant.RestaurantName));
            return orderNumber;
        }
    }
    
    /// <summary>
    /// To be called by <see cref="GetOrderFromCustomer"/>.
    /// Reads a string input from the user via the console.
    /// <para> Attempts to convert the input into an integer. </para>
    /// </summary>
    /// <returns> The valid integer quantity, otherwise <c>-1</c>. </returns>
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
        List<CustomerOrder> customerOrdersList = DisplayIO.GetCustomerOrders();

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

    // ? Make these overloads?
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
