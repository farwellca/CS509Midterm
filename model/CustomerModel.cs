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
        Console.WriteLine("You have logged in as a customer!");
    }
}