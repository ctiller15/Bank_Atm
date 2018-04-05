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

        static string MenuUserPrompt()
        {
            Console.WriteLine("What transaction would you like to do next?\n\n" +
                "(1) Deposit to savings\n" +
                "(2) Deposit to checking\n" +
                "(3) Withdraw from savings\n" +
                "(4) Withdraw from checking\n" +
                "(5) Transfer from checking to savings\n" +
                "(6) Transfer from savings to checking\n" +
                "(q) Quit the program");

            return(Console.ReadLine());
        }

        static void Main(string[] args)
        {
            string userOption;
            var chris = new User();

            // Greet the user...
            IntroduceUser(chris);

            //Ask what they want to do next...
            //Console.WriteLine("What transaction would you like to do next?\n\n" +
            //    "(1) Deposit to savings\n" +
            //    "(2) Deposit to checking\n" +
            //    "(3) Withdraw from savings\n" +
            //    "(4) Withdraw from checking\n" +
            //    "(5) Transfer from checking to savings\n" +
            //    "(6) Transfer from savings to checking\n" +
            //    "(q) Quit the program");

            //Console.ReadLine();
            userOption = MenuUserPrompt();
            Console.WriteLine(userOption);
            Console.ReadLine();

        }
    }
}
