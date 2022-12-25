using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.Drivers;
using OrangeHRMTestFramework.Common.Extensions;
using OrangeHRMTestFramework.Common.WebElements;
using OrangeHRMTestFramework.Data.Constants;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM.Forms
{
    public class BaseForm
    {
        private OrangeWebElement _successToastMessage = new(By.XPath("//div/p[@class='oxd-text oxd-text--p oxd-text--toast-message oxd-toast-content-text']"));
        protected string BaseInputLocator => "//label[contains(text(), '{0}')]//following::input[1]";
        protected string BaseDropdownLocator => "//label[contains(text(), '{0}')]//ancestor::div[@class='oxd-input-group oxd-input-field-bottom-space']"
            + "//div[@class='oxd-select-text oxd-select-text--active']";

        public void EnterDataToUsernameInput(string username)
        {
            var usernameInput = new OrangeWebElement(By.XPath(string.Format(BaseInputLocator, UserManagementFieldNames.Username)));
            usernameInput.SendKeys(username);
        }

        public void WaitUntilSuccessMessageDisplayed() => WebDriverFactory.Driver
            .GetWebDriverWait(pollingInterval: TimeSpan.FromSeconds(1)).Until(_ => _successToastMessage.Displayed);

        public string GetTextFromSuccessMessage()
        {
            WebDriverFactory.Driver.GetWebDriverWait(pollingInterval: TimeSpan.FromSeconds(1)).Until(_ => _successToastMessage.Displayed);
            var successMessageText = _successToastMessage.Text;

            return successMessageText;
        }
    }
}
