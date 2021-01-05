using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Strata.Exchange.IdentityServer.UITests
{
    [TestClass]
    public class LoginPageDriverTest
    {

        private IWebDriver webDriver;

        [TestMethod]
        public void Test_LoginPage_Success()
        {
            webDriver = new ChromeDriver();
            webDriver.Manage().Window.Maximize();

            LoginPageTest loginPage = new LoginPageTest(webDriver);

            loginPage.Test_LoginPage();

            webDriver.Close();

        }
    }
}
