namespace Tests;

using dal;
using model;
using Moq;
using FluentAssertions;
using System.Data;


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

public class CustomerTest
{
    private Mock<IDal> _mockDal;
    private CustomerModel cMod;
    private ConsoleTestWrapper _console;

    [Fact]
    public void withdrawCash_should_return_correct_balance()
    {
        List<string> userInputs = new List<string>() { "500" };

        _mockDal = new Mock<IDal>();
        _console = new ConsoleTestWrapper();
        _console.SetLines(userInputs);
        cMod = new CustomerModel(_mockDal.Object, _console);

        DataTable data = new DataTable();
        data.Columns.Add("ID", typeof(int));
        data.Columns.Add("Holder", typeof(string));
        data.Columns.Add("Balance", typeof(int));
        data.Columns.Add("Status", typeof(string));

        DataRow row = data.NewRow();
        row["ID"] = 2;
        row["Holder"] = "Bob Jones";
        row["Balance"] = 10000;
        row["Status"] = "Active";
        data.Rows.Add(row);

        _mockDal.Setup(x => x.GetCustAccount("cust1", "11111")).Returns(data);
        _mockDal.Setup(x => x.UpdateBalance(2, 10)).Returns(1);

        int result = cMod.withdrawCash("cust1", "11111");

        result.Should().Be(9500);
    }

    [Fact]
    public void withdrawCash_not_int_should_throw_not_valid_number()
    {
        List<string> userInputs = new List<string>() { "asdf" };

        _mockDal = new Mock<IDal>();
        _console = new ConsoleTestWrapper();
        _console.SetLines(userInputs);
        cMod = new CustomerModel(_mockDal.Object, _console);

        DataTable data = new DataTable();
        data.Columns.Add("ID", typeof(int));
        data.Columns.Add("Holder", typeof(string));
        data.Columns.Add("Balance", typeof(int));
        data.Columns.Add("Status", typeof(string));

        DataRow row = data.NewRow();
        row["ID"] = 2;
        row["Holder"] = "Bob Jones";
        row["Balance"] = 10000;
        row["Status"] = "Active";
        data.Rows.Add(row);

        _mockDal.Setup(x => x.GetCustAccount("cust1", "11111")).Returns(data);
        _mockDal.Setup(x => x.UpdateBalance(2, 10)).Returns(1);

        Action act = () => cMod.withdrawCash("cust1", "11111");

        act.Should().Throw<ArgumentException>()
            .WithMessage("Error. That is not a valid number.");
    }

    [Fact]
    public void withdrawCash_negative_should_throw_greater_than_zero()
    {
        List<string> userInputs = new List<string>() { "-1" };

        _mockDal = new Mock<IDal>();
        _console = new ConsoleTestWrapper();
        _console.SetLines(userInputs);
        cMod = new CustomerModel(_mockDal.Object, _console);

        DataTable data = new DataTable();
        data.Columns.Add("ID", typeof(int));
        data.Columns.Add("Holder", typeof(string));
        data.Columns.Add("Balance", typeof(int));
        data.Columns.Add("Status", typeof(string));

        DataRow row = data.NewRow();
        row["ID"] = 2;
        row["Holder"] = "Bob Jones";
        row["Balance"] = 10000;
        row["Status"] = "Active";
        data.Rows.Add(row);

        _mockDal.Setup(x => x.GetCustAccount("cust1", "11111")).Returns(data);
        _mockDal.Setup(x => x.UpdateBalance(2, 10)).Returns(1);

        Action act = () => cMod.withdrawCash("cust1", "11111");

        act.Should().Throw<ArgumentException>()
            .WithMessage("Error. The amount must be greater than zero.");
    }

    [Fact]
    public void withdrawCash_more_than_balance_should_throw_less_than_balance()
    {
        List<string> userInputs = new List<string>() { "200000" };

        _mockDal = new Mock<IDal>();
        _console = new ConsoleTestWrapper();
        _console.SetLines(userInputs);
        cMod = new CustomerModel(_mockDal.Object, _console);

        DataTable data = new DataTable();
        data.Columns.Add("ID", typeof(int));
        data.Columns.Add("Holder", typeof(string));
        data.Columns.Add("Balance", typeof(int));
        data.Columns.Add("Status", typeof(string));

        DataRow row = data.NewRow();
        row["ID"] = 2;
        row["Holder"] = "Bob Jones";
        row["Balance"] = 10000;
        row["Status"] = "Active";
        data.Rows.Add(row);

        _mockDal.Setup(x => x.GetCustAccount("cust1", "11111")).Returns(data);
        _mockDal.Setup(x => x.UpdateBalance(2, 10)).Returns(1);

        Action act = () => cMod.withdrawCash("cust1", "11111");

        act.Should().Throw<ArgumentException>()
            .WithMessage("Error. The amount must be less than your balance.");
    }

