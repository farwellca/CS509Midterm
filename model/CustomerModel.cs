namespace model;

using System.Data.Common;
using dal;
using System.Data;
using Microsoft.VisualBasic;
using System.Threading.Tasks.Dataflow;
using System.ComponentModel;
using System.Diagnostics.Contracts;

public static class CustomerModel
{

    public class Account
    {
        public int AccountNum { get; set; }
        public string Holder { get; set; }
        public int Balance { get; set; }
        public string Status { get; set; }
    }

    public static void ShowCustomerMenu(string username, string pin)
    {
        Console.Clear();
        Console.WriteLine("You have logged in as a customer!");

        bool done = false;

        var custDt = Dal.GetCustAccount(username, pin);
        var custAccount = new Account
        {
            AccountNum = (int)custDt.Rows[0]["ID"],
            Holder = (string)custDt.Rows[0]["Holder"],
            Balance = (int)custDt.Rows[0]["Balance"],
            Status = (string)custDt.Rows[0]["Status"]
        };

        while (!done)
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
    }

    private static void withdrawCash(Account a)
    {
        Console.Clear();

        bool validAmt = false;
        while (!validAmt)
        {
            Console.Write("Enter the withdrawl amount: ");
            String strAmount = Console.ReadLine();
            int amount;

            if (!int.TryParse(strAmount, out amount))
            {
                Console.WriteLine("That is not a valid number. Please try again.");
            }
            else if (amount < 0)
            {
                Console.WriteLine("Enter a number greater than 0. Please try again.");
            }
            else if (amount > a.Balance)
            {
                Console.WriteLine("Enter a number less than your balance. Please try again.");
            }
            else
            {
                validAmt = true;

                a.Balance -= amount;

                Dal.UpdateBalance(a.AccountNum, a.Balance);

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
    }

    private static void depositCash(Account a)
    {
        Console.Clear();

        bool validAmt = false;
        while (!validAmt)
        {
            Console.Write("Enter the cash amount to deposit: ");
            String strAmount = Console.ReadLine();
            int amount;

            if (!int.TryParse(strAmount, out amount))
            {
                Console.WriteLine("That is not a valid number. Please try again.");
            }
            else if (amount < 0)
            {
                Console.WriteLine("Enter a number greater than 0. Please try again.");
            }
            else
            {
                validAmt = true;

                a.Balance += amount;

                Dal.UpdateBalance(a.AccountNum, a.Balance);

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
    }

    private static void displayBalance(Account a)
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