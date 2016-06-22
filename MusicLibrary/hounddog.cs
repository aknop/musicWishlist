using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class Hounddog
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        
        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver(@"C:\Users\user\Downloads");
            baseURL = "http://localhost:55699/";
            verificationErrors = new StringBuilder();
        }
        
        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }
        
        [Test]
        public void TheHounddogTest()
        {
            // open | / | 
            driver.Navigate().GoToUrl(baseURL + "/");
            // click | link=Add an Artist | 
            driver.FindElement(By.LinkText("Add an Artist")).Click();
            // type | id=artistName | Elvis Presley
            driver.FindElement(By.Id("artistName")).Clear();
            driver.FindElement(By.Id("artistName")).SendKeys("Elvis Presley");
            // click | //input[@value='Create'] | 
            driver.FindElement(By.XPath("//input[@value='Create']")).Click();
            // click | link=Add an Album | 
            driver.FindElement(By.LinkText("Add an Album")).Click();
            // type | id=name | Loving You
            driver.FindElement(By.Id("name")).Clear();
            driver.FindElement(By.Id("name")).SendKeys("Loving You");
            // select | id=artist_id | label=Elvis Presley
            new SelectElement(driver.FindElement(By.Id("artist_id"))).SelectByText("Elvis Presley");
            // click | //input[@value='Create'] | 
            driver.FindElement(By.XPath("//input[@value='Create']")).Click();
            // click | link=Add a Genre | 
            driver.FindElement(By.LinkText("Add a Genre")).Click();
            // type | id=genreName | Rock 'n' Roll
            driver.FindElement(By.Id("genreName")).Clear();
            driver.FindElement(By.Id("genreName")).SendKeys("Rock 'n' Roll");
            // click | //input[@value='Create'] | 
            driver.FindElement(By.XPath("//input[@value='Create']")).Click();
            // click | link=My Music | 
            driver.FindElement(By.LinkText("My Music")).Click();
            // click | link=Create New | 
            driver.FindElement(By.LinkText("Create New")).Click();
            // type | id=name | Hound Dog
            driver.FindElement(By.Id("name")).Clear();
            driver.FindElement(By.Id("name")).SendKeys("Hound Dog");
            // select | id=artist_id | label=Elvis Presley
            new SelectElement(driver.FindElement(By.Id("artist_id"))).SelectByText("Elvis Presley");
            // select | id=album_id | label=Loving You
            new SelectElement(driver.FindElement(By.Id("album_id"))).SelectByText("Loving You");
            // type | id=track_number | 1
            driver.FindElement(By.Id("track_number")).Clear();
            driver.FindElement(By.Id("track_number")).SendKeys("1");
            // select | id=genre_id | label=Rock 'n' Roll
            new SelectElement(driver.FindElement(By.Id("genre_id"))).SelectByText("Rock 'n' Roll");
            // click | //input[@value='Create'] | 
            driver.FindElement(By.XPath("//input[@value='Create']")).Click();
            // click | link=Details | 
            driver.FindElement(By.LinkText("Details")).Click();
            // click | link=Back to List | 
            driver.FindElement(By.LinkText("Back to List")).Click();
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        
        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
        
        private string CloseAlertAndGetItsText() {
            try {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert) {
                    alert.Accept();
                } else {
                    alert.Dismiss();
                }
                return alertText;
            } finally {
                acceptNextAlert = true;
            }
        }
    }
}
