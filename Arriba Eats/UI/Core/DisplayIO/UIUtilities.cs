using System;

namespace DisplayIO;

/// <summary>
/// Provides helper methods for managing user interface interactions.
/// <para> Contains utility methods for dynamically formatting UI messages. </para>
/// </summary>
public static class UIUtilities
{
    private const string ENTER_CHOICE_TEMPLATE = "Please enter a choice between 1 and {0}:";
    public static string EnterChoiceStr(int maxChoice)
    {
        return string.Format(ENTER_CHOICE_TEMPLATE, maxChoice);
    }
}
