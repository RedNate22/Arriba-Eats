using System;
using Entities;
using UINavigation;
using UI;

namespace UIComponents;

// TODO xml
public class UserIO
{
    /// <summary>
    /// Updates the current <see cref="Deliverer"/>'s location to the given one, using the 
    /// publicly accessible method, <see cref="User.UpdateLocation"/>.
    /// </summary>
    /// <param name="newLocation"> The new location to update to. </param>
    public static void DelivererChangeLocation(string newLocation)
    {
        User user = SessionManager.ReturnCurrentUser();
        user.UpdateLocation(newLocation);
    }

    /// <summary>
    /// Calculates the total taxicab distance for the currently authenticated <see cref="Deliverer"/>
    /// to travel for the order.
    /// <para> The total distance is the sum of the distance between the <see cref="Deliverer"/>
    /// and <see cref="Restaurant"/>, plus the distance between the <see cref="Restaurant"/>
    /// and the <see cref="Customer"/>. </para>
    /// </summary>
    /// <param name="restaurant"> The <see cref="Restaurant"/> where the order was placed. </param>
    /// <param name="customer"> The <see cref="Customer"/> who placed the order. </param>
    /// <returns> The total taxicab distance for the <see cref="Deliverer"/> to travel. </returns>
    public static int CalculateTotalTaxiCabDistance(Restaurant restaurant, Customer customer)
    {
        Deliverer deliverer = SessionManager.ReturnCurrentDeliverer();

        int delivererToRestaurantDist = IODisplay.GetDistance(deliverer, restaurant);
        int restaurantToCustomer = IODisplay.GetDistance(customer, restaurant);
        int totalDistance = delivererToRestaurantDist + restaurantToCustomer;

        return totalDistance;
    }
}
