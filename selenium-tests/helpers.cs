using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using YamlDotNet.Serialization;
using System.IO;
using System.Collections.Generic;
using System;
using System.Threading;

namespace InterviewDemo

{
    public class UserActions
    {
        
        public static bool LogInToXero(IWebDriver _webDriver)
        {
            Credential xeroCred = YamlReader.getCredentialFromFile();
            var wait = new WebDriverWait(_webDriver, new TimeSpan(0, 0, 30));
            _webDriver.Navigate().GoToUrl("https://login.xero.com/");
            _webDriver.FindElement(By.CssSelector("#email")).SendKeys(xeroCred.Username);
            _webDriver.FindElement(By.CssSelector("#password")).SendKeys(xeroCred.Password);
            _webDriver.FindElement(By.CssSelector("#password")).Submit();
            
            //wait here for 2FA
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("input[data-automationid='auth-onetimepassword--input']")));
            Thread.Sleep(10000);
            
            //once a human has proved their existence
            try {
            _webDriver.FindElement(By.CssSelector("button[data-automationid='auth-submitcodebutton']")).Click();
            }
            // wait for the redirect to complete
            finally {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("div[data-automationid='gettingStartedPanel-description']")));
            }
            return true;
            
        }
    }
    class YamlReader
    {
        private const string filepath = @"/Users/cailyoung/repos/interview-demo-selenium/selenium-tests/credential.yaml";
        public static Credential getCredentialFromFile(){
            var input = new StreamReader(filepath);
            var deserializer = new DeserializerBuilder().Build();
            var cred = deserializer.Deserialize<Dictionary<string, string>>(input);
            Credential xeroCred = new Credential(cred["username"],cred["password"]);
            input.Dispose();
            return xeroCred;

        }
        
    }
}