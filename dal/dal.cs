using System.Data;
using System.Net.NetworkInformation;
using MySql.Data.MySqlClient;

namespace dal;

public static class Dal
{
    private const string connectionString = "server=host.docker.internal;port=3333;uid=root;pwd=a;database=atm";

    public static DataTable GetAll()
    {
        var dt = new DataTable();
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            using (var da = new MySqlDataAdapter(@"select * from Users;", connection))
            {
                da.Fill(dt);
            }
        }

        return dt;
    }

    public static DataTable GetLoginInfo()
    {
        var dt = new DataTable();
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            using (var da = new MySqlDataAdapter(@"select Username,Pin from Users;", connection))
            {
                da.Fill(dt);
            }
        }

        return dt;
    }

    public static DataTable GetUserType(string username, string pin)
    {
        var dt = new DataTable();
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            using (var da = new MySqlDataAdapter(@"select Type from Users where Username='" + username + "'and Pin='" + pin + "';", connection))
            {
                da.Fill(dt);
            }
        }

        return dt;
    }

    public static DataTable GetCustAccount(string username, string pin)
    {
        var dt = new DataTable();
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            using (var da = new MySqlDataAdapter(@"select * from Users join Accounts on Users.ID = Accounts.AccountNum where Username='" + username + "'and Pin='" + pin + "';", connection))
            {
                da.Fill(dt);
            }
        }

        return dt;
    }

}
