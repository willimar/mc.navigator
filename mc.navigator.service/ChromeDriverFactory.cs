using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace mc.navigator
{
    public static class ChromeDriverFactory
    {
        public static IWebDriver CreateDriver(string driverPath, DriverOptions options)
        {
            IWebDriver webDriver = new ChromeDriver(driverPath, (ChromeOptions)options, TimeSpan.FromSeconds(120));
            return webDriver;
        }

        public static DriverOptions DefaultOptions(bool headless)
        {
            var chromeOptions = new ChromeOptions();

            if (headless)
            {
                chromeOptions.AddArguments("headless");
            }
            
            chromeOptions.AddArguments("--no-sandbox");
            chromeOptions.AcceptInsecureCertificates = true;
            chromeOptions.PageLoadStrategy = PageLoadStrategy.Normal;

            return chromeOptions;
        }
    }
}
