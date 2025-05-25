using System;
using Entities;

namespace UIComponents;

// TODO xml
public class UserIO
{
    /// <summary>
    /// Welcomes the current <see cref="User"/> by their <see cref="User.Name"/>.
    /// </summary>
    public static void WelcomeUser()
    {
        var currentUser = SessionManager.ReturnCurrentUser();
        if (currentUser != null) DisplayIO.DisplayMessage($"Welcome back, {currentUser.Name}!");
        else DisplayIO.DisplayMessage("No user is currently logged in.");
    }

    /// <summary>
    /// Displays the relevant user information, depending on the <see cref="UserType"/>
    /// of the <see cref="User"/>.
    /// </summary>
    /// <param name="user"> The <see cref="User"/> to display the information of. </param>
    public static void DisplayUserInfo(User user)  // TODO make this an overload, 3 methods
    {
        var currentUser = SessionManager.ReturnCurrentUser();
        if (currentUser != null)
        {
            UserType userType = SessionManager.ReturnUserType();

            DisplayIO.DisplayMessage("Your user details are as follows:");
            DisplayIO.DisplayMessage($"Name: {user.Name}");
            DisplayIO.DisplayMessage($"Age: {user.Age}");
            DisplayIO.DisplayMessage($"Email: {user.Email}");
            DisplayIO.DisplayMessage($"Mobile: {user.Mobile}");

            switch (userType)
            {
                case UserType.Customer:
                    var customer = (Customer)user;
                    DisplayIO.DisplayMessage($"Location: {customer.Location}");

                    List<CustomerOrder> customerOrders = OrderIO.GetCustomerOrders();
                    decimal totalSpent = 0.00M;
                    foreach (CustomerOrder order in customerOrders)
                    {
                        totalSpent += order.GetTotalSpent();
                    }
                    DisplayIO.DisplayMessage($"You've made {customerOrders.Count} order(s) and spent a total of ${totalSpent:F2} here.");
                    break;

                case UserType.Deliverer:
                    var deliverer = (Deliverer)user;
                    DisplayIO.DisplayMessage($"Licence plate: {deliverer.LicencePlate}");

                    OrderRegistry.TryGetCurrentlyAssignedOrder(deliverer, out CustomerOrder activeOrder);
                    if (activeOrder != null)
                    {
                        DisplayIO.DisplayMessage("Current delivery:");
                        DisplayIO.DisplayMessage($"Order #{activeOrder.OrderNumber} from {activeOrder.Restaurant.RestaurantName} at {activeOrder.Restaurant.Location}.");
                        DisplayIO.DisplayMessage($"To be delivered to {activeOrder.Customer.Name} at {activeOrder.Customer.Location}.");
                    }
                    break;

                case UserType.Client:
                    var client = (Client)user;
                    DisplayIO.DisplayMessage($"Restaurant name: {client.RestaurantName}");
                    DisplayIO.DisplayMessage($"Restaurant style: {client.RestaurantStyle}");
                    DisplayIO.DisplayMessage($"Restaurant location: {client.Location}");
                    break;

                default:
                    DisplayIO.DisplayMessage("User's type is not defined.");
                    break;
            }
        }
        else DisplayIO.DisplayMessage("No user is currently logged in.");
    }

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
    /// Calculates the taxicab distance between two <see cref="User"/>s.
    /// </summary>
    /// <param name="userA"> The first user whose location will be compared. </param>
    /// <param name="userB"> The second user whose location will be compared. </param>
    /// <returns> The taxicab distance between both users. </returns>
    public static int GetDistance(User userA, User userB)
    {
        // userA A1, A2
        // userB B1, B2
        // Distance = (A1 - B1) + (A2 - B2)   

        string[] userACoords = userA.Location.Split(',');
        string[] userBCoords = userB.Location.Split(',');

        int a1 = int.Parse(userACoords[0]);
        int a2 = int.Parse(userACoords[1]);
        int b1 = int.Parse(userBCoords[0]);
        int b2 = int.Parse(userBCoords[1]);

        int distance = Math.Abs(a1 - b1) + Math.Abs(a2 - b2);
        return distance;
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

        int delivererToRestaurantDist = GetDistance(deliverer, restaurant);
        int restaurantToCustomer = GetDistance(customer, restaurant);
        int totalDistance = delivererToRestaurantDist + restaurantToCustomer;

        return totalDistance;
    }
}
