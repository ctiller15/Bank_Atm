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
            using (var reader = new StreamReader(FilePath + "bank_money.csv"))
            {
                while(reader.Peek() > -1)
                {
                    var line = reader.ReadLine().Split(',');
                    string name = line[0];
                    double[] bankArr = new double[] { Convert.ToDouble(line[1]), Convert.ToDouble(line[2]) };
                    BankData[name] = bankArr;
                    user.SetAccounts(bankArr[0], bankArr[1]);

                    foreach(var data in BankData)
                    {
                        Console.WriteLine($"{data.Key} , {data.Value[0]} , {data.Value[1]}");
                    }

                }
            }
        }

        public void UpdateBankAccounts(string name, double savings, double checking)
        {
            Console.WriteLine($"{name}, {savings}, {checking}");
            Console.ReadLine();
            double[] accounts = new double[] { savings, checking };

            Console.WriteLine($"{accounts[0]}, {accounts[1]}");
            BankData[name] = accounts;

            //foreach(var data in BankData)
            //{
            //    Console.WriteLine($"{data.Key} , {data.Value[0]}, {data.Value[1]}");
            //}
            //Console.ReadLine();

            using (var writer = new StreamWriter(FilePath + "bank_money.csv"))
            {
                foreach (var data in BankData)
                {
                    writer.WriteLine($"{data.Key} , {data.Value[0]}, {data.Value[1]}");
                }
            }
        }

        public UserData(string name, User user)
        {
            FilePath = $"../../../userData/{name}/";
            Directory.CreateDirectory("../../../userData");
            Directory.CreateDirectory(FilePath);
            PullBankAccountData(user);
            //UpdateBankAccounts(name, 0, 0);
            Console.WriteLine(FilePath);
        }

        //public WriteBankAcc{
            
        ////}
    }
}

