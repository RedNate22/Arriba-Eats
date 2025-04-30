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
            MainMenu mainMenu = new MainMenu();
            mainMenu.Run();
        }
    }
}
