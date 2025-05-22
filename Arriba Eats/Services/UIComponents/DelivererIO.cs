using System;
using Entities;
using UINavigation;
using UI;

namespace UIComponents;

/// <summary>
/// Contains various static methods for handling I/O with <see cref="Deliverer"/> users.
/// <para> Uses <see cref="IOUtilities"/> for input and output formatting and validation. </para>
/// </summary>
public static class DelivererIO
{
    public static bool DelivererAlreadyAssignedToOrder()
    {
        Deliverer user = SessionManager.ReturnCurrentDeliverer();
        return OrderRegistry.TryFindAssignedOrder(user);
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
        var customerOrdersList = IODisplay.GetCustomerOrders();

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

        IODisplay.DisplayMessage("   "
            + DelivererConstants.ORDER_HEADING_STR.PadRight(orderColumnWidth)
            + DelivererConstants.RESTAURANT_NAME_HEADING_STR.PadRight(restaurantColumnWidth)
            + DelivererConstants.LOC_HEADING_STR.PadRight(locationColumnWidth)
            + DelivererConstants.CUSTOMER_NAME_HEADING_STR.PadRight(customerColumnWidth)
            + DelivererConstants.LOC_HEADING_STR.PadRight(locationColumnWidth)
            + DelivererConstants.DISTANCE_HEADING_STR);

        int orderChoiceIndex = 1;
        for (int i = 0; i < customerOrdersList.Count(); i++)
        {
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
}
