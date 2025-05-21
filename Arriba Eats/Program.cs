using System;
using UINavigation;
using UI;
using Entities;  // ! Remove later

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
        /// <para> Initializes the program and delegates UI flow control to <see cref="UIFlowController.RunMenuSystem"/>
        /// where the <see cref="MainMenu"/> is first displayed to the user. </para>
        /// </summary>
        static void Main()
        {
            // ! Remove below
            // Customer
            string customerName = "CustomerTest";
            int customerAge = 0;
            string customerEmail = "Customer";
            string customerMobile = "0000000000";
            string customerPassword = "1";
            string customerLocation = "0,0";

            Customer customerTest = new Customer(customerName, customerAge, customerEmail,
                customerMobile, customerPassword, customerLocation);
            User.AddUser(UserType.Customer, customerTest);

            // Deliverer
            string delivererName = "DelivererTest";
            int delivererAge = 0;
            string delivererEmail = "Deliverer";
            string delivererMobile = "0000000000";
            string delivererPassword = "1";
            string delivererLicencePlate = "DELIVERER123";

            Deliverer delivererTest = new Deliverer(delivererName, delivererAge, delivererEmail,
                delivererMobile, delivererPassword, delivererLicencePlate);
            User.AddUser(UserType.Deliverer, delivererTest);
            
            // Client
            string clientName = "ClientTest";
            int clientAge = 0;
            string clientEmail = "Client";
            string clientMobile = "0000000000";
            string clientPassword = "1";
            string restaurantName = "ClientTest's Restaurant";
            RestaurantStyles restaurantStyle = RestaurantStyles.Australian;
            string clientLocation = "5,5";

            Client clientTest = new Client(clientName, clientAge, clientEmail, clientMobile,
                clientPassword, clientLocation, restaurantName, restaurantStyle); 
            User.AddUser(UserType.Client, clientTest);
            if (RestaurantRegistry.TryFindClientsRestaurant
                (clientTest, out Restaurant? clientTestRestaurant))
            {
                string testItemName = "Bob's Test Burger";
                decimal testItemPrice = 10.00M;
                if (clientTestRestaurant!.TryRegisterMenuItem(testItemName, testItemPrice)){}        
            }
            // ! Remove above
            
            MainMenu.WelcomeMessage();
            UIFlowController.RunMenuSystem();
        }
    }
}
