using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine
{
    class User
    {
        private double savingsBalance = 0;

        private double checkingBalance = 0;

        private double CheckValidWithdrawal(double withdrawn, double balance)
        {
            if(withdrawn > balance)
            {
                Console.WriteLine("You don't have enough money!!!");
                return balance;
            } else
            {
                return(balance - withdrawn);
            }
        }

        public void DisplaySavingsBalance()
        {
            Console.WriteLine($"Current savings balance: {savingsBalance}");
            Console.ReadLine();
        }

        public void DisplayCheckingBalance()
        {
            Console.WriteLine($"Current checking balance: {checkingBalance}");
            Console.ReadLine();
        }

        public void ShowUserAcc()
        {
            Console.WriteLine($"Checking: {GetCheckingBalance()}\n" +
                $"Savings: {GetSavingsBalance()}\n");
        }

        public double GetSavingsBalance()
        {
            return savingsBalance;
        }
        public double GetCheckingBalance()
        {
            return checkingBalance;
        }

        public void AdjustSavings(double amount, string option)
        {
            if(option == "deposit")
            {
                savingsBalance += amount;
                //DisplaySavingsBalance();
            } else if(option == "withdraw")
            {
                savingsBalance = CheckValidWithdrawal(amount, savingsBalance);

                //DisplaySavingsBalance();
            }
            //ShowUserAcc();
        }

        public void AdjustChecking(double amount, string option)
        {
            if(option == "deposit")
            {
                checkingBalance += amount;
                //DisplayCheckingBalance();
            } else if(option == "withdraw")
            {

                checkingBalance = CheckValidWithdrawal(amount, checkingBalance);

                //DisplayCheckingBalance();
            }
            //ShowUserAcc();
        }
        
    }
}
