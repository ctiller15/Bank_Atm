using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            var chris = new User();
            chris.DisplaySavingsBalance();
            chris.DisplayCheckingBalance();

        }
    }
}
