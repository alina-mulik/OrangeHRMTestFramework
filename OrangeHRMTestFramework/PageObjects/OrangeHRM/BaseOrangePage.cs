using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.Drivers;
using OrangeHRMTestFramework.Common.Extensions;
using OrangeHRMTestFramework.Common.WebElements;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM
{
    public class BaseOrangePage : BasePage
    {
        private OrangeWebElement _leftNavSearchInput = new(By.XPath("//div[@class='oxd-main-menu-search']/input"));
        private string _leftNavCategoryXPath = "//a[@class='oxd-main-menu-item'][1]";
        private string _topNavCategoryWithSubCategoryXPath = "//span[@class='oxd-topbar-body-nav-tab-item' and contains(text(), '{0}')]//ancestor::li";
        private string _topNavCategoryWithoutSubCategoryXPath = "//a[@class='oxd-topbar-body-nav-tab-item' and contains(text(), '{0}')]//ancestor::li";
        private string _topNavSubCategoryXPath = "//ul[@role='menu']//a[contains(text(), 'Users')]";
        private OrangeWebElement _successToastMessage = new(By.XPath("//div/p[@class='oxd-text oxd-text--p oxd-text--toast-message oxd-toast-content-text']"));

        public void ClickLeftNavCategory(string category)
        {
            _leftNavSearchInput.SendKeys(category);
            var leftNavCategory = new OrangeWebElement(By.XPath(_leftNavCategoryXPath));
            leftNavCategory.ClickWithScroll();
        }

        public string GetTextFromSuccessMessage()
        {
            WebDriverFactory.Driver.GetWebDriverWait(pollingInterval: TimeSpan.FromSeconds(1)).Until(_ => _successToastMessage.Displayed);
            var successMessageText = _successToastMessage.Text;

            return successMessageText;
        }

        public void ClickTopNavCategoryWithSubCategory(string categoryName, string? subCategoryName = null)
        {
            var topNavCategory = GetTopNavCategoryWithSubCategoriesElement(categoryName);
            if (!topNavCategory.GetValueOfClassAttribute().Contains("visited"))
            {
                topNavCategory.ClickWithScroll();
                if (subCategoryName != null)
                {
                    ClickTopNavSubCategory(subCategoryName);
                }
            }
            else
            {
                topNavCategory.ClickWithScroll();
                if (topNavCategory.GetValueOfClassAttribute().Contains("active") & subCategoryName != null)
                {
                    ClickTopNavSubCategory(subCategoryName);
                }
            }
        }

        public void ClickTopNavCategoryWithoutSubCategory(string categoryName) => new OrangeWebElement(By.XPath(string.Format(_topNavCategoryWithoutSubCategoryXPath, categoryName))).ClickWithScroll();

        private void ClickTopNavSubCategory(string? categoryName) => new OrangeWebElement(By.XPath(string.Format(_topNavSubCategoryXPath, categoryName))).ClickWithScroll();

        public void WaitUntilSuccessMessageDisplayed() => WebDriverFactory.Driver
            .GetWebDriverWait(pollingInterval: TimeSpan.FromSeconds(1)).Until(_ => _successToastMessage.Displayed);

        private OrangeWebElement GetTopNavCategoryWithSubCategoriesElement(string categoryName) => new OrangeWebElement(By.XPath(string.Format(_topNavCategoryWithSubCategoryXPath, categoryName)));
    }
}
