using NUnit.Framework;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using System.Collections.Generic;
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
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("input[data-ref='inputEl']")));
            _webDriver.FindElement(By.CssSelector("input[data-ref='inputEl']")).SendKeys("ANZ (NZ)");
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("ba-allbanks-filter-prompt")));
            _webDriver.FindElement(By.ClassName("ba-allbanks-filter-prompt")).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("#ba-banklist-1023 ul li")));
            var bankAccountElements = _webDriver.FindElements(By.CssSelector("#ba-banklist-1023 ul li"));
            var accountNames = new List<string>();
            foreach (IWebElement bankAccount in bankAccountElements) {
                string accountName = bankAccount.GetProperty("innerText");
                accountNames.Add(accountName);
            }
            Assert.Contains("ANZ (NZ)",accountNames);
        }
    }
}