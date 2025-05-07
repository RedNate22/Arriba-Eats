using System;

namespace Entities
{
    /// <summary>
    /// Represents a delivery person, inheriting from the <see cref="User"/> class.
    /// <para> A delivery person has a license plate for their vehicle. </para> 
    /// </summary>
    public class Deliverer : User
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="User"/> class
        /// with the specified details.
        /// </summary>
        /// <param name="name"> The deliverer's name. </param>
        /// <param name="age"> The deliverer's age. </param>
        /// <param name="email"> The deliverer's email. </param>
        /// <param name="mobile"> The deliverer's mobile. </param>
        /// <param name="password"> The deliverer's password. </param>
        /// <param name="location"> The deliverer's location. </param>
        public Deliverer(string name, int age, string email, string mobile, string password, string location) 
            : base(name, age, email, mobile, password, location)
            {

            }
    }
}