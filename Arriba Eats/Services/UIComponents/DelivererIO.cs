using System;
using Entities;
using UINavigation;
using UI;

namespace UIComponents;

/// <summary>
/// Contains various static methods for handling I/O specific to the <see cref="Deliverer"/> users.
/// <para> Uses <see cref="IOUtilities"/> for input and output formatting and validation. </para>
/// </summary>
public static class DelivererIO
{
    /// <summary>
    /// Gets the currently authenticated <see cref="Deliverer"/>, then
    /// determines whether they are already assigned to an active order.
    /// </summary>
    /// <returns> <c>true</c> if the <see cref="Deliverer"/> is assigned to
    /// an active order, otherwise, <c>false</c>. </returns>
    public static bool DelivererAlreadyAssignedToOrder()
    {
        Deliverer deliverer = SessionManager.ReturnCurrentDeliverer();
        return OrderRegistry.TryFindAlreadyAssignedOrder(deliverer);
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
        List<CustomerOrder> customerOrdersList = IODisplay.GetCustomerOrders();

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
        IODisplay.DisplayMessage("   "
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
            int totalDistance = CalculateTotalTaxiCabDistance(customerOrdersList[i].Restaurant,
                customerOrdersList[i].Customer);
            customerOrdersList[i].UpdateTotalDistance(totalDistance);

            IODisplay.DisplayMessage($"{orderChoiceIndex}: "
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
    /// Updates the current <see cref="Deliverer"/>'s location to the given one, using the 
    /// publicly accessible method, <see cref="User.UpdateLocation"/>.
    /// </summary>
    /// <param name="newLocation"> The new location to update to. </param>
    public static void DelivererChangeLocation(string newLocation)
    {
        User user = SessionManager.ReturnCurrentUser();
        user.UpdateLocation(newLocation);
    }

    /// <summary>
    /// Calculates the total taxicab distance for the currently authenticated <see cref="Deliverer"/>
    /// to travel for the order.
    /// <para> The total distance is the sum of the distance between the <see cref="Deliverer"/>
    /// and <see cref="Restaurant"/>, plus the distance between the <see cref="Restaurant"/>
    /// and the <see cref="Customer"/>. </para>
    /// </summary>
    /// <param name="restaurant"> The <see cref="Restaurant"/> where the order was placed. </param>
    /// <param name="customer"> The <see cref="Customer"/> who placed the order. </param>
    /// <returns> The total taxicab distance for the <see cref="Deliverer"/> to travel. </returns>
    public static int CalculateTotalTaxiCabDistance(Restaurant restaurant, Customer customer)
    {
        Deliverer deliverer = SessionManager.ReturnCurrentDeliverer();

        int delivererToRestaurantDist = IODisplay.GetDistance(deliverer, restaurant);
        int restaurantToCustomer = IODisplay.GetDistance(customer, restaurant);
        int totalDistance = delivererToRestaurantDist + restaurantToCustomer;

        return totalDistance;
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
}
