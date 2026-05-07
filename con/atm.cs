using System.ComponentModel.Design;
using System.Transactions;
using menu;

/// <summary>
/// Start point for application.
/// </summary>
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
