using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.Drivers;
using OrangeHRMTestFramework.Common.Extensions;
using OrangeHRMTestFramework.Common.WebElements;
using OrangeHRMTestFramework.Data.Constants;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM.Tabs
{
    public class BaseTab
    {
        private OrangeWebElement _successToastMessage = new(By.XPath("//div/p[@class='oxd-text oxd-text--p oxd-text--toast-message oxd-toast-content-text']"));
        protected string BaseTextBoxLocator => "//label[contains(text(), '{0}')]//following::input[1]";
        protected string BaseDropdownLocator => "//label[contains(text(), '{0}')]//ancestor::div[@class='oxd-input-group oxd-input-field-bottom-space']"
            + "//div[@class='oxd-select-text oxd-select-text--active']";

        public void EnterDataToUsernameInput(string username)
        {
            var usernameTextBox = new OrangeWebElement(By.XPath(string.Format(BaseTextBoxLocator, UserManagementFieldNames.Username)));
            usernameTextBox.SendKeys(username);
        }

        public void WaitUntilSuccessMessageDisplayed() =>  _successToastMessage.WaitUntilDisplayed();

        public string GetTextFromSuccessMessage()
        {
            _successToastMessage.WaitUntilDisplayed();
            var successMessageText = _successToastMessage.Text;

            return successMessageText;
        }
    }
}
