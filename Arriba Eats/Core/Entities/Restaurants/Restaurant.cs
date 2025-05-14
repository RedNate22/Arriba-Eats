using System;
using UIComponents;

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

    /// <summary>
    /// Called within the <see cref="Client"/> constructor.
    /// <para> Creates a <see cref="Restaurant"/> to hold a <see cref="_restaurantMenu"/>
    /// and allow <see cref="Client"/>'s to register new menu items and manage orders. </para>
    /// </summary>
    /// <param name="restaurantName"> The name of the restaurant. Parsed from <see cref="Client"/>. </param>
    /// <param name="restaurantStyle"> The style of the restaurant. Parsed from <see cref="Client"/>. </param>
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

    /// <summary>
    /// Displays each menu item from the current <see cref="Client"/>'s <see cref="Restaurant._restaurantMenu"/>.
    /// </summary>
    public void DisplayCurrentMenu()
    {
        foreach (KeyValuePair<decimal, string> menuItem in _restaurantMenu)
        {
            IODisplay.DisplayMessage($"${menuItem.Key, 7:F2} {menuItem.Value}");
        }
    }
}
