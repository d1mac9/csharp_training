using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        { }

        public ContactHelper Create(ContactData contact)
        {
            AddNewContact();
            FillContactForm(contact);
            SubmitContactCreation();
            manager.Navigator.ReturnToHomePage();
            return this;
        }
        public ContactHelper Modification(int p, ContactData newData)
        {
            IsContactExist();
            InitContactModification(p);
            FillContactForm(newData);
            SubmitContactModification();
            manager.Navigator.ReturnToHomePage();
            return this;
        }
        public ContactHelper Remove(int p)
        {
            SelectContact(p);
            RemoveContact();
            SubmitContactRemoval();
            manager.Navigator.ReturnToHomePage();
            return this;
        }
        public ContactHelper SubmitContactRemoval()
        {
            driver.SwitchTo().Alert().Accept();
            contactCache = null;
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }
        public ContactHelper InitContactModification(int index)
        {
            driver.FindElement(By.CssSelector("img[alt=\"Edit\"]")).Click();
            return this;
        }
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.XPath("(//input[@name='update'])[2]")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            return this;
        }
        public ContactHelper AddNewContact()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }
        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.FirstName);
            Type(By.Name("lastname"), contact.LastName);
            return this;
        }
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("(//input[@name='submit'])[2]")).Click();
            contactCache = null;
            return this;

        }
        public ContactHelper IsContactExist()
        {
            if (!IsElementPresent(By.Name("selected[]")))
            {
                ContactData contact = new ContactData("SSS", "QQQ");
                Create(contact);
            }
            return this;
        }

        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactList()
        {
                if (contactCache == null)
                {
                    contactCache = new List<ContactData>();
                    manager.Navigator.ReturnToHomePage();
                    ICollection<IWebElement> elements = driver.FindElements(By.Name("entry"));
                    foreach (IWebElement element in elements)
                    {
                        contactCache.Add(new ContactData(element.FindElement(By.XPath(".//td[3]")).Text, element.FindElement(By.XPath(".//td[2]")).Text));

                    }
                }
            Console.Out.Write(contactCache);
            return new List<ContactData>(contactCache);
        }

        public int GetContactsCount()
        {
            return driver.FindElements(By.Name("entry")).Count;
        }

    }
}