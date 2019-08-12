using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;
namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : ContactTestBase
    {
        [Test]
        public void TestContactModification()
        {
            app.Contacts.IsContactExist();
            ContactData newData = new ContactData("SSS", "ppp");
            List<ContactData> oldContacts = ContactData.GetAllFromDB();
            app.Contacts.Modification(oldContacts[0], newData);
            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactsCount());
            List<ContactData> newContacts = ContactData.GetAllFromDB();
            oldContacts[0].LastName = newData.LastName;
            oldContacts[0].FirstName = newData.FirstName;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}