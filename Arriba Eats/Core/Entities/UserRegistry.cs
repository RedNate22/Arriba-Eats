using System;

namespace Entities;

/// <summary>
/// Provides a centralised registry for storing and retrieving users 
/// based on their <see cref="UserType"/>.
/// <para> 
/// Provides several internal methods to safely store and retrieve user data
/// from <see cref="userDictionary"/>.
/// </para> 
/// </summary>
internal static class UserRegistry
{
    /// <summary>
    /// A dictionary that maps user types to their corresponding user instances.
    /// <para> Stores instances of users in lists, associated with the <see cref="UserType"/>.</para>
    /// </summary>
    private static Dictionary<UserType, List<User>> userDictionary = new();

    /// <summary>
    /// Registers a new user in the registry using the specified <see cref="UserType"/> to 
    /// dynamically add the user into the appropriate list.
    /// <para> Checks if a list associated with the given <see cref="UserType"/>
    /// exists, and if <c>true</c>, stores the user into the existing list. If <c>false</c>,
    /// creates a new list for the new <see cref="UserType"/> and stores the instance into this list. </para>
    /// </summary>
    /// <param name="userType"> The type of user being registered. </param>
    /// <param name="user"> The user instance to be stored into the list,
    /// associated with the specified user type. </param>
    internal static void RegisterUser(UserType userType, User user)
    {
        if (!userDictionary.ContainsKey(userType))
        {
            userDictionary[userType] = new List<User>();
        }
        userDictionary[userType].Add(user);
    }

    /// <summary>
    /// Searches the <see cref="userDictionary"/> to determine if any registered
    /// <see cref="User"/> contains an email address matching the provided input.
    /// </summary>
    /// <param name="email"> The email address to check against existing users. </param>
    /// <returns>
    /// <c>true</c> if a match is found within the registry, otherwise <c>false</c>.
    /// </returns>
    internal static bool EmailInRegistry(string email)
    {
        foreach (List<User> userList in userDictionary.Values)
        {
            foreach (User user in userList)
            {
                if (user.Email == email)
                {
                    return true;
                }
            }
        }
        return false;
    }

    internal static void FindUserByCredentials(string email, string password)
    {

    }
}
