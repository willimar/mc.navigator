using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using System.IO;

namespace mc.navigator.service.test
{
    [TestClass]
    public class NavigatorServiceTest
    {
        [TestMethod]
        public void NavigateTest()
        {
            var path = @"C:\Users\willi\source\repos\mc.navigator\";

            if (Directory.Exists(path))
            {
                using (var navigator = new Navigator(ChromeDriverFactory.CreateDriver(path, ChromeDriverFactory.DefaultOptions(false))))
                {
                    navigator
                        .Navigate(new Uri(@"http://www.tjrs.jus.br/site_php/consulta/index.php"))
                        .GetImageByXPath(@"//img[@name='img_check']", out Bitmap bitmap);

                    Assert.IsNotNull(bitmap);
                }
            }
        }
    }
}
