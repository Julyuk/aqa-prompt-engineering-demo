using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

// Додаткові допоміжні методи для ProductPage без зміни оригінального файлу
public static class ProductPageExtensions {
    public static string GetTitle(this ProductPage page, IWebDriver d){
        return d.FindElement(By.CssSelector(".product_main h1")).Text;
    }
    public static string GetSuccessBannerText(this ProductPage page, IWebDriver d, WebDriverWait w){
        return w.Until(x => x.FindElement(By.CssSelector(".alertinner"))).Text;
    }
}
