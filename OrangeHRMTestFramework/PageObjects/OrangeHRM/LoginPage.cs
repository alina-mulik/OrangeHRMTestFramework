using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.WebElements;
using OrangeHRMTestFramework.PageObjects.OrangeHRM.Tabs;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM
{
    public class LoginPage : BasePage
    {
        private OrangeWebElement _forgotPasswordLink = new(By.XPath("//p[contains(concat(' ', @class, ' '), 'orangehrm-login-forgot-header')]"));
        private OrangeWebElement _invalidCredentialsAlert = new(By.XPath("//div[@role='alert']"));
        public LoginTab LoginTab => new LoginTab();

        public ResetPasswordPage ClickForgotPasswordLink()
        {
            _forgotPasswordLink.ClickWithScroll();

            return new ResetPasswordPage();
        }

        public bool IsInvalidCredentialsAlertDisplayed() => _invalidCredentialsAlert.Displayed;

        public string GetTextFromInvalidCredentialsAlert() => new OrangeWebElement(By.XPath("//p[@class='oxd-text oxd-text--p oxd-alert-content-text']")).Text;
    }
}
