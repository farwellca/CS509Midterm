namespace Tests;

using dal;
using model;
using Moq;
using FluentAssertions;
using System.Data;

public class LoginTest
{
    private readonly Mock<IDal> _mockDal;
    private readonly LoginModel lMod;

    public LoginTest()
    {
        _mockDal = new Mock<IDal>();
        lMod = new LoginModel(_mockDal.Object);
    }

    [Fact]
    public void Login_valid_should_return_true()
    {
        DataTable data = new DataTable();
        
        data.Columns.Add("Username", typeof(string));
        data.Columns.Add("Pin", typeof(string));

        DataRow row = data.NewRow();
        row["Username"] = "cust1";
        row["Pin"] = "11111";
        data.Rows.Add(row);

        _mockDal.Setup(x => x.GetLoginInfo()).Returns(data);

        var result = lMod.Login("cust1", "11111");

        result.Should().Be(true);
    }

    [Fact]
    public void Login_invalid_should_return_false()
    {
        DataTable data = new DataTable();
        
        data.Columns.Add("Username", typeof(string));
        data.Columns.Add("Pin", typeof(string));

        DataRow row = data.NewRow();
        row["Username"] = "cust1";
        row["Pin"] = "11111";
        data.Rows.Add(row);

        _mockDal.Setup(x => x.GetLoginInfo()).Returns(data);

        var result = lMod.Login("cust2", "22222");

        result.Should().Be(false);
    }

    [Fact]
    public void getUserType_admin1_shound_be_admin()
    {
        DataTable data = new DataTable();
        
        data.Columns.Add("Type", typeof(string));

        DataRow row = data.NewRow();
        row["Type"] = "Admin";
        data.Rows.Add(row);

        _mockDal.Setup(x => x.GetUserType("admin1", "11111")).Returns(data);

        var result = lMod.GetUserType("admin1", "11111");

        result.Should().Be("Admin");
    }

    [Fact]
    public void getUserType_cust1_shound_be_customer()
    {
        DataTable data = new DataTable();
        
        data.Columns.Add("Type", typeof(string));

        DataRow row = data.NewRow();
        row["Type"] = "Customer";
        data.Rows.Add(row);

        _mockDal.Setup(x => x.GetUserType("cust1", "11111")).Returns(data);

        var result = lMod.GetUserType("cust1", "11111");

        result.Should().Be("Customer");
    }
}