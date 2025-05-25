using System;
using Entities;
using UI;

namespace UIComponents;

/// <summary>
/// Contains various static methods for handling I/O associated with the <see cref="RegistrationProcess"/>.
/// </summary>
public static class RegistrationIO
{
    /// <summary>
    /// Continuously prompts the <see cref="User"/> for a name until a valid input is provided.
    /// </summary>
    /// <remarks>
    /// The input is passed to <see cref="IOUtilities.IsValidName"/> for validation.
    /// If invalid, an error message is displayed, and the <see cref="User"/> is prompted again.
    /// </remarks>
    /// <returns>
    /// The validated and sanitized name as a string. This method loops until a valid input is provided.
    /// </returns>
    public static string GetName()
    {
        while (true)
        {
            DisplayIO.DisplayMessage("Please enter your name:");

            string name = DisplayIO.ReadInput();

            if (IOUtilities.IsValidName(name)) return name;
            else DisplayIO.DisplayMessage("Invalid name.");
        }
    }

    /// <summary>
    /// Continuously prompts the <see cref="User"/> for their age until a valid input is provided.
    /// </summary>
    /// <remarks>
    /// The input is first validated using <see cref="int.TryParse()"/> to ensure it is a number.
    /// If conversion succeeds, the integer is passed to <see cref="IOUtilities.IsValidAge()"/> 
    /// to verify that it falls within the allowed range (18-100).
    /// If the input is invalid, an error message is displayed, and the <see cref="User"/> is prompted again.
    /// </remarks>
    /// <returns>
    /// The validated age as an integer. This method loops until a valid input is provided.
    /// </returns>
    public static int GetAge()
    {
        while (true)
        {
            DisplayIO.DisplayMessage("Please enter your age (18-100):");

            if (int.TryParse(DisplayIO.ReadInput(), out int age))
            {
                if (IOUtilities.IsValidAge(age)) return age;
                else DisplayIO.DisplayMessage("Invalid age.");
            }

            else DisplayIO.DisplayMessage("Invalid age.");
        }
    }

    /// <summary>
    /// Continuously prompts the <see cref="User"/> for an email address until a valid, non-registered input is provided.
    /// </summary>
    /// <remarks>
    /// The input is validated using <see cref="IOUtilities.IsValidEmail(string)"/> to ensure it meets formatting criteria.
    /// If the email is valid, it checks <see cref="UserRegistry._userRegistry"/> to determine if it is already registered.
    /// If the email is already in use, an error message is displayed, and the <see cref="User"/> is prompted again.
    /// If the email is not registered, it is returned to the calling code for further processing.
    /// </remarks>
    /// <returns>
    /// The validated email as a string. This method loops until a valid input is provided.
    /// </returns>
    public static string GetEmail()
    {
        while (true)
        {
            DisplayIO.DisplayMessage("Please enter your email address:");

            string email = DisplayIO.ReadInput();

            if (IOUtilities.IsValidEmail(email))
            {
                if (User.EmailExists(email))
                {
                    DisplayIO.DisplayMessage("This email address is already in use.");
                    continue;
                }

                else return email;
            }

            else DisplayIO.DisplayMessage("Invalid email address.");
        }
    }

    /// <summary>
    /// Continuously prompts the <see cref="User"/> for a mobile phone number until a valid input is provided.
    /// </summary>
    /// <remarks>
    /// The input is passed to <see cref="IOUtilities.IsValidMobile(string)"/> for validation.
    /// If the input is invalid, an error message is displayed, and the <see cref="User"/> is prompted again.
    /// This method loops until a valid mobile number is entered.
    /// </remarks>
    /// <returns>
    /// The validated mobile number as a string.
    /// </returns>
    public static string GetMobile()
    {
        while (true)
        {
            DisplayIO.DisplayMessage("Please enter your mobile phone number:");

            string mobile = DisplayIO.ReadInput();

            if (IOUtilities.IsValidMobile(mobile)) return mobile;
            else DisplayIO.DisplayMessage("Invalid phone number.");
        }
    }

    /// <summary>
    /// Continuously prompts the <see cref="User"/> to enter a valid password and confirm it.
    /// </summary>
    /// <remarks>
    /// The first input is validated using <see cref="IOUtilities.IsValidPassword(string)"/> to ensure it meets security criteria.
    /// If valid, the <see cref="User"/> is prompted to re-enter the password for confirmation.
    /// The second input is validated using <see cref="IOUtilities.IsValidPasswordMatch(string, string)"/> to ensure both passwords match.
    /// If the password is invalid or mismatched, an error message is displayed, and the process repeats.
    /// This method loops until a valid and matching password is entered.
    /// </remarks>
    /// <returns>
    /// The validated password as a string.
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
            DisplayIO.DisplayMessage(PASSWORD_PROMPT);

