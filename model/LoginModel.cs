namespace model;

using System.Data.Common;
using dal;
using System.Data;
using System.Xml;

public interface ILoginModel
{
    bool Login(string username, string pin);
    string GetUserType(string username, string pin);
}

public class LoginModel : ILoginModel
{
    private readonly IDal _dal;

    public LoginModel(IDal dal)
    {
        _dal = dal;
    }

    public bool Login(string username, string pin)
    {
        bool valid = false;

        var dt = _dal.GetLoginInfo();
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

    public string GetUserType(string username, string pin)
    {
        var dt = _dal.GetUserType(username, pin);
        return (string)dt.Rows[0]["Type"];
    }
}
