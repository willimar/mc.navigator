using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Text;

namespace mc.navigator
{
    public static class FirefoxDriverFactory
    {
        public static IWebDriver CreateDriver(string driverPath, DriverOptions options)
        {
            IWebDriver webDriver = new FirefoxDriver(driverPath, (FirefoxOptions)options);
            return webDriver;
        }

        public static DriverOptions DefaultOptions(bool headless)
        {
            var chromeOptions = new FirefoxOptions();
            chromeOptions.AddArguments(headless ? "headless" : string.Empty);
            chromeOptions.AddArguments("--no-sandbox");
            chromeOptions.AddArguments("--disable-dev-shm-usage");

            return chromeOptions;
        }
    }
}
