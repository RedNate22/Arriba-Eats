using System;
using DisplayIO;
using Entities;

namespace UI;

public static class RegistrationProcess
{   
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
                string location = UIDisplay.GetLocation();

                Customer customer = new Customer(name, age, email, mobile, password,
                    location);
                Customer.AddUser(UserType.Customer, customer);

                UIDisplay.DisplayMessage($"You have been successfully registered as a customer, {name}!");
                break;
            
            case "DELIVERER":
                string licencePlate = UIDisplay.GetLicencePlate();

                Deliverer deliverer = new Deliverer(name, age, email, mobile, password, licencePlate);
                UIDisplay.DisplayMessage($"You have been successfully registered as a deliverer, {name}!");
                break;
            
            case "CLIENT":
                string restaurantName = UIDisplay.GetRestaurantName();
                //TODO enum restaurantStyle = UIDisplay.GetRestaurantStyle();
                //TODO int location = UIDisplay.GetLocation();
            
                //Client client = new Client();

                UIDisplay.DisplayMessage($"You have been successfully registered as a client, {name}!");
                break;
            
            default:
                break;
        }
    }
}