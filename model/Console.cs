public interface IConsole
{
    void Write(string message);
    void WriteLine(string message);
    string ReadLine();
    ConsoleKeyInfo ReadKey(bool b);
}

public class ConsoleWrapper : IConsole
{
    public void Write(string message)
    {
        Console.Write(message);
    }

    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }

    public string ReadLine()
    {
        return Console.ReadLine();
    }

    public ConsoleKeyInfo ReadKey(bool b)
    {
        return Console.ReadKey(b);
    }
}