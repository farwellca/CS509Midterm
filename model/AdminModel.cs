namespace model;

using System.Data.Common;
using dal;
using System.Data;
using Microsoft.VisualBasic;
using System.Threading.Tasks.Dataflow;
using System.ComponentModel;

public interface IAdminModel
{
    string GetAll();
    void ShowAdminMenu();
}

public class AdminModel : IAdminModel
{
    private readonly IDal _dal;
    private readonly IConsole _console;

    public AdminModel(IDal dal, IConsole console)
    {
        _dal = dal;
        _console = console;
    }

    public string GetAll()
    {
        string output = "";
        var dt = _dal.GetAll();

        foreach (DataRow r in dt.Rows)
        {
            output += (int)r["ID"] + " ";
            output += (string)r["Username"] + " ";
            output += (string)r["Pin"] + " ";
            output += (string)r["Type"] + "\n";
        }

        return output;
    }

    public void ShowAdminMenu()
    {
        Console.Clear();

        bool done = false;

        while (!done)
        {
            try
            {
                _console.WriteLine("\n1--Create New Account\n2--Delete Existing Accont\n3--Update Account Information\n4--Search for Account\n5--Exit");

                string? input = _console.ReadLine();

                switch (input)
                {
                    case "1":
                        createAccount();
                        break;
                    case "2":
                        deleteAccount();
                        break;
                    case "3":
                        updateAccount();
                        break;
                    case "4":
                        searchAccount();
                        break;
                    case "5":
                        Console.Clear();
                        _console.WriteLine("Have a nice day.");
                        done = true;
                        break;
                    default:
                        Console.Clear();
                        _console.WriteLine("Invalid input. Please try again.");
                        break;

                }
            }
            catch (Exception e)
            {
                _console.WriteLine(e.Message);
            }
        }
    }

    private void createAccount()
    {
        string login;
        string pin;
        string holder;
        int balance;
        string status;

        Console.Clear();
        _console.WriteLine("Create New Account");
        _console.Write("Login: ");
        login = _console.ReadLine();

        if (!validUsername(login) || login.Length == 0)
        {
            if (login.Length == 0)
            {
                throw new ArgumentException("Error. Username can not be blank.");
            }
            else
            {
                throw new ArgumentException("Error. Username already taken.");
            }
        }

        _console.Write("Pin: ");
        pin = _console.ReadLine();
        if (!validPin(pin))
        {
            throw new ArgumentException("Error. That pin is invalid. Please ensure it is a 5 digit integer.");
        }

        _console.Write("Holder's Name: ");
        holder = _console.ReadLine();
        if (holder.Length == 0)
        {
            throw new ArgumentException("Error. Holder's Name can not be blank.");
        }

        _console.Write("Starting Balance: ");
        string balStr = _console.ReadLine();

        if (!int.TryParse(balStr, out balance) || balance < 0)
        {
            throw new ArgumentException("Error. Starting balance must be a positive number.");
        }

        _console.Write("Status: ");
        status = _console.ReadLine();
        if (status.Length == 0)
        {
            throw new ArgumentException("Error. Status can not be blank.");
        }

        int newNum = _dal.CreateAccount(login, pin, holder, balance, status);
        _console.WriteLine("Account Successfully Created - the account number assigned is: " + newNum);

        _console.WriteLine("Press any key to continue.");
        _console.ReadKey(true);
        Console.Clear();

    }

    private bool validUsername(string login)
    {
        var dt = _dal.GetUsernames();

        foreach (DataRow r in dt.Rows)
        {
            if ((string)r["Username"] == login)
            {
                return false;
            }
        }

        return true;
    }

    private bool validPin(string pin)
    {
        int i;

        if (!int.TryParse(pin, out i) || pin.Length != 5)
        {
            return false;
        }
        else if (i < 0)
        {
            return false;
        }

        return true;
    }

    private void deleteAccount()
    {
        int act;

        _console.Write("Enter the account number to which you want to delete: ");
        string strNum = _console.ReadLine();

        if (!int.TryParse(strNum, out act))
        {
            throw new ArgumentException("Error. That is not a valid number.");
        }
        else if (act < 0)
        {
            throw new ArgumentException("Error. The number must be greater than 0.");
        }
        else
        {
            var dt = _dal.GetCustAccountByID(act);

            if (dt.Rows.Count < 1)
            {
                _console.WriteLine("No customer account found under that number.");

                _console.WriteLine("Press any key to continue.");
                _console.ReadKey(true);
                Console.Clear();
            }
            else
            {
                _console.Write("You wish to delete the account held by " + dt.Rows[0]["Holder"] + ". If this information is correct, please re-enter the account number: ");
                string strNum2 = _console.ReadLine();
                if (strNum != strNum2)
                {
                    throw new ArgumentException("Error. Account numbers do not match. Operation cancelled.");
                }
                else
                {
                    _console.WriteLine("Account Deleted Successfully");

                    _dal.DeleteAccount(act);

                    _console.WriteLine("Press any key to continue.");
                    _console.ReadKey(true);
                    Console.Clear();
                }
            }
        }
    }

