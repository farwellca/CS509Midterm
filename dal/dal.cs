using System.Data;
using System.Net;
using System.Net.NetworkInformation;
using MySql.Data.MySqlClient;

namespace dal;

/// <summary>
/// Handles the database connection and actions.
/// </summary>
public interface IDal
{
    /// <summary>
    /// Returns all users.
    /// </summary>
    /// <returns>A DataTable of all users.</returns>
    DataTable GetAll();
    /// <summary>
    /// Gets all username and pin combinations.
    /// </summary>
    /// <returns>A DataTable of all usernames and corresponding pins.</returns>
    DataTable GetLoginInfo();
    /// <summary>
    /// Gets the account type of a specified account.
    /// </summary>
    /// <param name="username">Account username.</param>
    /// <param name="pin">Account pin.</param>
    /// <returns>The account type, Adimin or Customer.</returns>
    DataTable GetUserType(string username, string pin);
    /// <summary>
    /// Gets the account information for a user by username and pin.
    /// </summary>
    /// <param name="username">Account username.</param>
    /// <param name="pin">Account pin.</param>
    /// <returns>A DataTable of the account information of a customer.</returns>
    DataTable GetCustAccount(string username, string pin);
    /// <summary>
    /// Gets the account information for a user by account number.
    /// </summary>
    /// <param name="actNum">The account ID.</param>
    /// <returns>A DataTable of the account information of a customer.</returns>
    DataTable GetCustAccountByID(int actNum);
    /// <summary>
    /// Updates the account balance for a user.
    /// </summary>
    /// <param name="id">The account ID.</param>
    /// <param name="balance">The new balance.</param>
    /// <returns>The number of database rows changed.</returns>
    int UpdateBalance(int id, int balance);
    /// <summary>
    /// Deletes an account from the database.
    /// </summary>
    /// <param name="account">The account to delete.</param>
    /// <returns>The number of database rows changed.</returns>
    int DeleteAccount(int account);
    /// <summary>
    /// Gets all the usernames from the database.
    /// </summary>
    /// <returns>A DataTable of all the usernames.</returns>
    DataTable GetUsernames();
    /// <summary>
    /// Creates a new account in the database.
    /// </summary>
    /// <param name="login">The account username.</param>
    /// <param name="pin">The account pin.</param>
    /// <param name="holder">The account holder.</param>
    /// <param name="balance">The account starting balance.</param>
    /// <param name="status">The account status.</param>
    /// <returns>The number of database rows changed.</returns>
    int CreateAccount(string login, string pin, string holder, int balance, string status);
    /// <summary>
    /// Updates the information for an account.
    /// </summary>
    /// <param name="dt">A DataTable holding the new account information.</param>
    /// <returns>The number of database rows changed.</returns>
    int UpdateAccount(DataTable dt);
}

public class Dal : IDal
{
    private const string connectionString = "server=host.docker.internal;port=3333;uid=root;pwd=a;database=atm";

    public DataTable GetAll()
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

    public DataTable GetLoginInfo()
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

    public DataTable GetUserType(string username, string pin)
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

    public DataTable GetCustAccount(string username, string pin)
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

    public DataTable GetCustAccountByID(int actNum)
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

    public int UpdateBalance(int id, int balance)
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

    public int DeleteAccount(int account)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        using var command = new MySqlCommand(@"delete from Users where Users.ID = @id;", connection);
        command.Parameters.AddWithValue("@Id", account);

        return command.ExecuteNonQuery();
    }

    public DataTable GetUsernames()
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

    public int CreateAccount(string login, string pin, string holder, int balance, string status)
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

    public int UpdateAccount(DataTable dt)
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
