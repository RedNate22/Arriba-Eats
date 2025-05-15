using System;
using UINavigation;
using UI;
using Entities;

namespace ArribaEats
{
    /// <summary> 
    /// Defines the main entry point to the program.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The main entry point to the program.
        /// <para> Calls <see cref="MainMenu.WelcomeMessage()"/> to welcome the user. </para>
        /// <para> Initializes the program and delegates UI flow control to <see cref="UIFlowController.RunMenuSystem"/> </para>
        /// </summary>
        static void Main()
        {
            // ! Test data REMOVE LATER
            /*
            // Customer
            string customerName = "CustomerTest";
            int customerAge = 0;
            string customerEmail = "Customer@gmail.com";
            string customerMobile = "0000000000";
            string customerPassword = "Password123";
            string customerLocation = "0,0";

            Customer customerTest = new Customer(customerName, customerAge, customerEmail,
                customerMobile, customerPassword, customerLocation);
            User.AddUser(UserType.Customer, customerTest);

            // Deliverer
            string delivererName = "DelivererTest";
            int delivererAge = 0;
            string delivererEmail = "Deliverer@gmail.com";
            string delivererMobile = "0000000000";
            string delivererPassword = "Password123";
            string delivererLicencePlate = "DELIVERER123";

            Deliverer delivererTest = new Deliverer(delivererName, delivererAge, delivererEmail,
                delivererMobile, delivererPassword, delivererLicencePlate);
            User.AddUser(UserType.Deliverer, delivererTest);
            
            // Client
            string clientName = "ClientTest";
            int clientAge = 0;
            string clientEmail = "Client@gmail.com";
            string clientMobile = "0000000000";
            string clientPassword = "Password123";
            string restaurantName = "ClientTest's Restaurant";
            RestaurantStyles restaurantStyle = RestaurantStyles.Australian;
            string clientLocation = "5,5";

            Client clientTest = new Client(clientName, clientAge, clientEmail, clientMobile,
                clientPassword, clientLocation, restaurantName, restaurantStyle); 
            User.AddUser(UserType.Client, clientTest);
            */
            
            MainMenu.WelcomeMessage();
            UIFlowController.RunMenuSystem();
        }
    }
}
