using System.Transactions;
using model;
using dal;

class ATM
{
    public static void Main()
    {
        bool loggedIn = false;

        Dal dal = new Dal();

        IAdminModel _adminModel = new AdminModel(dal);
        ICustomerModel _customerModel = new CustomerModel(dal);
        ILoginModel _loginModel = new LoginModel(dal);

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
                            _customerModel.ShowCustomerMenu(username, pin);
                            break;
                        case "Admin":
                            _adminModel.ShowAdminMenu();
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
}