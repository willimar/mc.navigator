using OpenQA.Selenium;
using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace mc.navigator
{
    public class Navigator: IDisposable
    {
        private readonly IWebDriver _webDriver;
        private IWebElement _element;

        public string Content { get { return this._webDriver.PageSource; } }
        public string GetElementText()
        { 
            return this._element.Text;
        }

        public string GetElementAtribute(string attribute)
        {
            return this._element.GetAttribute(attribute);
        }

        public Navigator(IWebDriver webDriver)
        {
            this._webDriver = webDriver;
        }
        
        public Navigator Navigate(Uri uri)
        {
            this._webDriver.Navigate().GoToUrl(uri);
            return this;
        }

        public Navigator LoadElementById(string value)
        {
            this._element = this._webDriver.FindElement(By.Id(value));
            return this;
        }

        public Navigator LoadElementByName(string value)
        {
            this._element = this._webDriver.FindElement(By.Name(value));
            return this;
        }

        public Navigator LoadElementByXPath(string value)
        {
            this._element = this._webDriver.FindElement(By.XPath(value));
            return this;
        }

        public Navigator ElementClick()
        {
            this._element.Click();
            return this;
        }

        public Navigator ElementSendKey(string text)
        {
            this._element.SendKeys(text);
            return this;
        }

        public Navigator ExecuteScript(string script)
        {
            var executor = this._webDriver as IJavaScriptExecutor;
            executor.ExecuteScript(script);
            return this;
        }

        public Navigator GetImageById(string elementId, out Bitmap bitmap)
        {
            var script =
                @"var c = document.createElement('canvas');
                var ctx = c.getContext('2d');
                var img = document.getElementById('" + elementId + @"');
                c.height=img.height;
                c.width=img.width;
                ctx.drawImage(img, 0, 0,img.width, img.height);
                var base64String = c.toDataURL();
                return base64String;";
            var base64string = ((IJavaScriptExecutor)this._webDriver).ExecuteScript(script) as string;
            var base64 = base64string.Split(',').Last();

            using (var stream = new MemoryStream(Convert.FromBase64String(base64)))
            {
                bitmap = new Bitmap(stream);
            }

            return this;
        }
        
        public Navigator GetImageByXPath(string xpath, out Bitmap bitmap)
        {
            ITakesScreenshot ssdriver = this._webDriver as ITakesScreenshot;
            Screenshot screenshot = ssdriver.GetScreenshot();

            using (var ms = new MemoryStream(screenshot.AsByteArray))
            {
                using (var fullImage = new Bitmap(ms))
                {
                    this.LoadElementByXPath(xpath);

                    Point point = this._element.Location;
                    int width = this._element.Size.Width;
                    int height = this._element.Size.Height;

                    bitmap = new Bitmap(width, height);
                    var flagX = 0;
                    var flagY = 0;
                    for (int x = point.X; x < point.X + width; x++)
                    {
                        for (int y = point.Y; y < point.Y + height; y++)
                        {
                            bitmap.SetPixel(flagX, flagY, fullImage.GetPixel(x, y));
                            flagY++;
                        }
                        flagY = 0;
                        flagX++;
                    }
                }
            }

            return this;
        }

        public Navigator Close()
        {
            this._webDriver.Close();
            return this;
        }

        public void Dispose()
        {
            if (this._webDriver != null)
            {
                this._webDriver.Dispose();
            }

            if (this._element != null)
            {
                this._element = null;
            }
        }
    }
}
