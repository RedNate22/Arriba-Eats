using System;
using Entities;

namespace UIComponents;

/// <summary>
/// Contains various static methods for I/O associated with the <see cref="User"/>, regardless of type.
/// </summary>
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
    /// Formats the <see cref="MenuConstants.LOG_OUT_TEMPLATE"/> string
    /// to dynamically display it in the correctly numbered position of the menu.
    /// </summary>
    /// <param name="menuChoiceNum"> The position number for the Log out option. </param>
    /// <returns> The formatted string, with the correctly numbered position. </returns>
    public static string LogOutStr(int menuChoiceNum)
    {
        const string LOG_OUT_STR = "{0}: Log out";
        return string.Format(LOG_OUT_STR, menuChoiceNum);
    }

    /// <summary>
    /// Displays the relevant user information, depending on the <see cref="UserType"/>
    /// of the <see cref="User"/>.
    /// </summary>
    /// <param name="user"> The <see cref="User"/> to display the information of. </param>
    public static void DisplayUserInfo(User user)
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
    /// Calculates the taxicab distance between a <see cref="User"/> and
    /// a <see cref="Restaurant"/>.
    /// </summary>
    /// <param name="user"> The user whose location will be compared. </param>
    /// <param name="restaurant"> The restaurant whose location will be compared. </param>
    /// <returns> The taxicab distance between the user and the restaurant. </returns>
    public static int GetDistance(User user, Restaurant restaurant)
    {
        string[] userCoords = user.Location.Split(',');
        string[] restaurantCoords = restaurant.Location.Split(',');

        int u1 = int.Parse(userCoords[0]);
        int u2 = int.Parse(userCoords[1]);
        int r1 = int.Parse(restaurantCoords[0]);
        int r2 = int.Parse(restaurantCoords[1]);

        int distance = Math.Abs(u1 - r1) + Math.Abs(u2 - r2);
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
