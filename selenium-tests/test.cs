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
            //new DriverManager().SetUpDriver(new ChromeConfig());
            //_webDriver = new ChromeDriver();
            if (UserActions.LogInToXero(_webDriver) == true) {
                // do nothing
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
            _webDriver.Navigate().GoToUrl("https://go.xero.com/Dashboard/");
            Assert.True(_webDriver.Title.Contains("Google"));
        }
    }
}