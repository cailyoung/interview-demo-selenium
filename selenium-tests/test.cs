using NUnit.Framework;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

using System;

namespace InterviewDemo
{
    [TestFixture]
    public class SeleniumAcceptanceTests
    {
        private IWebDriver _webDriver;

        [SetUp]
        public void SetUp()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            _webDriver = new ChromeDriver();
            try {
                UserActions.LogInToXero(_webDriver);
            }
            catch {
                throw new System.Exception("Browser wasn't available to log in");
            }
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver.Quit();
        }

        [Test]
        public void AddABankAccount()
        {
            _webDriver.Navigate().GoToUrl("https://go.xero.com/Banking/Account/#find");
            var wait = new WebDriverWait(_webDriver, new TimeSpan(0, 0, 30));
            IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("input[data-ref='inputEl']")));
            _webDriver.FindElement(By.CssSelector("input[data-ref='inputEl']")).SendKeys("ANZ (NZ)");
            IWebElement element2 = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("ba-allbanks-filter-prompt")));
            // yep
            _webDriver.FindElement(By.ClassName("ba-allbanks-filter-prompt")).Click();
            Assert.True(true);
        }
    }
}