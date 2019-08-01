using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
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
            // Use WebDriverManager to pull the Webdriver binary for runtime platform
            new DriverManager().SetUpDriver(new ChromeConfig());
            ChromeOptions options = new ChromeOptions();
            //options.AddArguments("--auto-open-devtools-for-tabs");
            _webDriver = new ChromeDriver(options);
            //_webDriver.Manage().Window.FullScreen();
            _webDriver.Navigate().GoToUrl("https://www.xero.com/");

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
            //_webDriver.Quit();
            _webDriver.Navigate().GoToUrl("https://my.xero.com/!xkcD/Dashboard");
            _webDriver.FindElement(By.CssSelector("#reset_demo")).Click();
            Actions resetButtonClicker = new Actions(_webDriver);
            var resetButton = _webDriver.FindElement(By.CssSelector("div#confirmResetDemo"));
            resetButtonClicker.MoveToElement(resetButton).Perform();
            resetButtonClicker.Click(resetButton).Perform();
            
            
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
            IWebElement targetAccountElement = null;
            foreach (IWebElement bankAccount in bankAccountElements) {
                string accountName = bankAccount.GetProperty("innerText");
                accountNames.Add(accountName);
                if (bankAccount.GetProperty("innerText") == "ANZ (NZ)") {
                    targetAccountElement = bankAccount;
                }
            }
            Assert.Contains("ANZ (NZ)",accountNames);
            targetAccountElement.Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains("account details"));
            _webDriver.FindElement(By.CssSelector("input[id^='accountname']")).SendKeys("Test Account Name");
            _webDriver.FindElement(By.CssSelector("input[id^='accounttype']")).Click();

            var bankAccountTypesElements = _webDriver.FindElements(By.CssSelector("li.ba-combo-list-item"));
            foreach (IWebElement bankAccountTypeElement in bankAccountTypesElements) {
                if (bankAccountTypeElement.GetProperty("innerText") == "Loan") {
                    //bankAccountTypeElement.Click(); this is a JS dropdown, this doesn't work
                    Actions builder = new Actions(_webDriver);
                    builder.MoveToElement(bankAccountTypeElement).Perform();
                    builder.Click(bankAccountTypeElement).Perform();

                }
            }

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("input[id^='accountnumber-1068']")));
            _webDriver.FindElement(By.CssSelector("input[id^='accountnumber-1068']")).SendKeys("123");
            
            Actions continueButtonClicker = new Actions(_webDriver);
            var continueButton = _webDriver.FindElement(By.CssSelector("a[data-automationid='continueButton']"));
            continueButtonClicker.MoveToElement(continueButton).Perform();
            continueButtonClicker.Click(continueButton).Perform();

            //wait for redirect
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("header#gettingStarted")));

            string successMessage = _webDriver.FindElement(By.CssSelector(".message p")).Text;
            //Is the account name in the success message?
            StringAssert.Contains("Test",successMessage);
            
            List<String> bankAccountNames = null;
            var bankAccountNameElements = _webDriver.FindElements(By.CssSelector("div.bank-header"));
            foreach (IWebElement bankAccountNameElement in bankAccountNameElements) {
                string bankAccountName = bankAccountNameElement.GetProperty("innerText");
                bankAccountNames.Add(bankAccountName);
            }
            Assert.Contains("Test Account Name",bankAccountNames);
        }
    }
}