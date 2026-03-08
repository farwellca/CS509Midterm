namespace model;

using System.Data.Common;
using dal;
using System.Data;
using Microsoft.VisualBasic;
using System.Threading.Tasks.Dataflow;

public static class CustomerModel
{
    public static void ShowCustomerMenu()
    {
        Console.Clear();
        Console.WriteLine("You have logged in as a customer!");

        bool done = false;

        while (!done)
        {
            Console.WriteLine("\n1--Withdraw Cash\n2--Deposit Cash\n3--Display Balance\n4--Exit");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Withdrawing cash.");
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("Depositing cash.");
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Displaying balance.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey(true);
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
}