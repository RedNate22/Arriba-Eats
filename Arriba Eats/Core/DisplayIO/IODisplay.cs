using System;
using Entities;

namespace DisplayIO;

/// <summary>
/// Handles input and output for the menus using the console.
/// <para> Uses <see cref="DisplayIO.IOUtilities"/> for input & output formatting and validation. </para>
/// </summary>
public static class IODisplay
{
    /// <summary>
    /// Writes an empty line to the screen, to be used as a line break between selections and menus.
    /// </summary>
    public static void DisplayEmptyLine()
    {
        Console.WriteLine();
    }
    
    /// <summary> Writes the specified string message to the screen. </summary>
    /// <param name="message">The string message to display to the screen.</param>
    public static void DisplayMessage(string message)
    {
        Console.WriteLine(message);
    }
    
    /// <summary>
    /// Uses <see cref="Console.ReadLine"/> to read an input from the user and 
    /// returns this as a string.
    /// <para> Trims any leading and trailing whitespace from the string. </para>
    /// <para> Assigns an empty value if the original input is empty. </para>
    /// </summary>
    /// <returns> The input as a string. </returns>
    public static string ReadInput()
    {
        string input = Console.ReadLine()?.Trim() ?? "";
        return input;
    }

    /// <summary>
    /// Reads a string input from the user via the console.
    /// <para> Attempts to convert the input into an integer. </para>
    /// </summary>
    /// <Returns> The integer value if valid, 
    /// or a default value (-1) if the input is invalid. </Returns>
    public static int GetChoice()
    {
        string? choice = ReadInput();
        
        if (int.TryParse(choice, out int result)) return result;
        else return -1;
    }

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
            DisplayMessage("Please enter your name:");
            
            string input = ReadInput();

            if (IOUtilities.IsValidName(input)) return input;
            else DisplayMessage("Invalid name.");
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
            DisplayMessage("Please enter your age (18-100):");

            if (int.TryParse(ReadInput(), out int input))
            {
                if (IOUtilities.IsValidAge(input)) return input;
                else DisplayMessage("Invalid age.");
            }
            
            else DisplayMessage("Invalid age.");
        }
    }


    // TODO XML
    /// <para> If the input is invalid, an error message is displayed and the user
    /// is prompted again until a valid input is given. </para>
    public static string GetEmail()
    {
        while (true)
        {
            DisplayMessage("Please enter your email address:");

            string input = ReadInput();
            
            if (IOUtilities.IsValidEmail(input))
            {
                if (User.EmailExists(input))
                {
                    DisplayMessage("This email address is already in use.");
                    continue;
                }

                else return input;
            }
            
            else DisplayMessage("Invalid email address.");
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
            DisplayMessage("Please enter your mobile phone number:");

            string input = ReadInput();

            if (IOUtilities.IsValidMobile(input)) return input;
            else DisplayMessage("Invalid phone number.");
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
            DisplayMessage(PASSWORD_PROMPT);

            string firstInput = ReadInput();

            if (IOUtilities.IsValidPassword(firstInput))
            {
                DisplayMessage("Please confirm your password:");
                string secondInput = ReadInput();

                if (IOUtilities.IsValidPasswordMatch(firstInput, secondInput))
                {
                    return firstInput;
                }

                else
                {
                    DisplayMessage("Passwords do not match.");
                    continue;
                }
            }

            else DisplayMessage("Invalid password.");
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
            DisplayMessage("Please enter your location (in the form of X,Y):");

            string input = ReadInput();

            if (IOUtilities.IsValidLocation(input)) return input;
            else DisplayMessage("Invalid location.");
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
            DisplayMessage("Please enter your licence plate:");

            string input = ReadInput();

            if (IOUtilities.IsValidLicencePlate(input)) return input;
            else DisplayMessage("Invalid licence plate.");
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
            DisplayMessage("Please enter your restaurant's name:");

            string input = ReadInput();

            if (IOUtilities.IsValidRestaurantName(input)) return input;
            else DisplayMessage("Invalid restaurant name.");
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
            DisplayMessage(STYLE_PROMPT);

            int choice = GetChoice();

            if (IOUtilities.IsValidRestaurantStyle(choice))
            {
                DisplayMessage($"{(RestaurantStyles)choice}");
                return (RestaurantStyles)choice;
            }
        }
    }
}
