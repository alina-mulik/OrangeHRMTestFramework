using Allure.Net.Commons;
using NUnit.Framework;
using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.Drivers;
using OrangeHRMTestFramework.Data;
using OrangeHRMTestFramework.Helpers;
using OrangeHRMTestFramework.Models;
using OrangeHRMTestFramework.PageObjects.OrangeHRM;

namespace OrangeHRMTestFramework.Tests
{
    public class BaseTest
    {
        private UserInfo _adminUser;

        public BaseTest(UserInfo user = null)
        {
            _adminUser = user;
        }

        [OneTimeSetUp]
        public void BaseTestOneTimeSetUp()
        {
            WebDriverFactory.Driver.Navigate().GoToUrl(TestSettings.OrangeLoginPageUrl);

            if (_adminUser != null)
            {
                LogInAsAnAdminUser();
            }
        }

        [TearDown]
        public void BaseTestTearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                TakeScreenshot();
                TakeAllureScreenshot();
            }
        }

        [OneTimeTearDown]
        public void BaseTestOneTimeTearDown()
        {
            WebDriverFactory.QuitDriver();
        }

        private void TakeScreenshot()
        {
            var screenshotPath = ScreenshotHelper.TakeScreenshot(WebDriverFactory.Driver, TestContext.CurrentContext.Test.Name);
            TestContext.AddTestAttachment(screenshotPath);
        }

        private void TakeAllureScreenshot()
        {
            var screenshot = ((ITakesScreenshot)WebDriverFactory.Driver).GetScreenshot().AsByteArray;
            AllureLifecycle.Instance.AddAttachment(TestContext.CurrentContext.Test.Name, "image/png", screenshot);
        }

        private void LogInAsAnAdminUser()
        {
            var loginForm = GenericPages.LoginPage.LoginTab;
            loginForm.EnterDataToUsernameInput(_adminUser.UserName);
            loginForm.EnterDataToPasswordInput(_adminUser.Password);
            loginForm.ClickLoginButton();
        }
    }
}
