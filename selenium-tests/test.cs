using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Test
{
    [TestFixture]
    public class Tests
    {
        private IWebDriver _webDriver;

        [SetUp]
        public void SetUp()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            _webDriver = new ChromeDriver();
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver.Quit();
        }

        [Test]
        public void Test()
        {
            _webDriver.Navigate().GoToUrl("https://www.google.com");
            Assert.True(_webDriver.Title.Contains("Google"));
        }
    }
}