using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine
{
    class UserData
    {
        public string FilePath { get; set; }

        public UserData(string name)
        {
            FilePath = $"../../../userData/{name}/";
            Console.WriteLine(FilePath);
        }
    }
}

