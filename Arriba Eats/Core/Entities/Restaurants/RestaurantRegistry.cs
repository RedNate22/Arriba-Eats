using System;
using DisplayIO;

namespace Entities;

// TODO xml
internal static class RestaurantRegistry
{
    /// <summary>
    /// A dictionary that maps <see cref="Client"/>'s to their owned <see cref="Restaurant"/>'s. 
    /// </summary>
    private static Dictionary<Client, Restaurant> _restaurantDictionary = new Dictionary<Client, Restaurant>();

    /// <summary>
    /// Registers a new <see cref="Client"/> and their associated <see cref="Restaurant"/>
    /// into the registry.
    /// <para> To be used to reference the correct <see cref="Restaurant"/> associated with
    /// a <see cref="Client"/> when the client is managing their restaurant's menu and orders. </para>
    /// </summary>
    /// <param name="client"> The <see cref="Client"/> instance being registered. </param>
    /// <param name="restaurant"> The <see cref="Restaurant"/> instance being registered. </param>
    internal static void AddRestaurant(Client client, Restaurant restaurant)
    {
        if (_restaurantDictionary.ContainsKey(client))
        {
            IODisplay.DisplayMessage("This client already owns a restaurant!");
        }

        else if (_restaurantDictionary.ContainsValue(restaurant))
        {
            IODisplay.DisplayMessage("This restaurant is already registered!");
        }

        else
        {
            _restaurantDictionary.Add(client, restaurant);
        }
    }

    /// <summary>
    /// Attempts to determine the <see cref="Restaurant"/> instance associated with the
    /// given <see cref="Client"/> by searching the <see cref="_restaurantDictionary"/>.
    /// </summary>
    /// <param name="client"> The client instance whose restaurant instance is being identified. </param>
    /// <param name="foundRestaurant"> Outputs the associated <see cref=""/></param>
    /// <returns> <c>true</c> if the client is found in the registry and their <see cref="Restaurant"/>
    /// is assigned. Otherwise <c>false</c>. </returns>
    public static bool TryFindClientsRestaurant (User client, out Restaurant? foundRestaurant)
    {
        foreach (KeyValuePair<Client, Restaurant> pair in _restaurantDictionary)
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
}
