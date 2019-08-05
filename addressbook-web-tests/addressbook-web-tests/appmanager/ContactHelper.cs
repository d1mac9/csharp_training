using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {

        }

        private List<ContactData> contactCache = null;
        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.GoToContactList();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr.odd"));
                foreach (IWebElement element in elements)
                {
                    contactCache.Add(new ContactData(element.Text));
                }
            }
            return new List<ContactData>(contactCache);
        }

        public int GetContactCount()
        {
            return driver.FindElements(By.CssSelector("tr.odd")).Count;
        }

        public ContactHelper SubmitContact()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public ContactHelper SelectContact(int id)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (id + 1) + "]")).Click();
            return this;
        }
        public ContactHelper Create(ContactData firstname)
        {
            manager.Navigator.GoToContactPage();
            FillContactForm(firstname);
            SubmitContact();
            return this;
        }
        public ContactHelper Modify(int id, ContactData newData)
        {
            manager.Navigator.GoToContactList();
            SelectContact(id);
            EditContact();
            FillContactForm(newData);
            SubmitContactModification();
            return this;
        }
        public ContactHelper Remove(int id)
        {
            SelectContact(id);
            EditContact();
            DeleteContact();
            return this;
        }

        public ContactHelper FillContactForm(ContactData firstname)
        {
            Type(By.Name("firstname"), firstname.Firstname);
            Type(By.Name("middlename"), firstname.Middlename);
            Type(By.Name("lastname"), firstname.Lastname);
            return this;
        }
        public ContactHelper EditContact()
        {
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[2]/td[8]/a/img")).Click();
            return this;
        }
        public ContactHelper DeleteContact()
        {
            driver.FindElement(By.XPath("//form[2]/input[2]")).Click();
            return this;
        }
        public string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
