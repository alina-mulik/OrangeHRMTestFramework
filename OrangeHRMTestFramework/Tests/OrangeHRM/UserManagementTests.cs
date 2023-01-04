using Allure.Net.Commons;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using OrangeHRMTestFramework.Data.Constants;
using OrangeHRMTestFramework.Data.Enums;
using OrangeHRMTestFramework.Helpers;
using OrangeHRMTestFramework.Models;
using OrangeHRMTestFramework.PageObjects.OrangeHRM;
using OrangeHRMTestFramework.PageObjects.OrangeHRM.Popups;
using Status = OrangeHRMTestFramework.Data.Enums.Status;

namespace OrangeHRMTestFramework.Tests.OrangeHRM
{
    [TestFixture]
    [AllureNUnit]
    public class UserManagementTests : BaseTest
    {
        private const string EssentialPasswordPart = "abc1!";
        private string _employeeName;

        public UserManagementTests() : base(UserInfo.AdminUserInfo)
        {

        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _employeeName = AddTestEmployeeAndGetName();
            GenericPages.UserManagementPage.ClickLeftNavCategory(LeftNavCategories.Admin);
        }

        [SetUp]
        public void SetUp()
        {
            GenericPages.UserManagementPage.ClickTopNavCategoryWithSubCategory(AdminTopNavCategories.UserManagement, AdminUserManagementSubCategories.Users);
        }

        [Test]
        [AllureTag("Regression")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureSuite("User Managemenet")]
        [AllureSubSuite("Add User Form")]
        [AllureDescription("Testing adding a new user.")]
        public void AddUserTest()
        {
            var userName = RandomHelper.GetRandomString(7);
            var userPassword = $"{RandomHelper.GetRandomString(7)}{EssentialPasswordPart}";
            string statusEnabled = Status.Enabled.ToString();
            string userRoleAdmin = UserRole.Admin.ToString();

            // Open Add User Page by clicking Add User Button
            var addUserPage = GenericPages.UserManagementPage.ClickAddUserButton();

            // Enter data to all inputs
            addUserPage.AddUserTab.EnterAndSelectValueInEmployeeNameFilterInput(_employeeName);
            addUserPage.AddUserTab.EnterDataToUsernameInput(userName);
            addUserPage.AddUserTab.EnterValueToPasswordAndConfirmPasswordInputs(userPassword);
            addUserPage.AddUserTab.SelectValueInStatusDropdown(Status.Enabled);
            addUserPage.AddUserTab.SelectValueInUserRoleDropdown(UserRole.Admin);
            addUserPage.AddUserTab.ClickSaveButtonWithWait();

            // Check success toast message
            var successMessage = addUserPage.GetTextFromSuccessMessage();
            Assert.AreEqual(OrangeMessages.SuccessfullyCreatedToastMessageText, successMessage);

            // Go to User Management Page
            GenericPages.UserManagementPage.ClickTopNavCategoryWithSubCategory(AdminTopNavCategories.UserManagement, AdminUserManagementSubCategories.Users);

            // Filter newly-created user by Username
            GenericPages.UserManagementPage.UserManagementFilter.EnterValueToUserNameFilterTextBox(userName);
            GenericPages.UserManagementPage.UserManagementFilter.ClickSearchButton();

            // Verify entries in the grid after filtering, check counts and field values
            var listOfUserNames = GenericPages.UserManagementPage.BasicDataGrid.GetCellValuesByColumnName(UserManagementFieldNames.Username);
            Assert.AreEqual(1, listOfUserNames.Count);
            Assert.AreEqual(userName, listOfUserNames[0]);
            var employeeNameFromGrid = GenericPages.UserManagementPage.BasicDataGrid.GetValueByColumnNameAndRowNumber(UserManagementFieldNames.EmployeeName, 1);
            Assert.AreEqual(_employeeName, employeeNameFromGrid);
            var statusFromGrid = GenericPages.UserManagementPage.BasicDataGrid.GetValueByColumnNameAndRowNumber(UserManagementFieldNames.Status, 1);
            Assert.AreEqual(statusEnabled, statusFromGrid);
            var userRoleFromGrid = GenericPages.UserManagementPage.BasicDataGrid.GetValueByColumnNameAndRowNumber(UserManagementFieldNames.UserRole, 1);
            Assert.AreEqual(userRoleAdmin, userRoleFromGrid);
        }

