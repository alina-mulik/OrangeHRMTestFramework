using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.WebElements;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM
{
    public class ChangeEmployeeProfilePicturePage: BaseOrangePage
    {
        private OrangeWebElement _editEmployeeImageElement = new(By.XPath("//div[@class='orangehrm-edit-employee-image']"));
        private OrangeWebElement _employeeImage = new(By.XPath("//img[@class='employee-image']"));
        private OrangeWebElement _changeImageInput = new(By.XPath("//input[@class='oxd-file-input']"));
        private OrangeWebElement _saveButton = new(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--secondary orangehrm-left-space']"));

        public void ClickOnChangeEmployeeImageElement() => _editEmployeeImageElement.Click();

        public void ChangeProfileImage(string filePath)
        {
            _changeImageInput.SendKeys(filePath);
        }

        public void ClickSaveButton() => _saveButton.ClickWithScroll();

        public string GetEmployeeImageSrcAttribute()
        {
            var srcAttribute = _employeeImage.GetDomAttribute("src");

            return srcAttribute;
        }
    }
}
