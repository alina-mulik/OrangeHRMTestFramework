using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.Drivers;
using OrangeHRMTestFramework.Common.WebElements;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM.DataGrids
{
    public class BasicDataGrid
    {
        private OrangeWebElement _tableElement = new(By.XPath("//div[@role='table']"));
        private string _paginationLocator = "//i[@class='oxd-icon bi-chevron-right']//ancestor::button[@class='oxd-pagination-page-item" +
            " oxd-pagination-page-item--previous-next']";

        public List<string> GetCellValuesByColumnName(string columnName)
        {
            WaitUntillDataGridIsDisplayed();
            var listOfColumnValues = new List<string>();
            var columnId = GetColumnId(columnName);
            var cellsByColumnId = _tableElement.FindElements(By.XPath($"//div[@role='cell'][{columnId}]"));
            _tableElement.ScrollIntoView();

            foreach (var cell in cellsByColumnId)
            {
                listOfColumnValues.Add(cell.Text);
            }

            return listOfColumnValues;
        }

        public List<string> GetCellValuesFromAllPages(string columnName)
        {
            var isPaginationDisplayed = CheckIfPaginationButtonDisplayed();
            var listOfColumnValuesFromAllPages = GetCellValuesByColumnName(columnName);
            while (isPaginationDisplayed)
            {
                ClickPaginationButton();
                var listOfValuesOnPage = GetCellValuesByColumnName(columnName);
                listOfColumnValuesFromAllPages.AddRange(listOfValuesOnPage);
                isPaginationDisplayed = CheckIfPaginationButtonDisplayed();
            }
            WebDriverFactory.Driver.Navigate().Refresh();

            return listOfColumnValuesFromAllPages;
        }


        public void ClickPaginationButton()
        {
            if (CheckIfPaginationButtonDisplayed())
            {
                var paginationButton = new OrangeWebElement(By.XPath(_paginationLocator));
                paginationButton.ClickWithScroll();
            }
        }

        public string GetValueByColumnNameAndRowNumber(string columnName, int rowNumber)
        {
            WaitUntillDataGridIsDisplayed();
            var listOfColumnValues = GetCellValuesByColumnName(columnName);
            var resultValue = listOfColumnValues[rowNumber - 1];

            return resultValue;
        }

        public void ClickDeleteButtonByRowNumber(int rowNumber) => ClickActionButtonByRowNumber(1, rowNumber);

        public void ClickEditButtonByRowNumber(int rowNumber) => ClickActionButtonByRowNumber(2, rowNumber);

        public void SortDescByColumnName(string columnName)
        {
            var listOfColumnNamesWithSortOption = new List<string>();
            var columnNamesWithSortingXPath = "//i[contains(concat('', @class, ''), 'oxd-table-header-sort-icon')]//ancestor::div[@role='columnheader']";
            var sortIconXpath = "(//div[@role='columnheader']//i[contains(concat('', @class, ''), 'oxd-table-header-sort-icon')])[{0}]";
            var sortDescDropdownOption = new OrangeWebElement(By.XPath("(//div[contains(concat('', @class, ''), '--active')]//li[@class='oxd-table-header-sort-dropdown-item']/span)[2]"));
            var listOfColumnElementsWithSorting = _tableElement.FindElements(By.XPath(columnNamesWithSortingXPath));

            foreach (var column in listOfColumnElementsWithSorting)
            {
                listOfColumnNamesWithSortOption.Add(column.Text);
            }

            var columnId = listOfColumnNamesWithSortOption.IndexOf(columnName) + 1;
            var sortElement = new OrangeWebElement(By.XPath(string.Format(sortIconXpath, columnId)));
            sortElement.ClickWithScroll();
            sortDescDropdownOption.Click();
            WaitUntillDataGridIsDisplayed();
        }

        private int GetColumnId(string columnName)
        {
            var columnsXpath = "//div[@role='columnheader']";
            var allColumns = _tableElement.FindElements(By.XPath(columnsXpath));
            var selectColumn = allColumns.Select(x => x).FirstOrDefault(x => x.Text == columnName);
            var index = allColumns.IndexOf(selectColumn);

            return index + 1;
        }

        private void WaitUntillDataGridIsDisplayed()
        {
            var rowElement = new OrangeWebElement(By.XPath("//div[@role='row']"));
            rowElement.WaitUntilDisplayed();
        }

        private void ClickActionButtonByRowNumber(int action, int rowInex)
        {
            var commonRowButtonLocator = new OrangeWebElement(By.XPath($"(//div[@role='row']//div[@role='cell'][last()]//button[{action}])[{rowInex}]"));
            commonRowButtonLocator.WaitUntilDisplayed();
            commonRowButtonLocator.ClickWithScroll();
        }

        private bool CheckIfPaginationButtonDisplayed()
        {

            var listOfPaginations = _tableElement.FindElements(By.XPath(_paginationLocator));
            if (listOfPaginations.Count != 0)
            {
                return true;
            }

            return false;
        }
    }
}
