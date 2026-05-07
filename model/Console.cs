/// <summary>
/// Interface wrapping console methods, needed for testing.
/// </summary>
public interface IConsole
{
    void Write(string message);
    void WriteLine(string message);
    string ReadLine();
    ConsoleKeyInfo ReadKey(bool b);
}

/// <summary>
/// Executes normal Console read and write methods for deployment.
/// </summary>
public class ConsoleWrapper : IConsole
{
    /// <summary>
    /// Writes to the console.
    /// </summary>
    /// <param name="message">The text to be displayed.</param>
    public void Write(string message)
    {
        Console.Write(message);
    }

    /// <summary>
    /// Writes to the console.
    /// </summary>
    /// <param name="message">The text to be displayed.</param>
    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }

    /// <summary>
    /// Reads user entered text from console.
    /// </summary>
    /// <returns>Text entered by user.</returns>
    public string ReadLine()
    {
        return Console.ReadLine();
    }

    /// <summary>
    /// Reads keystroke entered by user.
    /// </summary>
    /// <param name="b">True to not show keystroke in console.</param>
    /// <returns>Returns entered keystroke.</returns>
    public ConsoleKeyInfo ReadKey(bool b)
    {
        return Console.ReadKey(b);
    }
}
