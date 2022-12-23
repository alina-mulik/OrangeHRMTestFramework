using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.WebElements;
using OrangeHRMTestFramework.Data.Constants;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM
{
    public class LoginPage : BasePage
    {
        private OrangeWebElement _submitButton = new(By.XPath("//button[@type='submit']"));
        private OrangeWebElement _forgotPasswordLink = new(By.XPath("//p[contains(concat(' ', @class, ' '), 'orangehrm-login-forgot-header')]"));
        private OrangeWebElement _invalidCredentialsAlert = new(By.XPath("//div[@role='alert']"));
        private string BaseInputLocator => "//label[contains(text(), '{0}')]//following::div[1]/input";

        public void EnterDataToUsernameInput(string username)
        {
            var usernameInput = new OrangeWebElement(By.XPath(string.Format(BaseInputLocator, UserManagementFieldNames.Username)));
            usernameInput.SendKeys(username);
        }

        public void EnterDataToPasswordInput(string password)
        {
            var passwordInput = new OrangeWebElement(By.XPath(string.Format(BaseInputLocator, UserManagementFieldNames.Password)));
            passwordInput.SendKeys(password);
        }

        public void ClickLoginButton() => _submitButton.ClickWithScroll();

        public void ClickForgotPasswordLink() => _forgotPasswordLink.ClickWithScroll();

        public bool IsInvalidCredentialsAlertDisplayed() => _invalidCredentialsAlert.Displayed;

        public string GetTextFromInvalidCredentialsAlert() => new OrangeWebElement(By.XPath("//p[@class='oxd-text oxd-text--p oxd-alert-content-text']")).Text;
    }
}
