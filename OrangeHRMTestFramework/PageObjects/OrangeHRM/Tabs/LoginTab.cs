using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.WebElements;
using OrangeHRMTestFramework.Data.Constants;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM.Tabs
{
    public class LoginTab : BaseTab
    {
        private OrangeWebElement _submitButton = new(By.XPath("//button[@type='submit']"));

        public void EnterDataToPasswordInput(string password)
        {
            var passwordTextBox = new OrangeWebElement(By.XPath(string.Format(BaseTextBoxLocator, UserManagementFieldNames.Password)));
            passwordTextBox.SendKeys(password);
        }

        public void ClickLoginButton() => _submitButton.ClickWithScroll();
    }
}
