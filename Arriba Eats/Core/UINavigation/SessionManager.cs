using System;
using Entities;
using UIComponents;

namespace UINavigation;

/// <summary>
/// Manages the authentication state of a <see cref="User"/> within the application.
/// This class tracks the currently logged-in <see cref="User"/> and defines the methods for 
/// login and logout functionality.
/// <para> The session state is updated upon authentication and determines access to relevant menus
/// based on the <see cref="User"/>'s <see cref="UserType"/> through the <see cref="UI.LoginMenu"/>. </para> 
/// </summary>
public static class SessionManager
{
    private static User? _currentUser = null;

    /// <summary>
    /// Gets the current <see cref="User"/> logged into the application.
    /// </summary>
    public static User? CurrentUser 
    { 
        get { return _currentUser; } 
        private set { _currentUser = value; } 
    }

    /// <summary>
    /// Updates the application to recognise the current user logged into the application.
    /// <para> Based on the <see cref="UserType"/> of the active user, directs
    /// the application to display the appropriate menus. 
    /// i.e. <see cref="MenuState.CustomerMainMenu"/>.</para>
    /// </summary>
    /// <param name="currentUser"> The currently authenticated <see cref="User"/>. </param>
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
    /// is passed back up the chain to this method, which should then be assigned
    /// via <see cref="AuthenticateSession()"/> to finalise the authentication process. 
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

        IODisplay.DisplayMessage(EMAIL_STR);
        string email = IODisplay.ReadInput();
        
        IODisplay.DisplayMessage(PASSWORD_STR);
        string password = IODisplay.ReadInput();
        
        User? user = User.AuthenticateUser(email, password);
        if (user == null)
        {
            IODisplay.DisplayMessage(INVALID_EMAIL_PASSWORD);
            return null;
        }

        else return user;
    }

    /// <summary>
    /// Logs the user out by removing the referenced <see cref="User"/> from <see cref="CurrentUser"/>.
    /// Then changes to the Main Menu via <see cref="UIFlowController.ChangeMenu()"/>. 
    /// </summary>
    public static void Logout()
    {
        CurrentUser = null;
        IODisplay.DisplayMessage("You are now logged out.");
        UIFlowController.ChangeMenu(MenuState.MainMenu);
    }

    /// <summary>
    /// Retrieves the <see cref="UserType"/> of the currently authenticated user.
    /// </summary>
    /// <returns> 
    /// The associated <see cref="UserType"/> if a user is logged in,
    /// otherwise, returns <see cref="UserType.Default"/>.
    /// </returns>
    public static UserType ReturnUserType()
    {
        if (CurrentUser == null)
        {
            return UserType.Default;
        }

        else return User.GetUserType(CurrentUser);
    }
}
