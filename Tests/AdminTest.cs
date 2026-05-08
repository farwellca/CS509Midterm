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
    public void createAccount_negative_pin_throws_argument_exception()
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

    [Fact]
    public void deleteAccount_invalid_account_number_should_throw_argument_exception()
    {
        List<string> userInputs = new List<string>() { "asdf" };

        _mockDal = new Mock<IDal>();
        _console = new ConsoleTestWrapper();
        _console.SetLines(userInputs);
        aMod = new AdminModel(_mockDal.Object, _console);

        Action act = () => aMod.deleteAccount();

        act.Should().Throw<ArgumentException>()
            .WithMessage("Error. That is not a valid number.");
    }

    [Fact]
    public void deleteAccount_negative_account_number_should_throw_argument_exception()
    {
        List<string> userInputs = new List<string>() { "-2" };

        _mockDal = new Mock<IDal>();
        _console = new ConsoleTestWrapper();
        _console.SetLines(userInputs);
        aMod = new AdminModel(_mockDal.Object, _console);

        Action act = () => aMod.deleteAccount();

        act.Should().Throw<ArgumentException>()
            .WithMessage("Error. The number must be greater than 0.");
    }

    [Fact]
    public void deleteAccount_no_account_found_should_return_0()
    {
        List<string> userInputs = new List<string>() { "5" };

        _mockDal = new Mock<IDal>();
        _console = new ConsoleTestWrapper();
        _console.SetLines(userInputs);
        aMod = new AdminModel(_mockDal.Object, _console);

        DataTable data = new DataTable();
        data.Columns.Add("ID", typeof(int));
        data.Columns.Add("Username", typeof(string));
        data.Columns.Add("Pin", typeof(string));
        data.Columns.Add("Type", typeof(string));
        data.Columns.Add("AccountNum", typeof(int));
        data.Columns.Add("Holder", typeof(string));
        data.Columns.Add("Balance", typeof(int));
        data.Columns.Add("Status", typeof(string));

        _mockDal.Setup(x => x.GetCustAccountByID(5)).Returns(data);

        var act = aMod.deleteAccount();

        act.Should().Be(0);
    }

    [Fact]
    public void deleteAccount_act_num_not_matching_should_throw_argument_exception()
    {
        List<string> userInputs = new List<string>() { "2", "3" };

        _mockDal = new Mock<IDal>();
        _console = new ConsoleTestWrapper();
        _console.SetLines(userInputs);
        aMod = new AdminModel(_mockDal.Object, _console);

        DataTable data = new DataTable();
        data.Columns.Add("ID", typeof(int));
        data.Columns.Add("Username", typeof(string));
        data.Columns.Add("Pin", typeof(string));
        data.Columns.Add("Type", typeof(string));
        data.Columns.Add("AccountNum", typeof(int));
        data.Columns.Add("Holder", typeof(string));
        data.Columns.Add("Balance", typeof(int));
        data.Columns.Add("Status", typeof(string));

        DataRow row = data.NewRow();
        row["ID"] = 2;
        row["Username"] = "cust1";
        row["Pin"] = "11111";
        row["Type"] = "Customer";
        row["AccountNum"] = 2;
        row["Holder"] = "Bob Jones";
        row["Balance"] = 10000;
        row["Status"] = "Active";
        data.Rows.Add(row);

        _mockDal.Setup(x => x.GetCustAccountByID(2)).Returns(data);

        Action act = () => aMod.deleteAccount();

        act.Should().Throw<ArgumentException>()
            .WithMessage("Error. Account numbers do not match. Operation cancelled.");
    }

    [Fact]
    public void deleteAccount_should_return_rows_deleted()
    {
        List<string> userInputs = new List<string>() { "2", "2" };

        _mockDal = new Mock<IDal>();
        _console = new ConsoleTestWrapper();
        _console.SetLines(userInputs);
        aMod = new AdminModel(_mockDal.Object, _console);

        DataTable data = new DataTable();
        data.Columns.Add("ID", typeof(int));
        data.Columns.Add("Username", typeof(string));
        data.Columns.Add("Pin", typeof(string));
        data.Columns.Add("Type", typeof(string));
        data.Columns.Add("AccountNum", typeof(int));
        data.Columns.Add("Holder", typeof(string));
        data.Columns.Add("Balance", typeof(int));
        data.Columns.Add("Status", typeof(string));

        DataRow row = data.NewRow();
        row["ID"] = 2;
        row["Username"] = "cust1";
        row["Pin"] = "11111";
        row["Type"] = "Customer";
        row["AccountNum"] = 2;
        row["Holder"] = "Bob Jones";
        row["Balance"] = 10000;
        row["Status"] = "Active";
        data.Rows.Add(row);

        _mockDal.Setup(x => x.GetCustAccountByID(2)).Returns(data);
        _mockDal.Setup(x => x.DeleteAccount(2)).Returns(1);

        var act = aMod.deleteAccount();

        act.Should().Be(1);
    }

    [Fact]
    public void updateAccount_invaid_account_number_should_throw_argument_exception()
    {
        List<string> userInputs = new List<string>() { "asdfa" };

        _mockDal = new Mock<IDal>();
        _console = new ConsoleTestWrapper();
        _console.SetLines(userInputs);
        aMod = new AdminModel(_mockDal.Object, _console);

        Action act = () => aMod.updateAccount();

        act.Should().Throw<ArgumentException>()
            .WithMessage("Error. That is not a valid number.");
    }

    [Fact]
    public void updateAccount_negative_account_number_should_throw_argument_exception()
    {
        List<string> userInputs = new List<string>() { "-1" };

        _mockDal = new Mock<IDal>();
        _console = new ConsoleTestWrapper();
        _console.SetLines(userInputs);
        aMod = new AdminModel(_mockDal.Object, _console);

        Action act = () => aMod.updateAccount();

        act.Should().Throw<ArgumentException>()
            .WithMessage("Error. Number must be greater than zero.");
    }

    [Fact]
    public void updateAccount_no_account_found_should_return_0()
    {
        List<string> userInputs = new List<string>() { "5" };

        _mockDal = new Mock<IDal>();
        _console = new ConsoleTestWrapper();
        _console.SetLines(userInputs);
        aMod = new AdminModel(_mockDal.Object, _console);

        DataTable data = new DataTable();
        data.Columns.Add("ID", typeof(int));
        data.Columns.Add("Username", typeof(string));
        data.Columns.Add("Pin", typeof(string));
        data.Columns.Add("Type", typeof(string));
        data.Columns.Add("AccountNum", typeof(int));
        data.Columns.Add("Holder", typeof(string));
        data.Columns.Add("Balance", typeof(int));
        data.Columns.Add("Status", typeof(string));

        _mockDal.Setup(x => x.GetCustAccountByID(5)).Returns(data);

        var act = aMod.updateAccount();

        act.Should().Be(0);
    }

    [Fact]
    public void updateAccount_username_taken_should_throw_argument_exception()
    {
        List<string> userInputs = new List<string>() { "2", "", "", "cust2" };

        _mockDal = new Mock<IDal>();
        _console = new ConsoleTestWrapper();
        _console.SetLines(userInputs);
        aMod = new AdminModel(_mockDal.Object, _console);

        DataTable data = new DataTable();
        data.Columns.Add("ID", typeof(int));
        data.Columns.Add("Username", typeof(string));
        data.Columns.Add("Pin", typeof(string));
        data.Columns.Add("Type", typeof(string));
        data.Columns.Add("AccountNum", typeof(int));
        data.Columns.Add("Holder", typeof(string));
        data.Columns.Add("Balance", typeof(int));
        data.Columns.Add("Status", typeof(string));

        DataRow row = data.NewRow();
        row["ID"] = 2;
        row["Username"] = "cust1";
        row["Pin"] = "11111";
        row["Type"] = "Customer";
        row["AccountNum"] = 2;
        row["Holder"] = "Bob Jones";
        row["Balance"] = 10000;
        row["Status"] = "Active";
        data.Rows.Add(row);

        DataTable data2 = new DataTable();
        data2.Columns.Add("Username", typeof(string));

        DataRow row1 = data2.NewRow();
        row1["Username"] = "admin1";
        data2.Rows.Add(row1);

        DataRow row2 = data2.NewRow();
        row2["Username"] = "cust1";
        data2.Rows.Add(row2);

        DataRow row3 = data2.NewRow();
        row3["Username"] = "cust2";
        data2.Rows.Add(row3);

        _mockDal.Setup(x => x.GetCustAccountByID(2)).Returns(data);
        _mockDal.Setup(x => x.GetUsernames()).Returns(data2);

        Action act = () => aMod.updateAccount();

        act.Should().Throw<ArgumentException>()
            .WithMessage("Error. That username is already taken.");
    }

    [Fact]
    public void updateAccount_invalid_pin_should_throw_argument_exception()
    {
        List<string> userInputs = new List<string>() { "2", "", "", "", "asdf" };

        _mockDal = new Mock<IDal>();
        _console = new ConsoleTestWrapper();
        _console.SetLines(userInputs);
        aMod = new AdminModel(_mockDal.Object, _console);

        DataTable data = new DataTable();
        data.Columns.Add("ID", typeof(int));
        data.Columns.Add("Username", typeof(string));
        data.Columns.Add("Pin", typeof(string));
        data.Columns.Add("Type", typeof(string));
        data.Columns.Add("AccountNum", typeof(int));
        data.Columns.Add("Holder", typeof(string));
        data.Columns.Add("Balance", typeof(int));
        data.Columns.Add("Status", typeof(string));

        DataRow row = data.NewRow();
        row["ID"] = 2;
        row["Username"] = "cust1";
        row["Pin"] = "11111";
        row["Type"] = "Customer";
        row["AccountNum"] = 2;
        row["Holder"] = "Bob Jones";
        row["Balance"] = 10000;
        row["Status"] = "Active";
        data.Rows.Add(row);

        DataTable data2 = new DataTable();
        data2.Columns.Add("Username", typeof(string));

        DataRow row1 = data2.NewRow();
        row1["Username"] = "admin1";
        data2.Rows.Add(row1);

        DataRow row2 = data2.NewRow();
        row2["Username"] = "cust1";
        data2.Rows.Add(row2);

        DataRow row3 = data2.NewRow();
        row3["Username"] = "cust2";
        data2.Rows.Add(row3);

        _mockDal.Setup(x => x.GetCustAccountByID(2)).Returns(data);
        _mockDal.Setup(x => x.GetUsernames()).Returns(data2);

        Action act = () => aMod.updateAccount();

        act.Should().Throw<ArgumentException>()
            .WithMessage("Error. Pin is not a 5 digit integer.");
    }

    [Fact]
    public void updateAccount_all_same_should_return_1()
    {
        List<string> userInputs = new List<string>() { "2", "", "", "", "" };

        _mockDal = new Mock<IDal>();
        _console = new ConsoleTestWrapper();
        _console.SetLines(userInputs);
        aMod = new AdminModel(_mockDal.Object, _console);

        DataTable data = new DataTable();
        data.Columns.Add("ID", typeof(int));
        data.Columns.Add("Username", typeof(string));
        data.Columns.Add("Pin", typeof(string));
        data.Columns.Add("Type", typeof(string));
        data.Columns.Add("AccountNum", typeof(int));
        data.Columns.Add("Holder", typeof(string));
        data.Columns.Add("Balance", typeof(int));
        data.Columns.Add("Status", typeof(string));

        DataRow row = data.NewRow();
        row["ID"] = 2;
        row["Username"] = "cust1";
        row["Pin"] = "11111";
        row["Type"] = "Customer";
        row["AccountNum"] = 2;
        row["Holder"] = "Bob Jones";
        row["Balance"] = 10000;
        row["Status"] = "Active";
        data.Rows.Add(row);

        DataTable data2 = new DataTable();
        data2.Columns.Add("Username", typeof(string));

        DataRow row1 = data2.NewRow();
        row1["Username"] = "admin1";
        data2.Rows.Add(row1);

        DataRow row2 = data2.NewRow();
        row2["Username"] = "cust1";
        data2.Rows.Add(row2);

        DataRow row3 = data2.NewRow();
        row3["Username"] = "cust2";
        data2.Rows.Add(row3);

        _mockDal.Setup(x => x.GetCustAccountByID(2)).Returns(data);
        _mockDal.Setup(x => x.GetUsernames()).Returns(data2);
        _mockDal.Setup(x => x.UpdateAccount(data)).Returns(1);

        var act = aMod.updateAccount();

        act.Should().Be(1);
    }

    [Fact]
    public void updateAccount_all_changed_should_return_1()
    {
        List<string> userInputs = new List<string>() { "2", "Bobby Jones", "Inactive", "bobj", "54321" };

        _mockDal = new Mock<IDal>();
        _console = new ConsoleTestWrapper();
        _console.SetLines(userInputs);
        aMod = new AdminModel(_mockDal.Object, _console);

        DataTable data = new DataTable();
        data.Columns.Add("ID", typeof(int));
        data.Columns.Add("Username", typeof(string));
        data.Columns.Add("Pin", typeof(string));
        data.Columns.Add("Type", typeof(string));
        data.Columns.Add("AccountNum", typeof(int));
        data.Columns.Add("Holder", typeof(string));
        data.Columns.Add("Balance", typeof(int));
        data.Columns.Add("Status", typeof(string));

        DataRow row = data.NewRow();
        row["ID"] = 2;
        row["Username"] = "cust1";
        row["Pin"] = "11111";
        row["Type"] = "Customer";
        row["AccountNum"] = 2;
        row["Holder"] = "Bob Jones";
        row["Balance"] = 10000;
        row["Status"] = "Active";
        data.Rows.Add(row);

        DataTable data2 = new DataTable();
        data2.Columns.Add("Username", typeof(string));

        DataRow row1 = data2.NewRow();
        row1["Username"] = "admin1";
        data2.Rows.Add(row1);

        DataRow row2 = data2.NewRow();
        row2["Username"] = "cust1";
        data2.Rows.Add(row2);

        DataRow row3 = data2.NewRow();
        row3["Username"] = "cust2";
        data2.Rows.Add(row3);

        DataTable newVals = new DataTable();
        newVals.Columns.Add("ID", typeof(int));
        newVals.Columns.Add("Username", typeof(string));
        newVals.Columns.Add("Pin", typeof(string));
        newVals.Columns.Add("Type", typeof(string));
        newVals.Columns.Add("AccountNum", typeof(int));
        newVals.Columns.Add("Holder", typeof(string));
        newVals.Columns.Add("Balance", typeof(int));
        newVals.Columns.Add("Status", typeof(string));

        _mockDal.Setup(x => x.GetCustAccountByID(2)).Returns(data);
        _mockDal.Setup(x => x.GetUsernames()).Returns(data2);

        data.Rows[0]["Username"] = "bobj";
        data.Rows[0]["Pin"] = "54321";
        data.Rows[0]["Holder"] = "Bobby Jones";
        data.Rows[0]["Status"] = "Inactive";

        _mockDal.Setup(x => x.UpdateAccount(data)).Returns(1);

        var act = aMod.updateAccount();

        act.Should().Be(1);
    }

    [Fact]
    public void searchAccount_invalid_actnum_should_throw_argument_exception()
    {
        List<string> userInputs = new List<string>() { "asdf" };

        _mockDal = new Mock<IDal>();
        _console = new ConsoleTestWrapper();
        _console.SetLines(userInputs);
        aMod = new AdminModel(_mockDal.Object, _console);

        Action act = () => aMod.searchAccount();

        act.Should().Throw<ArgumentException>()
            .WithMessage("Error. That is not a valid number.");
    }

    [Fact]
    public void searchAccount_negative_actnum_should_throw_argument_exception()
    {
        List<string> userInputs = new List<string>() { "-1" };

        _mockDal = new Mock<IDal>();
        _console = new ConsoleTestWrapper();
        _console.SetLines(userInputs);
        aMod = new AdminModel(_mockDal.Object, _console);

        Action act = () => aMod.searchAccount();

        act.Should().Throw<ArgumentException>()
            .WithMessage("Error. The number must be greater than zero.");
    }

    [Fact]
    public void searchAccount_no_account_found_should_say_no_account_found()
    {
        List<string> userInputs = new List<string>() { "5" };

        _mockDal = new Mock<IDal>();
        _console = new ConsoleTestWrapper();
        _console.SetLines(userInputs);
        aMod = new AdminModel(_mockDal.Object, _console);

        DataTable data = new DataTable();
        data.Columns.Add("ID", typeof(int));
        data.Columns.Add("Username", typeof(string));
        data.Columns.Add("Pin", typeof(string));
        data.Columns.Add("Type", typeof(string));
        data.Columns.Add("AccountNum", typeof(int));
        data.Columns.Add("Holder", typeof(string));
        data.Columns.Add("Balance", typeof(int));
        data.Columns.Add("Status", typeof(string));

        _mockDal.Setup(x => x.GetCustAccountByID(5)).Returns(data);

        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw);

            string expected = "No customer account found under that number.\nPress any key to continue.";

            aMod.searchAccount();

            sw.ToString().Should().Contain(expected);
            Console.SetOut(new StreamWriter(Console.OpenStandardError()));
        }
    }

    [Fact]
    public void searchAccount_should_display_account()
    {
        List<string> userInputs = new List<string>() { "2" };

        _mockDal = new Mock<IDal>();
        _console = new ConsoleTestWrapper();
        _console.SetLines(userInputs);
        aMod = new AdminModel(_mockDal.Object, _console);

        DataTable data = new DataTable();
        data.Columns.Add("ID", typeof(int));
        data.Columns.Add("Username", typeof(string));
        data.Columns.Add("Pin", typeof(string));
        data.Columns.Add("Type", typeof(string));
        data.Columns.Add("AccountNum", typeof(int));
        data.Columns.Add("Holder", typeof(string));
        data.Columns.Add("Balance", typeof(int));
        data.Columns.Add("Status", typeof(string));

        DataRow row = data.NewRow();
        row["ID"] = 2;
        row["Username"] = "cust1";
        row["Pin"] = "11111";
        row["Type"] = "Customer";
        row["AccountNum"] = 2;
        row["Holder"] = "Bob Jones";
        row["Balance"] = 10000;
        row["Status"] = "Active";
        data.Rows.Add(row);

        _mockDal.Setup(x => x.GetCustAccountByID(2)).Returns(data);

        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw);

            string expected = "The account information is:" +
                                "\nAccount # " + data.Rows[0]["ID"] +
                                "\nHolder: " + data.Rows[0]["Holder"] +
                                "\nBalance: " + data.Rows[0]["Balance"] +
                                "\nStatus: " + data.Rows[0]["Status"] +
                                "\nLogin: " + data.Rows[0]["Username"] +
                                "\nPin: " + data.Rows[0]["Pin"];

            aMod.searchAccount();

            sw.ToString().Should().Contain(expected);
            Console.SetOut(new StreamWriter(Console.OpenStandardError()));
        }
    }
}
