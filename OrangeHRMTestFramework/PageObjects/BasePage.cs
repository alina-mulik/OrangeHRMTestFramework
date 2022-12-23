using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.WebElements;

namespace OrangeHRMTestFramework.PageObjects
{
    public class BasePage
    {
        private OrangeWebElement _orangeCopyrightFooter = new(By.XPath("//div[@class='oxd-layout-footer' or @class='orangehrm-copyright-wrapper']"));

        public string GetOrangeFooterText()
        {
            var copyrightElementsList = _orangeCopyrightFooter
                .FindElements(By.XPath("//p[@class='oxd-text oxd-text--p orangehrm-copyright']")).Select(_ => _.Text);

            return string.Join("", copyrightElementsList);
        }
    }
}
