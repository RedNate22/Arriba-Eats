using System;
// using UIComponents;

namespace Entities;

/// <summary>
/// Provides a centeralised registry for storing and retrieving <see cref="Restaurant"/>s,
/// along with their respective <see cref="Client"/> (owners).
/// </summary>
public static class RestaurantRegistry
{
    /// <summary>
    /// A dictionary that maps <see cref="Client"/>'s to their owned <see cref="Restaurant"/>'s. 
    /// </summary>
    private static Dictionary<Client, Restaurant> _restaurantRegistry = new Dictionary<Client, Restaurant>();

    /// <summary>
    /// Attempts to register a new <see cref="Client"/> and their associated <see cref="Restaurant"/>
    /// into the registry.
    /// <para> To be used to reference the correct <see cref="Restaurant"/> associated with
    /// a <see cref="Client"/> when the client is managing their restaurant's menu and orders. </para>
    /// </summary>
    /// <param name="client"> The <see cref="Client"/> instance being registered. </param>
    /// <param name="restaurant"> The <see cref="Restaurant"/> instance being registered. </param>
    /// <returns> <c>false</c> if the <see cref="Client"/> or <see cref="Restaurant"/> are already
    /// registered, otherwise, <c>true</c>, the registering is complete. </returns>
    public static bool AddRestaurant(Client client, Restaurant restaurant)
    {
        if (_restaurantRegistry.ContainsKey(client)) return false;  // Client already registered
        else if (_restaurantRegistry.ContainsValue(restaurant)) return false;  // Restaurant already registered
        else
        {
            _restaurantRegistry.Add(client, restaurant);
            return true;
        }
    }

    /// <summary>
    /// Attempts to determine the <see cref="Restaurant"/> instance associated with the
    /// given <see cref="Client"/> by searching the <see cref="_restaurantRegistry"/>.
    /// </summary>
    /// <param name="client"> The client instance whose restaurant instance is being identified. </param>
    /// <param name="foundRestaurant"> Outputs the associated <see cref=""/></param>
    /// <returns> <c>true</c> if the client is found in the registry and their <see cref="Restaurant"/>
    /// is assigned. Otherwise <c>false</c>. </returns>
    public static bool TryFindClientsRestaurant(User client, out Restaurant? foundRestaurant)
    {
        foreach (KeyValuePair<Client, Restaurant> pair in _restaurantRegistry)
        {
            if (pair.Key == client)
            {
                foundRestaurant = pair.Value;
                return true;
            }
        }

        foundRestaurant = null;
        return false;
    }

    /// <summary>
    /// Attempts to extract all current <see cref="Restaurant"/>s in the <see cref="_restaurantRegistry"/>
    /// into a list.
    /// </summary>
    /// <param name="restaurantsList"> The list of <see cref="Restaurant"/>s currently registered. </param>
    /// <returns> <c>true</c> if at least one <see cref="Restaurant"/> is found and added to the list,
    /// otherwise, <c>false</c>. </returns>
    public static bool TryListRestaurants(out List<Restaurant> restaurantsList)
    {
        restaurantsList = new List<Restaurant>();

        foreach (Restaurant restaurant in _restaurantRegistry.Values)
        {
            restaurantsList.Add(restaurant);
        }

        return restaurantsList.Count > 0;
    }
}