using System;

namespace Entities;

/// <summary>
/// Provides a centralised registry for storing and retrieving <see cref="User"/>s
/// based on their <see cref="UserType"/>.
/// <para> 
/// Provides several internal methods to safely store and retrieve user data
/// from <see cref="_userRegistry"/>.
/// </para> 
/// </summary>
internal static class UserRegistry
{
    /// <summary>
    /// A dictionary that maps <see cref="UserType"/>'s to their corresponding <see cref="User"/> instances.
    /// <para> Stores instances of users in lists, associated with the <see cref="UserType"/> as the key.</para>
    /// </summary>
    private static Dictionary<UserType, List<User>> _userRegistry = new Dictionary<UserType, List<User>>();

    /// <summary>
    /// Registers a new <see cref="User"/> into the registry using the specified <see cref="UserType"/> to 
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
        if (!_userRegistry.ContainsKey(userType))
        {
            _userRegistry[userType] = new List<User>();
        }
        _userRegistry[userType].Add(user);
    }

    /// <summary>
    /// Searches the <see cref="_userRegistry"/> to determine if any registered
    /// <see cref="User"/> contains an email address matching the provided input.
    /// </summary>
    /// <param name="email"> The email address to check against existing users. </param>
    /// <returns>
    /// <c>true</c> if a match is found within the registry, otherwise <c>false</c>.
    /// </returns>
    internal static bool EmailInRegistry(string email)
    {
        foreach (List<User> userList in _userRegistry.Values)
        {
            foreach (User user in userList)
            {
                if (email == user.Email)
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Attempts to verify the user credentials by searching the <see cref="_userRegistry"/>
    /// and finding a match.
    /// </summary>
    /// <param name="email"> The email address to match with the registered user. </param>
    /// <param name="password"> The password to match with the registered user. </param>
    /// <param name="foundUser"> The instance of the user if both email and password match. </param>
    /// <returns> <c>true</c> if a match is found, otherwise, <c>false</c>. </returns>
    internal static bool TryVerifyUserCredentials (string email, string password, out User? foundUser)
    {
        foreach (List<User> userList in _userRegistry.Values)
        {
            foreach (User user in userList)
            {
                if (email == user.Email && password == user.Password)
                {
                    foundUser = user;
                    return true;
                }
            }    
        }

        foundUser = null;
        return false;
    }

    /// <summary>
    /// Attempts to determine the <see cref="UserType"/> associated with the given
    /// <see cref="User"/> by searching the <see cref="_userRegistry"/>.
    /// </summary>
    /// <param name="user"> The user instance whose type is being identified. </param>
    /// <param name="foundUserType"> The found <see cref="UserType"/> associated with the user. </param> 
    /// <returns> <c>true</c> if the user is found in the registry and their <see cref="UserType"/>, 
    /// is assigned. Otherwise <c>false</c>, with <see cref="UserType.Default"/> assigned. </returns>
    internal static bool TryFindUserType (User user, out UserType foundUserType)
    {
        foreach (KeyValuePair<UserType, List<User>> pair in _userRegistry)
        {
            if (pair.Value.Contains(user))
            {
                foundUserType = pair.Key;
                return true;
            }
        }
        
        foundUserType = UserType.Default;
        return false;
    }
}
