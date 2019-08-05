using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string firstname;
        private string middlename = "";
        private string lastname = "";
        public ContactData(string firstname) // конструктор
        {
            this.firstname = firstname;
            //this.lastname = lastname;
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
            return Firstname == other.Firstname;
        }
        public override int GetHashCode()
        {
            return Firstname.GetHashCode();
        }

        public override string ToString()
        {
            return "name = " + Firstname;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))

            {
                return 1;
            }
            return Firstname.CompareTo(other.Firstname);
        }
        public string Firstname // св-во
        {
            get
            {
                return firstname;
            }
            set
            {
                firstname = value;
            }
        }
        public string Middlename // св-во
        {
            get
            {
                return middlename;
            }
            set
            {
                middlename = value;
            }
        }

        public string Lastname // св-во
        {
            get
            {
                return lastname;
            }
            set
            {
                lastname = value;
            }
        }
    }
}
