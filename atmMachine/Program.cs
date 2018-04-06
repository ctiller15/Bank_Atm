using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine
{
    class Program
    {
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
            bool finished = false;
            while(!finished)
            {
                double amount = 0;
                Console.WriteLine($"{accType}\n" +
                    $"How much would you like to {action}? ($)");

                try
                {
                    amount = Convert.ToDouble(Console.ReadLine());
                    //Console.WriteLine(amount);

                }
                catch (FormatException err)
                {
                    Console.WriteLine($"Exception caught: {err}");
                    Console.WriteLine("That wasn't a valid dollar amount");
                }
                finally
                {
                    UpdateAccType(user, amount, accType, action);
                    user.ShowUserAcc();

                    Console.WriteLine($"Would you like to keep {action}ing? (Y: yes) (N: no)");
                    string answer = Console.ReadLine();
                    if( answer.ToLower() == "y")
                    {
                        finished = false;
                    } else if(answer.ToLower() == "n")
                    {
                        finished = true;
                    }
                }
            }
            Console.WriteLine($"Finished {action}ing");
            user.ShowUserAcc();
        }

        // In each case, withdraw from the first account, and deposit into the other!
        static void TransferFunds(double acc1Funds, double acc2Funds, int option, User user)
        {
            double amount = 0;
            string accType1 = "";
            string accType2 = "";
            Console.WriteLine($"How much would you like to transfer?");

            try
            {
                // Check if it is a valid number.
                amount = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine(amount);
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
                    // Otherwise, commit to the transfer.
                    if(option == 1)
                    {
                        accType1 = "checking";
                        accType2 = "savings";
                    } else if(option == 2)
                    {
                        accType1 = "savings";
                        accType2 = "checking";
                    }
                    // Withdraw from the first account.
                    UpdateAccType(user, amount, accType1, "withdraw");
                    // Deposit into the second account.
                    UpdateAccType(user, amount, accType2, "deposit");
                    user.ShowUserAcc();
                }
            }
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

        static void Main(string[] args)
        {
            string userOption;
            bool isUserLoggedIn = true;
            var chris = new User();

            // Greet the user...
            IntroduceUser(chris);

            while (isUserLoggedIn)
            {
                //Ask what they want to do next...
                userOption = MenuUserPrompt(chris);
                isUserLoggedIn = HandleUserOption(userOption, chris);

            }

        }
    }
}