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
            Console.WriteLine($"Hello User! Welcome to your bank account.\n");
            user.ShowUserAcc();
            Console.ReadLine();
        }

        static string MenuUserPrompt(User user)
        {
            Console.Clear();
            user.ShowUserAcc();
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
                //Console.WriteLine(amount);

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
                user.AdjustSavings(amount, action);
            }
            else if (accType == "checking")
            {
                user.AdjustChecking(amount, action);
            }

            user.UserData.UpdateBankAccounts(user.Name, user.GetSavingsBalance(), user.GetCheckingBalance());
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
                TransferFunds(user.GetCheckingBalance(), user.GetSavingsBalance(), 1, user);
            } else if(option == "6")
            {
                TransferFunds(user.GetSavingsBalance(), user.GetCheckingBalance(), 2, user);
            } else if(option == "q")
            {
                Console.WriteLine("Quitting program...");
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

        static void RunBank()
        {
            string userOption;
            bool isUserLoggedIn = true;
            var chris = new User("chris", "1111");
            //AllUsersDB.AddUser("chris", "1111");
            //AllUsersDB.AddUser("notChris", "9999");

            //var chrisdata = new UserData(chris.Name);


            // Greet the user...
            IntroduceUser(chris);

            while (isUserLoggedIn)
            {
                //Ask what they want to do next...
                userOption = MenuUserPrompt(chris);
                isUserLoggedIn = HandleUserOption(userOption, chris);
            }
        }

        static void CreateUserAccount()
        {
            //bool isComplete = false;

            Console.WriteLine("What is your username?");
            string username = Console.ReadLine();

            Console.WriteLine("What is your pin?");
            string pin = Console.ReadLine();

            if (AllUsersDB.UsersList.ContainsKey(username.Trim()))
            {
                Console.WriteLine("username already taken. Sorry! Try again.");
            } else
            {
                Console.WriteLine($"Welcome to the bank of 'Give us your money'. Your username is {username} and your pin is {pin}");
            }
        }

        static void Main(string[] args)
        {
            // Iniialize users database.
 
            AllUsersDB.GetUsers();

            bool menuActive = true;


            while(menuActive)
            {
                Console.WriteLine("What would you like to do?\n" +
                "(1) Create Account\n" +
                "(2) Log In\n" +
                "(q) Exit program\n");

                string userOption = Console.ReadLine();

                if (userOption == "1")
                {
                    Console.WriteLine("Creating account...");
                    CreateUserAccount();
                }
                else if (userOption == "2")
                {
                    Console.WriteLine("Logging in...");
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

            Console.WriteLine("What's your username?");


            Console.WriteLine("What's your PIN?");

            //const string FILE_PATH = "../../../files/bank_info.csv";
            RunBank();
        }
    }
}