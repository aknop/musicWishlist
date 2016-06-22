using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;

namespace MusicTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ChromeDriver driver = new ChromeDriver(@"C:\Users\user\downloads");
            //FirefoxDriver driver = new FirefoxDriver();
            string baseURL = "localhost:55699";


            // open | / | 
            driver.Navigate().GoToUrl(baseURL);
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
            // click | link=Genres » | 
            driver.FindElement(By.LinkText("Genres »")).Click();
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

            Assert.AreEqual(driver.FindElement(By.XPath("//div[@id='mainContent']/table/tbody/tr/td/p/b")).Text, "Selenium IDE");

            driver.Close();
        }
    }
}
