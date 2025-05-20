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
    // TODO xml
    private static List<CustomerOrder> _orders = new List<CustomerOrder>();

    // TODO xml
    public static bool TryAddOrder(CustomerOrder customerOrder)
    {
        if (!_orders.Contains(customerOrder))
        {
            _orders.Add(customerOrder);
            return true;
        }
        else return false;
    }
}
