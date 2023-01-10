using Allure.Net.Commons;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OrangeHRMTestFramework.Common.Drivers;
using OrangeHRMTestFramework.Data.Constants;
using OrangeHRMTestFramework.Data.Constants.AllureConstants;
using OrangeHRMTestFramework.Helpers;
using OrangeHRMTestFramework.PageObjects.OrangeHRM;

namespace OrangeHRMTestFramework.Tests.OrangeHRM
{
    [TestFixture]
    [AllureSuite(AllureSuites.Login)]
    public class IntentionallyFailedTests : BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            WebDriverFactory.Driver.Navigate().Refresh();
        }

        [Test]
        [AllureSeverity(SeverityLevel.trivial)]
        [AllureDescription("Check footer on login page.")]
        public void CheckFooterOnLoginPageTestFailed()
        {
            // Check Footer text on the Login Page
            var actualFooterText = GenericPages.LoginPage.GetOrangeFooterText();
            Assert.AreEqual(OrangeMessages.InvalidOrangeFooterText, actualFooterText);
        }

        [Test]
        [AllureSeverity(SeverityLevel.trivial)]
        public void ResetPasswordTestFailed()
        {
            var randomUserName = RandomHelper.GetRandomString(7);

            // Click Forgot Password link to open Reset Password popup
            var resetPasswordPage = GenericPages.LoginPage.ClickForgotPasswordLink();

            // Enter data to User Name input and click Reset Password button on Reset Password Page
            resetPasswordPage.ResetPasswordTab.EnterDataToUsernameInput(randomUserName);
            var sendResetPasswordPage = resetPasswordPage.ResetPasswordTab.ClickResetPasswordButton();

            // Check that user is directed to the Send Password Reset page and check the success message displayed
            var resetPasswordSentText = sendResetPasswordPage.GetTextOfResetPasswordSentTitle();
            Assert.AreEqual(OrangeMessages.InvalidResetPasswordSuccessMessage, resetPasswordSentText);
        }
    }
}
