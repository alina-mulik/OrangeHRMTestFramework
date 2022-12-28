using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.WebElements;
using OrangeHRMTestFramework.Data.Constants;
using OrangeHRMTestFramework.Data.Enums;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM.Filters
{
    public class UserManagementFilter: BaseFilter
    {
        public void EnterValueToUserNameFilterTextBox(string value) => new OrangeWebElement(By.XPath(string.Format(BaseFilterTextBoxLocator, UserManagementFieldNames.Username))).SendKeys(value);

        public void SelectValueInStatusDropdown(string value)
        {
            SelectValueInDropdown(UserManagementFieldNames.Status, value);
        }

        public void SelectValueInUserRoleDropdown(string value)
        {
            SelectValueInDropdown(UserManagementFieldNames.UserRole, value);
        }

        public void EnterValuesToAllFiltersAndApply(string valueForUserName, string valueForEmployeeName, Status valueOfStatus, UserRole valueOfUserRole)
        {
            var statusValueString = valueOfStatus.ToString();
            var userRoleValueString = valueOfUserRole.ToString();
            EnterValueToUserNameFilterTextBox(valueForUserName);
            EnterAndSelectValueInEmployeeNameFilterInput(valueForEmployeeName);
            SelectValueInStatusDropdown(statusValueString);
            SelectValueInUserRoleDropdown(userRoleValueString);
            ClickSearchButton();
        }

        private void SelectValueInDropdown(string fieldName, string value)
        {
            var dropdownElement = new OrangeWebElement(By.XPath(string.Format(BaseDropdownLocator, fieldName)));
            dropdownElement.Click();
            var option = new OrangeWebElement(By.XPath($"//div[@role='listbox']/div[@role='option']/span[contains(text(), '{value}')]"));
            option.Click();
        }
    }
}
