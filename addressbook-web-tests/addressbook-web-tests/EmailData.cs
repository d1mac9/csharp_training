using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests

{
    class EmailData
    {
        private string email;
        private string email2 = "";
        private string email3 = "";

        public EmailData(string email) // конструктор
        {
            this.email = email;
        }
        public string Email // св-во
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }
        public string Email2 // св-во
        {
            get
            {
                return email2;
            }
            set
            {
                email2 = value;
            }
        }

        public string Email3 // св-во
        {
            get
            {
                return email3;
            }
            set
            {
                email3 = value;
            }
        }
    }
}
