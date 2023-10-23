using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    internal class HtmlTools
    {
       public static async Task HtmlToImage(string htmlContent)
        {
            // 启动 PuppeteerSharp
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            // 创建新的页面
            var page = await browser.NewPageAsync();

            try
            {
                // 设置视口大小（可选，根据需要调整）
                await page.SetViewportAsync(new ViewPortOptions
                {
                    Width = 1280,
                    Height = 4000
                });

                // 设置页面内容                
                await page.SetContentAsync(htmlContent);

                // 等待一段时间，以确保页面渲染完成（可根据需要调整）
                await Task.Delay(1000);

                // 将页面内容保存为图片
                var imageData = await page.ScreenshotDataAsync(new ScreenshotOptions
                {
                    Type = ScreenshotType.Png
                });

                // 将图片数据保存到文件
                var outputFile = "output.png";
                File.WriteAllBytes(outputFile, imageData);

                Console.WriteLine($"图片已保存到：{outputFile}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发生错误：{ex.Message}");
            }
            finally
            {
                // 关闭浏览器
                await browser.CloseAsync();
            }
        }

        //public static HtmlToImageByBrowser() {
        //    webBrowser = new WebBrowser();
        //    webBrowser.ScriptErrorsSuppressed = true; // 防止脚本错误弹窗
        //    webBrowser.ScrollBarsEnabled = false; // 禁用滚动条
        //    webBrowser.DocumentCompleted += WebBrowser_DocumentCompleted;
        //}
    }
}
