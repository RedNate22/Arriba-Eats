using System;
using UINavigation;
using UI;

namespace ArribaEats
{
    /// <summary> 
    /// Defines the main entry point to the program.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The main entry point to the program.
        /// <para> Calls <see cref="MainMenu.WelcomeMessage()"/> to welcome the user. </para>
        /// <para> Initializes the program and delegates UI flow control to <see cref="UIFlowController.RunMenuSystem"/>
        /// where the <see cref="MainMenu"/> is first displayed to the user. </para>
        /// </summary>
        static void Main()
        {
            MainMenu.WelcomeMessage();
            UIFlowController.RunMenuSystem();
        }
    }
}
