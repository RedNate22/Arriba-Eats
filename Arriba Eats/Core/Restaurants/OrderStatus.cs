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