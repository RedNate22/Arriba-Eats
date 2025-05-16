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
    /// to <see cref="UserRegistry.RegisterUser()"/>.
    /// </summary>
    /// <param name="userType"> The type of user being registered. </param>
    /// <param name="user"> The user instance to associate with the specified user type. </param>
    public static void AddUser(UserType userType, User user)
    {
        UserRegistry.RegisterUser(userType, user);
    }
    
    /// <summary>
    /// Checks whether the provided email exists with the <see cref="UserRegistry._userRegistry"/>.
    /// </summary>
    /// <param name="email"> The email address to search for in the registry. </param>
    /// <returns>
    /// <c>true</c> if a user with the specified email address currently exists in the registry,
    /// otherwise, <c>false</c>.
    /// </returns>
    public static bool EmailExists(string email)
    {
        if (UserRegistry.EmailInRegistry(email))
        {
            return true;
        }
        else return false;
    }

    /// <summary>
    /// Passes the user credentials to <see cref="UserRegistry.TryVerifyUserCredentials()"/>
    /// in an attempt to find a matching user. If successful, returns the instance of 
    /// the authenticated user.
    /// </summary>
    /// <param name="email"> The email to verify. </param>
    /// <param name="password"> The password to verify. </param>
    /// <returns> The instance of the authenticated user, otherwise <c>null</c>. </returns>
    public static User? AuthenticateUser(string email, string password)
    {
        if (UserRegistry.TryVerifyUserCredentials(email, password, out User? foundUser))
        {
            return foundUser;
        }
        
        else return null;
    }

    /// <summary>
    /// Retrieves the <see cref="UserType"/> associated with the given <see cref="User"/>
    /// by searching the <see cref="UserRegistry"/>.
    /// </summary>
    /// <param name="user"> The user instance whose type is being determined. </param>
    /// <returns> 
    /// The corresponding <see cref="UserType"/> of the user, otherwise <see cref="UserType.Default"/>.
    /// </returns>
    public static UserType GetUserType(User user)
    {
        return UserRegistry.TryFindUserType(user, out UserType foundUserType)
            ? foundUserType
            : UserType.Default;
    } 
}
