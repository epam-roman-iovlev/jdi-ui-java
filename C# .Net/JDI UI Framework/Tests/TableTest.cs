using Epam.JDI.Web.Selenium.DriverFactory;
using Epam.JDI.Web.Selenium.Elements.Complex.table;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using static Epam.JDI.Web.Selenium.DriverFactory.WebDriverUtils;

namespace Epam.Tests
{
    [TestClass]
    public class TableTests
    {

        public TestContext TestContext { get; set; }
        private IWebDriver Driver { set; get; }
        private Table Table { get; set; }

        [TestInitialize]
        public void MyTestInitialize()
        {
            //Driver = new WebDriverFactory().GetDriver();
            //Driver.Url = "http://ecse00100176.epam.com/";
            //Cookie login = new Cookie("authUser", "true", "ecse00100176.epam.com", "/", null);
            //Driver.Manage().Cookies.AddCookie(login);
            //Driver.Url = "http://ecse00100176.epam.com/page3.htm";
        }

        [TestMethod]
        public void GetDefaultDriverTest()
        {
            Table = new Table(By.CssSelector(".uui-table"));
        }

        [TestCleanup]
        public void TearDown()
        {
            KillAllRunWebDrivers();
        }
    }
}