public class ConsoleTestWrapper : IConsole
{
    public List<String> LinesToRead = new List<string>();

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
        string result = LinesToRead[0];
        LinesToRead.RemoveAt(0);
        return result;
    }

    public ConsoleKeyInfo ReadKey(bool b)
    {
        return new ConsoleKeyInfo('c', ConsoleKey.C, false, false, false);
    }

    public void SetLines(List<string> list)
    {
        LinesToRead = list;
    }
}
