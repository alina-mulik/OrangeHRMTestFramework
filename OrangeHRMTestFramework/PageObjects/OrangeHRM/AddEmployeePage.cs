using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.Drivers;
using OrangeHRMTestFramework.Common.Extensions;
using OrangeHRMTestFramework.Common.WebElements;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM
{
    public class AddEmployeePage : BaseOrangePage
    {
        private OrangeWebElement _firstNameInput = new(By.XPath("//input[@name='firstName']"));
        private OrangeWebElement _middleNameInput = new(By.XPath("//input[@name='middleName']"));
        private OrangeWebElement _lastNameInput = new(By.XPath("//input[@name='lastName']"));
        private OrangeWebElement _idInput = new(By.XPath("//label[contains(text(), 'Employee Id')]//following::div[1]/input"));
        private OrangeWebElement _uploadImageButton = new(By.XPath("//button[@class='oxd-icon-button employee-image-action']']"));
        private OrangeWebElement _cancelButton = new(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--ghost']"));
        private OrangeWebElement _saveButton = new(By.XPath("//button[@type='submit']"));

        public void EnterDataToAllInputs(string firstName, string lastName, string middleName)
        {
            _firstNameInput.SendKeys(firstName);
            _middleNameInput.SendKeys(middleName);
            _lastNameInput.SendKeys(lastName);
        }

        public void ClickSaveButton() => _saveButton.Click();

        public string GetValueFromIdInput() => _idInput.GetAttribute("value");
    }
}
