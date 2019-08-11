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
        private string allPhones;
        private string allContactInformation;
        private string allEmais;
        public ContactData(string first_name, string last_name)
        {
            FirstName = first_name;
            LastName = last_name;
        }
        public ContactData(string first_name)
        {
            FirstName = first_name;
        }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Address { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string HomePhone { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
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
        public string AllEmails
        {
            get
            {
                if (allEmais != null)
                {
                    return allEmais;
                }
                else
                {
                    return (CleanUp(Email1) + CleanUp(Email2) + CleanUp(Email3)).Trim();
                }
            }
            set
            {
                allEmais = value;
            }
        }
        public string AllContactInfo
        {
            get
            {
                if (allContactInformation != null)
                {
                    return allContactInformation;
                }
                else
                {
                    if (FirstName != "")
                    {
                        allContactInformation += FirstName;
                    }
                    else FirstName = null;

                    if (HomePhone != "")
                    {
                        allContactInformation += " " + LastName;
                    }
                    else HomePhone = null;

                    if (HomePhone != "")
                    {
                        allContactInformation += "\r\n\r\n" + "H: " + HomePhone;
                    }
                    else HomePhone = null;

                    if (MobilePhone != "")
                    {
                        allContactInformation += "\r\n" + "M: " + MobilePhone;
                    }
                    else MobilePhone = null;

                    if (WorkPhone != "")
                    {
                        allContactInformation += "\r\n" + "W: " + WorkPhone;
                    }
                    else WorkPhone = null;

                    if (Email1 != null)
                    {
                        allContactInformation += "\r\n\r\n" + Email1;
                    }

                    if (Email2 != null)
                    {
                        return allContactInformation += "\r\n" + Email2;
                    }

                    if (Email3 != null)
                    {
                        return allContactInformation += "\r\n" + Email3;
                    }

                    //return (FirstName + " " + LastName + "\r\n" + "\r\n" + "H:" + HomePhone + "\r\n"
                    // + "M:" + MobilePhone + "\r\n" + "W:" + WorkPhone).Trim();
                    return allContactInformation.Trim();
                }
            }
            set
            {
                allContactInformation = value;
            }
        }
        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ ()-]", "") + "\r\n";
        }
        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            if (LastName.CompareTo(other.LastName) == 0) //сравниваем фамилии, потом уже имена
            {
                return FirstName.CompareTo(other.FirstName);
            }
            return LastName.CompareTo(other.LastName);
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