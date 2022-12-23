using NUnit.Framework;
using OrangeHRMTestFramework.Common.Drivers;
using OrangeHRMTestFramework.Common.Extensions;
using OrangeHRMTestFramework.Data.Constants;
using OrangeHRMTestFramework.Data;
using OrangeHRMTestFramework.Helpers;
using OrangeHRMTestFramework.PageObjects.OrangeHRM;
using OrangeHRMTestFramework.PageObjects.OrangeHRM.Popups;

namespace OrangeHRMTestFramework.Tests.OrangeHRM
{
    public class LoginTests: BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            WebDriverFactory.Driver.Navigate().Refresh();
        }

        [Test]
        public void InvalidLoginTest()
        {
            var randomUserName = RandomHelper.GetRandomString(7);
            var randomPassword = RandomHelper.GetRandomString(7);

            // Check Footer text on the Login Page
            var loginPage = GenericPages.LoginPage;
            var actualFooterText = loginPage.GetOrangeFooterText();
            Assert.AreEqual(OrangeMessages.OrangeFooterText, actualFooterText);

            // Enter invalid data to UserName and Password inputs
            loginPage.EnterDataToUsernameInput(randomUserName);
            loginPage.EnterDataToPasswordInput(randomPassword);
            loginPage.ClickLoginButton();

            // Check that invalid login alert is displayed
            Assert.IsTrue(loginPage.IsInvalidCredentialsAlertDisplayed());

            // Check that invalid login message is correct
            var invalidLoginMessage = loginPage.GetTextFromInvalidCredentialsAlert();
            Assert.AreEqual(OrangeMessages.InvalidCredentialsMessage, invalidLoginMessage);
        }

        [Test]
        public void ResetPasswordTest()
        {
            var randomUserName = RandomHelper.GetRandomString(7);

            // Click Forgot Password link to open Reset Password popup
            GenericPages.LoginPage.ClickForgotPasswordLink();

            // Check that Reset Password page is opened
            var resetPasswordPageUrl = WebDriverFactory.Driver.GetCurrentUrl();
            Assert.AreEqual(TestSettings.OrangeResetPasswordPageUrl, resetPasswordPageUrl);

            // Enter data to User Name input and click Reset Password button
            var resetPasswordPage = GenericPages.ResetPasswordPage;
            resetPasswordPage.EnterDataToUsernameInput(randomUserName);
            resetPasswordPage.ClickResetPasswordButton();

            // Check that user is directed to the Send Password Reset page and check the success message displayed
            var sentResetPasswordPageUrl = WebDriverFactory.Driver.GetCurrentUrl();
            Assert.AreEqual(TestSettings.OrangeSendPasswordResetPageUrl, sentResetPasswordPageUrl);
            var resetPasswordSentText = GenericPages.SendPasswordResetPage.GetTextOfResetPasswordSentTitle();
            Assert.AreEqual(OrangeMessages.ResetPasswordSuccessMessage, resetPasswordSentText);

            // Check Footer text on the Send Password Reset page
            var actualFooterText = GenericPages.BaseOrangePage.GetOrangeFooterText();
            Assert.AreEqual(OrangeMessages.OrangeFooterText, actualFooterText);
        }
    }
}
