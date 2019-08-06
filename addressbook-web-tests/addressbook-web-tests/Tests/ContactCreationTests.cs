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
            ContactData firstName = new ContactData("pine", "123");
            //firstName.SecondName = "";
            //firstName.Lastname = "";

            List<ContactData> oldContacts = app.Contact.GetContactList();

            app.Contact.Create(firstName);

            //Assert.AreEqual(oldContacts.Count + 1, app.Contact.GetContactCount());

            List<ContactData> newContacts = app.Contact.GetContactList();
            oldContacts.Add(firstName);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts.Count-1, newContacts.Count);

        }
    }
}
