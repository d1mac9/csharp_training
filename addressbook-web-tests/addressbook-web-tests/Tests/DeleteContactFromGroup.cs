using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
namespace WebAddressbookTests
{
    public class DeleteContactFromGroup : AuthTestBase
    {
        [Test]
        public void TestDeleteContactFromGroup()
        {
            app.Contacts.IsContactExist();
            app.Groups.IsGroupExist();
            GroupData group = GroupData.GetAllFromDB()[9];
            ContactData contact;
            if (group == null)
            {
                app.Groups.IsGroupExist();
                group = GroupData.GetAllFromDB()[9];
            }

            List<ContactData> oldList = group.GetContact();

            if (group.GetContact().Count == 0)
            {
                contact = ContactData.GetAllFromDB()[0];
                app.Contacts.AddContactToGroup(contact, group);
            }
            else
            {
                contact = oldList[0];
            }
            contact = ContactData.GetAllFromDB().First();
            app.Contacts.IsAddedInGroup(contact, oldList, group);
            //действия
            app.Contacts.DeleteContactFromGroup(contact, group);
            List<ContactData> newList = group.GetContact();
            oldList.Remove(contact);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}