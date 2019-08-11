using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string first_name;
        private string last_name;
        private string middle_name = "";

        public ContactData(string first_name, string last_name)
        {
            this.first_name = first_name;
            this.last_name = last_name;
        }
        public ContactData(string first_name, string last_name, string middle_name)
        {
            this.first_name = first_name;
            this.last_name = last_name;
            this.middle_name = middle_name;
        }
        public string FirstName
        {
            get { return first_name; }
            set { first_name = value; }
        }

        public string LastName
        {
            get { return last_name; }
            set { last_name = value; }
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return LastName.CompareTo(other.LastName) + FirstName.CompareTo(other.FirstName);
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            if (LastName == other.LastName && FirstName == other.FirstName)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return LastName.GetHashCode() + FirstName.GetHashCode();
        }

        public override string ToString()
        {
            return LastName + FirstName;
        }
    }
}