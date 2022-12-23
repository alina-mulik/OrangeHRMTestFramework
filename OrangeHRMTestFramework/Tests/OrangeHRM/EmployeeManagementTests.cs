using NUnit.Framework;
using OrangeHRMTestFramework.Common.Drivers;
using OrangeHRMTestFramework.Common.Extensions;
using OrangeHRMTestFramework.Data;
using OrangeHRMTestFramework.Data.Constants;
using OrangeHRMTestFramework.Helpers;
using OrangeHRMTestFramework.Models;
using OrangeHRMTestFramework.PageObjects.OrangeHRM;
using OrangeHRMTestFramework.PageObjects.OrangeHRM.DataGrids;
using OrangeHRMTestFramework.PageObjects.OrangeHRM.Filters;
using OrangeHRMTestFramework.PageObjects.OrangeHRM.Popups;

namespace OrangeHRMTestFramework.Tests.OrangeHRM
{
    public class EmployeeManagementTests : BaseTest
    {
        public EmployeeManagementTests() : base(UserInfo.AdminUserInfo)
        {

        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            GenericPages.EmployeeListPage.ClickLeftNavCategory(LeftNavCategories.Pim);
        }

        [SetUp]
        public void SetUp()
        {
            GenericPages.EmployeeListPage.ClickTopNavCategoryWithoutSubCategory(PimTopNavCategories.EmployeeList);
        }

        [Test]
        public void AddEmployeeTest()
        {
            var firstName = RandomHelper.GetRandomString(7);
            var lastName = RandomHelper.GetRandomString(7);
            var middleName = RandomHelper.GetRandomString(7);

            // Go to the Add Employee Page using Add Employee button
            GenericPages.EmployeeListPage.ClickAddEmployeeButton();
            Assert.AreEqual(TestSettings.OrangeAddEmployeePageUrl, WebDriverFactory.Driver.GetCurrentUrl());

            // Enter data to all Inputs and click Save
            var id = GenericPages.AddEmployeePage.GetValueFromIdInput();
            Assert.NotNull(id);
            GenericPages.AddEmployeePage.EnterDataToAllInputs(firstName, lastName, middleName);
            GenericPages.AddEmployeePage.ClickSaveButton();

            // Check Success message
            var successMessage = GenericPages.AddEmployeePage.GetTextFromSuccessMessage();
            Assert.AreEqual(OrangeMessages.SuccessfullyCreatedToastMessageText, successMessage);

            // Go back to Employees list page
            GenericPages.EmployeeListPage.ClickTopNavCategoryWithoutSubCategory(PimTopNavCategories.EmployeeList);
            Assert.AreEqual(TestSettings.OrangeEmployeeListPageUrl, WebDriverFactory.Driver.GetCurrentUrl());

            // Find Employee By Employee Name using filters
            GenericFilters.EmployeeListFilter.EnterValueToEmployeeNameFilterInput(firstName);
            GenericFilters.EmployeeListFilter.ClickSearchButton();

            // Check counts and value of some fields in the Data Grid
            var listOfUserFirstMiddleNames = GenericDataGrids.BasicDataGrid.GetCellValuesByColumnName(PimDataGridFieldNames.FirstAndMiddleName);
            Assert.AreEqual(1, listOfUserFirstMiddleNames.Count);
            Assert.AreEqual($"{firstName} {middleName}", listOfUserFirstMiddleNames[0]);
            var lastNameFromGrid = GenericDataGrids.BasicDataGrid.GetValueByColumnNameAndRowIndex(PimDataGridFieldNames.LastName, 1);
            Assert.AreEqual(lastName, lastNameFromGrid);
        }

        [Test]
        public void DeleteEmployeeTest()
        {
            var firstName = RandomHelper.GetRandomString(7);
            var lastName = RandomHelper.GetRandomString(7);
            var middleName = RandomHelper.GetRandomString(7);

            // Add test Employee for deletion
            AddTestEmployee(firstName, lastName, middleName);

            // Go back to Employees list page
            GenericPages.EmployeeListPage.ClickTopNavCategoryWithoutSubCategory(PimTopNavCategories.EmployeeList);

            // Find Employee By Employee Name using filters
            GenericFilters.EmployeeListFilter.EnterValueToEmployeeNameFilterInput(firstName);
            GenericFilters.EmployeeListFilter.ClickSearchButton();

            // Check counts before deletion
            var listOfUserFirstMiddleNames = GenericDataGrids.BasicDataGrid.GetCellValuesByColumnName(PimDataGridFieldNames.FirstAndMiddleName);
            Assert.AreEqual(1, listOfUserFirstMiddleNames.Count);

            // Click Delete button and Delete employee entry
            GenericDataGrids.BasicDataGrid.ClickDeleteButtonByRowNumber(1);
            GenericPopups.DeleteEntryPopup.ClickYesDeleteEntryButton();

            // Check success deletion message
            var successDeletionMessage = GenericPages.EmployeeListPage.GetTextFromSuccessMessage();
            Assert.AreEqual(OrangeMessages.SuccessfullyDeletedToastMessageText, successDeletionMessage);

            // Check counts after the deletion
            var listOfUserFirstMiddleNamesAfterDeletion = GenericDataGrids.BasicDataGrid.GetCellValuesByColumnName(PimDataGridFieldNames.FirstAndMiddleName);
            Assert.AreEqual(0, listOfUserFirstMiddleNamesAfterDeletion.Count);
        }

