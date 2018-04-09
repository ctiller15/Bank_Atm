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

        public void PullBankAccountData(User user)
        {
            if(File.Exists(FilePath + "bank_money.csv"))
            {
                using (var reader = new StreamReader(FilePath + "bank_money.csv"))
                {
                    while (reader.Peek() > -1)
                    {
                        var line = reader.ReadLine().Split(',');
                        string name = line[0];
                        double[] bankArr = new double[] { Convert.ToDouble(line[1]), Convert.ToDouble(line[2]) };
                        BankData[name.Trim()] = bankArr;
                        //user.Accounts[0].SetAccounts(bankArr[0], bankArr[1]);
                    }
                }
            }

        }

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
                    writer.WriteLine($"{data.Key} , {data.Value[0]}, {data.Value[1]}");
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

        //public void PullAccountData(Account acc)
        //{
        //    if (File.Exists($"{FilePath}{acc.Name}/bank_money.csv"))
        //    {
        //        Console.WriteLine("File exists!");
        //        using (var reader = new StreamReader($"{FilePath}{acc.Name}/bank_money.csv"))
        //        {
        //            while (reader.Peek() > -1)
        //            {
        //                var line = reader.ReadLine();
        //                Console.WriteLine($"{ line[0]} {line[1]}");
        //                //string name = line[0];
        //                //double[] bankArr = new double[] { Convert.ToDouble(line[1]), Convert.ToDouble(line[2]) };
        //                //BankData[name.Trim()] = bankArr;
        //                //user.Accounts[0].SetAccounts(bankArr[0], bankArr[1]);
        //            }
        //        }
        //    }
        //}

        public UserData(string name)
        {
            FilePath = $"../../../userData/Users/{name}/";
            Directory.CreateDirectory("../../../userData/Users");
            Directory.CreateDirectory(FilePath);
            //PullBankAccountData(user);
        }

        public void LogTransactions(string action, double amount, string accName)
        {
            string Transaction = $"{action} {amount:C2} {DateTime.Now} {accName}\n";
            Console.WriteLine(Transaction);
            using (StreamWriter writer = File.AppendText(FilePath + "transaction_log.txt"))
            {
                writer.WriteLine(Transaction);
            }
        }
    }
}

