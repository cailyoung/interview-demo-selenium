using OpenQA.Selenium;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using System.IO;
using System;
using System.Collections.Generic;

namespace InterviewDemo

{
    public class Credential
    {
        public string Username;
        public string Password;
        public Credential(string username, string password){
                Username = username;
                Password = password;
            }
    }
    public class UserActions
    {
        
        public static bool LogInToXero(IWebDriver _webdriver)
        {
            YamlReader.getCredentialFromFile();
            _webdriver.Navigate().GoToUrl("https://login.xero.com/");
            _webdriver.FindElement(By.CssSelector("#email")).SendKeys("test");
            return true;
        }
    }
    class YamlReader
    {
        FileInfo file = new FileInfo("helpers.cs");
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