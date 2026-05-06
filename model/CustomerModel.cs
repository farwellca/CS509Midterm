namespace model;

using System.Data.Common;
using dal;
using System.Data;
using Microsoft.VisualBasic;
using System.Threading.Tasks.Dataflow;
using System.ComponentModel;
using System.Diagnostics.Contracts;

public interface ICustomerModel
{
    void ShowCustomerMenu(string username, string pin);
}

public class CustomerModel : ICustomerModel
{
    private readonly IDal _dal;

    public CustomerModel(IDal dal)
    {
        _dal = dal;
    }

    public class Account
    {
        public int AccountNum { get; set; }
        public string Holder { get; set; }
        public int Balance { get; set; }
        public string Status { get; set; }
    }

    public void ShowCustomerMenu(string username, string pin)
    {
        Console.Clear();

        bool done = false;

        var custDt = _dal.GetCustAccount(username, pin);
        var custAccount = new Account
        {
            AccountNum = (int)custDt.Rows[0]["ID"],
            Holder = (string)custDt.Rows[0]["Holder"],
            Balance = (int)custDt.Rows[0]["Balance"],
            Status = (string)custDt.Rows[0]["Status"]
        };

        while (!done)
        {
            try
            {
                Console.WriteLine("\n1--Withdraw Cash\n2--Deposit Cash\n3--Display Balance\n4--Exit");

                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        withdrawCash(custAccount);
                        break;
                    case "2":
                        depositCash(custAccount);
                        break;
                    case "3":
                        displayBalance(custAccount);
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("Have a nice day.");
                        done = true;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    private void withdrawCash(Account a)
    {
        Console.Clear();

        Console.Write("Enter the withdrawl amount: ");
        String strAmount = Console.ReadLine();
        int amount;

        if (!int.TryParse(strAmount, out amount))
        {
            throw new ArgumentException("Error. That is not a valid number.");
        }
        else if (amount < 0)
        {
            throw new ArgumentException("Error. The amount must be greater than zero.");
        }
        else if (amount > a.Balance)
        {
            throw new ArgumentException("Error. The amount must be less than your balance.");
        }
        else
        {
            a.Balance -= amount;

            _dal.UpdateBalance(a.AccountNum, a.Balance);

            Console.WriteLine("Cash Successfully Withdrawn.");
            Console.WriteLine("Account #" + a.AccountNum);
            Console.WriteLine("Date: " + DateTime.Now.ToString("M/d/yyyy"));
            Console.WriteLine("Withdrawn: " + amount);
            Console.WriteLine("Balance: " + a.Balance);

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey(true);
            Console.Clear();
        }
    }

    private void depositCash(Account a)
    {
        Console.Clear();

        Console.Write("Enter the cash amount to deposit: ");
        String strAmount = Console.ReadLine();
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
            a.Balance += amount;

            _dal.UpdateBalance(a.AccountNum, a.Balance);

            Console.WriteLine("Cash Deposited Successfully.");
            Console.WriteLine("Account #" + a.AccountNum);
            Console.WriteLine("Date: " + DateTime.Now.ToString("M/d/yyyy"));
            Console.WriteLine("Deposited: " + amount);
            Console.WriteLine("Balance: " + a.Balance);

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey(true);
            Console.Clear();
        }
    }

    private void displayBalance(Account a)
    {
        Console.Clear();
        Console.WriteLine("Account #" + a.AccountNum);
        Console.WriteLine("Date: " + DateTime.Now.ToString("M/d/yyyy"));
        Console.WriteLine("Balance: " + a.Balance);
        Console.WriteLine("Press any key to continue.");
        Console.ReadKey(true);
        Console.Clear();
    }
}