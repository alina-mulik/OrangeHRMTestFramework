using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.WebElements;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM.Popups
{
    public class DeleteEntryPopup
    {
        private OrangeWebElement _yesDeleteEntryButton = new(By.XPath("//i[@class='oxd-icon bi-trash oxd-button-icon']//ancestor::button"));
        private OrangeWebElement _noDeleteEntryButton = new(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--text orangehrm-button-margin']"));

        public void ClickYesDeleteEntryButton() => _yesDeleteEntryButton.Click();

        public void ClickNoDeleteEntryButton() => _noDeleteEntryButton.Click();
    }
}
