using System;

namespace Entities;

/// <summary>
/// Represents a customer, inheriting from the <see cref="User"/> class.
/// </summary>
public class Customer : User
{
    /// <summary>
    /// Initialises a new instance of the <see cref="Customer"/> class
    /// with the specified user details.
    /// </summary>
    /// <param name="name"> The customer's name. </param>
    /// <param name="age"> The customer's age. </param>
    /// <param name="email"> The customer's email. </param>
    /// <param name="mobile"> The customer's mobile. </param>
    /// <param name="password"> The customer's password. </param>
    /// <param name="location"> The customer's location. </param>
    public Customer(string name, int age, string email, string mobile, string password, string location)
        : base(name, age, email, mobile, password, location)
    {

    }
    
    //private List<> _customerOrder
}