        [Test]
        [AllureTag("Regression")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureSuite("User Managemenet")]
        [AllureSubSuite("User Managemenet Grid")]
        [AllureDescription("Testing deleting a user.")]
        public void DeleteUserTest()
        {
            var userName = RandomHelper.GetRandomString(7);
            var userPassword = $"{RandomHelper.GetRandomString(7)}{EssentialPasswordPart}";

            // Add test user
            AddTestUser(userName, userPassword);

            // Get back to User Management Page
            GenericPages.UserManagementPage.ClickTopNavCategoryWithSubCategory(AdminTopNavCategories.UserManagement, AdminUserManagementSubCategories.Users);

            // Apply filters using all existing inputs and dropdowns on User Management Page
            GenericPages.UserManagementPage.UserManagementFilter.EnterValuesToAllFiltersAndApply(userName, _employeeName, Status.Enabled, UserRole.Admin);

            // Click Delete button and Delete employee entry
            GenericPages.UserManagementPage.BasicDataGrid.ClickDeleteButtonByRowNumber(1);
            GenericPopups.DeleteEntryPopup.ClickYesDeleteEntryButton();

            // Check success deletion message
            var successDeletionMessage = GenericPages.EmployeeManagementPage.GetTextFromSuccessMessage();
            Assert.AreEqual(OrangeMessages.SuccessfullyDeletedToastMessageText, successDeletionMessage);

            // Check counts after the deletion
            var listOfUserFirstMiddleNamesAfterDeletion = GenericPages.UserManagementPage.BasicDataGrid.GetCellValuesByColumnName(PimDataGridFieldNames.FirstAndMiddleName);
            Assert.AreEqual(0, listOfUserFirstMiddleNamesAfterDeletion.Count);
        }

        [Test]
        [AllureTag("Regression")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureSuite("User Managemenet")]
        [AllureSubSuite("User Managemenet Grid")]
        [AllureDescription("Check User Management filtering using all filters.")]
        public void CheckUserManagementFilteringUsingAllFiltersTest()
        {
            var userName = RandomHelper.GetRandomString(7);
            var userPassword = $"{RandomHelper.GetRandomString(7)}{EssentialPasswordPart}";

            // Add test user
            AddTestUser(userName, userPassword);

            // Get back to User Management Page
            GenericPages.UserManagementPage.ClickTopNavCategoryWithSubCategory(AdminTopNavCategories.UserManagement, AdminUserManagementSubCategories.Users);

            // Apply filters using all existing inputs and dropdowns on User Management Page
            GenericPages.UserManagementPage.UserManagementFilter.EnterValuesToAllFiltersAndApply(userName, _employeeName, Status.Enabled, UserRole.Admin);

            // Check counts and value of some fields in the Data Grid
            var listOfUserNames = GenericPages.UserManagementPage.BasicDataGrid.GetCellValuesByColumnName(UserManagementFieldNames.Username);
            Assert.AreEqual(1, listOfUserNames.Count);
            Assert.AreEqual(userName, listOfUserNames[0]);
            var employeeNameFromGrid = GenericPages.UserManagementPage.BasicDataGrid.GetValueByColumnNameAndRowNumber(UserManagementFieldNames.EmployeeName, 1);
            Assert.AreEqual(_employeeName, employeeNameFromGrid);
        }

        [Test]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureSuite("User Managemenet")]
        [AllureSubSuite("Add User Form")]
        [AllureDescription("Check that all fields are required in Add User form.")]
        public void CheckAllFieldsRequiredWhileAddingUserTest()
        {
            // Click Add user button to open Add User Page
            var addUserPage = GenericPages.UserManagementPage.ClickAddUserButton();

            // Click Save button without entering any data to inputs
            addUserPage.AddUserTab.ClickSaveButtonWithWait();

            // Check that required message is displayed under each input
            var warningMessagesDisplayed = addUserPage.AddUserTab.GetWarningMessagesText();

            // Check Text and Counts of warnings
            Assert.AreEqual(6, warningMessagesDisplayed.Count);
            Assert.That(warningMessagesDisplayed, Is.All.EqualTo(OrangeMessages.RequiredWarningMessage));
        }

