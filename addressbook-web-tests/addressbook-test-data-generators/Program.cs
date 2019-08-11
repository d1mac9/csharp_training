using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAddressbookTests;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;

namespace addressbook_tests_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            string data_type = args[0];
            int count = Convert.ToInt32(args[1]);
            string file_name = args[2];
            string format = args[3];

            if (data_type == "groups")
            {
                List<GroupData> groups = new List<GroupData>();
                for (int i = 0; i < count; i++)
                {
                    groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                    {
                        Header = TestBase.GenerateRandomString(10),
                        Footer = TestBase.GenerateRandomString(10)
                    });
                }
                if (format == "excel")
                {
                    WriteGroupsToExcelFile(groups, file_name);
                }
                else
                {
                    StreamWriter writer = new StreamWriter(file_name);
                    if (format == "csv")
                    {
                        WriteGroupsToCsvFile(groups, writer);
                    }
                    else if (format == "xml")
                    {
                        WriteGroupsToXmlFile(groups, writer);
                    }
                    else if (format == "json")
                    {
                        WriteGroupsToJsonFile(groups, writer);
                    }
                    else
                    {
                        Console.Out.Write("Unrecognized format" + format);
                    }
                    writer.Close();
                }
            }
            else if (data_type == "contacts")
            {
                List<ContactData> contacts = new List<ContactData>();
                StreamWriter writer = new StreamWriter(file_name);
                for (int i = 0; i < count; i++)
                {
                    contacts.Add(new ContactData(TestBase.GenerateRandomString(5), TestBase.GenerateRandomString(5))
                    {
                        Address = TestBase.GenerateRandomString(5),
                        MobilePhone = Convert.ToString(TestBase.rnd.Next(1111, 9999)),
                        WorkPhone = Convert.ToString(TestBase.rnd.Next(1111, 9999)),
                        HomePhone = Convert.ToString(TestBase.rnd.Next(1111, 9999)),
                        Email1 = TestBase.GenerateRandomString(5) + "@" + TestBase.GenerateRandomString(5),
                        Email2 = TestBase.GenerateRandomString(5) + "@" + TestBase.GenerateRandomString(5),
                        Email3 = TestBase.GenerateRandomString(5) + "@" + TestBase.GenerateRandomString(5)
                    });
                }
                if (format == "xml")
                {
                    WriteContactsToXmlFile(contacts, writer);
                }
                else if (format == "json")
                {
                    WriteContactsToJsonFile(contacts, writer);
                }
                else
                {
                    Console.Out.Write("Unrecognized format" + format);
                }
                writer.Close();
            }
        }

        private static void WriteContactsToJsonFile(List<ContactData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }

        private static void WriteContactsToXmlFile(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);

        }

        static void WriteGroupsToExcelFile(List<GroupData> groups, string file_name)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = wb.ActiveSheet;

            int row = 1;
            foreach (GroupData group in groups)
            {
                sheet.Cells[row, 1] = group.Name;
                sheet.Cells[row, 2] = group.Header;
                sheet.Cells[row, 3] = group.Footer;
                row++;
            }
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), file_name);
            File.Delete(fullPath);
            wb.SaveAs(fullPath);
            wb.Close();
            app.Visible = false;
            app.Quit();
        }

        static void WriteGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(String.Format("${0},${1},${2}",
                    group.Name, group.Header, group.Footer));
            }
        }

        static void WriteGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }

        static void WriteGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
        }
    }
}