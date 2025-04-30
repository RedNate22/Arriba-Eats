using System;
using Menus;

namespace ArribaEats
{
    /// <summary>
    /// The main program
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            LoginMenu loginMenu = new LoginMenu();
            loginMenu.Run();
        }
    }
}
