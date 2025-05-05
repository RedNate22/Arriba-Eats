using System;

namespace Entities;

public static class UserRegister
{
    private static Dictionary<UserType, User> userDictionary = new();

    public static void AddUser(UserType userType, User user)
    {
        userDictionary.Add(userType, user);
    }
}
