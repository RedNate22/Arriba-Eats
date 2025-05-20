using System;

namespace Entities;

/// <summary>
/// Provides a centralised registry for storing and retrieving <see cref="CustomerOrder"/>s
/// made by <see cref="Customer"/>s for <see cref="Restaurant"/>s.
/// <para> Stores instances of <see cref="CustomerOrder"/>s in a list, with methods to store and
/// retrieve the orders or information regarding the orders. </para> 
/// </summary>
public static class OrderRegistry
{
    /// <summary>
    /// A list that contains all the <see cref="CustomerOrder"/>s ever made (confirmed), regardless
    /// of their <see cref="OrderStatus"/>.
    /// </summary>
    private static List<CustomerOrder> _orderRegistry = new List<CustomerOrder>();

    /// <summary>
    /// Attempts to add a <see cref="CustomerOrder"/> to the <see cref="_orderRegistry"/>. 
    /// </summary>
    /// <param name="customerOrder"> The <see cref="CustomerOrder"/> made by a <see cref="Customer"/>. </param>
    /// <returns> <c>true</c> if the registry does not already contain the <see cref="CustomerOrder"/>,
    /// and it is then added. Otherwise, <c>false</c>, and the order is not added. </returns>
    public static bool TryAddOrder(CustomerOrder customerOrder)
    {
        if (!_orderRegistry.Contains(customerOrder))
        {
            _orderRegistry.Add(customerOrder);
            return true;
        }
        else return false;
    }

    /*
    public static List<CustomerOrder> TryGetOrders(Restaurant restaurant)
    {
        foreach ()
    }
    */
}