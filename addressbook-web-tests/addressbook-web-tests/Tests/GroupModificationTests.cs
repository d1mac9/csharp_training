using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;
namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : GroupTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            app.Groups.IsGroupExist();
            GroupData newData = new GroupData("ytx");
            newData.Header = "hli";
            newData.Footer = "hlfgh";
            List<GroupData> oldGroups = GroupData.GetAllFromDB();
            app.Groups.Modify(oldGroups[8], newData);
            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupsCount());
            List<GroupData> newGroups = GroupData.GetAllFromDB();
            oldGroups[8].Name = newData.Name;
            oldGroups[8].Header = newData.Header;
            oldGroups[8].Footer = newData.Footer;
            //GroupData.GetAllFromDB()[0].Name = newData.Name;

            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}