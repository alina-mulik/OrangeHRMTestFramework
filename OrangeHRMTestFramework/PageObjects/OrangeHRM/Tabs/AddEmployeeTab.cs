using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.WebElements;
using OrangeHRMTestFramework.Data.Constants;
using OrangeHRMTestFramework.Helpers;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM.Tabs
{
    public class AddEmployeeTab : BaseTab
    {
        private OrangeWebElement _firstNameTextBox = new(By.XPath("//input[@name='firstName']"));
        private OrangeWebElement _middleNameTextBox = new(By.XPath("//input[@name='middleName']"));
        private OrangeWebElement _lastNameTextBox = new(By.XPath("//input[@name='lastName']"));
        private OrangeWebElement _idTextBox = new(By.XPath("//label[contains(text(), 'Employee Id')]//following::div[1]/input"));
        private OrangeWebElement _saveButton = new(By.XPath("//button[@type='submit']"));

        public void EnterDataToAllInputsAndClickSave(string firstName, string lastName, string middleName)
        {
            _firstNameTextBox.SendKeys(firstName);
            _middleNameTextBox.SendKeys(middleName);
            _lastNameTextBox.SendKeys(lastName);
            ClickSaveButton();

            // Check if warning message displayed after clicking Save button, because warning sometimes appers before and sometimes after Saving
            if (IsWarningMessageDisplayedByWarningMessageText(OrangeMessages.EmployeeIdAlreadyExistsWarningMessage))
            {
                ChangeValueInIdTextBoxIfWarningDisplayed();
                ClickSaveButton();
            }
        }

        public void ClickSaveButton() => _saveButton.Click();

        public string GetValueFromIdTextBox() => _idTextBox.GetElementValue();

        public void ChangeValueInIdTextBoxIfWarningDisplayed()
        {
            var isWarningDisplayed = IsWarningMessageDisplayedByWarningMessageText(OrangeMessages.EmployeeIdAlreadyExistsWarningMessage);
            while (isWarningDisplayed & GetValueFromIdTextBox().Length != 10)
            {
                var uniquePartOfId = RandomHelper.GetRandomString(1);
                _idTextBox.SendKeys(uniquePartOfId);
                isWarningDisplayed = IsWarningMessageDisplayedByWarningMessageText(OrangeMessages.EmployeeIdAlreadyExistsWarningMessage);
            }
        }

        private bool IsWarningMessageDisplayedByWarningMessageText(string warningText)
        {
            var warningMessages = _idTextBox.FindElements(By.XPath($"//span[contains(concat(' ', @class, ' '), 'oxd-input-field-error-message')][.='{warningText}']"));

            if (warningMessages.Count() > 0)
            {
                return true;
            }

            return false;
        }
    }
}
