using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

public class HomePage {
    private readonly IWebDriver d; private readonly WebDriverWait w;
    public HomePage(IWebDriver d, WebDriverWait w){ this.d=d; this.w=w; }
    public HomePage GoTo(){ d.Navigate().GoToUrl("http://books.toscrape.com/"); return this; }
    public CategoryPage OpenCategory(string name){
        w.Until(_ => d.FindElement(By.CssSelector(".nav-list")));
        d.FindElement(By.LinkText(name)).Click();
        return new CategoryPage(d, w);
    }
}