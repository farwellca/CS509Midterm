using System.Data;
using System.Net;
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

    public static DataTable GetCustAccountByID(int actNum)
    {
        var dt = new DataTable();
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            using (var da = new MySqlDataAdapter(@"select * from Users join Accounts on Users.ID = Accounts.AccountNum where Users.ID=" + actNum + ";", connection))
            {
                da.Fill(dt);
            }
        }

        return dt;
    }

    public static int UpdateBalance(int id, int balance)
    {
        int changed;

        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        using var command = new MySqlCommand(@"update Accounts 
                                                set Balance = @Balance
                                                where Accounts.AccountNum = @actNum;", connection);
        command.Parameters.AddWithValue("@Balance", balance);
        command.Parameters.AddWithValue("@actNum", id);

        changed = command.ExecuteNonQuery();

        return changed;
    }

    public static int DeleteAccount(int account)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        using var command = new MySqlCommand(@"delete from Users where Users.ID = @id;", connection);
        command.Parameters.AddWithValue("@Id", account);

        return command.ExecuteNonQuery();
    }

}
