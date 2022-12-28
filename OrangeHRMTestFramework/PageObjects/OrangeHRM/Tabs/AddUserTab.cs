using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.Drivers;
using OrangeHRMTestFramework.Common.Extensions;
using OrangeHRMTestFramework.Common.WebElements;
using OrangeHRMTestFramework.Data.Constants;
using OrangeHRMTestFramework.Data.Enums;
using SeleniumExtras.WaitHelpers;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM.Tabs
{
    public class AddUserTab : BaseTab
    {
        private OrangeWebElement _saveButton = new(By.XPath("//button[@type='submit']"));

        public void EnterAndSelectValueInEmployeeNameFilterInput(string value)
        {
            var employeeNameTextBox = new OrangeWebElement(By.XPath(string.Format(BaseTextBoxLocator, UserManagementFieldNames.EmployeeName)));
            employeeNameTextBox.SendKeys(value);
            var searchingElement = new OrangeWebElement(By.XPath("//div[@role='listbox']/div[@role='option']/span[1]"));
            WebDriverFactory.Driver.GetWebDriverWait(pollingInterval: TimeSpan.FromSeconds(1)).Until(_ => searchingElement.Text != "Searching...");
            var searchedResult = employeeNameTextBox.FindElements(By.XPath($"//div[@role='listbox']/div[@role='option']"));
            searchedResult.FirstOrDefault().Click();
        }

        public void EnterValueToPasswordAndConfirmPasswordInputs(string passwordValue)
        {
            var addUserHeaderElement = new OrangeWebElement(By.XPath("//h6[@class='oxd-text oxd-text--h6 orangehrm-main-title']"));
            var passwordInputs = addUserHeaderElement.FindElements(By.XPath(string.Format(BaseTextBoxLocator, UserManagementFieldNames.Password)));

            foreach (var input in passwordInputs)
            {
                input.SendKeys(passwordValue);
            }
        }

        public void SelectValueInStatusDropdown(Status value)
        {
            var statusValueToString = value.ToString();
            SelectValueInDropdown(UserManagementFieldNames.Status, statusValueToString);
        }

        public void SelectValueInUserRoleDropdown(UserRole value)
        {
            var userRoleValueString = value.ToString();
            SelectValueInDropdown(UserManagementFieldNames.UserRole, userRoleValueString);
        }

        public List<string> GetWarningMessagesText()
        {
            var listOfWarnings = new List<string>();
            var addUserHeaderElement = new OrangeWebElement(By.XPath(string.Format(BaseTextBoxLocator, UserManagementFieldNames.EmployeeName)));
            var warningMessages = addUserHeaderElement.FindElements(By.XPath("//span[@class='oxd-text oxd-text--span oxd-input-field-error-message oxd-input-group__message']"));

            foreach (var warning in warningMessages)
            {
                listOfWarnings.Add(warning.Text);
            }

            return listOfWarnings;
        }

        public void ClickSaveButtonWithWait()
        {
            // Waiter is added here because it takes a little bit longer to disappear for Username warning message
            WaitUntilWarningMessagesAreNotDisplayed();
            _saveButton.Click();
        }

        public void ClickSaveButton() =>  _saveButton.Click();

        public bool IsWarningMessageWithCertainTextIsDisplayed(string text)
        {
            WebDriverFactory.Driver.GetWebDriverWait().Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//span[@class='oxd-text oxd-text--span oxd-input-field-error-message oxd-input-group__message']"), text));
            return true;
        }

        private void WaitUntilWarningMessagesAreNotDisplayed()
        {
            if (GetWarningMessagesText().Count == 1)
            {
                WebDriverFactory.Driver.GetWebDriverWait(pollingInterval: TimeSpan.FromSeconds(1)).Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(
                        "//span[@class='oxd-text oxd-text--span oxd-input-field-error-message oxd-input-group__message']")));
            }
            else if (GetWarningMessagesText().Count > 1)
            {
                throw new Exception("There's more than 1 warning message displayed! Can't save the form!");
            }
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
