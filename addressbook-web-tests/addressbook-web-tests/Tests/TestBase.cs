using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
namespace WebAddressbookTests
{
    public class TestBase
    {
        protected ApplicationManager app;
        public static bool RERFORM_LONG_UI_CHECKS = true;
        [SetUp]
        public void SetupApplicationManager()
        {
            app = ApplicationManager.GetInstance();
            app.Auth.Login(new AccountData("admin", "secret"));
        }

        public static string GenerateRandomString(int max)
        {
            //int l = Convert.ToInt32(new Random().NextDouble() * max);
            string letter;
            var chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz123456789";
            int pos = 0;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < max; i++)
            {
                builder.Append(Convert.ToChar(32 + Convert.ToInt32(new Random().NextDouble() * 65)));
            }
            return builder.ToString();
        }
    }
}