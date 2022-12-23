using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.Drivers;
using OrangeHRMTestFramework.Common.Extensions;
using OrangeHRMTestFramework.Common.WebElements;
using OrangeHRMTestFramework.Data.Constants;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM.Filters
{
    public class UserManagementFilter: BaseFilter
    {
        public void EnterValueToUserNameFilterInput(string value) => new OrangeWebElement(By.XPath(string.Format(BaseFilterInputLocator, UserManagementFieldNames.Username))).SendKeys(value);

        public void SelectValueInEmployeeNameFilterInput(string value)
        {
            var employeeNameInput = new OrangeWebElement(By.XPath(string.Format(BaseFilterInputLocator, UserManagementFieldNames.EmployeeName)));
            employeeNameInput.SendKeys(value);
            var searchingElement = new OrangeWebElement(By.XPath("//div[@role='listbox']/div[@role='option']/span[1]"));
            WebDriverFactory.Driver.GetWebDriverWait(pollingInterval: TimeSpan.FromSeconds(1)).Until(_ => searchingElement.Text != "Searching...");
            var searchedResult = employeeNameInput.FindElements(By.XPath($"//div[@role='listbox']/div[@role='option']"));
            searchedResult.FirstOrDefault().Click();
        }

        public void SelectValueInFilterDropdown(string value, string dropdown)
        {
            var statusDropdown = new OrangeWebElement(By.XPath(string.Format(FilterDropdownXPath, dropdown)));
            statusDropdown.Click();
            var option = new OrangeWebElement(By.XPath($"//div[@role='listbox']/div[@role='option']/span[contains(text(), '{value}')]"));
            option.Click();
        }

        public void SelectValueInUserRoleFilterDropdown(int index) => new OrangeWebElement(By.XPath(string.Format(FilterDropdownXPath, UserManagementFieldNames.UserRole)));

        public void EnterValuesToAllFiltersAndApply(string valueForUserName, string valueForEmployeeName,
            string valueOfStatus, string valueOfUserRole)
        {
            EnterValueToUserNameFilterInput(valueForUserName);
            SelectValueInEmployeeNameFilterInput(valueForEmployeeName);
            SelectValueInFilterDropdown(valueOfStatus, UserManagementFieldNames.Status);
            SelectValueInFilterDropdown(valueOfUserRole, UserManagementFieldNames.UserRole);
            ClickSearchButton();
        }
    }
}
