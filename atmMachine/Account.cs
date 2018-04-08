﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine
{
    class Account
    {
        public string Name { get; set; }
        public string Owner { get; set; }

        private double savingsBalance = 0;

        private double checkingBalance = 0;

        private double CheckValidWithdrawal(double withdrawn, double balance)
        {
            if (withdrawn > balance)
            {
                Console.WriteLine("You don't have enough money!!!");
                return balance;
            }
            else
            {
                return (balance - withdrawn);
            }
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
            if (option == "deposit")
            {
                savingsBalance += amount;
            }
            else if (option == "withdraw")
            {
                savingsBalance = CheckValidWithdrawal(amount, savingsBalance);
            }
        }

        public void AdjustChecking(double amount, string option)
        {
            if (option == "deposit")
            {
                checkingBalance += amount;
            }
            else if (option == "withdraw")
            {
                checkingBalance = CheckValidWithdrawal(amount, checkingBalance);
            }
        }

        public void SetAccounts(double savingsAmt, double checkingAmt)
        {
            savingsBalance = savingsAmt;
            checkingBalance = checkingAmt;
        }

        public Account()
        {

        }

        public Account(string AccName, string AccOwner)
        {
            Name = AccName;
            Owner = AccOwner;
        }

    }
}