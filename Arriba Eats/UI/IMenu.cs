using System;

namespace UI
{
    /// <summary>
    /// Defines the contract for all menus.
    /// <para> Ensures all menus provide a method to display their contents. </para>
    /// </summary>
    public interface IMenu
    {
        /// <summary>
        /// Displays the menu to the user.
        /// <para> Implementations should handle rendering and interaction. </para>
        /// </summary>
        public void DisplayMenu();
    }
}

