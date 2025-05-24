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
    // ? Make these reusable? like instead of the later methods, use the IO to then check the list
    public static bool TryGetOrders(out List<CustomerOrder> getCustomerOrders, Customer customer)
    {
        getCustomerOrders = new List<CustomerOrder>();

        foreach (CustomerOrder order in _orderRegistry)
        {
            if (order.Customer == customer)
            {
                getCustomerOrders.Add(order);
            }
        }
        return getCustomerOrders.Count > 0;
    }

    // TODO xml
    public static bool TryGetOrders(out List<CustomerOrder> getRestaurantOrders, Client client)
    {
        getRestaurantOrders = new List<CustomerOrder>();

        if (!RestaurantRegistry.TryFindClientsRestaurant(SessionManager.ReturnCurrentUser(), out Restaurant? restaurant))
            return false;

        foreach (CustomerOrder order in _orderRegistry)
        {
            if (order.Restaurant == restaurant)
            {
                getRestaurantOrders.Add(order);
            }
        }
        return getRestaurantOrders.Count > 0;
    }

    // TODO xml
    public static bool TryGetOrders(out List<CustomerOrder> getOrdersToDeliver, Deliverer deliverer)
    {
        getOrdersToDeliver = new List<CustomerOrder>();

        foreach (CustomerOrder order in _orderRegistry)
        {
            if (order.Deliverer == null)
            {
                getOrdersToDeliver.Add(order);
            }
        }
        return getOrdersToDeliver.Count > 0;
    }

    /// <summary>
    /// Attempts to find the currently assigned order for the given <see cref="Deliverer"/>,
    /// then assigns this order to the <c>out</c> parameter. If no order is found, <c>null</c> is returned.
    /// </summary>
    /// <param name="deliverer"> The <see cref="Deliverer"/> to find the <see cref="CustomerOrder"/> for. </param>
    /// <param name="currentOrder"> The currently assigned <see cref="CustomerOrder"/> for the <see cref="Deliverer"/>. </param>
    /// <returns> <c>true</c>, and assigns the currently assigned order, otherwise, <c>false</c>, and
    /// <c>null</c> is assigned. </returns>
    public static bool TryGetCurrentlyAssignedOrder(Deliverer deliverer, out CustomerOrder currentOrder)
    {
        foreach (CustomerOrder order in _orderRegistry)
        {
            if (order.Deliverer == deliverer && order.OrderStatus != OrderStatus.Delivered)
            {
                currentOrder = order;
                return true;
            }
        }
        currentOrder = null!;
        return false;
    }

    /// <summary>
    /// Attempts to locate the given <see cref="Deliverer"/> and if they are currently assigned
    /// to an active order.
    /// </summary>
    /// <param name="deliverer"> The <see cref="Deliverer"/> to locate. </param>
    /// <returns> <c>true</c> if the <see cref="Deliverer"/> is found to be assigned to an active order,
    /// otherwise, <c>false</c>. </returns>
    public static bool TryFindAlreadyAssignedOrder(Deliverer deliverer)
    {
        foreach (CustomerOrder order in _orderRegistry)
        {
            if (order.Deliverer == deliverer && order.OrderStatus != OrderStatus.Delivered)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Attempts to locate the given <see cref="Deliverer"/> and if they are currently assigned
    /// to an active order, as well as in the process of delivering it.
    /// </summary>
    /// <param name="deliverer"> The <see cref="Deliverer"/> to locate. </param>
    /// <returns> <c>true</c> if the <see cref="Deliverer"/> is found and is currently delivering
    /// the order, otherwise, <c>false</c>. </returns>
    public static bool TryFindPickedUpOrder(Deliverer deliverer)
    {
        foreach (CustomerOrder order in _orderRegistry)
        {
            if (order.Deliverer == deliverer && order.OrderStatus == OrderStatus.BeingDelivered)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Attempts to validate whether the given <see cref="Deliverer"/> has already arrived at
    /// the <see cref="Restaurant"/> for the currently assigned order.
    /// </summary>
    /// <param name="deliverer"> The <see cref="Deliverer"/> to validate. </param>
    /// <returns> <c>true</c> if the <see cref="Deliverer"/> has already arrived, otherwise, <c>false</c>. </returns>
    public static bool TryFindDelivererAlreadyArrived(Deliverer deliverer)
    {
        foreach (CustomerOrder order in _orderRegistry)
        {
            if (order.Deliverer == deliverer && order.OrderStatus != OrderStatus.Delivered
                && order.DelivererArrivedAtRestaurant == true)
            {
                return true;
            }
        }
        return false;
    }
}