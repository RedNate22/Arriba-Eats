using System;
using Entities;
using UI;
using UINavigation;

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
    
    /// <summary> Writes the specified string message to the screen on a new line. </summary>
    /// <param name="message"> The string message to display to the screen. </param>
    public static void DisplayMessage(string message)
    {
        Console.WriteLine(message);
    }

    /// <summary> Writes the specified string message to the screen on the same line. </summary>
    /// <param name="message"> The string message to display to the screen. </param>
    public static void DisplayMessageSingleLine(string message)
    {
        Console.Write(message);
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
            
            string name = ReadInput();

            if (IOUtilities.IsValidName(name)) return name;
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

            if (int.TryParse(ReadInput(), out int age))
            {
                if (IOUtilities.IsValidAge(age)) return age;
                else DisplayMessage("Invalid age.");
            }
            
            else DisplayMessage("Invalid age.");
        }
    }


    /// <summary>
    /// Continously reads a string input from the user via the console until it meets
    /// the validation criteria.
    /// <para> If the email is invalid, an error message is displayed and the user
    /// is prompted again until a valid input is given. </para>
    /// <para> If the email is valid, it then checks the <see cref="UserRegistry.userDictionary"/>
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
            DisplayMessage("Please enter your email address:");

            string email = ReadInput();
            
            if (IOUtilities.IsValidEmail(email))
            {
                if (User.EmailExists(email))
                {
                    DisplayMessage("This email address is already in use.");
                    continue;
                }

                else return email;
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

            string mobile = ReadInput();

            if (IOUtilities.IsValidMobile(mobile)) return mobile;
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

            string firstPasswordInput = ReadInput();

            if (IOUtilities.IsValidPassword(firstPasswordInput))
            {
                DisplayMessage("Please confirm your password:");
                string secondPasswordInput = ReadInput();

                if (IOUtilities.IsValidPasswordMatch(firstPasswordInput, secondPasswordInput))
                {
                    return firstPasswordInput;
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

            string location = ReadInput();

            if (IOUtilities.IsValidLocation(location)) return location;
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

            string licencePlate = ReadInput();

            if (IOUtilities.IsValidLicencePlate(licencePlate)) return licencePlate;
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

            string restaurantName = ReadInput();

            if (IOUtilities.IsValidRestaurantName(restaurantName)) return restaurantName;
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

            int restaurantStyle = GetChoice();

            if (IOUtilities.IsValidRestaurantStyle(restaurantStyle))
            {
                return (RestaurantStyles)restaurantStyle;
            }
        }
    }

    /// <summary>
    /// Displays the relevant user information, depending on the <see cref="UserType"/>.
    /// </summary>
    /// <param name="user"></param>
    public static void DisplayUserInfo(User user)
    {
        UserType userType = SessionManager.ReturnUserType();

        DisplayMessage("Your user details are as follows:");
        DisplayMessage($"Name: {user.Name}");
        DisplayMessage($"Age: {user.Age}");
        DisplayMessage($"Email: {user.Email}");
        DisplayMessage($"Mobile: {user.Mobile}");
    
        switch (userType)
        {
            case UserType.Customer:
                var customer = (Customer)user;
                DisplayMessage($"Location: {customer.Location}");
                DisplayMessage($"You've made 0 order(s) and spent a total of $0.00 here.");
                break;
            
            case UserType.Deliverer:
                var deliverer = (Deliverer)user;
                DisplayMessage($"Licence plate: {deliverer.LicencePlate}");
                
                // If (the deliverer has an order not yet delivered)
                DisplayMessage($"""
                    Current delivery:
                    Order #ORDER_NO from RESTAURANT_NAME at RX,RY.
                    To be delivered to CUSTOMER_NAME at CX,CY.
                    """); 
                break;
            
            case UserType.Client:
                var client = (Client)user;
                DisplayMessage($"Restaurant name: {client.RestaurantName}");
                DisplayMessage($"Restaurant style: {client.RestaurantStyle}");
                DisplayMessage($"Restaurant location: {client.Location}");
                break;
        }
    }
    
    /// <summary>
    /// Welcomes the current <see cref="User"/> by their <see cref="User.Name"/>.
    /// </summary>
    public static void WelcomeUser()
    {
        if (SessionManager.CurrentUser != null) 
        {
            DisplayMessage($"Welcome back, {SessionManager.CurrentUser.Name}!");
        }

        else
        {
            DisplayMessage("No user is currently logged in.");
        }
    }
}
