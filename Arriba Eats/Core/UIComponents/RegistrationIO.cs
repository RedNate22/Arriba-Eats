using System;
using Entities;
using UINavigation;

namespace UIComponents;

/// <summary>
/// Contains various static methods for handling I/O with the user 
/// during the <see cref="UI.RegistrationProcess"/>.
/// <para> Uses <see cref="IOUtilities"/> for input & output formatting and validation. </para>
/// </summary>
public static class RegistrationIO
{
    /// <summary>
    /// Continuously reads a string input from the user via the console until it meets
    /// the validation criteria.
    /// <para> 
    /// Passes the string to <see cref="IOUtilities.IsValidName"/> to verify the input contains only
    /// valid characters and meets sanitisation requirements.
    /// </para>
    /// <para> If the input is invalid, an error message is displayed and the user
    /// is prompted again until a valid input is given. </para>
    /// </summary>
    /// <returns> The validated and sanitised name as a string. This method loops
    /// until a valid input is provided. </returns>
    public static string GetName()
    {
        while (true)
        {
            IODisplay.DisplayMessage("Please enter your name:");
            
            string name = IODisplay.ReadInput();

            if (IOUtilities.IsValidName(name)) return name;
            else IODisplay.DisplayMessage("Invalid name.");
        }
    }
    
    /// <summary>
    /// Continuously reads a string input from the user via the console until it meets 
    /// the validation criteria.
    /// <para> Attempts to convert input to int using <see cref="int.TryParse()"/>.</para>
    /// <para> 
    /// If successful, passes the integer to <see cref="IOUtilities.IsValidAge()"/>
    /// to validate whether it falls within range.
    /// </para>
    /// <para> If the input is invalid, an error message is displayed and the user
    /// is prompted again until a valid input is given. </para>
    /// </summary>
    /// <returns> The validated age as an integer. This method loops
    /// until a valid input is provided. </returns>
    public static int GetAge()
    {
        while (true)
        {
            IODisplay.DisplayMessage("Please enter your age (18-100):");

            if (int.TryParse(IODisplay.ReadInput(), out int age))
            {
                if (IOUtilities.IsValidAge(age)) return age;
                else IODisplay.DisplayMessage("Invalid age.");
            }
            
            else IODisplay.DisplayMessage("Invalid age.");
        }
    }

    /// <summary>
    /// Continously reads a string input from the user via the console until it meets
    /// the validation criteria.
    /// <para> If the email is invalid, an error message is displayed and the user
    /// is prompted again until a valid input is given. </para>
    /// <para> If the email is valid, it then checks the <see cref="UserRegistry._userDictionary"/>
    /// to determine if the email already currently exists amongst the registered users. </para>
    /// <para> If the email is already registered, it returns an error, prompting the user
    /// that email is already in use. </para>
    /// <para> If the email is not registered yet, it returns the string input to the calling code.
    /// Allowing it to be stored and passed to <see cref="User.AddUser()"/> later. </para>
    /// </summary>
    /// <returns> The validated email as a string. This method loops 
    /// until a valid input is provided. </returns>
    public static string GetEmail()
    {
        while (true)
        {
            IODisplay.DisplayMessage("Please enter your email address:");

            string email = IODisplay.ReadInput();
            
            if (IOUtilities.IsValidEmail(email))
            {
                if (User.EmailExists(email))
                {
                    IODisplay.DisplayMessage("This email address is already in use.");
                    continue;
                }

                else return email;
            }
            
            else IODisplay.DisplayMessage("Invalid email address.");
        }
    }

    /// <summary>
    /// Continuously reads a string input from the user via the console until it meets 
    /// the validation criteria.
    /// <para> 
    /// Passes the string to <see cref="IOUtilities.IsValidMobile()"/>
    /// to validate whether it meets the criteria.
    /// </para>
    /// <para> If the input is invalid, an error message is displayed and the user
    /// is prompted again until a valid input is given. </para>
    /// </summary>
    /// <returns> The validated mobile number as a string. This method loops
    /// until a valid input is provided. </returns>
    public static string GetMobile()
    {
        while (true)
        {
            IODisplay.DisplayMessage("Please enter your mobile phone number:");

            string mobile = IODisplay.ReadInput();

            if (IOUtilities.IsValidMobile(mobile)) return mobile;
            else IODisplay.DisplayMessage("Invalid phone number.");
        }
    }

