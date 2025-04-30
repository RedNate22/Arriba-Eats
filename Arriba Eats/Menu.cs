using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CAB201Menu
{
    public class Menu
    {

        private QUTUnit unit;
        private bool assigned;

        /// <summary>
        /// Specified default menu constructor.
        /// The value of assigned is false
        /// </summary>
        public Menu()
        {
            assigned = false;
        }

        /// <summary>
        /// Display the header and runs the menu
        /// </summary>
        public void Run()
        {
            DisplayHeader();
            bool keepGoing = true;
            // Keep running the menu until the user chooses to exit the program
            // This will be when the user selects exit in the DisplayMainMenu method
            while (keepGoing) {
                keepGoing = DisplayMainMenu();
            } 
        }


        /// <summary>
        /// Displays the main heading 
        /// </summary>
        private void DisplayHeader()
        {
            CmdLineUI.DisplayMessage("-------------------");
            CmdLineUI.DisplayMessage("     QUT    Menu   ");
            CmdLineUI.DisplayMessage("-------------------");

        }

        /// <summary>
        /// Displays the main menu to the screen
        /// </summary>
        /// <returns>Returns FALSE is the user wants to exit the program,  
        /// otherwise returns TRUE
        /// </returns>
        private bool DisplayMainMenu()
        {
            CmdLineUI.DisplayMessage();

            // The main menu strings 
            const string MAINMENU_STR = "Main Menu."; // Heading
            const string CREATEUNIT_STR = "Create the unit";//  Menu option 1   
            const string ADMINSTRATE_STR = "Administrate the unit"; // Menu option 2
            const string EXIT_STR = "Exit"; // Menu option 3

            // int for each option above
            const int CREATEUNIT_INT = 0, ADMINISTRATE_INT = 1, EXIT = 2;

            // Display the menu
            int option = CmdLineUI.GetOption(MAINMENU_STR, CREATEUNIT_STR, ADMINSTRATE_STR, EXIT_STR);

            // Make selection on user input from the CmdLineUI.GetOption method
            switch (option)
            {
                // Both the CreateUnitMenu and the CreateUnitMenu return the value of 'true'
                // The return value is unused at the moment but could be used in the program expands  
                case CREATEUNIT_INT: // If user choses 1 then proceed to the CreateUnitMenu
                    CreateUnitMenu();
                    break;
                case ADMINISTRATE_INT: // If user choses 2 then proceed to the AdministrateUnit
                    AdministrateUnit();
                    break;
                case EXIT:
                    CmdLineUI.DisplayMessage("Exiting");
                    return false; // Return false to the Run method, this will stop the loop and will ultimately exit the program 
                    break;
                default:
                    CmdLineUI.DisplayError("Wrong main menu choice");
                    break;
            }
            // Both the CreateUnitMenu amd the AdministrateUnit will break from the switch 
            // statement and will then return true. This will mean that the loop in the 
            // Run method will keep displaying the main menu
            return true; 

        }

        /// <summary>
        /// Gets information from a user and creates a new unit
        /// </summary>
        private void CreateUnitMenu()
        {
            // Get the information from the user 
            CmdLineUI.DisplayMessage("Enter the unit code:");
            string code = CmdLineUI.GetString();
            CmdLineUI.DisplayMessage("Enter the unit name:");
            string name = CmdLineUI.GetString();
            CmdLineUI.DisplayMessage("Enter the number of the initial students:");
            int initial = CmdLineUI.GetInt();
            CmdLineUI.DisplayMessage($"Enter the time and date of the first lecture using '{QUTConsts.DATETIMEFORMAT}' format:"); 
            DateTime first = CmdLineUI.GetDateTime();
            CmdLineUI.DisplayMessage($"Enter the time and date of the last lecture using '{QUTConsts.DATETIMEFORMAT}' format:");
            DateTime last = CmdLineUI.GetDateTime();

            // Create the new unit and report success 
            unit = new QUTUnit(code, name, initial, first, last);
            CmdLineUI.DisplayMessage("A new unit has been created.");
            CmdLineUI.DisplayMessage(unit);
            assigned = true;

            
        }


        /// <summary>
        /// A menu to administrate the unit. Will return generate an error  if the unit has not been created
        /// </summary>
        /// <returns>FALSE is the unit has been created or if something goes wrong with user interaction 
        /// otherwise TRUE.</returns>
        private bool AdministrateUnit()
        {
            // A control variable to need to menu running
            bool keepGoingAdmin = true;

            //  You could move this string to after the class declaration because it is used in more than 1 method
            const string CREATEUNIT_STR = "Create the unit"; 

            // If the unit has been assigned then generate a error
            if (!assigned)
            {
                CmdLineUI.DisplayError($"The unit has not been created. Please choose \"{CREATEUNIT_STR}\" first");
                return false;
            } else
            {
                // Keep going with the administration unit menu until the user chooses to
                // return to the previous menu. In this case the previous menu will be the
                // main menu
                while (keepGoingAdmin)
                {
                    keepGoingAdmin = AdministrateUnitMenu();
                }

            }
            return keepGoingAdmin; // should be false
        }


        /// <summary>
        /// A menu that allows to user to interact with the user
        /// </summary>
        /// <returns>FALSE is an error is generate, otherwise TRUE</returns>
        private bool AdministrateUnitMenu()
        {
            CmdLineUI.DisplayMessage();  // Add a blank line for formatting

            // As before add the string and integers 
            const string ADMINMENU_STR = "Administrate unit menu."; // Heading
            const string DISPLAY_STR = "Display the unit details"; // Option 1
            const string ADD_STR = "Add students"; // Option 2
            const string REMOVE_STR = "Remove students"; // Option 3
            const string ANOTHER_STR = "Another menu"; // Option 4
            const string RETURN_STR = "Return to first menu"; // Option 5

            const int DISPLAY_INT = 0, ADD_INT = 1, REMOVE_INT = 2, ANOTHER_INT=3, RETURN_INT = 4;

            int option = CmdLineUI.GetOption(ADMINMENU_STR, DISPLAY_STR,  ADD_STR, REMOVE_STR, ANOTHER_STR, RETURN_STR);

            // Make selection on user input from the CmdLineUI.GetOption method
            switch (option)
            {
                case DISPLAY_INT:
                    CmdLineUI.DisplayMessage(unit);
                    break;
                case ADD_INT:
                    AddStudentsToUnit();
                    break;
                case REMOVE_INT:
                    RemoveStudentsFromUnit();
                    break;
                case ANOTHER_INT:
                    // This will exit the method rather than existing the switch statement
                    // False will be returned, so the AdministrateUnit loop
                    // will be exited 
                    bool choice = AnotherMenu();
                    return choice;
                    break;
                case RETURN_INT:
                    // False will be returned, so the AdministrateUnit loop
                    // will be exited 
                    return false;
                    break;
                default:
                    CmdLineUI.DisplayError("Wrong administration menu choice");
                    break; 
            }
            // The DisplayMessage, AddStudentsToUnit, RemoveStudentsFromUnit will
            // all break the switch statement and return true
            return true;

        }


        /// <summary>
        /// Allows the user to add students to the unit
        /// </summary>
        /// <returns>TRUE</returns>
        private bool AddStudentsToUnit()
        {
            CmdLineUI.DisplayMessage("How many students to you want to add to the unit?");
            int moreStudents = CmdLineUI.GetInt();
            unit.IncreaseStudents(moreStudents);
            CmdLineUI.DisplayMessage($"{moreStudents} students have been added.");
            return true;
        }

        /// <summary>
        /// Allows the user to remove students from the unit
        /// </summary>
        /// <returns>TRUE</returns>
        private bool RemoveStudentsFromUnit()
        {
            CmdLineUI.DisplayMessage("How many students to you want to remove from the unit?");
            int lessStudents = CmdLineUI.GetInt();
            unit.DecreaseStudents(lessStudents);
            CmdLineUI.DisplayMessage($"{lessStudents} students have been removed.");
            return true;
        }

        /// <summary>
        /// An example of another menu. Where use can return to the previous menu
        /// or return to the first menu.
        /// </summary>
        /// <returns>TRUE to return to the previous menu
        ///           FALSE to return to the previous-previous menu</returns>
        private bool AnotherMenu()
        {

            CmdLineUI.DisplayMessage();  // Add a blank line for formatting

            // As before add the string and integers 
            const string ADMINMENU_STR = "Another menu"; // Heading
            const string PREVIOUS_STR = "Return to previous menu"; // Option 2
            const string PRE_PREVIOUS_STR = "Return to previous-previous (first) menu"; // Option 2

            const int PREVIOUS_INT = 0, PRE_PREVIOUS_INT = 1;

            int option = CmdLineUI.GetOption(ADMINMENU_STR, PREVIOUS_STR, PRE_PREVIOUS_STR);


            switch (option)
            {
                case PREVIOUS_INT:
                    return true;
                    break;
                case PRE_PREVIOUS_INT:
                    return false;
                    break;
                default:
                    CmdLineUI.DisplayError("Wrong another menu choice");
                    break;
            }

            return true;
        }

    }


}
