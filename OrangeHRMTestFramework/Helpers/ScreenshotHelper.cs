using OpenQA.Selenium;

namespace OrangeHRMTestFramework.Helpers
{
    public static class ScreenshotHelper
    {
        public static string TakeScreenshot(IWebDriver driver, string screenshotName)
        {
            var name = screenshotName.Length <= 46 ? screenshotName : screenshotName.Substring(0, 46);
            var fileNameBase = $"{name}_{DateTime.Now:yyyyMMdd_HHss}.png";

            // get directory path where screenshots will be saved
            var screenshotDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}\\DriverErrorScreenshots";

            // if the directory does not exist - create it
            if (!Directory.Exists(screenshotDirectory))
            {
                Directory.CreateDirectory(screenshotDirectory);
            }

            // convert IWebDriver to ITakeScreenshot and call GetScreenshot method to get the object with type Screenshot
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();

            // combine screenshot name with directory path
            var screenshotPath = Path.Combine(screenshotDirectory, fileNameBase);

            // save file to the directory
            screenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);

            return screenshotPath;
        }
    }
}
