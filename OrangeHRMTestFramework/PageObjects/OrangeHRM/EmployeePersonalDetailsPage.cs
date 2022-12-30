using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.Drivers;
using OrangeHRMTestFramework.Common.Extensions;
using OrangeHRMTestFramework.Common.WebElements;
using OrangeHRMTestFramework.PageObjects.OrangeHRM.Tabs;
using SeleniumExtras.WaitHelpers;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM
{
    public class EmployeePersonalDetailsPage : BaseOrangePage
    {
        public EmployeePersonalDetailsTab EmployeePersonalDetailsTab => new EmployeePersonalDetailsTab();
    }
}
