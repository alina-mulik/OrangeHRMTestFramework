using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.WebElements;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM.Filters
{
    public class BaseFilter
    {
        private OrangeWebElement _searchButton = new(By.XPath("//form[@class='oxd-form']//button[@type='submit']"));
        private OrangeWebElement _resetButton = new(By.XPath("//form[@class='oxd-form']//button[@type='button']"));
        protected string BaseFilterInputLocator => "//label[contains(text(), '{0}')]//following::div[1]//input";
        protected string FilterDropdownXPath => "//label[contains(text(), '{0}')]//ancestor::div[@class='oxd-input-group oxd-input-field-bottom-space']"
                                                + "//div[@class='oxd-select-text oxd-select-text--active']";
        protected string FilterInputXPath => "//label[contains(text(), '{0}')]//ancestor::div[@class='oxd-input-group " +
            "oxd-input-field-bottom-space']//input";
        protected string FilterInputWithSuggestionsXPath => "//label[contains(text(), '{0}')]//ancestor::div[@class='oxd-input-group " +
            "oxd-input-field-bottom-space']//input";

        public void ClickSearchButton() => _searchButton.Click();

        public void ClickResetButton() => _resetButton.Click();
    }
}
