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

        public void DisplaySavingsBalance()
        {
            Console.WriteLine(savingsBalance);
            Console.ReadLine();
        }

        public void DisplayCheckingBalance()
        {
            Console.WriteLine(checkingBalance);
            Console.ReadLine();
        }

        public void AdjustSavings(double amount, string option)
        {
            if(option == "deposit")
            {
                savingsBalance += amount;
                Console.WriteLine(savingsBalance);
            } else if(option == "withdraw")
            {
                savingsBalance -= amount;
                Console.WriteLine(savingsBalance);
            }
        }

        public void AdjustChecking(double amount, string option)
        {
            if(option == "deposit")
            {
                checkingBalance += amount;
                Console.WriteLine(checkingBalance);
            } else if(option == "withdraw")
            {
                savingsBalance -= amount;
                Console.WriteLine(checkingBalance);
            }
        }
        
    }
}
