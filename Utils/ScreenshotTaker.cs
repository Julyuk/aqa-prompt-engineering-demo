using System;
using System.IO;
using OpenQA.Selenium;

public static class ScreenshotTaker {
    public static void TryTake(IWebDriver driver, string path){
        try {
            if(driver is ITakesScreenshot sh){
                var ss = sh.GetScreenshot();
                Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                ss.SaveAsFile(path);
                Console.WriteLine($"[screenshot] saved: {path}");
            }
        } catch(Exception e){
            Console.WriteLine($"[screenshot] failed: {e.Message}");
        }
    }
}
