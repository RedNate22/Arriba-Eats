using System;

namespace Entities;

// TODO xml
public class Restaurant
{
    /// <summary>
    /// Get the restaurant's name.
    /// </summary>
    public string RestaurantName { get; private set; }

    /// <summary>
    /// Get the restaurant's style.
    /// </summary>
    public RestaurantStyles RestaurantStyle { get; private set; }

    // TODO xml
    public Restaurant (string restaurantName, RestaurantStyles restaurantStyle)
    {
        RestaurantName = restaurantName;
        RestaurantStyle = restaurantStyle;
    }

    // TODO
    private static Dictionary<decimal, string> _restaurantMenu = new Dictionary<decimal, string>();
}
