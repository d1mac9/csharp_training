using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData newData = new ContactData("zzz");
            newData.Middlename = "ttt";

            List<ContactData> oldContacts = app.Contact.GetContactList();

            app.Contact.Modify(0, newData);

            //Assert.AreEqual(oldContacts.Count, app.Contact.GetContactCount());

            List<ContactData> newContacts = app.Contact.GetContactList();
            oldContacts[0].Firstname = newData.Firstname;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts.Count, newContacts.Count);
        }
    }
}