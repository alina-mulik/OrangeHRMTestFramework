using Allure.Net.Commons;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using OrangeHRMTestFramework.Common.Drivers;
using OrangeHRMTestFramework.Data.Constants;
using OrangeHRMTestFramework.Helpers;
using OrangeHRMTestFramework.PageObjects.OrangeHRM;

namespace OrangeHRMTestFramework.Tests.OrangeHRM
{
    [TestFixture]
    [AllureNUnit]
    public class LoginTests: BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            WebDriverFactory.Driver.Navigate().Refresh();
        }

        [Test]
        [AllureTag("Regression")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureSuite("Login")]
        [AllureDescription("Check invalid login flow.")]
        public void InvalidLoginTest()
        {
            var randomUserName = RandomHelper.GetRandomString(7);
            var randomPassword = RandomHelper.GetRandomString(7);

            // Check Footer text on the Login Page
            var actualFooterText = GenericPages.LoginPage.GetOrangeFooterText();
            Assert.AreEqual(OrangeMessages.OrangeFooterText, actualFooterText);

            // Enter invalid data to UserName and Password inputs
            GenericPages.LoginPage.LoginTab.EnterDataToUsernameInput(randomUserName);
            GenericPages.LoginPage.LoginTab.EnterDataToPasswordInput(randomPassword);
            GenericPages.LoginPage.LoginTab.ClickLoginButton();

            // Check that invalid login alert is displayed
            Assert.IsTrue(GenericPages.LoginPage.IsInvalidCredentialsAlertDisplayed());

            // Check that invalid login message is correct
            var invalidLoginMessage = GenericPages.LoginPage.GetTextFromInvalidCredentialsAlert();
            Assert.AreEqual(OrangeMessages.InvalidCredentialsMessage, invalidLoginMessage);
        }

        [Test]
        [AllureTag("Regression")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureSuite("Login")]
        [AllureDescription("Check reset password flow.")]
        public void ResetPasswordTest()
        {
            var randomUserName = RandomHelper.GetRandomString(7);

            // Click Forgot Password link to open Reset Password popup
            var resetPasswordPage = GenericPages.LoginPage.ClickForgotPasswordLink();

            // Enter data to User Name input and click Reset Password button on Reset Password Page
            resetPasswordPage.ResetPasswordTab.EnterDataToUsernameInput(randomUserName);
            var sendResetPasswordPage = resetPasswordPage.ResetPasswordTab.ClickResetPasswordButton();

            // Check that user is directed to the Send Password Reset page and check the success message displayed
            var resetPasswordSentText = sendResetPasswordPage.GetTextOfResetPasswordSentTitle();
            Assert.AreEqual(OrangeMessages.ResetPasswordSuccessMessage, resetPasswordSentText);

            // Check Footer text
            var actualFooterText = sendResetPasswordPage.GetOrangeFooterText();
            Assert.AreEqual(OrangeMessages.OrangeFooterText, actualFooterText);
        }
    }
}
