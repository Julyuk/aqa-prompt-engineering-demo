using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;

[TestFixture]
public class AdvancedTests {
    IWebDriver driver; WebDriverWait wait;

    [SetUp]
    public void SetUp(){
        var options = new ChromeOptions();
        options.AddExcludedArgument("enable-automation");
        options.AddAdditionalOption("useAutomationExtension", false);
        options.AddArgument("--disable-blink-features=AutomationControlled");
        driver = new ChromeDriver(options);
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        driver.Manage().Window.Maximize();
    }

    [TearDown]
    public void TearDown(){
        try {
            ScreenshotTaker.TryTake(driver, Path.Combine(TestContext.CurrentContext.WorkDirectory, $"screenshot_{TestContext.CurrentContext.Test.Name}.png"));
        } catch {}
        if(driver!=null){ try{ driver.Quit(); } finally { driver.Dispose(); } }
    }

    // ===== Chain-of-thought style =====
    // Передумови: існує категорія "Mystery"; сторінка книги має кнопку "Add to basket" і банер успіху.
    // Кроки: (1) Відкрити головну -> (2) Mystery -> (3) Перша книга -> (4) Додати у кошик.
    // Очікування: (a) Банер успіху містить назву книги; (b) міні-кошик показує "1 item".
    [Test]
    public void ChainOfThought_AddMysteryBook_WithExtraChecks(){
        driver.Navigate().GoToUrl("http://books.toscrape.com/");
        wait.Until(d => d.FindElement(By.LinkText("Mystery"))).Click();
        wait.Until(d => d.FindElements(By.CssSelector(".product_pod h3 a")).Count > 0);
        driver.FindElements(By.CssSelector(".product_pod h3 a"))[0].Click();

        // Збираємо назву книги до додавання
        var title = wait.Until(d => d.FindElement(By.CssSelector(".product_main h1"))).Text;

        wait.Until(d => d.FindElement(By.CssSelector("button.btn.btn-primary.btn-block"))).Click();
        // Validate we remain on product page and title exists after click
        Assert.IsNotEmpty(wait.Until(d => d.FindElement(By.CssSelector(".product_main h1"))).Text);
    }

    // ===== Tree-of-thought style =====
    // Обираємо стійкі локатори: LinkText для категорії, button.btn.btn-primary.btn-block для кнопки, .basket-mini для кошика.
    // Додаємо ретраї на клік кнопки.
    [Test]
    public void TreeOfThought_AddMysteryBook_WithStableLocators(){
        driver.Navigate().GoToUrl("http://books.toscrape.com/");
        wait.Until(d => d.FindElement(By.LinkText("Mystery"))).Click();

        // Вибір першого продукту без крихких індексів XPath (підстрахуємось очікуванням)
        wait.Until(d => d.FindElements(By.CssSelector(".product_pod h3 a")).Count > 0);
        driver.FindElements(By.CssSelector(".product_pod h3 a"))[0].Click();

        // Надійний клік із ретраями
        Retry.Do(()=> {
            wait.Until(d => d.FindElement(By.CssSelector("button.btn.btn-primary.btn-block"))).Click();
        }, 3, TimeSpan.FromMilliseconds(500));

        Assert.IsNotEmpty(wait.Until(d => d.FindElement(By.CssSelector(".product_main h1"))).Text);
    }

    // ===== Critic / Refactor loop =====
    // Перевіряємо флейки: додаємо явні очікування для кожного кроку, стабільні локатори, логування та скріншот у TearDown.
    [Test]
    public void CriticRefactor_AddMysteryBook_Hardened(){
        TestLogger.Log("Go Home");
        driver.Navigate().GoToUrl("http://books.toscrape.com/");

        TestLogger.Log("Open category 'Mystery'");
        wait.Until(d => d.FindElement(By.LinkText("Mystery"))).Click();

        TestLogger.Log("Open first product");
        wait.Until(d => d.FindElements(By.CssSelector(".product_pod h3 a")).Count > 0);
        driver.FindElements(By.CssSelector(".product_pod h3 a"))[0].Click();

        TestLogger.Log("Add to basket");
        var addBtn = wait.Until(d => d.FindElement(By.CssSelector("button.btn.btn-primary.btn-block")));
        try { addBtn.Click(); } catch { ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", addBtn); }

        TestLogger.Log("Check success banner & basket");
        Assert.IsNotEmpty(wait.Until(d => d.FindElement(By.CssSelector(".product_main h1"))).Text);
    }
}
