using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Team28DeliveryTests2
{
    class SeleniumTest
    {
        IWebDriver driver;
        [SetUp]
        public void Initlialize()
        {
            driver = new ChromeDriver();   
        }

        [Test]
        public void TestOpenApp()
        {
            driver.Url = "http://ifb299team28delivery.azurewebsites.net/";
        }

        [Test]
        public void LogInAdmin()
        {
            driver.Navigate().GoToUrl("http://ifb299team28delivery.azurewebsites.net/Account/Login");
            IWebElement username  = driver.FindElement(By.Id("Email"));
            username.SendKeys("admin@delivery.au");
            IWebElement password = driver.FindElement(By.Id("Password"));
            password.SendKeys("#Passord1");
            driver.FindElement(By.Id("LogIn")).Click();

        }

        [Test]
        public void LogOutAdmin()
        {
            driver.FindElement(By.Id("LogIn")).Click();

        }

        [Test]
        public void RegisterUser()
        {
            driver.Navigate().GoToUrl("http://ifb299team28delivery.azurewebsites.net/Account/Register");
            IWebElement FirstName = driver.FindElement(By.Id("FirstName"));
            FirstName.SendKeys("Cecilie");
            IWebElement LastName = driver.FindElement(By.Id("LastName"));
            LastName.SendKeys("Skar");
            IWebElement Email = driver.FindElement(By.Id("Email"));
            Email.SendKeys("cecilieskar@hotmail.com");
            IWebElement password = driver.FindElement(By.Id("Password"));
            password.SendKeys("#Passord1");
            IWebElement conPassword = driver.FindElement(By.Id("ConfirmPassword"));
            conPassword.SendKeys("#Passord1");
            IWebElement PhoneNumber = driver.FindElement(By.Id("Phone"));
            PhoneNumber.SendKeys("004747648923");
            IWebElement StreetAddress = driver.FindElement(By.Id("StreetAddress"));
            StreetAddress.SendKeys("62 Browning st");
            IWebElement Suburb = driver.FindElement(By.Id("Suburb"));
            Suburb.SendKeys("South Brisbane");
            IWebElement PostalCode = driver.FindElement(By.Id("PostalCode"));
            PostalCode.SendKeys("4101");
            IWebElement State = driver.FindElement(By.Id("State"));
            State.SendKeys("Brisbane");
            IWebElement Country = driver.FindElement(By.Id("Country"));
            Country.SendKeys("Australia");
            driver.FindElement(By.Id("Register")).Click();

        }

        [Test]
        public void LogInUser()
        {
            driver.Navigate().GoToUrl("http://ifb299team28delivery.azurewebsites.net/Account/Login");
            IWebElement username = driver.FindElement(By.Id("Email"));
            username.SendKeys("cecilieskar@hotmail.com");
            IWebElement password = driver.FindElement(By.Id("Password"));
            password.SendKeys("#Passord1");
            driver.FindElement(By.Id("LogIn")).Click();

        }



        [TearDown]
        public void EndApp()
        {
            driver.Close();
        }

    }
}
