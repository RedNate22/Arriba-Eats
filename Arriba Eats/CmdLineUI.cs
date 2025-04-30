using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAB201Menu
{
    static class CmdLineUI
    {


        /// <summary>
        /// Displays a blank line to the screen.
        /// </summary>
        public static void DisplayMessage()
        {
            Console.WriteLine();
        }

        /// <summary>
        /// Displays a message to the screen.
        /// </summary>
        /// <param name="msg">The message to display</param>
        public static void DisplayMessage(string msg)
        {
            Console.WriteLine(msg);
        }


        /// <summary>
        /// Displays a an object's ToString method 
        /// </summary>
        /// <param name="msg">The message to display</param>
        public static void DisplayMessage(object o)
        {
            Console.WriteLine(o.ToString());
        }


        /// <summary>
        /// Displays an error message to the screen.
        /// </summary>
        /// <param name="msg">The message to display</param>
        public static void DisplayError(string msg)
        {
            Console.WriteLine("#####");
            Console.WriteLine($"#Error - {msg}.");
            Console.WriteLine("#####");
        }

        /// <summary>
        /// Displays an error message and asks the user to try again.
        /// </summary>
        /// <param name="msg">The message to display</param>
        public static void DisplayErrorAgain(string msg)
        {

            Console.WriteLine("#####");
            Console.WriteLine($"#Error - {msg}, please try again.");
            Console.WriteLine("#####");
        }



        /// <summary>
        /// Reads in a string from console and returns it
        /// </summary>
        /// <returns>A string representation of the users input</returns>
        public static string GetString()
        {
            string input = Console.ReadLine();
            return input;
        }

        /// <summary>
        /// Reads in a string from console, converts it to a Int32
        /// and returns the converted value
        /// </summary>
        /// <returns>A Int32 representation of the users input</returns>
        public static int GetInt()
        {
            string input = Console.ReadLine();
            int i = int.Parse(input);
            return i;
        }

        /// <summary>
        /// Print the message and the,
        /// Reads in a string from console, converts it to a Int32
        /// and returns the converted value
        /// </summary>
        /// <param name="msg">A message to print out</param>
        /// <returns>A Int32 representation of the users input</returns>      
        public static int GetInt(string msg)
        {
            Console.WriteLine($"{msg}");
            string input = Console.ReadLine();
            int i = int.Parse(input);
            return i;
        }

        /// <summary>
        /// Reads in a string from console, converts it to a double
        /// and returns the converted value
        /// </summary>
        /// <returns>A double floating point representation of the users input</returns>
        public static double GetDouble()
        {
            string input = Console.ReadLine();
            double d = Double.Parse(input);
            return d;
        }



        /// <summary>
        /// Prints message (msg) to the screen.
        /// Reads in a string from console, converts it to a boolean
        /// and returns the converted value
        /// </summary>
        /// <returns>A boolean representation of the users input</returns>
        public static bool GetBool()
        {
            string input = Console.ReadLine();
            bool b = Boolean.Parse(input);
            return b;
        }


        /// <summary>
        /// Reads in a string and converts it to a DateTime using the QUT consistent HH:mm dd/MM/yyyy format
        /// All dates must be in this format so 1/2/2000 should be 01/02/2000 or you will ge an error
        /// https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparseexact?view=net-8.0
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDateTime()
        {
            string input = Console.ReadLine();
            DateTime result;
            string format = QUTConsts.DATETIMEFORMAT; // Expected format is "HH:mm dd/MM/yyyy"
            bool dtWorked = DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
            if (!dtWorked)
            {
                DisplayError("Incorrect Date Format");
            }
            return result;
        }

        /// <summary>
        /// Displays a menu, with the options numbered from 1 to options.Length,
        /// the gets a validated integer in the range 1..options.Length. 
        /// Subtracts 1, then returns the result. If the supplied list of options 
        /// is empty, returns an error value (-1).
        /// </summary>
        /// <param name="title">A heading to display before the menu is listed.</param>
        /// <param name="options">The list of objects to be displayed.</param>
        /// <returns>Return value is either -1 (if no options are provided) or a 
        /// value in 0 .. (options.Length-1).</returns>
        public static int GetOption(string title, params object[] options)
        {
            // Defensive error checking - There should be at least 1 option, but we need double check.
            if (options.Length <= 0)
            {
                return -1;
            }

            // Setting up some formatting so the menu looks nice
            Console.WriteLine(title);
            int digitsNeeded = (int)(1 + Math.Floor(Math.Log10(options.Length)));
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{(i + 1).ToString().PadLeft(digitsNeeded)}. {options[i]}");
            }

            // Highlighting the importance of diversity. The upper limit will depend of the number of elements passed.
            int option = GetInt($"Please enter a choice between 1 and {options.Length}.");

            // need to subtract 1 to align because programers count from zero 
            return option - 1; 
        }



    }
}
