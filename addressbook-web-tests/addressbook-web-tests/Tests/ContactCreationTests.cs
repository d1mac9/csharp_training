using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            ContactData firstname = new ContactData("pine");
            //firstname.Middlename = "";
            //firstname.Lastname = "";

            List<ContactData> oldContacts = app.Contact.GetContactList();

            app.Contact.Create(firstname);

            //Assert.AreEqual(oldContacts.Count + 1, app.Contact.GetContactCount());

            List<ContactData> newContacts = app.Contact.GetContactList();
            oldContacts.Add(firstname);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts.Count-1, newContacts.Count);

        }
    }
}
