using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.WebElements;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM
{
    public class EditPersonalDetailsPage : BaseOrangePage
    {
        private OrangeWebElement _firstNameInput = new(By.XPath("//input[@name='firstName']"));
        private OrangeWebElement _middleNameInput = new(By.XPath("//input[@name='middleName']"));
        private OrangeWebElement _lastNameInput = new(By.XPath("//input[@name='lastName']"));
        private OrangeWebElement _saveButton = new(By.XPath("(//button[@type='submit'])[1]"));

        public void ClearPreviousValuesAndEnterNewDataToAllInputs(string firstName, string lastName, string middleName)
        {
            _firstNameInput.Clear();
            _firstNameInput.SendKeys(firstName);
            _middleNameInput.Clear();
            _middleNameInput.SendKeys(middleName);
            _lastNameInput.Clear();
            _lastNameInput.SendKeys(lastName);
        }

        public void ClickSaveButton() => _saveButton.ClickWithScroll();
    }
}
