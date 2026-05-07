namespace model;

using System.Data.Common;
using dal;
using System.Data;
using System.Xml;

/// <summary>
/// Interface for handling user login actions.
/// </summary>
public interface ILoginModel
{
    /// <summary>
    /// Checks to see if a username and pin combination is valid.
    /// </summary>
    /// <param name="username">The username to check.</param>
    /// <param name="pin">The pin to check.</param>
    /// <returns>True if username and password are valid, false otherwise.</returns>
    bool Login(string username, string pin);
    /// <summary>
    /// Gets the account type of a user.
    /// </summary>
    /// <param name="username">The account username.</param>
    /// <param name="pin">The account pin.</param>
    /// <returns>The account type of a user, Admin or Customer.</returns>
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
