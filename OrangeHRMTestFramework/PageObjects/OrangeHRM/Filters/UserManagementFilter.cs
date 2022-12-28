using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.WebElements;
using OrangeHRMTestFramework.Data.Constants;
using OrangeHRMTestFramework.Data.Enums;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM.Filters
{
    public class UserManagementFilter: BaseFilter
    {
        public void EnterValueToUserNameFilterInput(string value) => new OrangeWebElement(By.XPath(string.Format(BaseFilterInputLocator, UserManagementFieldNames.Username))).SendKeys(value);

        public void SelectValueInStatusDropdown<TEnum>(TEnum value) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            SelectValueInDropdown(UserManagementFieldNames.Status, value);
        }

        public void SelectValueInUserRoleDropdown<TEnum>(TEnum value) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            SelectValueInDropdown(UserManagementFieldNames.UserRole, value);
        }

        public void EnterValuesToAllFiltersAndApply(string valueForUserName, string valueForEmployeeName, Status valueOfStatus, UserRole valueOfUserRole)
        {
                EnterValueToUserNameFilterInput(valueForUserName);
                EnterAndSelectValueInEmployeeNameFilterInput(valueForEmployeeName);
                SelectValueInStatusDropdown(valueOfStatus);
                SelectValueInUserRoleDropdown(valueOfUserRole);
                ClickSearchButton();
        }

        private void SelectValueInDropdown<TEnum>(string fieldName, TEnum value) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            if ((typeof(TEnum).IsEnum))
            {
                var dropdownElement = new OrangeWebElement(By.XPath(string.Format(BaseDropdownLocator, fieldName)));
                dropdownElement.Click();
                string optionName = value.ToString();
                var option = new OrangeWebElement(By.XPath($"//div[@role='listbox']/div[@role='option']/span[contains(text(), '{optionName}')]"));
                option.Click();
            }
        }
    }
}