    [Fact]
    public void depositCash_not_int_should_throw_not_valid_number()
    {
        List<string> userInputs = new List<string>() { "asdf" };

        _mockDal = new Mock<IDal>();
        _console = new ConsoleTestWrapper();
        _console.SetLines(userInputs);
        cMod = new CustomerModel(_mockDal.Object, _console);

        DataTable data = new DataTable();
        data.Columns.Add("ID", typeof(int));
        data.Columns.Add("Holder", typeof(string));
        data.Columns.Add("Balance", typeof(int));
        data.Columns.Add("Status", typeof(string));

        DataRow row = data.NewRow();
        row["ID"] = 2;
        row["Holder"] = "Bob Jones";
        row["Balance"] = 10000;
        row["Status"] = "Active";
        data.Rows.Add(row);

        _mockDal.Setup(x => x.GetCustAccount("cust1", "11111")).Returns(data);
        _mockDal.Setup(x => x.UpdateBalance(2, 10)).Returns(1);

        Action act = () => cMod.depositCash("cust1", "11111");

        act.Should().Throw<ArgumentException>()
            .WithMessage("Error. That is not a valid number.");
    }

    [Fact]
    public void depositCash_negative_should_throw_greater_than_zero()
    {
        List<string> userInputs = new List<string>() { "-1" };

        _mockDal = new Mock<IDal>();
        _console = new ConsoleTestWrapper();
        _console.SetLines(userInputs);
        cMod = new CustomerModel(_mockDal.Object, _console);

        DataTable data = new DataTable();
        data.Columns.Add("ID", typeof(int));
        data.Columns.Add("Holder", typeof(string));
        data.Columns.Add("Balance", typeof(int));
        data.Columns.Add("Status", typeof(string));

        DataRow row = data.NewRow();
        row["ID"] = 2;
        row["Holder"] = "Bob Jones";
        row["Balance"] = 10000;
        row["Status"] = "Active";
        data.Rows.Add(row);

        _mockDal.Setup(x => x.GetCustAccount("cust1", "11111")).Returns(data);
        _mockDal.Setup(x => x.UpdateBalance(2, 10)).Returns(1);

        Action act = () => cMod.depositCash("cust1", "11111");

        act.Should().Throw<ArgumentException>()
            .WithMessage("Error. The amount must be greater than zero.");
    }

    [Fact]
    public void depositCash_should_return_correct_balance()
    {
        List<string> userInputs = new List<string>() { "500" };

        _mockDal = new Mock<IDal>();
        _console = new ConsoleTestWrapper();
        _console.SetLines(userInputs);
        cMod = new CustomerModel(_mockDal.Object, _console);

        DataTable data = new DataTable();
        data.Columns.Add("ID", typeof(int));
        data.Columns.Add("Holder", typeof(string));
        data.Columns.Add("Balance", typeof(int));
        data.Columns.Add("Status", typeof(string));

        DataRow row = data.NewRow();
        row["ID"] = 2;
        row["Holder"] = "Bob Jones";
        row["Balance"] = 10000;
        row["Status"] = "Active";
        data.Rows.Add(row);

        _mockDal.Setup(x => x.GetCustAccount("cust1", "11111")).Returns(data);
        _mockDal.Setup(x => x.UpdateBalance(2, 10)).Returns(1);

        int result = cMod.depositCash("cust1", "11111");

        result.Should().Be(10500);
    }

    [Fact]
    public void displayBalance_should_display_balance()
    {
        _mockDal = new Mock<IDal>();
        _console = new ConsoleTestWrapper();
        cMod = new CustomerModel(_mockDal.Object, _console);

        DataTable data = new DataTable();
        data.Columns.Add("ID", typeof(int));
        data.Columns.Add("Holder", typeof(string));
        data.Columns.Add("Balance", typeof(int));
        data.Columns.Add("Status", typeof(string));

        DataRow row = data.NewRow();
        row["ID"] = 2;
        row["Holder"] = "Bob Jones";
        row["Balance"] = 10000;
        row["Status"] = "Active";
        data.Rows.Add(row);

        _mockDal.Setup(x => x.GetCustAccount("cust1", "11111")).Returns(data);

        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw);

            string expected = "Account #2\n" +
                                "Date: " + DateTime.Now.ToString("M/d/yyyy") + "\n" +
                                "Balance: 10000\n";

            cMod.displayBalance("cust1", "11111");

            sw.ToString().Should().Contain(expected);
            Console.SetOut(new StreamWriter(Console.OpenStandardError()));
        }
    }
}