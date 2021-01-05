using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strata.Exchange.IdentityServer.UITests
{
    public class LoginPageTest
    {
        private readonly IWebDriver _webDriver;

        public LoginPageTest(IWebDriver webDriver)
        {
            this._webDriver = webDriver;
        }

        //[FindsBy(How = How.Id, Using = "Username")]
        //public IWebElement UserName { get; set; }

        //[FindsBy(How = How.XPath, Using= "Password")]
        //public IWebElement Password { get; set; }

        //[FindsBy(How = How.XPath, Using = "/html/body/div[2]/div/div[2]/div/div/div[2]/form/button[1]")]
        //public IWebElement LoginButton { get; set; }

        public void Test_LoginPage()
        {
            this._webDriver.Navigate().GoToUrl("https://localhost:8471/Account/Login");

            this._webDriver.FindElement(By.Id("Username")).SendKeys("Alice");
            this._webDriver.FindElement(By.Id("Password")).SendKeys("alice");

            //this.UserName.SendKeys("Alice");
            //this.Password.SendKeys("alice");
            this._webDriver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div[2]/form/button[1]")).Click();

            IWebElement welcomeScreen = this._webDriver.FindElement(By.XPath(@"/html/body/div[2]/div/ul/li[2]/a"));

            if(welcomeScreen != null)
            {
                welcomeScreen.Click();
            }
        }

    }
}
