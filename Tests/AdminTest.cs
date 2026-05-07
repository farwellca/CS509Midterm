namespace Tests;

using dal;
using model;
using Moq;
using FluentAssertions;
using System.Data;


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

    [Fact]
    public void createAccount_blank_holder_throws_argument_exception()
    {
        List<string> userInputs = new List<string>() { "cust5", "55555", "" };

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
            .WithMessage("Error. Holder's Name can not be blank.");
    }

    [Fact]
    public void createAccount_invalid_balance_throws_argument_exception()
    {
        List<string> userInputs = new List<string>() { "cust5", "55555", "bob", "Sdfgs" };

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
            .WithMessage("Error. Starting balance must be a positive number.");
    }

    [Fact]
    public void createAccount_blank_status_throws_argument_exception()
    {
        List<string> userInputs = new List<string>() { "cust5", "55555", "bob", "100", "" };

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
            .WithMessage("Error. Status can not be blank.");
    }

    [Fact]
    public void createAccount_valid_returns_account_number()
    {
        List<string> userInputs = new List<string>() { "cust5", "55555", "bob", "100", "active" };

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
        _mockDal.Setup(x => x.CreateAccount("cust5", "55555", "bob", 100, "active")).Returns(5);

        var act = aMod.createAccount();

        act.Should().Be(5);
    }

}
