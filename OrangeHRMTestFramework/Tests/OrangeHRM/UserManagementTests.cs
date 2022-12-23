using NUnit.Framework;
using OrangeHRMTestFramework.Common.Drivers;
using OrangeHRMTestFramework.Data.Constants;
using OrangeHRMTestFramework.Helpers;
using OrangeHRMTestFramework.Models;
using OrangeHRMTestFramework.PageObjects.OrangeHRM;
using OrangeHRMTestFramework.PageObjects.OrangeHRM.DataGrids;
using OrangeHRMTestFramework.PageObjects.OrangeHRM.Filters;

namespace OrangeHRMTestFramework.Tests.OrangeHRM
{
    public class UserManagementTests : BaseTest
    {
        private string _employeeName;
        private readonly string _userName = RandomHelper.GetRandomString(7);
        private readonly string _userPassword = RandomHelper.GetRandomString(7);


        public UserManagementTests() : base(UserInfo.AdminUserInfo)
        {

        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            GenericPages.UserManagementPage.ClickLeftNavCategory(LeftNavCategories.Admin);
            GenericPages.UserManagementPage.ClickTopNavCategoryWithSubCategory(AdminTopNavCategories.UserManagement, AdminUserManagementSubCategories.Users);
        }

        [SetUp]
        public void SetUp()
        {
            WebDriverFactory.Driver.Navigate().Refresh();
        }

        [Test]
        public void CheckUserManagementFilteringTest()
        {
            // Apply filters using all existing inputs and dropdowns on User Management Page
            GenericFilters.UserManagementFilter.EnterValuesToAllFiltersAndApply("Admin", "Paul Collings", "Enabled", "Admin");

            // Check counts and value of some fields in the Data Grid
            var listOfUserNames = GenericDataGrids.BasicDataGrid.GetCellValuesByColumnName(UserManagementFieldNames.Username);
            Assert.AreEqual(1, listOfUserNames.Count);

        }

        [Test]
        public void AddUserTest()
        {
            GenericPages.UserManagementPage.ClickAddUserButton();
        }

        [Test]
        public void DeleteUserTest()
        {

        }

        [TearDown]
        public void TearDown()
        {

        }
    }
}
