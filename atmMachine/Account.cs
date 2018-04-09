using System;
using System.IO;
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

        private string FilePath { get; set; }

        public void PullAccountData()
        {
            double savings = 0;
            double checking = 0;
            if (File.Exists($"{FilePath}/bank_money.csv"))
            {
                //Console.WriteLine("File exists!");
                using (var reader = new StreamReader($"{FilePath}/bank_money.csv"))
                {
                    while (reader.Peek() > -1)
                    {
                        var line = reader.ReadLine().Split(',');
                        
                        // The first value of a line can be "savings" or "checking".
                        // Save those values to a variable, and then set the values.
                        if(line[0].ToLower().Trim() == "savings")
                        {
                            savings = Convert.ToDouble(line[1]);
                        } else if(line[0].ToLower().Trim() == "checking")
                        {
                            checking = Convert.ToDouble(line[1]);
                        }
                    }
                }
            }
            SetAccounts(savings, checking);
        }

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
            FilePath = $"../../../userData/Users/{Owner}/{Name}";
            PullAccountData();
        }

    }
}
