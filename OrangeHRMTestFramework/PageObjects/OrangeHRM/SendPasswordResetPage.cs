using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.WebElements;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM
{
    public class SendPasswordResetPage : BasePage
    {
        private OrangeWebElement _resetPasswordSentHeader = new(By.XPath("//div[@class='orangehrm-card-container']//h6[contains(concat(' ', @class, ' '), 'orangehrm-forgot-password-title')]"));

        public string GetTextOfResetPasswordSentTitle() => _resetPasswordSentHeader.Text;
    }
}
