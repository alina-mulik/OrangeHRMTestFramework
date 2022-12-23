using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.WebElements;
using OrangeHRMTestFramework.Data.Constants;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM
{
    public class ResetPasswordPage : BasePage
    {
        private OrangeWebElement _resetPasswordButton = new(By.XPath("//button[contains(concat(' ', @class, ' '), 'orangehrm-forgot-password-button--reset')]"));
        private OrangeWebElement _cancelButton = new(By.XPath("//button[contains(concat(' ', @class, ' '), 'orangehrm-forgot-password-button--cancel')]"));
        private string BaseInputLocator => "//label[contains(text(), '{0}')]//following::div[1]/input";

        public void EnterDataToUsernameInput(string username)
        {
            var usernameInput = new OrangeWebElement(By.XPath(string.Format(BaseInputLocator, UserManagementFieldNames.Username)));
            usernameInput.SendKeys(username);
        }

        public void ClickResetPasswordButton() => _resetPasswordButton.ClickWithScroll();

        public void ClickCancelButton() => _cancelButton.ClickWithScroll();
    }
}
