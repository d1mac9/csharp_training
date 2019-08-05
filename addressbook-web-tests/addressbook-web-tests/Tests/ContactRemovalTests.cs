using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            List<ContactData> oldContacts = app.Contact.GetContactList();

            app.Contact.Remove(0);

            //Assert.AreEqual(oldContacts.Count - 1, app.Contact.GetContactCount());

            List<ContactData> newContacts = app.Contact.GetContactList();

            oldContacts.RemoveAt(0);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts.Count + 1, newContacts.Count);


        }
    }
}