using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine
{
    class UserInterface
    {
        public static UsersDB AllUsersDB = new UsersDB();

        public static MoneyOps MoneyFuncs = new MoneyOps();

        static void IntroduceUser(User user)
        {
            Console.Clear();
            Console.WriteLine($"Hello {user.Name}! Welcome to your bank account.\n");
            user.Accounts[0].ShowUserAcc();
            Console.ReadLine();
        }

        public string MenuUserPrompt(Account acc)
        {
            Console.Clear();
            acc.ShowUserAcc();
            Console.WriteLine("What transaction would you like to do next?\n\n" +
                "(1) Deposit to savings\n" +
                "(2) Deposit to checking\n" +
                "(3) Withdraw from savings\n" +
                "(4) Withdraw from checking\n" +
                "(5) Transfer from checking to savings\n" +
                "(6) Transfer from savings to checking\n" +
                "(q) Log out");

            return (Console.ReadLine());
        }

        public void LoggedInMenu()
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

        static void CreateUserAccount()
        {
            bool isComplete = false;
            Console.Clear();
            Console.WriteLine("Creating account:");

            while (!isComplete)
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

                    if (pin == "q")
                    {
                        isComplete = true;
                    }
                    else if (pin.Count() >= 4)
                    {
                        AllUsersDB.AddUser(username, pin);
                        Console.WriteLine($"Welcome to the bank of 'Give us your money'. Your username is {username} and your pin is {pin}");
                        isComplete = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid PIN. A valid PIN must be at least four numbers long");
                    }

                }
            }

        }

        public void LogUserIn()
        {
            bool userLoginMenu = true;

            while (userLoginMenu)
            {
                Console.WriteLine("What is your username?\n" +
                    "(q) = Quit");
                string username = Console.ReadLine();

                Console.WriteLine("What is your pin?\n" +
                    "(q) = Quit");
                string pin = Console.ReadLine();

                if (AllUsersDB.UsersList.ContainsKey(username.Trim()))
                {
                    if (AllUsersDB.UsersList[username.Trim()] == pin.Trim())
                    {
                        Console.WriteLine($"Logging in as {username}...");
                        // Add a 'choose account' screen first!
                        HandleBankAccounts(username, pin);
                        userLoginMenu = false;
                        //RunBank(username, pin);
                    }
                    else if (pin == "q")
                    {
                        Console.WriteLine("exiting...");
                        userLoginMenu = false;
                    }
                    else
                    {
                        Console.WriteLine("Invalid username/password combination");
                    }
                }
                else if (username == "q")
                {
                    Console.WriteLine("exiting...");
                    userLoginMenu = false;
                }
                else
                {
                    Console.WriteLine("Invalid username/password combination");
                }
            }

        }

        public void HandleBankAccounts(string name, string pin)
        {
            var user = new User(name, pin);
            bool handled = false;

            while (!handled)
            {
                Console.WriteLine("What would you like to do?\n" +
                    "(1) Create a new bank account. \n" +
                    "(2) Manage an account\n" +
                    "(3) Close an account\n" +
                    "(q) Exit to previous menu");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.WriteLine("Creating new account...");
                    user.CreateAccount();
                }
                else if (choice == "2")
                {
                    Console.WriteLine("Managing account...");
                    // Show all accounts.
                    user.GetAccounts();


                    // Variables for when the user chooses the account and for the account to be referenced in the future.
                    bool chosen = false;

                    while (!chosen)
                    {
                        // Allow user to pick which account they want to work with.
                        bool hasAccount = user.ShowAccounts();

                        if (hasAccount)
                        {
                            string accChoice = Console.ReadLine();
                            int accRef;
                            bool isNumeric = int.TryParse(accChoice, out int n);
                            if (isNumeric)
                            {
                                if (Convert.ToInt32(accChoice) > user.Accounts.Count())
                                {
                                    Console.WriteLine("not a valid option");
                                }
                                else
                                {
                                    accRef = Convert.ToInt32(accChoice) - 1;
                                    Console.WriteLine("Opening account...");
                                    chosen = true;
                                    RunBank(user.Accounts[accRef]);
                                }
                            }
                            else if (accChoice == "q")
                            {
                                Console.WriteLine("Exiting selection...");
                                chosen = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid option");
                            }
                        }
                        else
                        {
                            chosen = true;
                        }


                        // THEN run bank with that account.
                    }

                }
                else if (choice == "3")
                {
                    Console.WriteLine("Closing account...");
                    user.CloseAccount();
                }
                else if (choice == "q")
                {
                    Console.WriteLine("Exiting to previous menu...");
                    handled = true;
                }
                else
                {
                    Console.WriteLine("Invalid Choice.");
                }
            }
        }

        static bool HandleUserOption(string option, Account useracc)
        {
            if (option == "1")
            {
                MoneyFuncs.ModifyBankAcc(useracc, "deposit", "savings");
            }
            else if (option == "2")
            {
                MoneyFuncs.ModifyBankAcc(useracc, "deposit", "checking");
            }
            else if (option == "3")
            {
                MoneyFuncs.ModifyBankAcc(useracc, "withdraw", "savings");
            }
            else if (option == "4")
            {
                MoneyFuncs.ModifyBankAcc(useracc, "withdraw", "checking");
            }
            else if (option == "5")
            {
                MoneyFuncs.TransferFunds(useracc.GetCheckingBalance(), useracc.GetSavingsBalance(), 1, useracc);
            }
            else if (option == "6")
            {
                MoneyFuncs.TransferFunds(useracc.GetSavingsBalance(), useracc.GetCheckingBalance(), 2, useracc);
            }
            else if (option == "q")
            {
                Console.Clear();
                Console.WriteLine($"Logging out of {useracc.Name}...");
                return false;
            }
            else
            {
                Console.WriteLine("Invalid option");
            }
            return true;
        }

        public void RunBank(Account acc)
        {
            string userOption;
            bool isAccountLoggedIn = true;

            while (isAccountLoggedIn)
            {
                //Ask what they want to do next...
                userOption = MenuUserPrompt(acc);
                isAccountLoggedIn = HandleUserOption(userOption, acc);
            }
        }
    }
}
