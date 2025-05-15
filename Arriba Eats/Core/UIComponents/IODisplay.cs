using System;
using Entities;
using UINavigation;

namespace UIComponents;

/// <summary>
/// Contains various static methods for handling general I/O with the user.
/// </summary>
public static class IODisplay
{
    /// <summary>
    /// Writes an empty line to the screen, to be used as a line break between selections and menus.
    /// </summary>
    public static void DisplayEmptyLine()
    {
        Console.WriteLine();
    }

    /// <summary> Writes the specified string message to the screen on a new line. </summary>
    /// <param name="message"> The string message to display to the screen. </param>
    public static void DisplayMessage(string message)
    {
        Console.WriteLine(message);
    }

    /// <summary> Writes the specified string message to the screen on the same line. </summary>
    /// <param name="message"> The string message to display to the screen. </param>
    public static void DisplayMessageSingleLine(string message)
    {
        Console.Write(message);
    }

    /// <summary>
    /// Uses <see cref="Console.ReadLine"/> to read an input from the user and 
    /// returns this as a string.
    /// <para> Trims any leading and trailing whitespace from the string. </para>
    /// <para> Assigns an empty value if the original input is empty. </para>
    /// </summary>
    /// <returns> The input as a string. </returns>
    public static string ReadInput()
    {
        string input = Console.ReadLine()?.Trim() ?? "";
        return input;
    }

    /// <summary>
    /// Reads a string input from the user via the console.
    /// <para> Attempts to convert the input into an integer. </para>
    /// </summary>
    /// <Returns> The integer value if valid, 
    /// or a default value (-1) if the input is invalid. </Returns>
    public static int GetChoice()
    {
        string? choice = ReadInput();

        if (int.TryParse(choice, out int result)) return result;
        else return -1;
    }

    /// <summary>
    /// Displays the relevant user information, depending on the <see cref="UserType"/>
    /// of the <see cref="User"/>.
    /// </summary>
    /// <param name="user"></param>
    public static void DisplayUserInfo(User user)
    {
        if (SessionManager.CurrentUser != null)
        {
            UserType userType = SessionManager.ReturnUserType();

            DisplayMessage("Your user details are as follows:");
            DisplayMessage($"Name: {user.Name}");
            DisplayMessage($"Age: {user.Age}");
            DisplayMessage($"Email: {user.Email}");
            DisplayMessage($"Mobile: {user.Mobile}");

            switch (userType)
            {
                case UserType.Customer:
                    var customer = (Customer)user;
                    DisplayMessage($"Location: {customer.Location}");
                    DisplayMessage($"You've made 0 order(s) and spent a total of $0.00 here.");
                    break;

                case UserType.Deliverer:
                    var deliverer = (Deliverer)user;
                    DisplayMessage($"Licence plate: {deliverer.LicencePlate}");

                    // If (the deliverer has an order not yet delivered)
                    /*
                    DisplayMessage($"""
                        Current delivery:
                        Order #ORDER_NO from RESTAURANT_NAME at RX,RY.
                        To be delivered to CUSTOMER_NAME at CX,CY.
                        """);
                    */
                    break;

                case UserType.Client:
                    var client = (Client)user;
                    DisplayMessage($"Restaurant name: {client.RestaurantName}");
                    DisplayMessage($"Restaurant style: {client.RestaurantStyle}");
                    DisplayMessage($"Restaurant location: {client.Location}");
                    break;

                default:
                    DisplayMessage("User's type is not defined.");  // ? turn this into a const?
                    break;
            }
        }

        else DisplayMessage("No user is currently logged in.");  // ? turn this into a const?
    }

    /// <summary>
    /// Welcomes the current <see cref="User"/> by their <see cref="User.Name"/>.
    /// </summary>
    public static void WelcomeUser()
    {
        if (SessionManager.CurrentUser != null)
        {
            DisplayMessage($"Welcome back, {SessionManager.CurrentUser.Name}!");
        }

        else
        {
            DisplayMessage("No user is currently logged in.");
        }
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
        // user u1, u2
        // restaurant r1, r2
        // Distance = (u1 - r1) + (u2 - r2)   

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
    /// Calculates the taxicab distance between two <see cref="User"/>s.
    /// </summary>
    /// <param name="userA"> The first user whose location will be compared. </param>
    /// <param name="userB"> The second user whose location will be compared. </param>
    /// <returns> The taxicab distance between the user and the restaurant. </returns>
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
}