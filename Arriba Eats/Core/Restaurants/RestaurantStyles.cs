using System;

namespace Entities;

/// <summary>
/// Defines the various styles of <see cref="Restaurant"/>s.
/// <para> Used to classify the style of cuisine for the restaurant. </para>
/// </summary>
public enum RestaurantStyles
{
    /// <summary>
    /// Italian cuisine.
    /// </summary>
    Italian = 1,

    /// <summary>
    /// French cuisine.
    /// </summary>
    French = 2,

    /// <summary>
    /// Chinese cuisine.
    /// </summary>
    Chinese = 3,

    /// <summary>
    /// Japanese cuisine.
    /// </summary>
    Japanese = 4,

    /// <summary>
    /// American cuisine.
    /// </summary>
    American = 5,

    /// <summary>
    /// Australian cuisine.
    /// </summary>
    Australian = 6
}
