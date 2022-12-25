using NUnit.Framework;
using OrangeHRMTestFramework.Common.Drivers;
using OrangeHRMTestFramework.Data;
using OrangeHRMTestFramework.Helpers;
using OrangeHRMTestFramework.Models;
using OrangeHRMTestFramework.PageObjects.OrangeHRM.Forms;

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
            var loginForm = GenericForms.LoginForm;
            loginForm.EnterDataToUsernameInput(TestSettings.OrangeAdminUserName);
            loginForm.EnterDataToPasswordInput(TestSettings.OrangeAdminPassword);
            loginForm.ClickLoginButton();
        }
    }
}
