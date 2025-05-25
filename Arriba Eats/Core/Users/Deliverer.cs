using System;

namespace Entities;

/// <summary>
/// Represents a delivery person, inheriting from the <see cref="User"/> class.
/// <para> A deliverer can take accept delivery jobs from <see cref="Restaurant"/>s
/// to deliver orders to a <see cref="Customer"/>. 
/// A deliverer has a licence plate for their vehicle. </para> 
/// </summary>
public class Deliverer : User
{
    /// <summary>
    /// Get the deliverer's license place.
    /// </summary>
    public string LicencePlate { get; private set; }

    /// <summary>
    /// Initialises a new instance of the <see cref="User"/> class
    /// with the specified details.
    /// <para> As location is provided later after initialisation,
    /// here it is currently optional and is automatically set to a default value. </para>
    /// </summary>
    /// <param name="name"> The deliverer's name. </param>
    /// <param name="age"> The deliverer's age. </param>
    /// <param name="email"> The deliverer's email. </param>
    /// <param name="mobile"> The deliverer's mobile. </param>
    /// <param name="password"> The deliverer's password. </param>
    /// <param name="location"> The deliverer's location. Defaults to 0,0. </param>
    public Deliverer(string name, int age, string email, string mobile, string password,
        string licencePlate, string location = "0,0")
        : base(name, age, email, mobile, password, location)
    {
        LicencePlate = licencePlate;
    }
}