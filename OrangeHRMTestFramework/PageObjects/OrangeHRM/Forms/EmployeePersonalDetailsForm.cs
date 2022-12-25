using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.WebElements;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM.Forms
{
    public class EmployeePersonalDetailsForm : BaseForm
    {
        private OrangeWebElement _firstNameInput = new(By.XPath("//input[@name='firstName']"));
        private OrangeWebElement _middleNameInput = new(By.XPath("//input[@name='middleName']"));
        private OrangeWebElement _lastNameInput = new(By.XPath("//input[@name='lastName']"));
        private OrangeWebElement _saveButton = new(By.XPath("(//button[@type='submit'])[1]"));

        public void EditPreviousValuesInAllNameInputs(string value)
        {
            // Added clicks here because sometimes SendKeys didn't work
            _firstNameInput.Click();
            _firstNameInput.SendKeys(value);
            _middleNameInput.Click();
            _middleNameInput.SendKeys(value);
            _lastNameInput.Click();
            _lastNameInput.SendKeys(value);
        }

        public void ClickSaveButton() => _saveButton.ClickWithScroll();
    }
}
