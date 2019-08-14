using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;
using System.Threading;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        { }
        public void Create(ContactData contact)
        {
            AddNewContact();
            FillContactForm(contact);
            SubmitContactCreation();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
            manager.Navigator.ReturnToHomePage();
        }
        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(20))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
            manager.Navigator.ReturnToHomePage();
        }

        public void DeleteContactFromGroup(ContactData contact, GroupData group)
        {
            SelectGroup(group.Name);
            SelectContact(contact.Id);
            CommitDeleteContactFromGroup();
        }

        private void SelectGroup(string name)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(name);
        }

        private void CommitDeleteContactFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        public void CommitAddContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        public void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        public void SelectContact(string contactId)
        {
            driver.FindElement(By.Id(contactId)).Click();
        }

        public void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        public void IsAddedInGroup(ContactData contact, List<ContactData> groupList, GroupData group)
        {
            if (groupList.Count == 0)
            {
                AddContactToGroup(contact, group);
            }
        }

        public void Modification(ContactData contact, ContactData newData)
        {
            IsContactExist();
            InitContactModification(contact.Id);
            FillContactForm(newData);
            SubmitContactModification();
            manager.Navigator.ReturnToHomePage();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        public void Remove(ContactData contact)
        {
            SelectContact(contact.Id);
            RemoveContact();
            SubmitContactRemoval();
            Thread.Sleep(1000);
            contactCache = null;
        }

        public ContactHelper SubmitContactRemoval()
        {
            driver.SwitchTo().Alert().Accept();
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }
        public ContactHelper InitContactModification(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]' and @value='" + index + "'])")).FindElement(By.XPath("(//img[@alt='Edit'])")).Click();
            //driver.FindElement(By.CssSelector("img[alt=\"Edit\"]")).Click();
            //driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + (index + 1) + "]")).Click();
            return this;
        }


        public ContactHelper InitContactModification(string ID)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]' and @value='" + ID + "'])")).FindElement(By.XPath("(//img[@alt='Edit'])")).Click();
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
            Type(By.Name("middlename"), contact.MiddleName);
            Type(By.Name("address"), contact.Address);
            Type(By.Name("mobile"), contact.MobilePhone);
            Type(By.Name("home"), contact.HomePhone);
            Type(By.Name("work"), contact.WorkPhone);
            Type(By.Name("email"), contact.Email1);
            Type(By.Name("email2"), contact.Email2);
            Type(By.Name("email3"), contact.Email3);
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
            string allEmais = cells[4].Text;
            return new ContactData(first_name, last_name)
            {
                Address = address,
                AllPhones = allPhones,
                AllEmails = allEmais
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
            string email1 = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");
            return new ContactData(first_name, last_name)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Email1 = email1,
                Email2 = email2,
                Email3 = email3
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