            string firstPasswordInput = DisplayIO.ReadInput();

            if (IOUtilities.IsValidPassword(firstPasswordInput))
            {
                DisplayIO.DisplayMessage("Please confirm your password:");
                string secondPasswordInput = DisplayIO.ReadInput();

                if (IOUtilities.IsValidPasswordMatch(firstPasswordInput, secondPasswordInput))
                {
                    return firstPasswordInput;
                }

                else
                {
                    DisplayIO.DisplayMessage("Passwords do not match.");
                    continue;
                }
            }

            else DisplayIO.DisplayMessage("Invalid password.");
        }
    }

    /// <summary>
    /// Continuously prompts the <see cref="User"/> for a location until a valid input is provided.
    /// </summary>
    /// <remarks>
    /// The input is validated using <see cref="IOUtilities.IsValidLocation(string)"/> to ensure it meets required criteria.
    /// If the input is invalid, an error message is displayed, and the <see cref="User"/> is prompted again.
    /// This method loops until a valid location is entered.
    /// </remarks>
    /// <returns>
    /// The validated location as a string.
    /// </returns>
    public static string GetLocation()
    {
        const string ENTER_LOCATION_STR = "Please enter your location (in the form of X,Y):";

        while (true)
        {
            DisplayIO.DisplayMessage(ENTER_LOCATION_STR);

            string location = DisplayIO.ReadInput();

            if (IOUtilities.IsValidLocation(location)) return location;
            else DisplayIO.DisplayMessage("Invalid location.");
        }
    }

    /// <summary>
    /// Continuously prompts the <see cref="User"/> to enter a valid licence plate.
    /// </summary>
    /// <remarks>
    /// The input is validated using <see cref="IOUtilities.IsValidLicencePlate(string)"/> to ensure it meets the required criteria.
    /// If the input is invalid, an error message is displayed, and the <see cref="User"/> is prompted again.
    /// This method loops until a valid licence plate is entered.
    /// </remarks>
    /// <returns>
    /// The validated licence plate as a string.
    /// </returns>
    public static string GetLicencePlate()
    {
        while (true)
        {
            DisplayIO.DisplayMessage("Please enter your licence plate:");

            string licencePlate = DisplayIO.ReadInput();

            if (IOUtilities.IsValidLicencePlate(licencePlate)) return licencePlate;
            else DisplayIO.DisplayMessage("Invalid licence plate.");
        }
    }

    /// <summary>
    /// Continuously prompts the <see cref="User"/> to enter a valid <see cref="Restaurant"/> name.
    /// </summary>
    /// <remarks>
    /// The input is validated using <see cref="IOUtilities.IsValidRestaurantName(string)"/> to ensure it meets the required criteria.
    /// If the input is invalid, an error message is displayed, and the <see cref="User"/> is prompted again.
    /// This method loops until a valid restaurant name is entered.
    /// </remarks>
    /// <returns>
    /// The validated restaurant name as a string.
    /// </returns>
    public static string GetRestaurantName()
    {
        while (true)
        {
            DisplayIO.DisplayMessage("Please enter your restaurant's name:");

            string restaurantName = DisplayIO.ReadInput();

            if (IOUtilities.IsValidRestaurantName(restaurantName)) return restaurantName;
            else DisplayIO.DisplayMessage("Invalid restaurant name.");
        }
    }

    /// <summary>
    /// Continuously prompts the <see cref="User"/> to select a restaurant style until a valid choice is provided.
    /// </summary>
    /// <remarks>
    /// The input is retrieved using <see cref="DisplayIO.GetChoice()"/> and validated using <see cref="IOUtilities.IsValidRestaurantStyle(int)"/>.
    /// If the input is invalid, an error message is displayed, and the <see cref="User"/> is prompted again.
    /// Upon validation, the integer choice is cast to a <see cref="RestaurantStyles"/> enum and returned.
    /// This method loops until a valid input is provided.
    /// </remarks>
    /// <returns>
    /// The validated <see cref="Restaurant"/> style as a <see cref="RestaurantStyles"/> enum.
    /// </returns>
    public static RestaurantStyles GetRestaurantStyle()
    {
        const string STYLE_PROMPT_STR = """
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
            DisplayIO.DisplayMessage(STYLE_PROMPT_STR);

            int restaurantStyle = DisplayIO.GetChoice();

            if (IOUtilities.IsValidRestaurantStyle(restaurantStyle))
            {
                return (RestaurantStyles)restaurantStyle;
            }
            else DisplayIO.InvalidChoice();
        }
    }
}