        [Test]
        [AllureTag("Regression")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureSuite("User Managemenet")]
        [AllureSubSuite("Add User Form")]
        [AllureDescription("Check that user can't be created with the Username that already exists.")]
        public void UserCantBeCreatedWithUserNameThatAlreadyExistsTest()
        {
            var userName = RandomHelper.GetRandomString(7);
            var userPassword = $"{RandomHelper.GetRandomString(7)}{EssentialPasswordPart}";

            // Add  first test user
            AddTestUser(userName, userPassword);

            // Get back to User Management Page
            GenericPages.UserManagementPage.ClickTopNavCategoryWithSubCategory(AdminTopNavCategories.UserManagement, AdminUserManagementSubCategories.Users);

            // Click Add user button to open Add User Page
            var addUserPage = GenericPages.UserManagementPage.ClickAddUserButton();

            // Enter the same data as for the first user
            addUserPage.AddUserTab.EnterAndSelectValueInEmployeeNameFilterInput(_employeeName);
            addUserPage.AddUserTab.EnterValueToPasswordAndConfirmPasswordInputs(userPassword);
            addUserPage.AddUserTab.EnterDataToUsernameInput(userName);
            addUserPage.AddUserTab.SelectValueInStatusDropdown(Status.Enabled);
            addUserPage.AddUserTab.SelectValueInUserRoleDropdown(UserRole.Admin);
            addUserPage.AddUserTab.ClickSaveButton();

            // Check that warning message is displayed
            var isWarningDisplayed = addUserPage.AddUserTab.IsWarningMessageWithCertainTextIsDisplayed(OrangeMessages.AlreadyExistsWarningMessage);
            Assert.IsTrue(isWarningDisplayed);
        }

        [Test]
        [AllureSeverity(SeverityLevel.minor)]
        [AllureSuite("User Managemenet")]
        [AllureSubSuite("User Managemenet Grid")]
        [AllureDescription("Check sorting bu Username desc on User Management page.")]
        public void SortUserListByUserNameDescTest()
        {
            // Get cell values from the DataGrid before sorting
            var userNamesListBeforeSorting = GenericPages.UserManagementPage.BasicDataGrid.GetCellValuesFromAllPages(UserManagementFieldNames.Username);

            // Get expected result by  sorting list of values desc
            var expectedResultList = userNamesListBeforeSorting.OrderByDescending(x => x);

            // Sort Usernames on the Data Grid by Username desc
            GenericPages.UserManagementPage.BasicDataGrid.SortDescByColumnName(UserManagementFieldNames.Username);

            // Get cell values from the DataGrid after sorting
            var userNamesListAfterSorting = GenericPages.UserManagementPage.BasicDataGrid.GetCellValuesFromAllPages(UserManagementFieldNames.Username);

            // Check that values in the Grid are really sorted by desc
            Assert.AreEqual(expectedResultList, userNamesListAfterSorting);
        }

        private string AddTestEmployeeAndGetName()
        {
            var firstName = RandomHelper.GetRandomString(7);
            var lastName = RandomHelper.GetRandomString(7);
            var middleName = RandomHelper.GetRandomString(7);
            GenericPages.EmployeeManagementPage.ClickLeftNavCategory(LeftNavCategories.Pim);
            GenericPages.EmployeeManagementPage.ClickTopNavCategoryWithoutSubCategory(PimTopNavCategories.EmployeeList);
            var addEmployeePage = GenericPages.EmployeeManagementPage.ClickAddEmployeeButton();
            addEmployeePage.AddEmployeeTab.EnterDataToAllInputsAndClickSave(firstName, lastName, middleName);
            addEmployeePage.WaitUntilSuccessMessageDisplayed();

            return $"{firstName} {lastName}";
        }

        [AllureStep("Add new User step")]
        private void AddTestUser(string userName, string userPassword)
        {
            var addUserPage = GenericPages.UserManagementPage.ClickAddUserButton();
            addUserPage.AddUserTab.EnterAndSelectValueInEmployeeNameFilterInput(_employeeName);
            addUserPage.AddUserTab.EnterValueToPasswordAndConfirmPasswordInputs(userPassword);
            addUserPage.AddUserTab.EnterDataToUsernameInput(userName);
            addUserPage.AddUserTab.SelectValueInStatusDropdown(Status.Enabled);
            addUserPage.AddUserTab.SelectValueInUserRoleDropdown(UserRole.Admin);
            addUserPage.AddUserTab.ClickSaveButtonWithWait();
            addUserPage.WaitUntilSuccessMessageDisplayed();
        }
    }
}
