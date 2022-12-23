using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.Drivers;
using OrangeHRMTestFramework.Common.Extensions;
using OrangeHRMTestFramework.Common.WebElements;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM
{
    public class EmployeeListPage : BaseOrangePage
    {
        private OrangeWebElement _addEmployeeButton = new(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--secondary']"));

        public void ClickAddEmployeeButton() => _addEmployeeButton.Click();
    }
}
