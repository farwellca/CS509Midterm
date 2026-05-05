using System.Transactions;
using model;

bool loggedIn = false;

while (!loggedIn)
{
    try
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
            throw new ArgumentException("Error: Invalid login information.");
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}
