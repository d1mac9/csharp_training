using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void ContactCrationTest()
        {
            ContactData firstname = new ContactData("ass","apple");
            firstname.Middlename = "";
            firstname.Lastname = "fff";

            app.Contact.AddContact(firstname);
            app.Auth.LogOut();
        }
    }
}