    /// <summary>
    /// Continuously reads a string input from the user via the console until it meets
    /// the validation criteria. Then reads a second string input from the user
    /// and validates whether both inputs match.
    /// <para> Passes the first input to <see cref="IOUtilities.IsValidPassword()"/> to
    /// validate whether it meets the criteria. </para>
    /// <para> Then passes the second input to <see cref="IOUtilities.IsValidPasswordMatch()"/>
    /// to validate both inputs match.</para>
    /// <para> If the input is invalid, or the passwords do not match, 
    /// an error message is displayed and the user
    /// is prompted again until a valid input is given. </para>
    /// </summary>
    /// <returns> 
    /// The first input password as a string. This method loops until the 
    /// first input is valid, and matches the second input.
    /// </returns>
    public static string GetPassword()
    {
        const string PASSWORD_PROMPT = """
            Your password must:
            - be at least 8 characters long
            - contain a number
            - contain a lowercase letter
            - contain an uppercase letter
            Please enter a password:
            """;

        while (true)
        {
            IODisplay.DisplayMessage(PASSWORD_PROMPT);

            string firstPasswordInput = IODisplay.ReadInput();

            if (IOUtilities.IsValidPassword(firstPasswordInput))
            {
                IODisplay.DisplayMessage("Please confirm your password:");
                string secondPasswordInput = IODisplay.ReadInput();

                if (IOUtilities.IsValidPasswordMatch(firstPasswordInput, secondPasswordInput))
                {
                    return firstPasswordInput;
                }

                else
                {
                    IODisplay.DisplayMessage("Passwords do not match.");
                    continue;
                }
            }

            else IODisplay.DisplayMessage("Invalid password.");
        }
    }

    /// <summary>
    /// Continuously reads a string input from the user via the console until it meets
    /// the validation criteria.
    /// <para> Passes the string to <see cref="IOUtilities.IsValidLocation()"/> to
    /// validate whether it meets the criteria. </para>
    /// <para> If the input is invalid, an error message is displayed and the user
    /// is prompted again until a valid input is given. </para>
    /// </summary>
    /// <returns> The validated location as a string. This method loops
    /// until a valid input is provided. </returns>
    public static string GetLocation()
    {
        while (true)
        {
            IODisplay.DisplayMessage("Please enter your location (in the form of X,Y):");

            string location = IODisplay.ReadInput();

            if (IOUtilities.IsValidLocation(location)) return location;
            else IODisplay.DisplayMessage("Invalid location.");
        }
    }

    /// <summary>
    /// Continuously reads a string input from the user via the console until it meets
    /// the validation criteria.
    /// <para> Passes the string to <see cref="IOUtilities.IsValidLicencePlate()"/> to
    /// validate wheter it meets the criteria. </para>
    /// <para> If the input is invalid, an error message is displayed and the user
    /// is prompted again until a valid input is given. </para>
    /// </summary>
    /// <returns> The validated licence plate as a string. This method loops
    /// until a valid input is provided. </returns>
    public static string GetLicencePlate()
    {
        while (true)
        {
            IODisplay.DisplayMessage("Please enter your licence plate:");

            string licencePlate = IODisplay.ReadInput();

            if (IOUtilities.IsValidLicencePlate(licencePlate)) return licencePlate;
            else IODisplay.DisplayMessage("Invalid licence plate.");
        }
    }

    /// <summary>
    /// Continuously reads a string input from the user via the console until it meets
    /// the validation criteria.
    /// <para> Passes the string to <see cref="IOUtilities.IsValidRestaurantName()"/> to
    /// validate whether it meets the criteria. </para>
    /// <para> If the input is invalid, an error message is displayed and the user
    /// is prompted again until a valid input is given. </para>
    /// </summary>
    /// <returns> The validated restaurant name as a string. This method loops
    /// until a valid input is provided. </returns>
    public static string GetRestaurantName()
    {
        while (true)
        {
            IODisplay.DisplayMessage("Please enter your restaurant's name:");

            string restaurantName = IODisplay.ReadInput();

            if (IOUtilities.IsValidRestaurantName(restaurantName)) return restaurantName;
            else IODisplay.DisplayMessage("Invalid restaurant name.");
        }
    }
    
    /// <summary>
    /// Continuously reads a string input from the user and attempts to convert it using
    /// <see cref="GetChoice"/> via the console until it meets the validation criteria.
    /// <para> Passes the string to <see cref="IOUtilities.IsValidRestaurantStyle()"/>
    /// to validate whether it meets the criteria. </para>
    /// </summary>
    /// <returns> The validated restaurant style as a <see cref="RestaurantStyles"/> object.
    /// This method loops until a valid input is provided. </returns>
    public static RestaurantStyles GetRestaurantStyle()
    {
        const string STYLE_PROMPT = """
            Please select your restaurant's style:
            1: Italian
            2: French
            3: Chinese
            4: Japanese
            5: American
            6: Australian
            Please enter a choice between 1 and 6:
            """;

        while (true)
        {
            IODisplay.DisplayMessage(STYLE_PROMPT);

            int restaurantStyle = IODisplay.GetChoice();

            if (IOUtilities.IsValidRestaurantStyle(restaurantStyle))
            {
                return (RestaurantStyles)restaurantStyle;
            }
        }
    }
}
