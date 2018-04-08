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

        public Dictionary<string, string> UsersList = new Dictionary<string, string>();

        public UsersDB()
        {
            GetUsers();
        }

        public void GetUsers()
        {
            using (var reader = new StreamReader(FilePath))
            {
                while (reader.Peek() > -1)
                {
                    var line = reader.ReadLine().Split(',');
                    if(!UsersList.ContainsKey(line[0]))
                    {
                        UsersList.Add(line[0].Trim(), line[1].Trim());
                    }
                }
            }
        }

        public void AddUser(string username, string pin)
        {
            UsersList.Add(username.Trim(), pin.Trim());
            using(var writer = new StreamWriter(FilePath)){
                foreach (var user in UsersList)
                {
                    writer.WriteLine($"{user.Key}, {user.Value}");
                }
            }
        }
    }
}
