using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine
{
    class MoneyOps
    {
        public void ModifyBankAcc(Account useracc, string action, string accType)
        {
            double amount = 0;
            Console.WriteLine($"{accType}\n" +
                $"How much would you like to {action}? ($)");

            TrySubmittedVal(useracc, amount, accType, action, UpdateAccType);
        }

        public void TrySubmittedVal(Account acc, double amt, string type, string action, Action<Account, double, string, string> Run)
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
                Run(acc, amt, type, action);
            }
        }

        // In each case, withdraw from the first account, and deposit into the other!
        public void TransferFunds(double acc1Funds, double acc2Funds, int option, Account useracc)
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
                if (amount > acc1Funds)
                {
                    Console.WriteLine("That won't work! Aborting transfer");
                }
                else
                {
                    RunTransfer(useracc, amount, accType1, accType2);
                }
            }
        }

        public void RunTransfer(Account useracc, double amt, string accType1, string accType2)
        {

            // Withdraw from the first account.
            UpdateAccType(useracc, amt, accType1, "withdraw");
            // Deposit into the second account.
            UpdateAccType(useracc, amt, accType2, "deposit");
        }

        public void UpdateAccType(Account useracc, double amount, string accType, string action)
        {
            if (accType == "savings")
            {
                useracc.AdjustSavings(amount, action);
            }
            else if (accType == "checking")
            {
                useracc.AdjustChecking(amount, action);
            }

            UserData userdata = new UserData(useracc.Owner);
            userdata.UpdateBankAccounts(useracc);
            userdata.LogTransactions(action, amount, useracc);

            Console.WriteLine("We're logging!");
            Console.ReadLine();
        }
    }
}
