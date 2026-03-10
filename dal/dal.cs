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

    public static DataTable GetUsernames()
    {
        var dt = new DataTable();
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            using (var da = new MySqlDataAdapter(@"select Username from Users;", connection))
            {
                da.Fill(dt);
            }
        }

        return dt;
    }

    public static int CreateAccount(string login, string pin, string holder, int balance, string status)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        using var command = new MySqlCommand(@"insert into Users (Username, Pin, Type) values (@Username, @Pin, @Type);", connection);
        command.Parameters.AddWithValue("@Username", login);
        command.Parameters.AddWithValue("@Pin", pin);
        command.Parameters.AddWithValue("@Type", "Customer");

        command.ExecuteNonQuery();

        int lastID = (int)command.LastInsertedId;

        using var command2 = new MySqlCommand(@"insert into Accounts values (@Id, @Holder, @Balance, @Status);", connection);
        command2.Parameters.AddWithValue("@Id", lastID);
        command2.Parameters.AddWithValue("@Holder", holder);
        command2.Parameters.AddWithValue("@Balance", balance);
        command2.Parameters.AddWithValue("@Status", status);

        command2.ExecuteNonQuery();

        return lastID;
    }

    public static int UpdateAccount(DataTable dt)
    {
        int changed;

        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        using var command = new MySqlCommand(@"update Users 
                                                set Username = @Username,
                                                    Pin = @Pin
                                                where Users.ID = @Id;", connection);
        command.Parameters.AddWithValue("@Id", dt.Rows[0]["ID"]);
        command.Parameters.AddWithValue("@Username", dt.Rows[0]["Username"]);
        command.Parameters.AddWithValue("@Pin", dt.Rows[0]["Pin"]);

        changed = command.ExecuteNonQuery();

        using var command2 = new MySqlCommand(@"update Accounts 
                                                set Holder = @Holder,
                                                    Status = @Status
                                                where Accounts.AccountNum = @Id;", connection);
        command2.Parameters.AddWithValue("@Holder", dt.Rows[0]["Holder"]);
        command2.Parameters.AddWithValue("@Status", dt.Rows[0]["Status"]);
        command2.Parameters.AddWithValue("@Id", dt.Rows[0]["ID"]);



        changed += command2.ExecuteNonQuery();

        return changed;
    }

}
