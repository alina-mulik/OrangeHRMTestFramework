using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.WebElements;
using OrangeHRMTestFramework.PageObjects.OrangeHRM.DataGrids;
using OrangeHRMTestFramework.PageObjects.OrangeHRM.Filters;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM
{
    public class EmployeeManagementPage : BaseOrangePage
    {
        public BasicDataGrid BasicDataGrid => new BasicDataGrid();
        public EmployeeListFilter EmployeeListFilter => new EmployeeListFilter();

        private OrangeWebElement _addEmployeeButton = new(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--secondary']"));

        public void ClickAddEmployeeButton() => _addEmployeeButton.Click();
    }
}
