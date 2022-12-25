using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.WebElements;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM.Forms
{
    public class ResetPasswordForm : BaseForm
    {
        private OrangeWebElement _resetPasswordButton = new(By.XPath("//button[contains(concat(' ', @class, ' '), 'orangehrm-forgot-password-button--reset')]"));

        public void ClickResetPasswordButton() => _resetPasswordButton.ClickWithScroll();
    }
}
