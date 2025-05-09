using System;

namespace Entities;

/// <summary>
/// Provides a centralised registry for storing and retrieving users 
/// based on their <see cref="UserType"/>.
/// </summary>
public static class UserRegistry
{
    /// <summary>
    /// A dictionary that maps user types to their corresponding user instances.
    /// </summary>
    private static Dictionary<UserType, User> userDictionary = new();

    /// <summary>
    /// Registers a new user in the registry using the specified user type.
    /// </summary>
    /// <param name="userType"> The type of user being registered. </param>
    /// <param name="user"> The user instance to associate with the specified user type. </param>
    public static void RegisterUser(UserType userType, User user)
    {
        userDictionary.Add(userType, user);
    }

    // TODO add check (try/except(?)) for adding a user already registered^^^
}
