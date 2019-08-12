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
            int l = Convert.ToInt32(new Random().NextDouble() * max);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < l; i++)
            {
                builder.Append(Convert.ToChar(32 + Convert.ToInt32(new Random().NextDouble() * 65)));
            }
            return builder.ToString();
        }
    }
}