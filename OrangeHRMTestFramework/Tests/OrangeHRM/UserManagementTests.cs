using NUnit.Framework;
using OrangeHRMTestFramework.Data.Constants;
using OrangeHRMTestFramework.Data.Enums;
using OrangeHRMTestFramework.Helpers;
using OrangeHRMTestFramework.Models;
using OrangeHRMTestFramework.PageObjects.OrangeHRM;
using OrangeHRMTestFramework.PageObjects.OrangeHRM.DataGrids;
using OrangeHRMTestFramework.PageObjects.OrangeHRM.Filters;
using OrangeHRMTestFramework.PageObjects.OrangeHRM.Forms;
using OrangeHRMTestFramework.PageObjects.OrangeHRM.Popups;

namespace OrangeHRMTestFramework.Tests.OrangeHRM
{
    public class UserManagementTests : BaseTest
    {
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
        public void AddUserTest()
        {
            var userName = RandomHelper.GetRandomString(7);
            var userPassword = $"{RandomHelper.GetRandomString(7)}abc1!";
            string statusEnabled = Enum.GetName(typeof(Status), Status.Enabled);
            string userRoleAdmin = Enum.GetName(typeof(UserRole), UserRole.Admin);

            // Open Add User Page by clicking Add User Button
            GenericPages.UserManagementPage.ClickAddUserButton();

            // Enter data to all inputs
            var addUserForm = GenericForms.AddUserForm;
            addUserForm.EnterAndSelectValueInEmployeeNameFilterInput(_employeeName);
            addUserForm.EnterDataToUsernameInput(userName);
            addUserForm.EnterValueToPasswordAndConfirmPasswordInputs(userPassword);
            addUserForm.SelectValueInStatusDropdown(Status.Enabled);
            addUserForm.SelectValueInUserRoleDropdown(UserRole.Admin);
            addUserForm.ClickSaveButtonWithWait();

            // Check success toast message
            var successMessage = addUserForm.GetTextFromSuccessMessage();
            Assert.AreEqual(OrangeMessages.SuccessfullyCreatedToastMessageText, successMessage);

            // Go to User Management Page
            GenericPages.UserManagementPage.ClickTopNavCategoryWithSubCategory(AdminTopNavCategories.UserManagement, AdminUserManagementSubCategories.Users);

            // Filter newly-created user by Username
            GenericFilters.UserManagementFilter.EnterValueToUserNameFilterInput(userName);
            GenericFilters.UserManagementFilter.ClickSearchButton();

            // Verify entries in the grid after filtering, check counts and field values
            var listOfUserNames = GenericDataGrids.BasicDataGrid.GetCellValuesByColumnName(UserManagementFieldNames.Username);
            Assert.AreEqual(1, listOfUserNames.Count);
            Assert.AreEqual(userName, listOfUserNames[0]);
            var employeeNameFromGrid = GenericDataGrids.BasicDataGrid.GetValueByColumnNameAndRowIndex(UserManagementFieldNames.EmployeeName, 1);
            Assert.AreEqual(_employeeName, employeeNameFromGrid);
            var statusFromGrid = GenericDataGrids.BasicDataGrid.GetValueByColumnNameAndRowIndex(UserManagementFieldNames.Status, 1);
            Assert.AreEqual(statusEnabled, statusFromGrid);
            var userRoleFromGrid = GenericDataGrids.BasicDataGrid.GetValueByColumnNameAndRowIndex(UserManagementFieldNames.UserRole, 1);
            Assert.AreEqual(userRoleAdmin, userRoleFromGrid);
        }

        [Test]
        public void DeleteUserTest()
        {
            var userName = RandomHelper.GetRandomString(7);
            var userPassword = $"{RandomHelper.GetRandomString(7)}abc1!";

            // Add test user
            AddTestUser(userName, userPassword, UserRole.Admin, Status.Enabled);

            // Get back to User Management Page
            GenericPages.UserManagementPage.ClickTopNavCategoryWithSubCategory(AdminTopNavCategories.UserManagement, AdminUserManagementSubCategories.Users);

            // Apply filters using all existing inputs and dropdowns on User Management Page
            GenericFilters.UserManagementFilter.EnterValuesToAllFiltersAndApply(userName, _employeeName, Status.Enabled, UserRole.Admin);

            // Click Delete button and Delete employee entry
            GenericDataGrids.BasicDataGrid.ClickDeleteButtonByRowNumber(1);
            GenericPopups.DeleteEntryPopup.ClickYesDeleteEntryButton();

            // Check success deletion message
            var successDeletionMessage = GenericPages.EmployeeManagementPage.GetTextFromSuccessMessage();
            Assert.AreEqual(OrangeMessages.SuccessfullyDeletedToastMessageText, successDeletionMessage);

            // Check counts after the deletion
            var listOfUserFirstMiddleNamesAfterDeletion = GenericDataGrids.BasicDataGrid.GetCellValuesByColumnName(PimDataGridFieldNames.FirstAndMiddleName);
            Assert.AreEqual(0, listOfUserFirstMiddleNamesAfterDeletion.Count);
        }

