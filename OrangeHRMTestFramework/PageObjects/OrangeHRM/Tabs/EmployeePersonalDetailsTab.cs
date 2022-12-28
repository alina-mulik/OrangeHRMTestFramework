using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.WebElements;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM.Tabs
{
    public class EmployeePersonalDetailsTab : BaseTab
    {
        private OrangeWebElement _firstNameTextBox = new(By.XPath("//input[@name='firstName']"));
        private OrangeWebElement _middleNameTextBox = new(By.XPath("//input[@name='middleName']"));
        private OrangeWebElement _lastNameTextBox = new(By.XPath("//input[@name='lastName']"));
        private OrangeWebElement _saveButton = new(By.XPath("(//button[@type='submit'])[1]"));

        public void EditPreviousValuesInAllNameInputs(string value)
        {
            // Added clicks here because sometimes SendKeys didn't work
            _firstNameTextBox.Click();
            _firstNameTextBox.SendKeys(value);
            _middleNameTextBox.Click();
            _middleNameTextBox.SendKeys(value);
            _lastNameTextBox.Click();
            _lastNameTextBox.SendKeys(value);
        }

        public void ClickSaveButton() => _saveButton.ClickWithScroll();
    }
}
