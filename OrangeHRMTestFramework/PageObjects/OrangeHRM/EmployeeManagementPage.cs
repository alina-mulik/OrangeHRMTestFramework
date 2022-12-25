using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.WebElements;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM
{
    public class EmployeeManagementPage : BaseOrangePage
    {
        private OrangeWebElement _addEmployeeButton = new(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--secondary']"));

        public void ClickAddEmployeeButton() => _addEmployeeButton.Click();
    }
}
