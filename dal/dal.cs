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

}
