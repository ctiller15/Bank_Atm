using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine
{
    class UsersDB
    {
        private string FilePath = "../../../userData/storedUsers.txt";

        private Dictionary<string, string> UsersList = new Dictionary<string, string>();

        public UsersDB()
        {
            Console.WriteLine(FilePath);
        }

        public void GetUsers()
        {
            using (var reader = new StreamReader(FilePath))
            {
                while (reader.Peek() > -1)
                {
                    var line = reader.ReadLine().Split(',');
                    Console.WriteLine($"{line[0]} {line[1]}");
                    UsersList.Add(line[0], line[1]);
                    //morseMap.Add(Char.ToLower(Convert.ToChar(line[0])), line[1]);
                    //reverseMorseMap.Add(line[1], Char.ToLower(Convert.ToChar(line[0])));
                }
            }
        }

        public void AddUser(string username, string pin)
        {
            //Console.WriteLine($"{username} , {pin}");
            //Console.WriteLine(UsersList);
            UsersList.Add(username.Trim(), pin.Trim());
            using(var writer = new StreamWriter(FilePath)){
                foreach (var user in UsersList)
                {
                    Console.WriteLine($"{user.Key} , {user.Value}");
                    writer.WriteLine($"{user.Key}, {user.Value}");
                }
            }

        }
    }
}
