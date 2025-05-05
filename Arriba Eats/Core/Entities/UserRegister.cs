using System;

namespace Entities;

public static class UserRegister
{
    private static Dictionary<UserType, User> userDictionary = new();

    public static void RegisterUser(UserType userType, User user)
    {
        userDictionary.Add(userType, user);
    }
}
