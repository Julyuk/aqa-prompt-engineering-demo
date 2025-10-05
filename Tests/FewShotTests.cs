using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using OpenQA.Selenium.Support.UI;
using System;

[TestFixture]
public class FewShotTests {
    IWebDriver driver; WebDriverWait wait;
    [SetUp] public void SetUp(){
        var options = new ChromeOptions();
        options.AddExcludedArgument("enable-automation");
        options.AddAdditionalOption("useAutomationExtension", false);
        options.AddArgument("--disable-blink-features=AutomationControlled");
        driver=new ChromeDriver(options);
        wait=new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        driver.Manage().Window.Maximize();
    }
    [Test]
    public void AddMysteryBook_FewShot(){
        var product = new HomePage(driver, wait).GoTo()
            .OpenCategory("Mystery")
            .OpenFirstProduct();
        product.AddToBasket();
        Assert.IsNotEmpty(product.GetTitle(driver));
    }
    [TearDown] public void TearDown(){ if(driver!=null){ try{ driver.Quit(); } finally { driver.Dispose(); } } }
}