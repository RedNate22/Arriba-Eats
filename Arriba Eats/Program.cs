using System;
using UINavigation;

namespace ArribaEats
{
    /// <summary> 
    /// Defines the main entry point to the program.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The main entry point to the program.
        /// <para> Initializes the program and delegates UI flow control to <see cref="UIFlowController.RunMenuSystem"/>. </para>
        /// <para> Starts the menu loop, ensuring state transitions and user interaction handling. </para>
        /// </summary>
        static void Main()
        {
            UIFlowController.RunMenuSystem();
        }
    }
}
