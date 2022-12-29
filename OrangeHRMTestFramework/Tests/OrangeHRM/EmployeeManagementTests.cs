using NUnit.Framework;
using OrangeHRMTestFramework.Common.Drivers;
using OrangeHRMTestFramework.Data;
using OrangeHRMTestFramework.Data.Constants;
using OrangeHRMTestFramework.Helpers;
using OrangeHRMTestFramework.HttpClients;
using OrangeHRMTestFramework.Models;
using OrangeHRMTestFramework.PageObjects.OrangeHRM;
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
            GenericPages.EmployeeManagementPage.ClickLeftNavCategory(LeftNavCategories.Pim);
        }

        [SetUp]
        public void SetUp()
        {
            GenericPages.EmployeeManagementPage.ClickTopNavCategoryWithoutSubCategory(PimTopNavCategories.EmployeeList);
        }

        [Test]
        public void AddEmployeeTest()
        {
            var firstName = RandomHelper.GetRandomString(7);
            var lastName = RandomHelper.GetRandomString(7);
            var middleName = RandomHelper.GetRandomString(7);

            // Go to the Add Employee Page using Add Employee button
            GenericPages.EmployeeManagementPage.ClickAddEmployeeButton();

            // Check that Id input is not empty
            var addEmployeeForm = GenericPages.AddEmployeePage.AddEmployeeTab;
            var id = addEmployeeForm.GetValueFromIdTextBox();
            Assert.NotNull(id);

            // Enter data to all Inputs and click Save
            addEmployeeForm.EnterDataToAllInputs(firstName, lastName, middleName);
            addEmployeeForm.ClickSaveButton();

            // Check Success message
            var successMessage = GenericPages.EmployeeManagementPage.GetTextFromSuccessMessage();
            Assert.AreEqual(OrangeMessages.SuccessfullyCreatedToastMessageText, successMessage);

            // Go back to Employees list page
            GenericPages.EmployeeManagementPage.ClickTopNavCategoryWithoutSubCategory(PimTopNavCategories.EmployeeList);

            // Find Employee By Employee Name using filters
            GenericPages.EmployeeManagementPage.EmployeeListFilter.EnterAndSelectValueInEmployeeNameFilterInput(firstName);
            GenericPages.EmployeeManagementPage.EmployeeListFilter.ClickSearchButton();

            // Check counts and value of some fields in the Data Grid
            var listOfUserFirstMiddleNames = GenericPages.EmployeeManagementPage.BasicDataGrid.GetCellValuesByColumnName(PimDataGridFieldNames.FirstAndMiddleName);
            Assert.AreEqual(1, listOfUserFirstMiddleNames.Count);
            Assert.AreEqual($"{firstName} {middleName}", listOfUserFirstMiddleNames[0]);
            var lastNameFromGrid = GenericPages.EmployeeManagementPage.BasicDataGrid.GetValueByColumnNameAndRowNumber(PimDataGridFieldNames.LastName, 1);
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
            GenericPages.EmployeeManagementPage.ClickTopNavCategoryWithoutSubCategory(PimTopNavCategories.EmployeeList);

            // Find Employee By Employee Name using filters
            GenericPages.EmployeeManagementPage.EmployeeListFilter.EnterAndSelectValueInEmployeeNameFilterInput(firstName);
            GenericPages.EmployeeManagementPage.EmployeeListFilter.ClickSearchButton();

            // Check counts before deletion
            var listOfUserFirstMiddleNames = GenericPages.EmployeeManagementPage.BasicDataGrid.GetCellValuesByColumnName(PimDataGridFieldNames.FirstAndMiddleName);
            Assert.AreEqual(1, listOfUserFirstMiddleNames.Count);

            // Click Delete button and Delete employee entry
            GenericPages.EmployeeManagementPage.BasicDataGrid.ClickDeleteButtonByRowNumber(1);
            GenericPopups.DeleteEntryPopup.ClickYesDeleteEntryButton();

            // Check success deletion message
            var successDeletionMessage = GenericPages.EmployeeManagementPage.GetTextFromSuccessMessage();
            Assert.AreEqual(OrangeMessages.SuccessfullyDeletedToastMessageText, successDeletionMessage);

            // Check counts after the deletion
            var listOfUserFirstMiddleNamesAfterDeletion = GenericPages.EmployeeManagementPage.BasicDataGrid.GetCellValuesByColumnName(PimDataGridFieldNames.FirstAndMiddleName);
            Assert.AreEqual(0, listOfUserFirstMiddleNamesAfterDeletion.Count);
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
            GenericPages.EmployeeManagementPage.ClickTopNavCategoryWithoutSubCategory(PimTopNavCategories.EmployeeList);

            // Find Employee By Employee Name using filters
            GenericPages.EmployeeManagementPage.EmployeeListFilter.EnterAndSelectValueInEmployeeNameFilterInput(firstName);
            GenericPages.EmployeeManagementPage.EmployeeListFilter.ClickSearchButton();

            // Check counts before deletion
            var listOfUserFirstMiddleNames = GenericPages.EmployeeManagementPage.BasicDataGrid.GetCellValuesByColumnName(PimDataGridFieldNames.FirstAndMiddleName);
            Assert.AreEqual(1, listOfUserFirstMiddleNames.Count);

            // Click Delete button and click Cancel
            GenericPages.EmployeeManagementPage.BasicDataGrid.ClickDeleteButtonByRowNumber(1);
            GenericPopups.DeleteEntryPopup.ClickNoDeleteEntryButton();

            // Check counts after the deletion
            var listOfUserFirstMiddleNamesAfterDeletion = GenericPages.EmployeeManagementPage.BasicDataGrid.GetCellValuesByColumnName(PimDataGridFieldNames.FirstAndMiddleName);
            Assert.AreEqual(1, listOfUserFirstMiddleNamesAfterDeletion.Count);
        }

        [Test]
        public void EditEmployeeTest()
        {
            var firstName = RandomHelper.GetRandomString(7);
            var lastName = RandomHelper.GetRandomString(7);
            var middleName = RandomHelper.GetRandomString(7);
            var stringForChange = RandomHelper.GetRandomString(3);

            // Add test Employee for deletion
            AddTestEmployee(firstName, lastName, middleName);

            // Go back to Employees list page
            GenericPages.EmployeeManagementPage.ClickTopNavCategoryWithoutSubCategory(PimTopNavCategories.EmployeeList);

            // Find Employee By Employee Name using filters
            GenericPages.EmployeeManagementPage.EmployeeListFilter.EnterAndSelectValueInEmployeeNameFilterInput(firstName);
            GenericPages.EmployeeManagementPage.EmployeeListFilter.ClickSearchButton();

            // Check counts before editing just in case
            var listOfNames = GenericPages.EmployeeManagementPage.BasicDataGrid.GetCellValuesByColumnName(PimDataGridFieldNames.FirstAndMiddleName);
            Assert.AreEqual(1, listOfNames.Count);

            // Click Edit button and check that user is directed to Edit Employee Page
            GenericPages.EmployeeManagementPage.BasicDataGrid.ClickEditButtonByRowNumber(1);

            // Make changes to name fields and save them
            GenericPages.EmployeePersonalDetailsPage.EmployeePersonalDetailsTab.EditPreviousValuesInAllNameInputs(stringForChange);
            GenericPages.EmployeePersonalDetailsPage.EmployeePersonalDetailsTab.ClickSaveButton();

            // Check successful update message
            var successUpdateMessage = GenericPages.EmployeeManagementPage.GetTextFromSuccessMessage();
            Assert.AreEqual(OrangeMessages.SuccessfullyUpdatedToastMessageText, successUpdateMessage);

            // Go to the Employee List page and try to filter entry using new value
            GenericPages.EmployeeManagementPage.ClickTopNavCategoryWithoutSubCategory(PimTopNavCategories.EmployeeList);
            GenericPages.EmployeeManagementPage.EmployeeListFilter.EnterAndSelectValueInEmployeeNameFilterInput($"{firstName}{stringForChange}");
            GenericPages.EmployeeManagementPage.EmployeeListFilter.ClickSearchButton();

            // Check counts and field values
            var listOfUserFirstMiddleNamesAfterUpdate = GenericPages.EmployeeManagementPage.BasicDataGrid.GetCellValuesByColumnName(PimDataGridFieldNames.FirstAndMiddleName);
            Assert.AreEqual(1, listOfUserFirstMiddleNamesAfterUpdate.Count);
            Assert.AreEqual($"{firstName}{stringForChange} {middleName}{stringForChange}", listOfUserFirstMiddleNamesAfterUpdate[0]);
            var lastNameValue = GenericPages.EmployeeManagementPage.BasicDataGrid.GetValueByColumnNameAndRowNumber(PimDataGridFieldNames.LastName, 1);
            Assert.AreEqual($"{lastName}{stringForChange}", lastNameValue);
        }

        [Test]
        public void EditEmployeeImageTest()
        {
            const string fileName = "catmem.jpg";
            var firstName = RandomHelper.GetRandomString(7);
            var lastName = RandomHelper.GetRandomString(7);
            var middleName = RandomHelper.GetRandomString(7);
            var fileFullPath = FilesHelper.GetFilesForUploadDir() + fileName;

            // Add test Employee for deletion
            AddTestEmployee(firstName, lastName, middleName);

            // Go back to Employees list page
            GenericPages.EmployeeManagementPage.ClickTopNavCategoryWithoutSubCategory(PimTopNavCategories.EmployeeList);

            // Find Employee By Employee Name using filters
            GenericPages.EmployeeManagementPage.EmployeeListFilter.EnterAndSelectValueInEmployeeNameFilterInput(firstName);
            GenericPages.EmployeeManagementPage.EmployeeListFilter.ClickSearchButton();

            // Check counts before editing just in case
            var listOfNames = GenericPages.EmployeeManagementPage.BasicDataGrid.GetCellValuesByColumnName(PimDataGridFieldNames.FirstAndMiddleName);
            Assert.AreEqual(1, listOfNames.Count);

            // Click Edit button and check that user is directed to Edit Employee Page
            GenericPages.EmployeeManagementPage.BasicDataGrid.ClickEditButtonByRowNumber(1);

            // Click on the Picture to open Edit Picture page
            var employeeImageChangePage = GenericPages.ChangeEmployeeProfilePicturePage;
            employeeImageChangePage.ClickOnChangeEmployeeImageElement();

            // Change Picture
            employeeImageChangePage.ChangeProfileImage(fileFullPath);
            employeeImageChangePage.ClickSaveButton();

            // Check successful update message
            var successUpdateMessage = GenericPages.EmployeeManagementPage.GetTextFromSuccessMessage();
            Assert.AreEqual(OrangeMessages.SuccessfullyUpdatedToastMessageText, successUpdateMessage);

            // Get src attribute of new image and send request to it to ensure the response returns with success code.
            // P.S.: Couldn't check content type or content length here, because I get Empty Content in response :( However, the image is updated.
            WebDriverFactory.Driver.Navigate().Refresh();
            var changedImageSrc = employeeImageChangePage.GetEmployeeImageSrcAttribute();
            var isSuccessCodeReturned = IsRequestToImageSuccessful($"{TestSettings.OrangeBasePageUrl}{changedImageSrc}");
            Assert.IsTrue(isSuccessCodeReturned);
        }

        private void AddTestEmployee(string firstName, string lastName, string middleName)
        {
            var addEmployeeForm = GenericPages.AddEmployeePage.AddEmployeeTab;
            GenericPages.EmployeeManagementPage.ClickAddEmployeeButton();
            addEmployeeForm.EnterDataToAllInputs(firstName, lastName, middleName);
            addEmployeeForm.ClickSaveButton();
            GenericPages.EmployeeManagementPage.WaitUntilSuccessMessageDisplayed();
        }

        private bool IsRequestToImageSuccessful(string srcAttribute)
        {
            var response = BasicHttpClient.PerformGetRequest(srcAttribute, null);
            var isSuccessStatusCodeReturned = response.IsSuccessStatusCode;

            return isSuccessStatusCodeReturned;
        }
    }
}
