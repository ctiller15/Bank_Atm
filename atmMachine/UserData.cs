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

        //public void PullBankAccountData(User user)
        //{
        //    if(File.Exists(FilePath + "bank_money.csv"))
        //    {
        //        using (var reader = new StreamReader(FilePath + "bank_money.csv"))
        //        {
        //            while (reader.Peek() > -1)
        //            {
        //                var line = reader.ReadLine().Split(',');
        //                string name = line[0];
        //                double[] bankArr = new double[] { Convert.ToDouble(line[1]), Convert.ToDouble(line[2]) };
        //                BankData[name.Trim()] = bankArr;
        //            }
        //        }
        //    }
        //}

        //public void UpdateBankAccounts(string name, double savings, double checking)
        public void UpdateBankAccounts(Account acc)
        {
            double[] accounts = new double[] { acc.GetSavingsBalance(), acc.GetCheckingBalance() };
            foreach (var data in BankData)
            {
                Console.WriteLine($"{data.Key}, {acc.Name} , {data.Value[0]} , {data.Value[1]}");
            }
            Console.WriteLine(BankData.ContainsKey(acc.Name.Trim()));
            Console.WriteLine($"{acc.Name.GetType()}, {acc.Name}");
            // Ha! Redundant.
            if(!BankData.ContainsKey(acc.Name.Trim()))
            {
                BankData[acc.Name.Trim()] = accounts;
            } else
            {
                BankData[acc.Name.Trim()] = accounts;
            }

            Console.WriteLine($"{accounts[0]}, {accounts[1]}");
            foreach (var data in BankData)
            {
                Console.WriteLine($"Logging key... {data.Key}");
                Console.WriteLine($"{data.Key} , {data.Value[0]}, {data.Value[1]}");
            }
            Console.ReadLine();

            using (var writer = new StreamWriter($"{FilePath}{acc.Name}/bank_money.csv"))
            {
                foreach (var data in BankData)
                {
                    Console.WriteLine("Writing to file...");
                    //writer.WriteLine($"{data.Key} , {data.Value[0]}, {data.Value[1]}");
                    writer.WriteLine($"savings , {data.Value[0]}");
                    writer.WriteLine($"checking , {data.Value[1]}");
                }
            }
            Console.WriteLine("-----------------------------------");
        }

        public void SaveAccount(Account acc)
        {
            Directory.CreateDirectory($"{FilePath}{acc.Name}");
            using (var writer = new StreamWriter($"{FilePath}{acc.Name}/bank_money.csv"))
            {
                writer.WriteLine($"savings , {acc.GetSavingsBalance()}");
                writer.WriteLine($"checking , {acc.GetCheckingBalance()}");
            }
        }

        public UserData(string name)
        {
            FilePath = $"../../../userData/Users/{name}/";
            Directory.CreateDirectory("../../../userData/Users");
            Directory.CreateDirectory(FilePath);
            //PullBankAccountData(user);
        }


        // TODO: Adjust this so it outputs in the right location.
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