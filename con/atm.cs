using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Transactions;
using menu;

/// <summary>
/// Start point for application.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "entry point")]
public class ATM
{
    /// <summary>
    /// Main start point.
    /// </summary>
    public static void Main()
    {
        Menu menu = new Menu();
        menu.Run();
    }
}
