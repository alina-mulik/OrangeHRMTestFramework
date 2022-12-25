using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.Drivers;
using OrangeHRMTestFramework.Common.Extensions;
using OrangeHRMTestFramework.Common.WebElements;
using OrangeHRMTestFramework.Data.Constants;
using OrangeHRMTestFramework.Data.Enums;
using SeleniumExtras.WaitHelpers;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM.Forms
{
    public class AddUserForm : BaseForm
    {
        private OrangeWebElement _saveButton = new(By.XPath("//button[@type='submit']"));

        public void EnterAndSelectValueInEmployeeNameFilterInput(string value)
        {
            var employeeNameInput =
                new OrangeWebElement(By.XPath(string.Format(BaseInputLocator, UserManagementFieldNames.EmployeeName)));
            employeeNameInput.SendKeys(value);
            var searchingElement = new OrangeWebElement(By.XPath("//div[@role='listbox']/div[@role='option']/span[1]"));
            WebDriverFactory.Driver.GetWebDriverWait(pollingInterval: TimeSpan.FromSeconds(1)).Until(_ => searchingElement.Text != "Searching...");
            var searchedResult = employeeNameInput.FindElements(By.XPath($"//div[@role='listbox']/div[@role='option']"));
            searchedResult.FirstOrDefault().Click();
        }

        public void EnterValueToPasswordAndConfirmPasswordInputs(string passwordValue)
        {
            var addUserHeaderElement = new OrangeWebElement(By.XPath("//h6[@class='oxd-text oxd-text--h6 orangehrm-main-title']"));
            var passwordInputs = addUserHeaderElement.FindElements(By.XPath(string.Format(BaseInputLocator, UserManagementFieldNames.Password)));

            foreach (var input in passwordInputs)
            {
                input.SendKeys(passwordValue);
            }
        }

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

        public List<string> GetWarningMessagesText()
        {
            var listOfWarnings = new List<string>();
            var addUserHeaderElement = new OrangeWebElement(By.XPath(string.Format(BaseInputLocator, UserManagementFieldNames.EmployeeName)));
            var warningMessages = addUserHeaderElement.FindElements(By.XPath("//span[@class='oxd-text oxd-text--span oxd-input-field-error-message oxd-input-group__message']"));

            foreach (var warning in warningMessages)
            {
                listOfWarnings.Add(warning.Text);
            }

            return listOfWarnings;
        }

        public void ClickSaveButtonWithWait()
        {
            // Waiter is added here because for Username warning message it takes a little bit longer to disappear
            WaitUntilWarningMessagesAreNotDisplayed();
            _saveButton.Click();
        }

        public void ClickSaveButton()
        {
            _saveButton.Click();
        }

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
    }
}
