using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine
{
    class Program
    {
        public static UsersDB AllUsersDB = new UsersDB();

        static void IntroduceUser(User user)
        {
            Console.Clear();
            Console.WriteLine($"Hello {user.Name}! Welcome to your bank account.\n");
            user.Accounts[0].ShowUserAcc();
            Console.ReadLine();
        }

        static string MenuUserPrompt(User user)
        {
            Console.Clear();
            user.Accounts[0].ShowUserAcc();
            Console.WriteLine("What transaction would you like to do next?\n\n" +
                "(1) Deposit to savings\n" +
                "(2) Deposit to checking\n" +
                "(3) Withdraw from savings\n" +
                "(4) Withdraw from checking\n" +
                "(5) Transfer from checking to savings\n" +
                "(6) Transfer from savings to checking\n" +
                "(q) Log out");

            return(Console.ReadLine());
        }

        static void ModifyBankAcc(User user, string action, string accType)
        {
            double amount = 0;
            Console.WriteLine($"{accType}\n" +
                $"How much would you like to {action}? ($)");

            TrySubmittedVal(user, amount, accType, action, UpdateAccType);
        }

        static void TrySubmittedVal(User user, double amt, string type, string action, Action<User, double, string, string> Run)
        {
            try
            {
                amt = Convert.ToDouble(Console.ReadLine());
            }
            catch (FormatException err)
            {
                Console.WriteLine($"Exception caught: {err}");
                Console.WriteLine("That wasn't a valid dollar amount");
            }
            finally
            {
                Run(user, amt, type, action);
            }
        }

        // In each case, withdraw from the first account, and deposit into the other!
        static void TransferFunds(double acc1Funds, double acc2Funds, int option, User user)
        {
            double amount = 0;
            string accType1 = "";
            string accType2 = "";

            if (option == 1)
            {
                accType1 = "checking";
                accType2 = "savings";
            }
            else if (option == 2)
            {
                accType1 = "savings";
                accType2 = "checking";
            }
            Console.WriteLine($"How much would you like to transfer?");

            try
            {
                // Check if it is a valid number.
                amount = Convert.ToDouble(Console.ReadLine());
            }
            catch (FormatException err)
            {
                Console.WriteLine($"Exception caught: {err}");
                Console.WriteLine("That wasn't a valid dollar amount!");
            }
            finally
            {
                Console.WriteLine(amount);
                if(amount > acc1Funds)
                {
                    Console.WriteLine("That won't work! Aborting transfer");
                } else
                {
                    RunTransfer(user, amount, accType1, accType2);
                }
            }
        }

        static void RunTransfer(User user, double amt, string accType1, string accType2)
        {

            // Withdraw from the first account.
            UpdateAccType(user, amt, accType1, "withdraw");
            // Deposit into the second account.
            UpdateAccType(user, amt, accType2, "deposit");
        }

        static void UpdateAccType(User user, double amount, string accType, string action)
        {
            if (accType == "savings")
            {
                user.Accounts[0].AdjustSavings(amount, action);
            }
            else if (accType == "checking")
            {
                user.Accounts[0].AdjustChecking(amount, action);
            }

            user.UserData.UpdateBankAccounts(user.Name, user.Accounts[0].GetSavingsBalance(), user.Accounts[0].GetCheckingBalance());
            user.UserData.LogTransactions(action, amount, accType);
            Console.WriteLine("We're logging!");
            Console.ReadLine();
        }

        static bool HandleUserOption(string option, User user)
        {
            if (option == "1")
            {
                ModifyBankAcc(user, "deposit", "savings");
            }
            else if (option == "2")
            {
                ModifyBankAcc(user, "deposit", "checking");
            }
            else if (option == "3")
            {
                ModifyBankAcc(user, "withdraw", "savings");
            }
            else if (option == "4")
            {
                ModifyBankAcc(user, "withdraw", "checking");
            } else if(option == "5")
            {
                TransferFunds(user.Accounts[0].GetCheckingBalance(), user.Accounts[0].GetSavingsBalance(), 1, user);
            } else if(option == "6")
            {
                TransferFunds(user.Accounts[0].GetSavingsBalance(), user.Accounts[0].GetCheckingBalance(), 2, user);
            } else if(option == "q")
            {
                Console.Clear();
                Console.WriteLine($"Logging out of {user.Name}...");
                return false;
            } else
            {
                Console.WriteLine("Invalid option");
            }
            return true;
        }

        static void CreateUser(string name, string PIN)
        {
            var user = new User(name, PIN);
        }

        static void RunBank(string name, string pin)
        {
            string userOption;
            bool isUserLoggedIn = true;
            var user = new User(name, pin);

            // Greet the user...
            IntroduceUser(user);

            while (isUserLoggedIn)
            {
                //Ask what they want to do next...
                userOption = MenuUserPrompt(user);
                isUserLoggedIn = HandleUserOption(userOption, user);
            }
        }

        static void CreateUserAccount()
        {
            bool isComplete = false;
            Console.Clear();
            Console.WriteLine("Creating account:");

            while(!isComplete)
            {
                Console.WriteLine("What is your username? \n" +
                    "(q = quit)");
                string username = Console.ReadLine();

                if (AllUsersDB.UsersList.ContainsKey(username.Trim()) && username.Count() > 1)
                {
                    Console.WriteLine("username already taken. Sorry! Try again.");
                }
                else if (username == "q")
                {
                    Console.WriteLine("Returning to main menu...");
                    isComplete = true;
                }
                else
                {
                    Console.WriteLine("What is your pin?\n" +
                        "(q = quit)");
                    string pin = Console.ReadLine();

                    if(pin == "q")
                    {
                        isComplete = true;
                    } else if(pin.Count() >= 4)
                    {
                        AllUsersDB.AddUser(username, pin);
                        Console.WriteLine($"Welcome to the bank of 'Give us your money'. Your username is {username} and your pin is {pin}");
                        isComplete = true;
                    } else
                    {
                        Console.WriteLine("Invalid PIN. A valid PIN must be at least four numbers long");
                    }

                }
            }

        }

        static void LogUserIn()
        {
            Console.WriteLine("What is your username?");
            string username = Console.ReadLine();

            Console.WriteLine("What is your pin?");
            string pin = Console.ReadLine();

            if(AllUsersDB.UsersList.ContainsKey(username.Trim()))
            {
                if(AllUsersDB.UsersList[username.Trim()] == pin.Trim())
                {
                    Console.WriteLine($"Logging in as {username}...");
                    // Add a 'choose account' screen first!
                    HandleBankAccounts(username, pin);
                    //RunBank(username, pin);
                }
            }
        }

        static void HandleBankAccounts(string name, string pin)
        {
            var user = new User(name, pin);
            bool handled = false;

            while(!handled)
            {
                Console.WriteLine("What would you like to do?\n" +
                    "(1) Create a new bank account. \n" +
                    "(2) Manage an account\n" +
                    "(3) Close an account\n" +
                    "(q) Exit to previous menu");

                string choice = Console.ReadLine();
                
                if(choice == "1")
                {
                    Console.WriteLine("Creating new account...");
                    user.CreateAccount();
                } else if(choice == "2")
                {
                    Console.WriteLine("Managing account...");
                    // Show all accounts.
                    user.GetAccounts();
                    // Allow user to pick which account they want to work with.
                    // THEN run bank with that account.
                }
                else if(choice == "3")
                {
                    Console.WriteLine("Closing account...");
                }
                else if(choice == "q")
                {
                    Console.WriteLine("Exiting to previous menu...");
                    handled = true;
                }
                else
                {
                    Console.WriteLine("Invalid Choice.");
                }
            }
            

            // List out all accounts here.


        }

        static void LoggedInMenu()
        {
            bool menuActive = true;

            while (menuActive)
            {
                Console.WriteLine("What would you like to do?\n" +
                "(1) Create Account\n" +
                "(2) Log In\n" +
                "(q) Exit program\n");

                string userOption = Console.ReadLine();

                if (userOption == "1")
                {
                    CreateUserAccount();
                }
                else if (userOption == "2")
                {
                    LogUserIn();
                }
                else if (userOption == "q")
                {
                    Console.WriteLine("Ending program. Hope you enjoyed!");
                    menuActive = false;
                }
                else
                {
                    Console.WriteLine("Invalid option. Please try again.");
                }
            }
        }

        static void Main(string[] args)
        {
            // Iniialize users database.
 
            AllUsersDB.GetUsers();

            LoggedInMenu();

            //bool menuActive = true;


            //while(menuActive)
            //{
            //    Console.WriteLine("What would you like to do?\n" +
            //    "(1) Create Account\n" +
            //    "(2) Log In\n" +
            //    "(q) Exit program\n");

            //    string userOption = Console.ReadLine();

            //    if (userOption == "1")
            //    {
            //        CreateUserAccount();
            //    }
            //    else if (userOption == "2")
            //    {
            //        LogUserIn();
            //    }
            //    else if (userOption == "q")
            //    {
            //        Console.WriteLine("Ending program. Hope you enjoyed!");
            //        menuActive = false;
            //    }
            //    else
            //    {
            //        Console.WriteLine("Invalid option. Please try again.");
            //    }
            //}
        }
    }
}