        [Test]
        public void EditEmployeeTest()
        {
            var firstName = RandomHelper.GetRandomString(7);
            var lastName = RandomHelper.GetRandomString(7);
            var middleName = RandomHelper.GetRandomString(7);
            var firstNameChanged = RandomHelper.GetRandomString(7);
            var lastNameChanged = RandomHelper.GetRandomString(7);
            var middleNameChanged = RandomHelper.GetRandomString(7);

            // Add test Employee for deletion
            AddTestEmployee(firstName, lastName, middleName);

            // Go back to Employees list page
            GenericPages.EmployeeListPage.ClickTopNavCategoryWithoutSubCategory(PimTopNavCategories.EmployeeList);

            // Find Employee By Employee Name using filters
            GenericFilters.EmployeeListFilter.EnterValueToEmployeeNameFilterInput(firstName);
            GenericFilters.EmployeeListFilter.ClickSearchButton();

            // Check counts before deletion
            var listOfNames = GenericDataGrids.BasicDataGrid.GetCellValuesByColumnName(PimDataGridFieldNames.FirstAndMiddleName);
            Assert.AreEqual(1, listOfNames.Count);

            // Click Edit button and check that user is directed to Edit Employee Page
            GenericDataGrids.BasicDataGrid.ClickEditButtonByRowNumber(1);

            // Make changes to name fields and save them
            GenericPages.EditPersonalDetailsPage.ClearPreviousValuesAndEnterNewDataToAllInputs(firstNameChanged, lastNameChanged, middleNameChanged);
            GenericPages.EditPersonalDetailsPage.ClickSaveButton();

            // Check successful update message
            var successUpdateMessage = GenericPages.EmployeeListPage.GetTextFromSuccessMessage();
            Assert.AreEqual(OrangeMessages.SuccessfullyUpdatedToastMessageText, successUpdateMessage);

            // Go to the Employee List page and try to filter entry using new value
            GenericPages.EmployeeListPage.ClickTopNavCategoryWithoutSubCategory(PimTopNavCategories.EmployeeList);
            GenericFilters.EmployeeListFilter.EnterValueToEmployeeNameFilterInput(firstNameChanged);
            GenericFilters.EmployeeListFilter.ClickSearchButton();

            // Check counts and field values
            var listOfUserFirstMiddleNamesAfterUpdate = GenericDataGrids.BasicDataGrid.GetCellValuesByColumnName(PimDataGridFieldNames.FirstAndMiddleName);
            Assert.AreEqual(1, listOfUserFirstMiddleNamesAfterUpdate.Count);
            Assert.AreEqual($"{firstNameChanged} {middleNameChanged}", listOfUserFirstMiddleNamesAfterUpdate[0]);
        }

        [Test]
        public void CancelEmployeeDeletionTest()
        {
            var firstName = RandomHelper.GetRandomString(7);
            var lastName = RandomHelper.GetRandomString(7);
            var middleName = RandomHelper.GetRandomString(7);

            // Add test Employee for deletion
            AddTestEmployee(firstName, lastName, middleName);

            // Go back to Employees list page
            GenericPages.EmployeeListPage.ClickTopNavCategoryWithoutSubCategory(PimTopNavCategories.EmployeeList);

            // Find Employee By Employee Name using filters
            GenericFilters.EmployeeListFilter.EnterValueToEmployeeNameFilterInput(firstName);
            GenericFilters.EmployeeListFilter.ClickSearchButton();

            // Check counts before deletion
            var listOfUserFirstMiddleNames = GenericDataGrids.BasicDataGrid.GetCellValuesByColumnName(PimDataGridFieldNames.FirstAndMiddleName);
            Assert.AreEqual(1, listOfUserFirstMiddleNames.Count);

            // Click Delete button and click Cancel
            GenericDataGrids.BasicDataGrid.ClickDeleteButtonByRowNumber(1);
            GenericPopups.DeleteEntryPopup.ClickNoDeleteEntryButton();

            // Check counts after the deletion
            var listOfUserFirstMiddleNamesAfterDeletion = GenericDataGrids.BasicDataGrid.GetCellValuesByColumnName(PimDataGridFieldNames.FirstAndMiddleName);
            Assert.AreEqual(1, listOfUserFirstMiddleNamesAfterDeletion.Count);
        }

        private void AddTestEmployee(string firstName, string lastName, string middleName)
        {
            GenericPages.EmployeeListPage.ClickAddEmployeeButton();
            GenericPages.AddEmployeePage.EnterDataToAllInputs(firstName, lastName, middleName);
            GenericPages.AddEmployeePage.ClickSaveButton();
        }
    }
}
