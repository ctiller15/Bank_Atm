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
            Console.WriteLine($"Hello User! Welcome to your bank account.\n" +
                $"Checking: {user.GetCheckingBalance()}\n" +
                $"Savings: {user.GetSavingsBalance()}");
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            var chris = new User();
            //chris.DisplaySavingsBalance();
            //chris.DisplayCheckingBalance();
            //Console.WriteLine($"Hello Chris! Welcome to your bank account.\n" +
            //    $"Checking: {chris.GetCheckingBalance()}\n" +
            //    $"Savings: {chris.GetSavingsBalance()}");
            //Console.ReadLine();
            IntroduceUser(chris);

        }
    }
}
