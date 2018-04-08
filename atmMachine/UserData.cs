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
                        user.SetAccounts(bankArr[0], bankArr[1]);

                        //foreach (var data in BankData)
                        //{
                        //    Console.WriteLine($"{data.Key},{name},{data.Value[0]},{data.Value[1]}");
                        //    Console.WriteLine($"{data.Key.Trim() == name.Trim()}");
                        //}

                    }
                }
            }

        }

        public void UpdateBankAccounts(string name, double savings, double checking)
        {
            //Console.WriteLine($"{name}, {savings}, {checking}");
            //Console.ReadLine();
            double[] accounts = new double[] { savings, checking };
            foreach (var data in BankData)
            {
                Console.WriteLine($"{data.Key}, {name} , {data.Value[0]} , {data.Value[1]}");
            }
            Console.WriteLine(BankData.ContainsKey(name.Trim()));
            Console.WriteLine($"{name.GetType()}, {name}");
            if(!BankData.ContainsKey(name.Trim()))
            {
                BankData[name.Trim()] = accounts;
            } else
            {
                BankData[name.Trim()] = accounts;
            }

            Console.WriteLine($"{accounts[0]}, {accounts[1]}");
            foreach (var data in BankData)
            {
                Console.WriteLine($"Logging key... {data.Key}");
                Console.WriteLine($"{data.Key} , {data.Value[0]}, {data.Value[1]}");
            }
            Console.ReadLine();

            //foreach(var data in BankData)
            //{
            //    Console.WriteLine($"{data.Key} , {data.Value[0]}, {data.Value[1]}");
            //}
            //Console.ReadLine();

            using (var writer = new StreamWriter(FilePath + "bank_money.csv"))
            {
                foreach (var data in BankData)
                {
                    Console.WriteLine("Writing to file...");
                    writer.WriteLine($"{data.Key} , {data.Value[0]}, {data.Value[1]}");
                }
            }
            Console.WriteLine("-----------------------------------");
        }

        public UserData(string name, User user)
        {
            FilePath = $"../../../userData/Users/{name}/";
            Directory.CreateDirectory("../../../userData/Users");
            Directory.CreateDirectory(FilePath);
            PullBankAccountData(user);
            //UpdateBankAccounts(name, 0, 0);
            //Console.WriteLine(FilePath);
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

        //public WriteBankAcc{
            
        ////}
    }
}

