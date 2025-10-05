using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

public class ProductPage {
    private readonly IWebDriver d; private readonly WebDriverWait w;
    private By AddBtn => By.CssSelector(".btn.btn-primary.btn-block");
    public ProductPage(IWebDriver d, WebDriverWait w){ this.d=d; this.w=w; }
    public void AddToBasket(){
        var btn = w.Until(_ => d.FindElement(AddBtn));
        ((IJavaScriptExecutor)d).ExecuteScript("arguments[0].scrollIntoView({block:'center'});", btn);
        try { btn.Click(); } catch { ((IJavaScriptExecutor)d).ExecuteScript("arguments[0].click();", btn); }
    }
    public string BasketMiniText() => d.FindElement(By.CssSelector(".product_main h1")).Text;
}