        [Test]
        public void CheckUserManagementFilteringUsingAllFiltersTest()
        {
            var userName = RandomHelper.GetRandomString(7);
            var userPassword = $"{RandomHelper.GetRandomString(7)}abc1!";

            // Add test user
            AddTestUser(userName, userPassword, UserRole.Admin, Status.Enabled);

            // Get back to User Management Page
            GenericPages.UserManagementPage.ClickTopNavCategoryWithSubCategory(AdminTopNavCategories.UserManagement, AdminUserManagementSubCategories.Users);

            // Apply filters using all existing inputs and dropdowns on User Management Page
            GenericFilters.UserManagementFilter.EnterValuesToAllFiltersAndApply(userName, _employeeName, Status.Enabled, UserRole.Admin);

            // Check counts and value of some fields in the Data Grid
            var listOfUserNames = GenericDataGrids.BasicDataGrid.GetCellValuesByColumnName(UserManagementFieldNames.Username);
            Assert.AreEqual(1, listOfUserNames.Count);
            Assert.AreEqual(userName, listOfUserNames[0]);
            var employeeNameFromGrid = GenericDataGrids.BasicDataGrid.GetValueByColumnNameAndRowIndex(UserManagementFieldNames.EmployeeName, 1);
            Assert.AreEqual(_employeeName, employeeNameFromGrid);
        }

        [Test]
        public void CheckAllFieldsRequiredWhileAddingUserTest()
        {
            // Click Add user button to open Add User Page
            GenericPages.UserManagementPage.ClickAddUserButton();

            // Click Save button without entering any data to inputs
            GenericForms.AddUserForm.ClickSaveButtonWithWait();

            // Check that required message is displayed under each input
            var warningMessagesDisplayed = GenericForms.AddUserForm.GetWarningMessagesText();

            // Check Text and Counts of warnings
            Assert.AreEqual(6, warningMessagesDisplayed.Count);
            Assert.That(warningMessagesDisplayed, Is.All.EqualTo(OrangeMessages.RequiredWarningMessage));
        }

        [Test]
        public void UserCantBeCreatedWithUserNameThatAlreadyExistsTest()
        {
            var userName = RandomHelper.GetRandomString(7);
            var userPassword = $"{RandomHelper.GetRandomString(7)}abc1!";

            // Add  first test user
            AddTestUser(userName, userPassword, UserRole.Admin, Status.Enabled);

            // Get back to User Management Page
            GenericPages.UserManagementPage.ClickTopNavCategoryWithSubCategory(AdminTopNavCategories.UserManagement, AdminUserManagementSubCategories.Users);

            // Click Add user button to open Add User Page
            GenericPages.UserManagementPage.ClickAddUserButton();

            // Enter the same data as for the first user
            var addUserForm = GenericForms.AddUserForm;
            addUserForm.EnterAndSelectValueInEmployeeNameFilterInput(_employeeName);
            addUserForm.EnterValueToPasswordAndConfirmPasswordInputs(userPassword);
            addUserForm.EnterDataToUsernameInput(userName);
            addUserForm.SelectValueInStatusDropdown(Status.Enabled);
            addUserForm.SelectValueInUserRoleDropdown(UserRole.Admin);
            addUserForm.ClickSaveButton();

            // Check that warning message is displayed
            var isWarningDisplayed = addUserForm.IsWarningMessageWithCertainTextIsDisplayed(OrangeMessages.AlreadyExistsWarningMessage);
            Assert.IsTrue(isWarningDisplayed);
        }

        [Test]
        public void SortUserListByUserNameDescTest()
        {
            // Get cell values from the DataGrid before sorting
            var userNamesListBeforeSorting = GenericDataGrids.BasicDataGrid.GetCellValuesByColumnName(UserManagementFieldNames.Username);

            // Get expected result by  sorting list of values desc
            var expectedResultList = userNamesListBeforeSorting.OrderByDescending(x => x);

            // Sort Usernames on the Data Grid by Username desc
            GenericDataGrids.BasicDataGrid.SortDescByColumnName(UserManagementFieldNames.Username);

            // Get cell values from the DataGrid after sorting
            var userNamesListAfterSorting = GenericDataGrids.BasicDataGrid.GetCellValuesByColumnName(UserManagementFieldNames.Username);

            // Check that values in the Grid are really sorted by desc
            Assert.AreEqual(expectedResultList, userNamesListAfterSorting);
        }

        private string AddTestEmployeeAndGetName()
        {
            var firstName = RandomHelper.GetRandomString(7);
            var lastName = RandomHelper.GetRandomString(7);
            var middleName = RandomHelper.GetRandomString(7);
            var employeeManagementPage = GenericPages.EmployeeManagementPage;
            employeeManagementPage.ClickLeftNavCategory(LeftNavCategories.Pim);
            employeeManagementPage.ClickTopNavCategoryWithoutSubCategory(PimTopNavCategories.EmployeeList);
            employeeManagementPage.ClickAddEmployeeButton();
            var addEmployeeForm = GenericForms.AddEmployeeForm;
            addEmployeeForm.EnterDataToAllInputs(firstName, lastName, middleName);
            addEmployeeForm.ClickSaveButton();
            addEmployeeForm.WaitUntilSuccessMessageDisplayed();

            return $"{firstName} {lastName}";
        }

        private void AddTestUser(string userName, string userPassword, UserRole valueRole, Status valueStatus)
        {
            GenericPages.UserManagementPage.ClickAddUserButton();
            var addUserForm = GenericForms.AddUserForm;
            addUserForm.EnterAndSelectValueInEmployeeNameFilterInput(_employeeName);
            addUserForm.EnterValueToPasswordAndConfirmPasswordInputs(userPassword);
            addUserForm.EnterDataToUsernameInput(userName);
            addUserForm.SelectValueInStatusDropdown(valueStatus);
            addUserForm.SelectValueInUserRoleDropdown(valueRole);
            addUserForm.ClickSaveButtonWithWait();
            addUserForm.WaitUntilSuccessMessageDisplayed();
        }
    }
}
