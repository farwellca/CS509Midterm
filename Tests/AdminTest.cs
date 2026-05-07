namespace Tests;

using dal;
using model;
using Moq;
using FluentAssertions;
using System.Data;


/*public class ConsoleTestWrapper : IConsole
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
}*/

public class AdminTest
{
    private Mock<IDal> _mockDal;
    private AdminModel aMod;
    private ConsoleTestWrapper _console;

    [Fact]
    public void createAccount_blank_username_throws_argument_exception()
    {
        List<string> userInputs = new List<string>() { "" };

        _mockDal = new Mock<IDal>();
        _console = new ConsoleTestWrapper();
        _console.SetLines(userInputs);
        aMod = new AdminModel(_mockDal.Object, _console);

        DataTable data = new DataTable();
        data.Columns.Add("Username", typeof(string));

        DataRow row = data.NewRow();
        row["Username"] = "admin1";
        data.Rows.Add(row);

        DataRow row1 = data.NewRow();
        row1["Username"] = "cust1";
        data.Rows.Add(row1);

        _mockDal.Setup(x => x.GetUsernames()).Returns(data);

        Action act = () => aMod.createAccount();

        act.Should().Throw<ArgumentException>()
            .WithMessage("Error. Username can not be blank.");
    }

    [Fact]
    public void createAccount_username_taken_throws_argument_exception()
    {
        List<string> userInputs = new List<string>() { "cust1" };

        _mockDal = new Mock<IDal>();
        _console = new ConsoleTestWrapper();
        _console.SetLines(userInputs);
        aMod = new AdminModel(_mockDal.Object, _console);

        DataTable data = new DataTable();
        data.Columns.Add("Username", typeof(string));

        DataRow row = data.NewRow();
        row["Username"] = "admin1";
        data.Rows.Add(row);

        DataRow row1 = data.NewRow();
        row1["Username"] = "cust1";
        data.Rows.Add(row1);

        _mockDal.Setup(x => x.GetUsernames()).Returns(data);

        Action act = () => aMod.createAccount();

        act.Should().Throw<ArgumentException>()
            .WithMessage("Error. Username already taken.");
    }

    [Fact]
    public void createAccount_invalid_pin_throws_argument_exception()
    {
        List<string> userInputs = new List<string>() { "cust5", "adsf" };

        _mockDal = new Mock<IDal>();
        _console = new ConsoleTestWrapper();
        _console.SetLines(userInputs);
        aMod = new AdminModel(_mockDal.Object, _console);

        DataTable data = new DataTable();
        data.Columns.Add("Username", typeof(string));

        DataRow row = data.NewRow();
        row["Username"] = "admin1";
        data.Rows.Add(row);

        DataRow row1 = data.NewRow();
        row1["Username"] = "cust1";
        data.Rows.Add(row1);

        _mockDal.Setup(x => x.GetUsernames()).Returns(data);

        Action act = () => aMod.createAccount();

        act.Should().Throw<ArgumentException>()
            .WithMessage("Error. That pin is invalid. Please ensure it is a 5 digit integer.");
    }

}