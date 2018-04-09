using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine
{
    class UserData
    {
        public string FilePath { get; set; }

        private Dictionary<string, double[]> BankData = new Dictionary<string, double[]>();

        public void UpdateBankAccounts(Account acc)
        {
            double[] accounts = new double[] { acc.GetSavingsBalance(), acc.GetCheckingBalance() };

            BankData[acc.Name.Trim()] = accounts;

            using (var writer = new StreamWriter($"{FilePath}{acc.Name}/bank_money.csv"))
            {
                foreach (var data in BankData)
                {
                    writer.WriteLine($"savings , {data.Value[0]}");
                    writer.WriteLine($"checking , {data.Value[1]}");
                }
            }
            Console.WriteLine("-----------------------------------");
        }

        // Saving the account.
        public void SaveAccount(Account acc)
        {
            Directory.CreateDirectory($"{FilePath}{acc.Name}");
            using (var writer = new StreamWriter($"{FilePath}{acc.Name}/bank_money.csv"))
            {
                writer.WriteLine($"savings , {acc.GetSavingsBalance()}");
                writer.WriteLine($"checking , {acc.GetCheckingBalance()}");
            }
        }

        // Initializing the UserData class.
        public UserData(string name)
        {
            FilePath = $"../../../userData/Users/{name}/";
            Directory.CreateDirectory("../../../userData/Users");
            Directory.CreateDirectory(FilePath);
        }

        // Logs the transactions for each individual account.
        public void LogTransactions(string action, double amount, Account acc)
        {
            string Transaction = $"{action} {amount:C2} {DateTime.Now} {acc.Name}\n";
            Console.WriteLine(Transaction);
            using (StreamWriter writer = File.AppendText($"{FilePath}{acc.Name}/transaction_log.txt"))
            {
                writer.WriteLine(Transaction);
            }
        }
    }
}