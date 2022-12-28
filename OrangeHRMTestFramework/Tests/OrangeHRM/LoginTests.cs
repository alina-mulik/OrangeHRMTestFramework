﻿using NUnit.Framework;
using OrangeHRMTestFramework.Common.Drivers;
using OrangeHRMTestFramework.Data.Constants;
using OrangeHRMTestFramework.Helpers;
using OrangeHRMTestFramework.PageObjects.OrangeHRM;

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
            var loginForm = LoginPage.LoginTab;
            loginForm.EnterDataToUsernameInput(randomUserName);
            loginForm.EnterDataToPasswordInput(randomPassword);
            loginForm.ClickLoginButton();

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

            // Enter data to User Name input and click Reset Password button on Reset Password Page
            var resetPasswordPage = ResetPasswordPage.ResetPasswordTab;
            resetPasswordPage.EnterDataToUsernameInput(randomUserName);
            resetPasswordPage.ClickResetPasswordButton();

            // Check that user is directed to the Send Password Reset page and check the success message displayed
            var resetPasswordSentText = GenericPages.SendPasswordResetPage.GetTextOfResetPasswordSentTitle();
            Assert.AreEqual(OrangeMessages.ResetPasswordSuccessMessage, resetPasswordSentText);

            // Check Footer text
            var actualFooterText = GenericPages.LoginPage.GetOrangeFooterText();
            Assert.AreEqual(OrangeMessages.OrangeFooterText, actualFooterText);
        }
    }
}
