using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;
namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : ContactTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            app.Contacts.IsContactExist();
            List<ContactData> oldContacts = ContactData.GetAllFromDB();
            ContactData toBeRemoved = oldContacts[0];
            app.Contacts.Remove(toBeRemoved);
            List<ContactData> newContacts = ContactData.GetAllFromDB();
            Assert.AreEqual(oldContacts.Count - 1, app.Contacts.GetContactsCount());
            oldContacts.RemoveAt(0);
            //oldContacts.Sort();
            //newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(toBeRemoved.Id, contact.Id);
            }
        }

    }
}