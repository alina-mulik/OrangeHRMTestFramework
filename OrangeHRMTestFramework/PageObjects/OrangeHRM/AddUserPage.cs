using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.WebElements;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM
{
    public class AddUserPage : BaseOrangePage
    {
        private OrangeWebElement _doubleClickOutput = new(By.XPath("//p[@id='doubleClickMessage']"));
        private OrangeWebElement _rightClickOutput = new(By.XPath("//p[@id='rightClickMessage']"));

    }
}
