using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allContactInformation;
        private string allEmais;
        public ContactData() { }
        public ContactData(string first_name, string last_name)
        {
            FirstName = first_name;
            LastName = last_name;
        }
        public ContactData(string first_name)
        {
            FirstName = first_name;
        }

        [Column(Name = "firstname")]
        public string FirstName { get; set; }


        [Column(Name = "lastname")]
        public string LastName { get; set; }

        [Column(Name = "middlename")]
        public string MiddleName { get; set; }

        [Column(Name = "address")]
        public string Address { get; set; }

        [Column(Name = "mobile")]
        public string MobilePhone { get; set; }

        [Column(Name = "work")]
        public string WorkPhone { get; set; }

        [Column(Name = "home")]
        public string HomePhone { get; set; }

        [Column(Name = "email")]
        public string Email1 { get; set; }

        [Column(Name = "email2")]
        public string Email2 { get; set; }

        [Column(Name = "email3")]
        public string Email3 { get; set; }

        [Column(Name = "id"), PrimaryKey]
        public string Id { get; set; }

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }

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

        public static List<ContactData> GetAllFromDB()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:0")
                        select c).ToList(); //запрос в БД списка групп

            }
        }

        public List<GroupData> GetGroups()
        {
            using (AddressBookDB db = new AddressBookDB())
            {

                var query = from g in db.Groups
                            join t in db.GCR on g.Id equals t.GroupId
                            where t.ContactId == Id
                            select g;

                return query.Distinct().ToList();
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