using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.WebElements;
using OrangeHRMTestFramework.PageObjects.OrangeHRM.DataGrids;
using OrangeHRMTestFramework.PageObjects.OrangeHRM.Filters;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM
{
    public class UserManagementPage : BaseOrangePage
    {
        public BasicDataGrid BasicDataGrid => new BasicDataGrid();
        public UserManagementFilter UserManagementFilter => new UserManagementFilter();

        private OrangeWebElement _addUserButton = new(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--secondary']"));

        public void ClickAddUserButton() => _addUserButton.ClickWithScroll();
    }
}
