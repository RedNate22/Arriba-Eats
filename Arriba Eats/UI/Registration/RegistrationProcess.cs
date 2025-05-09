using System;
using DisplayIO;
using Entities;

namespace UI;

/// <summary>
/// Manages the process for registering a user to the application.
/// <para> Allows users of different types to be registered into the 
/// <see cref="UserRegistry"/> using the <see cref="User.AddUser()"/> method. </para>
/// </summary>
public static class RegistrationProcess
{   
    /// <summary>
    /// Registers a new user based on the specified type selected in
    /// <see cref="RegistrationMenu"/>.
    /// <para> 
    /// Uses multiple methods defined in <see cref="IODisplay"/> to
    /// prompt the user for input, which in turn use <see cref="IOUtilities"/>
    /// to check for valid formatting and input before returning the value here to
    /// be assigned. 
    /// </para>
    /// <para> 
    /// Based on the <see cref="UserType"/> specified, further prompts for
    /// more specific data to be assigned before registering the user. 
    /// </para>
    /// </summary>
    /// <param name="userType"> The type of user to register.
    /// Expected values: "CUSTOMER", "DELIVERER", or "CLIENT". </param>
    public static void Register(string userType)
    {
        string name = IODisplay.GetName();
        int age = IODisplay.GetAge();
        string email = IODisplay.GetEmail();
        string mobile = IODisplay.GetMobile();
        string password = IODisplay.GetPassword();

        switch (userType)
        {
            case "CUSTOMER":
                string customerLocation = IODisplay.GetLocation();

                Customer customer = new Customer(name, age, email, mobile, password,
                    customerLocation);
                User.AddUser(UserType.Customer, customer);

                IODisplay.DisplayMessage($"You have been successfully registered as a customer, {name}!");
                break;
            
            case "DELIVERER":
                string licencePlate = IODisplay.GetLicencePlate();

                Deliverer deliverer = new Deliverer(name, age, email, mobile, password, licencePlate);
                User.AddUser(UserType.Deliverer, deliverer);
                
                IODisplay.DisplayMessage($"You have been successfully registered as a deliverer, {name}!");
                break;
            
            case "CLIENT":
                string restaurantName = IODisplay.GetRestaurantName();
                RestaurantStyles restaurantStyle = IODisplay.GetRestaurantStyle();
                string clientLocation = IODisplay.GetLocation();
            
                Client client = new Client(name, age, email, mobile, password, clientLocation, restaurantName, restaurantStyle);
                User.AddUser(UserType.Client, client);
                
                IODisplay.DisplayMessage($"You have been successfully registered as a client, {name}!");
                break;
            
            default:
                break;
        }
    }
}