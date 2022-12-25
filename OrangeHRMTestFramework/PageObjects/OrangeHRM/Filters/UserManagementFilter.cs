using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.WebElements;
using OrangeHRMTestFramework.Data.Constants;
using OrangeHRMTestFramework.Data.Enums;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM.Filters
{
    public class UserManagementFilter: BaseFilter
    {
        public void EnterValueToUserNameFilterInput(string value) => new OrangeWebElement(By.XPath(string.Format(BaseFilterInputLocator, UserManagementFieldNames.Username))).SendKeys(value);

        public void SelectValueInStatusDropdown(Status value)
        {
            var statusDropdown = new OrangeWebElement(By.XPath(string.Format(BaseDropdownLocator, UserManagementFieldNames.Status)));
            statusDropdown.Click();
            string name = Enum.GetName(typeof(Status), value);
            var option = new OrangeWebElement(By.XPath($"//div[@role='listbox']/div[@role='option']/span[contains(text(), '{name}')]"));
            option.Click();
        }

        public void SelectValueInUserRoleDropdown(UserRole value)
        {
            var statusDropdown = new OrangeWebElement(By.XPath(string.Format(BaseDropdownLocator, UserManagementFieldNames.UserRole)));
            statusDropdown.Click();
            string name = Enum.GetName(typeof(UserRole), value);
            var option = new OrangeWebElement(By.XPath($"//div[@role='listbox']/div[@role='option']/span[contains(text(), '{name}')]"));
            option.Click();
        }

        public void EnterValuesToAllFiltersAndApply(string valueForUserName, string valueForEmployeeName,
            Status valueOfStatus, UserRole valueOfUserRole)
        {
            EnterValueToUserNameFilterInput(valueForUserName);
            EnterAndSelectValueInEmployeeNameFilterInput(valueForEmployeeName);
            SelectValueInStatusDropdown(valueOfStatus);
            SelectValueInUserRoleDropdown(valueOfUserRole);
            ClickSearchButton();
        }
    }
}
