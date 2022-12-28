using System.Collections.ObjectModel;
using System.Drawing;
using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.Drivers;
using OrangeHRMTestFramework.Common.Extensions;

namespace OrangeHRMTestFramework.Common.WebElements
{
    public class OrangeWebElement : IWebElement
    {
        protected By By { get; set; }

        public OrangeWebElement(By by)
        {
            By = by;
        }

        protected IWebElement WebElement => WebDriverFactory.Driver.GetWebElementWhenExists(By);

        public string TagName => WebElement.TagName;

        public string Text => WebElement.Text;

        public bool Enabled => WebElement.Enabled;

        public bool Selected => WebElement.Selected;

        public Point Location => WebElement.Location;

        public Size Size => WebElement.Size;

        public bool Displayed => WebElement.Displayed;

        public void Clear() => WebElement.Clear();

        public void ClickWithScroll()
        {
            ScrollIntoView();
            WebElement.Click();
        }

        public void Click()
        {
            WebElement.Click();
        }

        public void DoubleClick()
        {
            WebDriverFactory.Actions.DoubleClick(WebElement).Build().Perform();
        }

        public void RightClick()
        {
            WebDriverFactory.Actions.ContextClick(WebElement).Build().Perform();
        }

        public IWebElement FindElement(By by) => WebElement.FindElement(by);

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            try
            {
                return WebElement.FindElements(by);
            }
            catch (StaleElementReferenceException)
            {
                return WebElement.FindElements(by);
            }
        }

        public string GetAttribute(string attributeName) => WebElement.GetAttribute(attributeName);

        public string GetCssValue(string propertyName) => WebElement.GetCssValue(propertyName);

        public string GetDomAttribute(string attributeName) => WebElement.GetDomAttribute(attributeName);

        public string GetDomProperty(string propertyName) => WebElement.GetDomProperty(propertyName);

        public ISearchContext GetShadowRoot() => WebElement.GetShadowRoot();

        public string GetElementPlaceholder() => WebElement.GetAttribute("placeholder");

        public void SendKeys(string text) => WebElement.SendKeys(text);

        public void Submit() => WebElement.Submit();

        public void WaitUntilDisplayed() => WebDriverFactory.Driver.GetWebDriverWait(pollingInterval: TimeSpan.FromSeconds(1)).Until(_ => WebElement.Displayed);

        // method to scroll element into view using JavaScript
        public void ScrollIntoView() => WebDriverFactory.JavaScriptExecutor.ExecuteScript("arguments[0].scrollIntoView(false)", WebElement);

        public string GetElementValue() => WebElement.GetAttribute("value");

        // method to get value of class attribute
        public string GetValueOfClassAttribute() => GetAttribute("class");

        public bool IsElementDisabledByAttribute()
        {
            var isDisabled = WebElement.GetAttribute("disabled");
            if (isDisabled != "true")
            {
                return false;
            }

            return true;
        }
    }
}
