using System;

namespace Entities
{
    /// <summary>
    /// Represents a client, inheriting from the <see cref="User"/> class.
    /// <para> A client owns a restaurant and has a specific restaurant style. </para>
    /// </summary>
    public class Client : User
    {
        /// <summary>
        /// Get the client's restaurant's name.
        /// </summary>
        public string RestaurantName { get; private set; }

        /// <summary>
        /// Get the client's restaurant's style.
        /// </summary>
        public RestaurantStyles RestaurantStyle { get; private set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="Client"/> class
        /// with the specified user details.
        /// </summary>
        /// <param name="name"> The client's name. </param>
        /// <param name="age"> The client's age. </param>
        /// <param name="email"> The client's email. </param>
        /// <param name="mobile"> The client's mobile. </param>
        /// <param name="password"> The client's password. </param>
        /// <param name="location"> The client's location. </param>
        /// <param name="restaurantName"> The client's restaurant's name. </param>
        /// <param name="restaurantStyle"> The client's restaurant's style. </param>
        public Client(string name, int age, string email, string mobile, string password, string location,
            string restaurantName, RestaurantStyles restaurantStyle) 
            : base(name, age, email, mobile, password, location)
            {
                RestaurantName = restaurantName;
                RestaurantStyle = restaurantStyle; 
            }
    }
}