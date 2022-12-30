using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.WebElements;
using OrangeHRMTestFramework.Data.Constants;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM.Tabs
{
    public class BaseTab
    {
        protected string BaseTextBoxLocator => "//label[contains(text(), '{0}')]//following::input[1]";
        protected string BaseDropdownLocator => "//label[contains(text(), '{0}')]//ancestor::div[@class='oxd-input-group oxd-input-field-bottom-space']"
            + "//div[@class='oxd-select-text oxd-select-text--active']";

        public void EnterDataToUsernameInput(string username)
        {
            var usernameTextBox = new OrangeWebElement(By.XPath(string.Format(BaseTextBoxLocator, UserManagementFieldNames.Username)));
            usernameTextBox.SendKeys(username);
        }
    }
}
