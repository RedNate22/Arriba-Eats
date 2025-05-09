using System;
using Entities;

namespace UINavigation;

/// <summary>
/// 
/// </summary>
public static class SessionManager
{
    private static User? _currentUser = null;

    /// <summary>
    /// 
    /// </summary>
    public static User? CurrentUser 
    { 
        get { return _currentUser; } 
        set { _currentUser = value; } 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="currentUser"></param>
    public static void AuthenticateSession(User currentUser)
    {
        CurrentUser = currentUser;
    }
}
