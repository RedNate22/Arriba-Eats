using System;
using DisplayIO;

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

    /// <summary>
    /// The food menu of a <see cref="Restaurant"/>.
    /// <para> Menu items are stored with their <c>decimal</c> price as the key, 
    /// and their <c>string</c> name as the value. </para>
    /// </summary>
    private static Dictionary<decimal, string> _restaurantMenu = new Dictionary<decimal, string>();

    /// <summary>
    /// Registers a new menu item in the <see cref="Restaurant"/>'s menu.
    /// The item is stored with its associated price as the key and the name as the value.
    /// </summary>
    /// <param name="itemPrice"> The price of the menu item, represented as a <c>decimal</c>. </param>
    /// <param name="itemName"> The name of the menu item, represented as a <c>string</c>. </param>
    public void RegisterMenuItem(decimal itemPrice, string itemName)
    {
        _restaurantMenu.Add(itemPrice, itemName);
    }

    // TODO xml
    public void DisplayCurrentMenu()
    {
        foreach (KeyValuePair<decimal, string> menuItem in _restaurantMenu)
        {
            IODisplay.DisplayMessage($"${menuItem.Key, 7:F2} {menuItem.Value}");
        }
    }
}
