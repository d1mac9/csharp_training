using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;
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
            System.Console.Out.Write(contactCache);
            return new List<ContactData>(contactCache);
        }
        public int GetContactsCount()
        {
            return driver.FindElements(By.Name("entry")).Count;
        }
        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string last_name = cells[1].Text;
            string first_name = cells[2].Text;
            string address = cells[3].Text;
            string allPhones = cells[5].Text;
            return new ContactData(first_name, last_name)
            {
                Address = address,
                AllPhones = allPhones
            };
        }
        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(0);
            string first_name = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string last_name = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            return new ContactData(first_name, last_name)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone
            };
        }

        public ContactData GetContactInformationFromDetails(int index)
        {
            manager.Navigator.GoToHomePage();
            OpenContactDetails(0);
            string allContactInformation = driver.FindElement(By.CssSelector("div[id='content']")).Text;

            return new ContactData(allContactInformation)
            {
                AllContactInfo = allContactInformation
            };
        }

        public ContactHelper OpenContactDetails(int index)
        {
            driver.FindElement(By.CssSelector("img[alt=\"Details\"]")).Click();
            return this;
        }
    }
}