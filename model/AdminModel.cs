namespace model;

using System.Data.Common;
using dal;
using System.Data;
using Microsoft.VisualBasic;
using System.Threading.Tasks.Dataflow;

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
                    Console.WriteLine("Create Account");
                    break;
                case "2":
                    Console.WriteLine("Delete Account");
                    break;
                case "3":
                    Console.WriteLine("Update Account");
                    break;
                case "4":
                    Console.WriteLine("Search Account");
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
                Console.WriteLine("That is not a valid number. Pleaase try again.");
            }
            else if (act < 0)
            {
                Console.WriteLine("Enter a number greater than 0. Pleaase try again.");
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
                    Console.WriteLine("The account information is:");
                    Console.WriteLine("Account # " + dt.Rows[0]["ID"]);
                    Console.WriteLine("Holder: " + dt.Rows[0]["Holder"]);
                    Console.WriteLine("Balance: " + dt.Rows[0]["Balance"]);
                    Console.WriteLine("Status: " + dt.Rows[0]["Status"]);
                    Console.WriteLine("Login: " + dt.Rows[0]["Username"]);
                    Console.WriteLine("Pin: " + dt.Rows[0]["Pin"]);

                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey(true);
                    Console.Clear();
                }
            }
        }
    }


}
