// See https://aka.ms/new-console-template for more information

using System.Transactions;
using model;

bool loggedIn = false;

while (!loggedIn)
{
    Console.Write("Enter login: ");
    string? username = Console.ReadLine();

    Console.Write("Enter Pin code: ");
    string? pin = Console.ReadLine();

    if (LoginModel.Login(username, pin))
    {
        Console.WriteLine("Success!");

        switch (LoginModel.getUserType(username, pin))
        {
            case "Customer":
                CustomerModel.ShowCustomerMenu(username, pin);
                break;
            case "Admin":
                AdminModel.ShowAdminMenu();
                break;
        }

        loggedIn = true;
    }
    else
    {
        Console.WriteLine("\nInvalid login. Please try again.");
    }
}