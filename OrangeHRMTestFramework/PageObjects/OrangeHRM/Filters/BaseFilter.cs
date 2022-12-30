using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.Drivers;
using OrangeHRMTestFramework.Common.Extensions;
using OrangeHRMTestFramework.Common.WebElements;
using OrangeHRMTestFramework.Data.Constants;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM.Filters
{
    public class BaseFilter
    {
        private OrangeWebElement _searchButton = new(By.XPath("//form[@class='oxd-form']//button[@type='submit']"));
        private OrangeWebElement _resetButton = new(By.XPath("//form[@class='oxd-form']//button[@type='button']"));
        protected string BaseFilterTextBoxLocator => "//label[contains(text(), '{0}')]//following::input[1]";
        protected string BaseDropdownLocator => "//label[contains(text(), '{0}')]//ancestor::div[@class='oxd-input-group oxd-input-field-bottom-space']"
            + "//div[@class='oxd-select-text oxd-select-text--active']";

        public void EnterAndSelectValueInEmployeeNameFilterTextBox(string value)
        {
            var employeeNameTextBox = new OrangeWebElement(By.XPath(string.Format(BaseFilterTextBoxLocator, UserManagementFieldNames.EmployeeName)));
            employeeNameTextBox.SendKeys(value);
            var searchingElement = new OrangeWebElement(By.XPath("//div[@role='listbox']/div[@role='option']/span[1]"));
            WebDriverFactory.Driver.GetWebDriverWait(pollingInterval: TimeSpan.FromSeconds(1)).Until(_ => searchingElement.Text != "Searching...");
            var searchedResult = employeeNameTextBox.FindElements(By.XPath($"//div[@role='listbox']/div[@role='option']"));
            searchedResult.FirstOrDefault().Click();
        }

        public void ClickSearchButton() => _searchButton.Click();

        public void ClickResetButton() => _resetButton.Click();
    }
}
