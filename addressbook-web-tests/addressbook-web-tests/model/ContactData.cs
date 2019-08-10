using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        public string allPhones;
        public string allEmails;


        public ContactData(string firstName, string secondName) // конструктор
        {
            FirstName = firstName;
            SecondName = secondName;
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
            return FirstName == other.FirstName
                && SecondName == other.SecondName;
        }
        public override int GetHashCode()
        {
            return (FirstName + " " + SecondName).GetHashCode();
        }

        public override string ToString()
        {
            return "name = " + FirstName;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))

            {
                return 1;
            }
            return FirstName.CompareTo(other.FirstName);
        }
        public string FirstName { get; set; } // св-во
        public string SecondName { get; set; }
        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Email2 { get; set; }

        public string Email3 { get; set; }
        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (CleanEm(Email) + CleanEm(Email2) + CleanEm(Email3)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        private string CleanEm(string email)
        {
            if (email == null || email == "")
            {
                return "";
            }
            return email + "\r\n";

        }
        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        private string CleanUp(string phone)
        {
            if(phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ -()]", "") + "\r\n";
        }
    }
}
