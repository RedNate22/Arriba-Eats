using System;

namespace Menus
{
    /// <summary>
    /// <para> Defines the contract for all menus.</para>
    /// <para> Ensures all menus provide a method to display their contents. </para>
    /// </summary>
    public interface IMenu
    {
        /// <summary>
        /// <para> Displays the menu to the user. </para>
        /// <para> Implementations should handle rendering and interaction. </para>
        /// </summary>
        public void DisplayMenu();
    }
}

