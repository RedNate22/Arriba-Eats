using System;
using UIComponents;

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

    // TODO xml
    public static bool TryGetOrders(out List<CustomerOrder> getCustomerOrders, Customer customer)
    {
        getCustomerOrders = new List<CustomerOrder>();
        bool customerHasOrders = false;

        foreach (CustomerOrder order in _orderRegistry)
        {
            if (order.Customer == customer)
            {
                customerHasOrders = true;
                break;
            }
        }

        if (customerHasOrders == false) return false;
        else
        {
            foreach (CustomerOrder order in _orderRegistry)
            {
                if (order.Customer == customer)
                {
                    getCustomerOrders.Add(order);
                }
            }
            return true;
        }
    }

    public static bool TryGetOrders(out List<CustomerOrder> getRestaurantOrders, Client client)
    {
        getRestaurantOrders = new List<CustomerOrder>();
        bool restaurantHasOrders = false;

        if (RestaurantRegistry.TryFindClientsRestaurant(SessionManager.TryGetCurrentUser(), out Restaurant? restaurant))
        {
            foreach (CustomerOrder order in _orderRegistry)
            {
                if (order.Restaurant == restaurant)
                {
                    restaurantHasOrders = true;
                    break;
                }
            }
        }
        else return false;

        if (restaurantHasOrders)
        {
            foreach (CustomerOrder order in _orderRegistry)
            {
                if (order.Restaurant == restaurant)
                {
                    getRestaurantOrders.Add(order);
                }
            }
            return true;
        }
        else return false;
    }
}