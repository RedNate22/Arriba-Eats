using System;

namespace Entities;

/// <summary>
/// Defines the various statuses of a <see cref="CustomerOrder"/>.
/// </summary>
public enum OrderStatus
{
    /// <summary>
    /// The order has been ordered. This is the first state.
    /// </summary>
    Ordered,

    /// <summary>
    /// The order is being cooked. This is the second state.
    /// </summary>
    Cooking,

    /// <summary>
    /// The order has been cooked. This is the third state.
    /// </summary>
    Cooked,

    /// <summary>
    /// The order is being delivered. This is the fourth state.
    /// </summary>
    BeingDelivered,

    /// <summary>
    /// The order has been delivered. This is the final state.
    /// </summary>
    Delivered
}