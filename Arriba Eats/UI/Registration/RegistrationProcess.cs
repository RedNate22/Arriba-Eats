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
    /// Uses multiple methods defined in <see cref="UIDisplay"/> to
    /// prompt the user for input, which in turn use <see cref="UIUtilities"/>
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
        string name = UIDisplay.GetName();
        int age = UIDisplay.GetAge();
        string email = UIDisplay.GetEmail();
        string mobile = UIDisplay.GetMobile();
        string password = UIDisplay.GetPassword();

        switch (userType)
        {
            case "CUSTOMER":
                string customerLocation = UIDisplay.GetLocation();

                Customer customer = new Customer(name, age, email, mobile, password,
                    customerLocation);
                Customer.AddUser(UserType.Customer, customer);

                UIDisplay.DisplayMessage($"You have been successfully registered as a customer, {name}!");
                break;
            
            case "DELIVERER":
                string licencePlate = UIDisplay.GetLicencePlate();

                Deliverer deliverer = new Deliverer(name, age, email, mobile, password, licencePlate);
                Deliverer.AddUser(UserType.Deliverer, deliverer);
                
                UIDisplay.DisplayMessage($"You have been successfully registered as a deliverer, {name}!");
                break;
            
            case "CLIENT":
                string restaurantName = UIDisplay.GetRestaurantName();
                RestaurantStyles restaurantStyle = UIDisplay.GetRestaurantStyle();
                string clientLocation = UIDisplay.GetLocation();
            
                Client client = new Client(name, age, email, mobile, password, clientLocation, restaurantName, restaurantStyle);
                Client.AddUser(UserType.Client, client);
                
                UIDisplay.DisplayMessage($"You have been successfully registered as a client, {name}!");
                break;
            
            default:
                break;
        }
    }
}