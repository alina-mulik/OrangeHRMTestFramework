using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.WebElements;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM.Tabs
{
    public class ResetPasswordTab : BaseTab
    {
        private OrangeWebElement _resetPasswordButton = new(By.XPath("//button[contains(concat(' ', @class, ' '), 'orangehrm-forgot-password-button--reset')]"));

        public void ClickResetPasswordButton() => _resetPasswordButton.ClickWithScroll();
    }
}
