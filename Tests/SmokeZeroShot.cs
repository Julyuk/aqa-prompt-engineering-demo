using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using OpenQA.Selenium.Support.UI;
using System;

[TestFixture]
public class SmokeZeroShot {
    IWebDriver driver; WebDriverWait wait;

    [SetUp]
    public void SetUp() {
        var options = new ChromeOptions();
        options.AddExcludedArgument("enable-automation");
        options.AddAdditionalOption("useAutomationExtension", false);
        options.AddArgument("--disable-blink-features=AutomationControlled");
        driver = new ChromeDriver(options);
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        driver.Manage().Window.Maximize();
    }

    [Test]
    public void AddMysteryBookToBasket_ZeroShot() {
        driver.Navigate().GoToUrl("http://books.toscrape.com/");
        driver.FindElement(By.LinkText("Mystery")).Click();
        driver.FindElement(By.CssSelector(".product_pod h3 a")).Click();
        driver.FindElement(By.CssSelector(".btn.btn-primary.btn-block")).Click();
        var productTitle = wait.Until(d => d.FindElement(By.CssSelector(".product_main h1"))).Text;
        Assert.IsNotEmpty(productTitle);
    }

    [TearDown] public void TearDown(){ if(driver!=null){ try{ driver.Quit(); } finally { driver.Dispose(); } } }
}