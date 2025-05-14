using System;
using Entities;
using UINavigation;

namespace UIComponents;

/// <summary>
/// Handles general input and output for the menus using the console.
/// <para> Uses <see cref="IOUtilities"/> for input & output formatting and validation. </para>
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
    /// Displays the relevant user information, depending on the <see cref="UserType"/>.
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
                    DisplayMessage($"""
                        Current delivery:
                        Order #ORDER_NO from RESTAURANT_NAME at RX,RY.
                        To be delivered to CUSTOMER_NAME at CX,CY.
                        """); 
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
}