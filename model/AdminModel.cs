namespace model;

using System.Data.Common;
using dal;
using System.Data;
using Microsoft.VisualBasic;
using System.Threading.Tasks.Dataflow;
using System.ComponentModel;

public static class AdminModel
{
    public static string GetAll()
    {
        string output = "";
        var dt = Dal.GetAll();

        foreach (DataRow r in dt.Rows)
        {
            output += (int)r["ID"] + " ";
            output += (string)r["Username"] + " ";
            output += (string)r["Pin"] + " ";
            output += (string)r["Type"] + "\n";
        }

        return output;
    }

    public static void ShowAdminMenu()
    {
        Console.Clear();

        bool done = false;

        while (!done)
        {
            Console.WriteLine("\n1--Create New Account\n2--Delete Exicting Accont\n3--Update Account Information\n4--Search for Account\n5--Exit");

            string? input = Console.ReadLine();

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

    private static void createAccount()
    {
        string login;
        string pin;
        string holder;
        int balance;
        string status;

        Console.Clear();
        Console.WriteLine("Create new Account");
        Console.Write("Login: ");
        login = Console.ReadLine();

        while (!validUsername(login) || login.Length == 0)
        {
            if (login.Length == 0)
            {
                Console.WriteLine("Username can not be blank.");
            }
            else
            {
                Console.WriteLine("That username is already taken. Please pick another.");
            }
            Console.Write("Login: ");
            login = Console.ReadLine();
        }

        Console.Write("Pin: ");
        pin = Console.ReadLine();
        while (!validPin(pin))
        {
            Console.WriteLine("That pin is invalid. Please ensure it is a 5 digit integer.");
            Console.Write("Pin: ");
            pin = Console.ReadLine();
        }

        Console.Write("Holder's Name: ");
        holder = Console.ReadLine();
        while (holder.Length == 0)
        {
            Console.WriteLine("Holder name can not be blank.");
            Console.Write("Holder: ");
            holder = Console.ReadLine();
        }

        Console.Write("Starting Balance: ");
        string balStr = Console.ReadLine();

        while (!int.TryParse(balStr, out balance) || balance < 0)
        {
            Console.WriteLine("Starting balance must be a positive number.");
            Console.Write("Starting Balance: ");
            balStr = Console.ReadLine();
        }

        Console.Write("Status: ");
        status = Console.ReadLine();
        while (status.Length == 0)
        {
            Console.WriteLine("Status can not be blank.");
            Console.Write("Status: ");
            status = Console.ReadLine();
        }

        int newNum = Dal.CreateAccount(login, pin, holder, balance, status);
        Console.WriteLine("Account Successfully Created - the account number assigned is: " + newNum);

        Console.WriteLine("Press any key to continue.");
        Console.ReadKey(true);
        Console.Clear();

    }

    private static bool validUsername(string login)
    {
        var dt = Dal.GetUsernames();

        foreach (DataRow r in dt.Rows)
        {
            if ((string)r["Username"] == login)
            {
                return false;
            }
        }

        return true;
    }

    private static bool validPin(string pin)
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

    private static void deleteAccount()
    {
        int act;
        bool valid = false;

        while (!valid)
        {
            Console.Write("Enter the account number to which you want to delete: ");
            string strNum = Console.ReadLine();

            if (!int.TryParse(strNum, out act))
            {
                Console.WriteLine("That is not a valid number. Please try again.");
            }
            else if (act < 0)
            {
                Console.WriteLine("Enter a number greater than 0. Please try again.");
            }
            else
            {
                valid = true;
                var dt = Dal.GetCustAccountByID(act);

                if (dt.Rows.Count < 1)
                {
                    Console.WriteLine("No customer account found under that number.");

                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey(true);
                    Console.Clear();
                }
                else
                {
                    bool matching = false;

                    while (!matching)
                    {
                        Console.Write("You wish to delete the account held by " + dt.Rows[0]["Holder"] + ". If this information is correct, please re-enter the account number: ");
                        string strNum2 = Console.ReadLine();
                        if (strNum != strNum2)
                        {
                            Console.WriteLine("That number does not match the first, please try again.");
                        }
                        else
                        {
                            matching = true;
                            Console.WriteLine("Account Deleted Successfully");

                            Dal.DeleteAccount(act);

                            Console.WriteLine("Press any key to continue.");
                            Console.ReadKey(true);
                            Console.Clear();
                        }
                    }
                }

            }
        }
    }

    private static void updateAccount()
    {
        int act;
        bool valid = false;

        while (!valid)
        {
            Console.Write("Enter the account number to which you want to update: ");
            string strNum = Console.ReadLine();

            if (!int.TryParse(strNum, out act))
            {
                Console.WriteLine("That is not a valid number. Please try again.");
            }
            else if (act < 0)
            {
                Console.WriteLine("Enter a number greater than 0. Please try again.");
            }
            else
            {
                valid = true;
                var dt = Dal.GetCustAccountByID(act);

                if (dt.Rows.Count < 1)
                {
                    Console.WriteLine("No customer account found under that number.");

                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey(true);
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine();

                    displayAccount(dt);

                    string newHolder, newStatus, newLogin, newPin;

                    Console.WriteLine("\nEnter the new information or leave blank to keep the old.");

                    Console.Write("Holder: ");
                    newHolder = Console.ReadLine();

                    if (newHolder.Length == 0)
                    {
                        newHolder = (string)dt.Rows[0]["Holder"];
                    }

                    Console.Write("Status: ");
                    newStatus = Console.ReadLine();

                    if (newStatus.Length == 0)
                    {
                        newStatus = (string)dt.Rows[0]["Status"];
                    }

                    bool validLogin = false;
                    Console.Write("Login: ");
                    newLogin = Console.ReadLine();

                    while (!validLogin)
                    {

                        if (newLogin.Length == 0 || newLogin == (string)dt.Rows[0]["Username"])
                        {
                            newLogin = (string)dt.Rows[0]["Username"];
                            validLogin = true;
                        }
                        else if (!validUsername(newLogin))
                        {
                            Console.WriteLine("That username is already taken. Please pick another.");
                            Console.Write("Login: ");
                            newLogin = Console.ReadLine();
                        }
                        else
                        {
                            validLogin = true;
                        }
                    }

                    Console.Write("Pin Code: ");
                    newPin = Console.ReadLine();

                    while (!validPin(newPin) && !(newPin.Length == 0))
                    {
                        Console.WriteLine("That pin is invalid. Please ensure it is a 5 digit integer.");
                        Console.Write("Pin Code: ");
                        newPin = Console.ReadLine();
                    }

                    if (newPin.Length == 0)
                    {
                        newPin = (string)dt.Rows[0]["Pin"];
                    }


                    dt.Rows[0]["Holder"] = newHolder;
                    dt.Rows[0]["Status"] = newStatus;
                    dt.Rows[0]["Username"] = newLogin;
                    dt.Rows[0]["Pin"] = newPin;

                    Console.WriteLine("\nAccount updated.");
                    displayAccount(dt);

                    Dal.UpdateAccount(dt);


                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey(true);
                    Console.Clear();
                }

            }
        }
    }

    private static void searchAccount()
    {
        int act;
        bool valid = false;

        while (!valid)
        {
            Console.Write("Enter the Account Number: ");
            string strNum = Console.ReadLine();

            if (!int.TryParse(strNum, out act))
            {
                Console.WriteLine("That is not a valid number. Please try again.");
            }
            else if (act < 0)
            {
                Console.WriteLine("Enter a number greater than 0. Please try again.");
            }
            else
            {
                valid = true;
                var dt = Dal.GetCustAccountByID(act);

                if (dt.Rows.Count < 1)
                {
                    Console.Clear();
                    Console.WriteLine("No customer account found under that number.");

                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey(true);
                    Console.Clear();
                }
                else
                {
                    Console.Clear();

                    displayAccount(dt);

                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey(true);
                    Console.Clear();
                }
            }
        }
    }

    private static void displayAccount(DataTable dt)
    {
        Console.WriteLine("The account information is:");
        Console.WriteLine("Account # " + dt.Rows[0]["ID"]);
        Console.WriteLine("Holder: " + dt.Rows[0]["Holder"]);
        Console.WriteLine("Balance: " + dt.Rows[0]["Balance"]);
        Console.WriteLine("Status: " + dt.Rows[0]["Status"]);
        Console.WriteLine("Login: " + dt.Rows[0]["Username"]);
        Console.WriteLine("Pin: " + dt.Rows[0]["Pin"]);
    }
}
