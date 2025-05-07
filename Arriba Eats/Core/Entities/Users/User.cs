using System;

namespace Entities;

/// <summary>
/// Represents a user with essential details such as name, age, email, mobile
/// password, and location.
/// <para> Serves as an abstract base class for specified user types. </para> 
/// </summary>
public abstract class User
{
    /// <summary>
    /// Gets the user's name.
    /// </summary>
    public string Name { get; private set; }
    
    /// <summary>
    /// Gets the user's age.
    /// </summary>
    public int Age { get; private set; }
    
    /// <summary>
    /// Gets the user's email.
    /// </summary>
    public string Email { get; private set; }
    
    /// <summary>
    /// Gets the user's mobile.
    /// </summary>
    public string Mobile { get; private set; }
    
    /// <summary>
    /// Gets the user's password.
    /// </summary>
    public string Password { get; private set; }
    
    /// <summary>
    /// Gets the user's location.
    /// </summary>
    public string Location { get; private set; }
    
    /// <summary>
    /// Initialises a new instance of the <see cref="User"/> class with
    /// the specified details.
    /// </summary>
    /// <param name="name"> The name of the user. </param>
    /// <param name="age"> The age of the user. </param>
    /// <param name="email"> The email of the user. </param>
    /// <param name="mobile"> The mobile of the user. </param>
    /// <param name="password"> The password of the user. </param>
    /// <param name="location"> The location of the user. </param>
    protected User(string name, int age, string email, string mobile, string password, string location)
    {
        Name = name;
        Age = age;
        Email = email;
        Mobile = mobile;
        Password = password;
        Location = location;
    }

    /// <summary>
    /// Adds a user to the registry by passing the specified parameters
    /// to <see cref="UserRegister.RegisterUser()"/>.
    /// </summary>
    /// <param name="userType"> The type of user being registered. </param>
    /// <param name="user"> The user instance to associate with the specified user type. </param>
    public static void AddUser(UserType userType, User user)
    {
        UserRegistry.RegisterUser(userType, user);
    }
}
