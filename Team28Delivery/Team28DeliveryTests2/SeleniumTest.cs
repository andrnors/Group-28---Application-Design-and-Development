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
            driver.Url = "http://28deliveryifb299.azurewebsites.net/";
        }

        [Test]
        public void AdminTest()
        {
            driver.Navigate().GoToUrl("http://28deliveryifb299.azurewebsites.net/Account/Login");
            IWebElement username  = driver.FindElement(By.Id("Email"));
            username.SendKeys("admin@28delivery.au");
            IWebElement password = driver.FindElement(By.Id("Password"));
            password.SendKeys("#Password1");
            driver.FindElement(By.Id("LogIn")).Click();

            driver.Navigate().GoToUrl("http://28deliveryifb299.azurewebsites.net/Admin/ApplicationUsers");


        }


        [Test]
        public void UserTest()
        {
            driver.Navigate().GoToUrl("http://28deliveryifb299.azurewebsites.net/Account/Register");
            IWebElement FirstName = driver.FindElement(By.Id("FirstName"));
            FirstName.SendKeys("Henrik");
            IWebElement LastName = driver.FindElement(By.Id("LastName"));
            LastName.SendKeys("Baug");
            IWebElement Email = driver.FindElement(By.Id("Email"));
            Email.SendKeys("henrikbaug@hotmail.com");
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

            driver.Navigate().GoToUrl("http://28deliveryifb299.azurewebsites.net/Account/Login");
            IWebElement username = driver.FindElement(By.Id("Email"));
            username.SendKeys("eirikbaug@hotmail.com");
            IWebElement password1 = driver.FindElement(By.Id("Password"));
            password1.SendKeys("#Passord1");
            driver.FindElement(By.Id("LogIn")).Click();

            driver.Navigate().GoToUrl("http://28deliveryifb299.azurewebsites.net/Home/Order");
            IWebElement pStreetAddress = driver.FindElement(By.Id("Pickup_StreetAddress"));
            pStreetAddress.SendKeys("14 Delvare st");
            IWebElement pSuburb = driver.FindElement(By.Id("Pickup_Suburb"));
            pSuburb.SendKeys("Nathan");
            IWebElement pPostalCode = driver.FindElement(By.Id("Pickup_PostalCode"));
            pPostalCode.SendKeys("4101");
            IWebElement pState = driver.FindElement(By.Id("Pickup_State"));
            pState.SendKeys("Brisbane");
            IWebElement pCountry = driver.FindElement(By.Id("Pickup_Country"));
            pCountry.SendKeys("Australia");
            IWebElement specialInfo = driver.FindElement(By.Id("PackageDetails_SpecialInfo"));
            specialInfo.SendKeys("Fragile");
            IWebElement weight = driver.FindElement(By.Id("PackageDetails_Weight"));
            weight.SendKeys("299");
            IWebElement priority = driver.FindElement(By.Id("PackageDetails_Priority"));
            priority.SendKeys("HIGH");
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            js.ExecuteScript("$('#PackageDetails.PickupTimegfhjk').click();");
            IWebElement rName = driver.FindElement(By.Id("PackageDetails_RecieverName"));
            rName.SendKeys("Monika Hansen");
            IWebElement dAddress = driver.FindElement(By.Id("Delivery_StreetAddress"));
            dAddress.SendKeys("11 Tors veg");
            IWebElement dSuburb = driver.FindElement(By.Id("Delivery_Suburb"));
            dSuburb.SendKeys("Nardo");
            IWebElement dPostalCode = driver.FindElement(By.Id("Delivery_PostalCode"));
            dPostalCode.SendKeys("7011");
            IWebElement dState = driver.FindElement(By.Id("Delivery_State"));
            dState.SendKeys("Trondheim");
            IWebElement dCountry = driver.FindElement(By.Id("Delivery_Country"));
            dCountry.SendKeys("Norway");
            driver.FindElement(By.Id("submit")).Click();

            driver.Navigate().GoToUrl("http://28deliveryifb299.azurewebsites.net/Manage/ViewOrders");


        }

       



        [TearDown]
        public void EndApp()
        {
            driver.Close();
        }

    }
}
