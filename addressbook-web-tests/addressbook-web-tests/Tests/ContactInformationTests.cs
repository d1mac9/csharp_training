﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {
        [Test]
        public void TestContactInformation()
        {
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);
            //Проверки:
            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);
        }

        [Test]
        public void TestContactInformationDetails()
        {
            ContactData fromDetails = app.Contacts.GetContactInformationFromDetails(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);
            //Проверки:
            Assert.AreEqual(fromDetails.AllContactInfo, fromForm.AllContactInfo);
        }
    }
}