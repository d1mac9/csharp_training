using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    class GroupData
    {
        private string name;
        private string header = "";
        private string footer = "";
        public GroupData(string name) // конструктор
        {
            this.name = name;
        }

        public string Name // св-во
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public string Header // св-во
        {
            get
            {
                return header;
            }
            set
            {
                header = value;
            }
        }

        public string Footer // св-во
        {
            get
            {
                return footer;
            }
            set
            {
                footer = value;
            }
        }
    }
}
