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


}
