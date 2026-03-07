// See https://aka.ms/new-console-template for more information

using System.Transactions;
using model;

Console.WriteLine("Hello, World!");

Console.WriteLine(AdminModel.GetAll());



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
        //Console.WriteLine(LoginModel.getUserType(username, pin));

        switch (LoginModel.getUserType(username, pin))
        {
            case "Customer":
                CustomerModel.ShowCustomerMenu();
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