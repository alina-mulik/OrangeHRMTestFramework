using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.WebElements;
using OrangeHRMTestFramework.Data.Constants;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM.Forms
{
    public class LoginForm : BaseForm
    {
        private OrangeWebElement _submitButton = new(By.XPath("//button[@type='submit']"));

        public void EnterDataToPasswordInput(string password)
        {
            var passwordInput = new OrangeWebElement(By.XPath(string.Format(BaseInputLocator, UserManagementFieldNames.Password)));
            passwordInput.SendKeys(password);
        }

        public void ClickLoginButton() => _submitButton.ClickWithScroll();
    }
}
