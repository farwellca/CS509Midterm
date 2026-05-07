namespace model;

using System.Data.Common;
using dal;
using System.Data;
using Microsoft.VisualBasic;
using System.Threading.Tasks.Dataflow;
using System.ComponentModel;
using System.Diagnostics.Contracts;

/// <summary>
/// Handles customer actions.
/// </summary>
public interface ICustomerModel
{
    /// <summary>
    /// Withdraws cash from the user's account.
    /// </summary>
    /// <param name="username">Current user's username.</param>
    /// <param name="pin">Current user's pin.</param>
    /// <returns>The new account balance.</returns>
    int withdrawCash(string username, string pin);
    /// <summary>
    /// Deposits cash to the user's account.
    /// </summary>
    /// <param name="username">Current user's username.</param>
    /// <param name="pin">Current user's pin.</param>
    /// <returns>The new account balance.</returns>
    int depositCash(string username, string pin);
    /// <summary>
    /// Displays the user's account information.
    /// </summary>
    /// <param name="username">Current user's username.</param>
    /// <param name="pin">Current user's pin.</param>
    void displayBalance(string username, string pin);
}

public class CustomerModel : ICustomerModel
{
    private readonly IDal _dal;
    private readonly IConsole _console;
    public Account account;

    public CustomerModel(IDal dal, IConsole console)
    {
        _dal = dal;
        _console = console;
        account = new Account();
    }

    /// <summary>
    /// Class holding temporary information for a user's account.
    /// </summary>
    public class Account
    {
        public int AccountNum { get; set; }
        public string Holder { get; set; }
        public int Balance { get; set; }
        public string Status { get; set; }
    }

    private void loadAccount(string username, string pin)
    {
        var custDt = _dal.GetCustAccount(username, pin);
        account.AccountNum = (int)custDt.Rows[0]["ID"];
        account.Holder = (string)custDt.Rows[0]["Holder"];
        account.Balance = (int)custDt.Rows[0]["Balance"];
        account.Status = (string)custDt.Rows[0]["Status"];
    }

    public int withdrawCash(string username, string pin)
    {

        loadAccount(username, pin);
        Console.Clear();

        _console.Write("Enter the withdrawl amount: ");
        String strAmount = _console.ReadLine();
        int amount;

        if (!int.TryParse(strAmount, out amount))
        {
            throw new ArgumentException("Error. That is not a valid number.");
        }
        else if (amount < 0)
        {
            throw new ArgumentException("Error. The amount must be greater than zero.");
        }
        else if (amount > account.Balance)
        {
            throw new ArgumentException("Error. The amount must be less than your balance.");
        }
        else
        {
            account.Balance -= amount;

            _dal.UpdateBalance(account.AccountNum, account.Balance);

            _console.WriteLine("Cash Successfully Withdrawn.");
            _console.WriteLine("Account #" + account.AccountNum);
            _console.WriteLine("Date: " + DateTime.Now.ToString("M/d/yyyy"));
            _console.WriteLine("Withdrawn: " + amount);
            _console.WriteLine("Balance: " + account.Balance);

            _console.WriteLine("Press any key to continue.");
            _console.ReadKey(true);
            Console.Clear();

            return account.Balance;
        }
    }

    public int depositCash(string username, string pin)
    {
        loadAccount(username, pin);
        Console.Clear();

        _console.Write("Enter the cash amount to deposit: ");
        String strAmount = _console.ReadLine();
        int amount;

        if (!int.TryParse(strAmount, out amount))
        {
            throw new ArgumentException("Error. That is not a valid number.");
        }
        else if (amount < 0)
        {
            throw new ArgumentException("Error. The amount must be greater than zero.");
        }
        else
        {
            account.Balance += amount;

            _dal.UpdateBalance(account.AccountNum, account.Balance);

            _console.WriteLine("Cash Deposited Successfully.");
            _console.WriteLine("Account #" + account.AccountNum);
            _console.WriteLine("Date: " + DateTime.Now.ToString("M/d/yyyy"));
            _console.WriteLine("Deposited: " + amount);
            _console.WriteLine("Balance: " + account.Balance);

            _console.WriteLine("Press any key to continue.");
            _console.ReadKey(true);
            Console.Clear();

            return account.Balance;
        }
    }

    public void displayBalance(string username, string pin)
    {
        loadAccount(username, pin);
        Console.Clear();
        _console.WriteLine("Account #" + account.AccountNum);
        _console.WriteLine("Date: " + DateTime.Now.ToString("M/d/yyyy"));
        _console.WriteLine("Balance: " + account.Balance);
        _console.WriteLine("Press any key to continue.");
        _console.ReadKey(true);
        Console.Clear();
    }
}
