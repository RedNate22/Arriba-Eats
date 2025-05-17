using System;
using UIComponents;

namespace Entities;

/// <summary>
/// Represents a restaurant owned by a <see cref="Client"/>.
/// <para> A restaurant has a name, style, location and menu. </para>
/// </summary>
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
    /// Get the restaurant's location.
    /// </summary>
    public string Location { get; private set; }

    /// <summary>
    /// Displayed to indicate the <see cref="_restaurantMenu"/> is empty.
    /// </summary>
    private const string MENU_EMPTY_STR = "The menu is currently empty.";

    /// <summary>
    /// Called within the <see cref="Client"/> constructor.
    /// <para> Creates a <see cref="Restaurant"/> to hold a <see cref="_restaurantMenu"/>
    /// and allow <see cref="Client"/>'s to register new menu items and manage orders. </para>
    /// </summary>
    /// <param name="restaurantName"> The name of the restaurant. Parsed from <see cref="Client"/>. </param>
    /// <param name="restaurantStyle"> The style of the restaurant. Parsed from <see cref="Client"/>. </param>
    public Restaurant(string restaurantName, RestaurantStyles restaurantStyle, string location)
    {
        RestaurantName = restaurantName;
        RestaurantStyle = restaurantStyle;
        Location = location;
    }

    /// <summary>
    /// The food menu of a <see cref="Restaurant"/>.
    /// <para> Menu items are stored with their <c>string</c> name as the key,
    /// and their <c>decimal</c> price as the value. </para>
    /// </summary>
    private Dictionary<string, decimal> _restaurantMenu = new Dictionary<string, decimal>();

    /// <summary>
    /// Attempts to register a new menu item in the <see cref="Restaurant"/>'s menu.
    /// The item is stored with its associated name as the key and the price as the value.
    /// </summary>
    /// <param name="itemPrice"> The price of the menu item, represented as a <c>decimal</c>. </param>
    /// <param name="itemName"> The name of the menu item, represented as a <c>string</c>. </param>
    /// <returns> <c>true</c> if the item is not already registered, otherwise <c>false</c>. </returns>
    public bool TryRegisterMenuItem(string itemName, decimal itemPrice)
    {
        if (!_restaurantMenu.ContainsKey(itemName))
        {
            _restaurantMenu.Add(itemName, itemPrice);
            return true;
        }

        else return false;
    }

    /// <summary>
    /// Displays each menu item from the current <see cref="Client"/>'s <see cref="_restaurantMenu"/>.
    /// </summary>
    public void DisplayCurrentlyRegisteredMenuItems()
    {
        if (_restaurantMenu.Count != 0)
        {
            foreach (KeyValuePair<string, decimal> menuItem in _restaurantMenu)
            {
                IODisplay.DisplayMessage($"${menuItem.Value} {menuItem.Key,7:F2}");
            }
        }
    }

    // TODO xml
    public void GetMenu(out List<decimal> getRestaurantMenuPrices, out List<string> getRestaurantMenuItemNames)
    {
        getRestaurantMenuPrices = new List<decimal>();
        getRestaurantMenuItemNames = new List<string>();

        if (_restaurantMenu.Count != 0)
        {
            foreach (KeyValuePair<string, decimal> menuItem in _restaurantMenu)
            {
                getRestaurantMenuPrices.Add(menuItem.Value);
                getRestaurantMenuItemNames.Add(menuItem.Key);
            }
        }
    }
}
