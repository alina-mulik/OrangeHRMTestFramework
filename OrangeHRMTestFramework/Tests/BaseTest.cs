using NUnit.Framework;
using OrangeHRMTestFramework.Common.Drivers;
using OrangeHRMTestFramework.Data;
using OrangeHRMTestFramework.Helpers;
using OrangeHRMTestFramework.Models;
using OrangeHRMTestFramework.PageObjects.OrangeHRM;
using OrangeHRMTestFramework.PageObjects.OrangeHRM.Popups;

namespace OrangeHRMTestFramework.Tests
{
    public class BaseTest
    {
        private UserInfo _userInfo;

        public BaseTest(UserInfo userInfo = null)
        {
            _userInfo = userInfo;
        }

        [OneTimeSetUp]
        public void BaseTestOneTimeSetUp()
        {
            WebDriverFactory.Driver.Navigate().GoToUrl(TestSettings.OrangeLoginPageUrl);

            if (_userInfo != null)
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

        private void LogInAsAnAdminUser()
        {
            var loginPage = GenericPages.LoginPage;
            loginPage.EnterDataToUsernameInput(TestSettings.OrangeAdminUserName);
            loginPage.EnterDataToPasswordInput(TestSettings.OrangeAdminPassword);
            loginPage.ClickLoginButton();
        }
    }
}
