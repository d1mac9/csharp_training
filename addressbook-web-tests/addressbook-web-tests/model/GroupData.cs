using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class GroupData : IEquatable<GroupData>
    {
        private string name;
        private string header = "";
        private string footer = "";
        public GroupData(string name) // конструктор
        {
            this.name = name;
        }

        public bool Equals(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Name == other.Name;
        }
        public int GetHashCode()
        {
            return Name.GetHashCode();
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
