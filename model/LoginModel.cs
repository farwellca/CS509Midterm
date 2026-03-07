namespace model;

using System.Data.Common;
using dal;
using System.Data;
using System.Xml;

public static class LoginModel
{
    public static bool Login(string username, string pin)
    {
        bool valid = false;

        var dt = Dal.GetLoginInfo();
        foreach (DataRow r in dt.Rows)
        {
            if ((string)r["Username"] == username && (string)r["Pin"] == pin)
            {
                valid = true;
                break;
            }

        }
        return valid;
    }

    public static string getUserType(string username, string pin)
    {
        var dt = Dal.GetUserType(username, pin);
        return (string)dt.Rows[0]["Type"];
    }
}
