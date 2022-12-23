using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.WebElements;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM
{
    public class UserManagementPage : BaseOrangePage
    {
        private OrangeWebElement _addUserButton = new(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--secondary']"));

        public void ClickAddUserButton() => _addUserButton.ClickWithScroll();
    }
}
