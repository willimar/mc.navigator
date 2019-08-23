using System;
using System.IO;

namespace mc.navigator.console
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\Users\willi\source\repos\mc.navigator\";

            if (Directory.Exists(path))
            {
                using (var navigator = new Navigator(ChromeDriverFactory.CreateDriver(path, ChromeDriverFactory.DefaultOptions(false))))
                {
                    navigator
                        .Navigate(new Uri(@"http://www.tjrs.jus.br/site_php/consulta/index.php"))
                        .LoadElementByName("img_check")
                        .Navigate(new Uri(navigator.GetElementAtribute("src")));

                    var img = navigator.Content;
                }
            }
        }
    }
}
