using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
namespace WebAddressbookTests
{
    public class AddContactToGroupTests : AuthTestBase
    {
        [Test]
        public void TestAddContactToGroup()
        {
            app.Contacts.IsContactExist();
            app.Groups.IsGroupExist();
            GroupData group = GroupData.GetAllFromDB()[0];
            List<ContactData> oldList = group.GetContact();
            ContactData contact;
            if (GroupData.GetAllFromDB().Count == oldList.Count)
            {
                contact = new ContactData("SSS", "QQQ"); //создем новый контакт, если в группу входят все контакты
                app.Contacts.Create(contact);
            }
            contact = ContactData.GetAllFromDB().Except(oldList).First();
            //действия
            app.Contacts.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContact();
            oldList.Add(contact);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}