    private void updateAccount()
    {
        int act;

        _console.Write("Enter the account number to which you want to update: ");
        string strNum = _console.ReadLine();

        if (!int.TryParse(strNum, out act))
        {
            throw new ArgumentException("Error. That is not a valid number.");
        }
        else if (act < 0)
        {
            throw new ArgumentException("Error. Number must be greater than zero.");
        }
        else
        {
            var dt = _dal.GetCustAccountByID(act);

            if (dt.Rows.Count < 1)
            {
                _console.WriteLine("No customer account found under that number.");

                _console.WriteLine("Press any key to continue.");
                _console.ReadKey(true);
                Console.Clear();
            }
            else
            {
                _console.Write("\n");

                displayAccount(dt);

                string newHolder, newStatus, newLogin, newPin;

                _console.WriteLine("\nEnter the new information or leave blank to keep the old.");

                _console.Write("Holder: ");
                newHolder = _console.ReadLine();

                if (newHolder.Length == 0)
                {
                    newHolder = (string)dt.Rows[0]["Holder"];
                }

                _console.Write("Status: ");
                newStatus = _console.ReadLine();

                if (newStatus.Length == 0)
                {
                    newStatus = (string)dt.Rows[0]["Status"];
                }

                _console.Write("Login: ");
                newLogin = _console.ReadLine();

                if (newLogin.Length == 0 || newLogin == (string)dt.Rows[0]["Username"])
                {
                    newLogin = (string)dt.Rows[0]["Username"];
                }
                else if (!validUsername(newLogin))
                {
                    throw new ArgumentException("Error. That username is already taken.");
                }

                _console.Write("Pin Code: ");
                newPin = _console.ReadLine();

                if (!validPin(newPin) && !(newPin.Length == 0))
                {
                    throw new ArgumentException("Error. Pin is not a 5 digit integer.");
                }

                if (newPin.Length == 0)
                {
                    newPin = (string)dt.Rows[0]["Pin"];
                }

                dt.Rows[0]["Holder"] = newHolder;
                dt.Rows[0]["Status"] = newStatus;
                dt.Rows[0]["Username"] = newLogin;
                dt.Rows[0]["Pin"] = newPin;

                _console.WriteLine("\nAccount updated.");
                displayAccount(dt);

                _dal.UpdateAccount(dt);

                _console.WriteLine("Press any key to continue.");
                _console.ReadKey(true);
                Console.Clear();
            }
        }
    }

    private void searchAccount()
    {
        int act;

        _console.Write("Enter the Account Number: ");
        string strNum = _console.ReadLine();

        if (!int.TryParse(strNum, out act))
        {
            throw new ArgumentException("Error. That is not a valid number.");
        }
        else if (act < 0)
        {
            throw new ArgumentException("Error. The number must be greater than zero.");
        }
        else
        {
            var dt = _dal.GetCustAccountByID(act);

            if (dt.Rows.Count < 1)
            {
                Console.Clear();
                _console.WriteLine("No customer account found under that number.");

                _console.WriteLine("Press any key to continue.");
                _console.ReadKey(true);
                Console.Clear();
            }
            else
            {
                Console.Clear();

                displayAccount(dt);

                _console.WriteLine("Press any key to continue.");
                _console.ReadKey(true);
                Console.Clear();
            }
        }
    }

    private void displayAccount(DataTable dt)
    {
        _console.WriteLine("The account information is:");
        _console.WriteLine("Account # " + dt.Rows[0]["ID"]);
        _console.WriteLine("Holder: " + dt.Rows[0]["Holder"]);
        _console.WriteLine("Balance: " + dt.Rows[0]["Balance"]);
        _console.WriteLine("Status: " + dt.Rows[0]["Status"]);
        _console.WriteLine("Login: " + dt.Rows[0]["Username"]);
        _console.WriteLine("Pin: " + dt.Rows[0]["Pin"]);
    }
}
