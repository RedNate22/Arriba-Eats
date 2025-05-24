using System;

namespace Entities;

/// <summary>
/// Defines the various statuses of a <see cref="CustomerOrder"/>.
/// </summary>
public enum OrderStatus
{   
    /// <summary>
    /// The default state of the order's status. Before the order
    /// has been confirmed.
    /// </summary>
    NotOrdered = 0,

    /// <summary>
    /// The order has been ordered. This is the first state.
    /// </summary>
    Ordered = 1,

    /// <summary>
    /// The order is being cooked. This is the second state.
    /// </summary>
    Cooking = 2,

    /// <summary>
    /// The order has been cooked. This is the third state.
    /// </summary>
    Cooked = 3,

    /// <summary>
    /// The order is being delivered. This is the fourth state.
    /// </summary>
    BeingDelivered = 4,

    /// <summary>
    /// The order has been delivered. This is the final state.
    /// </summary>
    Delivered = 5
}

/// <summary>
/// A public class to hold the method for returning the string equivalent of
/// an <see cref="OrderStatus"/>. 
/// </summary>
public static class DisplayOrderStatus
{
    /// <summary>
    /// Parses the given <see cref="OrderStatus"/> and returns the equivalent
    /// string, for properly displaying the status.
    /// </summary>
    /// <param name="orderStatus"> The <see cref="OrderStatus"/> to get the string of. </param>
    /// <returns> The string equivalent of the given <see cref="OrderStatus"/>, otherwise
    /// if the status does not exist, returns an empty string. </returns>
    public static string DisplayStatus(OrderStatus orderStatus)
    {
        switch (orderStatus)
        {
            case OrderStatus.NotOrdered:
                return "Not Ordered";
            case OrderStatus.Ordered:
                return "Ordered";
            case OrderStatus.Cooking:
                return "Cooking";
            case OrderStatus.Cooked:
                return "Cooked";
            case OrderStatus.BeingDelivered:
                return "Being Delivered";
            case OrderStatus.Delivered:
                return "Delivered";
            default:
                return "";
        }
    }   
}