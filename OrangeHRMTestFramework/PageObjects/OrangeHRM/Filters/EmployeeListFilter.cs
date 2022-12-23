using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.WebElements;
using OrangeHRMTestFramework.Data.Constants;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM.Filters
{
    public class EmployeeListFilter : BaseFilter
    {
        public void EnterValueToEmployeeNameFilterInput(string value) => new OrangeWebElement(By.XPath(string.Format(BaseFilterInputLocator, PimFilterFieldNames.EmployeeName))).SendKeys(value);
    }
}
