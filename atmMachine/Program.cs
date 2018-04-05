﻿using System;
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

        static void HandleUserOption(string option)
        {
                if(option == "1")
                {
                    Console.WriteLine("Depositing to savings...");
                } else if(option == "2")
                {
                    Console.WriteLine("Depositing to checking...");
                } else if(option == "3")
                {
                    Console.WriteLine("Withdrawing from savings...");
                } else if(option == "4")
                {
                    Console.WriteLine("Withdrawing from checking...");
                } else if(option == "5")
                {
                    Console.WriteLine("Transfer from checking to savings...");
                } else if(option == "6")
                {
                    Console.WriteLine("Transfer from savings to checking...");
                } else if(option == "q")
                {
                    Console.WriteLine("Quitting program...");
                } else
                {
                    Console.WriteLine("That wasn't an option! Try again!");
                }
        }

        static void Main(string[] args)
        {
            string userOption;
            bool isUserLoggedIn = true;
            var chris = new User();

            // Greet the user...
            IntroduceUser(chris);

            while(isUserLoggedIn)
            {
                //Ask what they want to do next...
                userOption = MenuUserPrompt();
                HandleUserOption(userOption);

            }

        }
    }
}
