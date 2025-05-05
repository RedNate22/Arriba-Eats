using System;
using UINavigation;

namespace ArribaEats
{
    /// <summary> 
    /// The main entry point to the program.
    /// <para> Calls the <cref = "UIFlowController.RunMenuSystem"/> method on UIFlowController to begin the main loop, checking for menu state transitions and validating them. </para>
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            UIFlowController.RunMenuSystem();
        }
    }
}
