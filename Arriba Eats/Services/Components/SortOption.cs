using System;

namespace UIComponents;

/// <summary>
/// Defines the various sorthing methods when 
/// calling <see cref="CustomerIO.DisplayRestaurantsList()"/>.
/// </summary>
public enum SortOption
{
    /// <summary>
    /// Sort <see cref="Entities.Restaurant"/>s alphabetically
    /// by <see cref="Entities.Restaurant.RestaurantName"/>.
    /// </summary>
    Alphabetically = 1,
    
    /// <summary>
    /// Sort <see cref="Entities.Restaurant"/>s by
    /// distance between restaurant and the <see cref="Entities.User"/>.
    /// </summary>
    ByDistance = 2,
    
    /// <summary>
    /// Sort <see cref="Entities.Restaurant"/>s by their
    /// <see cref="Entities.Restaurant.RestaurantStyle"/>.
    /// </summary>
    ByStyle = 3,
    
    /// <summary>
    /// Sort <see cref="Entities.Restaurant"/>s by their
    /// average rating.
    /// </summary>
    ByAverageRating = 4
}