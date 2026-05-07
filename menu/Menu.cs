namespace menu;

using model;
using dal;

/// <summary>
/// Handles UI for login and menus.
/// </summary>
public class Menu
{
    private readonly IAdminModel _adminModel;
    private readonly ICustomerModel _customerModel;
    private readonly ILoginModel _loginModel;
    private readonly IConsole console;

    public Menu()
    {
        Dal dal = new Dal();

        console = new ConsoleWrapper();
        _adminModel = new AdminModel(dal, console);
        _customerModel = new CustomerModel(dal, console);
        _loginModel = new LoginModel(dal);
    }

    /// <summary>
    /// Handles UI for logging in and admin and customer menus.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
	public void Run()
    {
        bool loggedIn = false;

        while (!loggedIn)
        {
            try
            {

                Console.Write("Enter login: ");
                string? username = Console.ReadLine();

                Console.Write("Enter Pin code: ");
                string? pin = Console.ReadLine();

                if (_loginModel.Login(username, pin))
                {
                    Console.WriteLine("Success!");

                    switch (_loginModel.GetUserType(username, pin))
                    {
                        case "Customer":
                            ShowCustomerMenu(username, pin);
                            break;
                        case "Admin":
                            ShowAdminMenu();
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
    }

    private void ShowCustomerMenu(string username, string pin)
    {
        Console.Clear();

        bool done = false;

        while (!done)
        {
            try
            {
                console.WriteLine("\n1--Withdraw Cash\n2--Deposit Cash\n3--Display Balance\n4--Exit");

                string? input = console.ReadLine();

                switch (input)
                {
                    case "1":
                        _customerModel.withdrawCash(username, pin);
                        break;
                    case "2":
                        _customerModel.depositCash(username, pin);
                        break;
                    case "3":
                        _customerModel.displayBalance(username, pin);
                        break;
                    case "4":
                        Console.Clear();
                        console.WriteLine("Have a nice day.");
                        done = true;
                        break;
                    default:
                        Console.Clear();
                        console.WriteLine("Invalid input. Please try again.");
                        break;
                }
            }
            catch (Exception e)
            {
                console.WriteLine(e.Message);
            }
        }
    }

    private void ShowAdminMenu()
    {
        Console.Clear();

        bool done = false;

        while (!done)
        {
            try
            {
                console.WriteLine("\n1--Create New Account\n2--Delete Existing Accont\n3--Update Account Information\n4--Search for Account\n5--Exit");

                string? input = console.ReadLine();

                switch (input)
                {
                    case "1":
                        _adminModel.createAccount();
                        break;
                    case "2":
                        _adminModel.deleteAccount();
                        break;
                    case "3":
                        _adminModel.updateAccount();
                        break;
                    case "4":
                        _adminModel.searchAccount();
                        break;
                    case "5":
                        Console.Clear();
                        console.WriteLine("Have a nice day.");
                        done = true;
                        break;
                    default:
                        Console.Clear();
                        console.WriteLine("Invalid input. Please try again.");
                        break;
                }
            }
            catch (Exception e)
            {
                console.WriteLine(e.Message);
            }
        }
    }
}
