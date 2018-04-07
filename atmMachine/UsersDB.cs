using System;
using System.Collections.Generic;
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

        public void AddUser(string username, string pin)
        {
            //Console.WriteLine($"{username} , {pin}");
            //Console.WriteLine(UsersList);
            UsersList.Add(username.Trim(), pin.Trim());
            foreach (var user in UsersList)
            {
                Console.WriteLine($"{user.Key} , {user.Value}");
            }
        }
    }
}
