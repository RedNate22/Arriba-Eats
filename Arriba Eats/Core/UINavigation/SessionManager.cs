using System;
using Entities;
using DisplayIO;

namespace UINavigation;

// TODO xml
public static class SessionManager
{
    private static User? _currentUser = null;

    // TODO xml
    public static User? CurrentUser 
    { 
        get { return _currentUser; } 
        private set { _currentUser = value; } 
    }

    // TODO xml
    public static void AuthenticateSession(User currentUser)
    {
        CurrentUser = currentUser;
    }

    /// <summary>
    /// Attempts to authenticate a user by prompting them to enter an email and password.
    /// <para> Passes the credentials to <see cref="User.AuthenticateUser()"/> which
    /// then pass them to <see cref="UserRegistry.TryVerifyUserCredentials()"/>. </para>
    /// <para> 
    /// If a match is found within the registry, the associated user instance
    /// is passed back up the chain to this method, which should then be assigned to 
    /// <see cref="CurrentUser"/> to finalise the authentication process. 
    /// </para>
    /// <para>
    /// If the credentials are invalid a <c>null</c> value is returned, and the user is prompted. 
    /// The user should then be returned to the main menu.
    /// </para>
    /// </summary>
    /// <returns> The user instance of the authenticated user, otherwise <c>null</c>. </returns>
    public static User? Login()
    {
        const string EMAIL_STR = "Email: ";
        const string PASSWORD_STR = "Password: ";
        const string INVALID_EMAIL_PASSWORD = "Invalid email or password.";

        IODisplay.DisplayMessageSingleLine(EMAIL_STR);
        string email = IODisplay.ReadInput();
        
        IODisplay.DisplayMessageSingleLine(PASSWORD_STR);
        string password = IODisplay.ReadInput();
        
        IODisplay.DisplayEmptyLine();

        User? user = User.AuthenticateUser(email, password);
        if (user == null)
        {
            IODisplay.DisplayMessage(INVALID_EMAIL_PASSWORD);
            return null;
        }

        else return user;
    }

    // TODO xml
    public static UserType ReturnUserType(User user)
    {
        return User.GetUserType(user);
    }
}
