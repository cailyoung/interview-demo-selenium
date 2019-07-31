using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using InterviewDemo;

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
                // kill the test execution
            }
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver.Quit();
        }

        [Test]
        public void OpenGoogleAndCheckTitle()
        {
            _webDriver.Navigate().GoToUrl("https://my.xero.com/!xkcD/Action/OrganisationLogin/!Q81RZ");
            Assert.True(_webDriver.FindElement(By.ClassName("xrh-appbutton--text")).ToString().Contains("Demo"));
            Assert.True(_webDriver.Title.Contains("Google"));
        }
    }
}