using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine
{
    class User
    {
        public UserData UserData { get; set; }

        public List<Account> Accounts = new List<Account>();

        public string Name { get; set; }

        public string PersonalIdentificationNumber { get; set; }

        public void CreateAccount()
        {
            Console.WriteLine("What is the name of the account?");

            string AccName = Console.ReadLine();
            Account newAcc = new Account(AccName, Name);
            Console.WriteLine(newAcc);
            Accounts.Add(newAcc);
            // Now save the account.
            UserData.SaveAccount(newAcc);
            Console.WriteLine($"Account: {newAcc.Name} successfully created");
        }

        public void GetAccounts()
        {
            Console.WriteLine($"Getting all {Name} bank accounts...");
            var directories = Directory.GetDirectories($"../../../userData/Users/{Name}");
            //Console.WriteLine(directories);


            foreach(var dir in directories)
            {
                // Apparently this is the best way to just get the names of the directories. I tried Path.GetDirectoryName and that instead returned the entire path.
                //Console.WriteLine(Path.GetFileName(dir));
                var nameCheck = Accounts.Where(p => p.Name.Trim() == Path.GetFileName(dir).Trim());
                //Console.WriteLine(nameCheck);
                //Console.WriteLine(nameCheck.Count());
                if (nameCheck.Count() == 0)
                {
                    Account savedAcc = new Account(Path.GetFileName(dir), Name);
                    Accounts.Add(savedAcc);
                }

            }

        }

        public bool ShowAccounts()
        {
            if(Accounts.Count() > 0)
            {
                Console.WriteLine("Choose an account\n");

                for (int i = 0; i < Accounts.Count(); i++)
                {
                    Console.WriteLine($"({i + 1}) {Accounts[i].Name}");
                }
                Console.WriteLine("(q) Quit");
                return true;
            } else
            {
                Console.WriteLine("You don't have any accounts yet! Please create one.\n\n");
                return false;
            }


        }

        public void CloseAccount()
        {
            GetAccounts();
            bool noChoice = true;
            while(noChoice)
            {
                if (Accounts.Count() > 0)
                {
                    Console.WriteLine("Which account would you like to close?\n");
                    for (int i = 0; i < Accounts.Count(); i++)
                    {
                        Console.WriteLine($"({i + 1}) {Accounts[i].Name}");
                    }
                    Console.WriteLine("(q) Quit");

                    // Pick the proper file.
                    string option = Console.ReadLine();

                    if(int.TryParse(option, out int n))
                    {
                        if (Convert.ToInt32(option) - 1 <= Accounts.Count())
                        {
                            // Then remove account folder. Completely.
                            Directory.Delete($"../../../userData/Users/{Name}/{Accounts[Convert.ToInt32(option) - 1].Name}/", true);
                            // Remove from list.
                            Accounts.RemoveAt(Convert.ToInt32(option) - 1);
                            Console.WriteLine("Account closed");
                        } else
                        {
                            Console.WriteLine("Invalid option");
                        }
                    } else if(option == "q")
                    {
                        noChoice = false;
                    } else
                    {
                        Console.WriteLine("Invalid option");
                    }


                }
                else
                {
                    Console.WriteLine("You don't have any accounts.");
                    noChoice = false;
                }
            }

        }
        
        public User(string name, string PIN)
        {
            // Initializing a user and the database connection.
            Name = name;
            PersonalIdentificationNumber = PIN;
            UserData = new UserData(name);

        }
    }
}
