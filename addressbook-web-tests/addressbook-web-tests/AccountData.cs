using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    class AccountData
    {
        private string username;
        private string password;
        
        public AccountData(string username, string password) // конструктор
        {
            this.username = username; // присваиваем значение, которое передано как параметр
            this.password = password;
        }
        public string Username // свойство
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }

        public string Password // свойство
        {
            get // возвращает
            {
                return password;
            }
            set // присваивает
            {
                password = value;
            }
        }
    }
}
