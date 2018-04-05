﻿using System;
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
                DisplaySavingsBalance();
            } else if(option == "withdraw")
            {
                if(amount > savingsBalance)
                {
                    //We can't do it!
                    Console.WriteLine("You don't have enough money!!!");
                } else {
                    savingsBalance -= amount;
                }
                //catch (Exception e)
                //{
                //    Console.WriteLine(e);
                //}

                DisplaySavingsBalance();
            }
        }

        public void AdjustChecking(double amount, string option)
        {
            if(option == "deposit")
            {
                checkingBalance += amount;
                DisplayCheckingBalance();
            } else if(option == "withdraw")
            {
                if(amount > checkingBalance)
                {
                    // We can't withdraw!!!
                    Console.WriteLine("You don't have enough money!!!");
                } else
                {
                    checkingBalance -= amount;
                }

                DisplayCheckingBalance();
            }
        }
        
    }
}
