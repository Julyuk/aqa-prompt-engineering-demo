using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

public class CategoryPage {
    private readonly IWebDriver d; private readonly WebDriverWait w;
    public CategoryPage(IWebDriver d, WebDriverWait w){ this.d=d; this.w=w; }
    public ProductPage OpenFirstProduct(){
        w.Until(_ => d.FindElements(By.CssSelector(".product_pod h3 a")).Count > 0);
        var el = d.FindElements(By.CssSelector(".product_pod h3 a"))[0];
        ((IJavaScriptExecutor)d).ExecuteScript("arguments[0].scrollIntoView({block:'center'});", el);
        try { el.Click(); } catch { ((IJavaScriptExecutor)d).ExecuteScript("arguments[0].click();", el); }
        return new ProductPage(d, w);
    }
}