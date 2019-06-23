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
        public void ContactCreationTest()
        {
            ContactData firstname = new ContactData("pine","apple");
            //firstname.Middlename = "";
            //firstname.Lastname = "";

            app.Contact.Create(firstname);
            app.Auth.LogOut();
        }
    }
}
