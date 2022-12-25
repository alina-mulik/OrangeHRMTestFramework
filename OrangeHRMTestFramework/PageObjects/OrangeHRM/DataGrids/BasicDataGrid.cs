using OpenQA.Selenium;
using OrangeHRMTestFramework.Common.Drivers;
using OrangeHRMTestFramework.Common.Extensions;
using OrangeHRMTestFramework.Common.WebElements;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM.DataGrids
{
    public class BasicDataGrid
    {
        private OrangeWebElement _tableElement = new(By.XPath("//div[@role='table']"));

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

        public string GetValueByColumnNameAndRowIndex(string columnName, int rowNumber)
        {
            WaitUntillDataGridIsDisplayed();
            var listOfColumnValues = GetCellValuesByColumnName(columnName);
            var resultValue = listOfColumnValues[rowNumber - 1];

            return resultValue;
        }

        public void ClickDeleteButtonByRowNumber(int rowNumber)
        {
            var deleteButtonLocator = "(//div[@role='cell'][last()])[{0}]//button[@type='button'][1]";
            var deleteButtonByRowNumber = new OrangeWebElement(By.XPath(string.Format(deleteButtonLocator, rowNumber)));
            deleteButtonByRowNumber.ClickWithScroll();
        }

        public void ClickEditButtonByRowNumber(int rowNumber)
        {
            var editButtonLocator = "(//div[@role='cell'][last()])[{0}]//button[@type='button'][2]";
            var editButtonByRowNumber = new OrangeWebElement(By.XPath(string.Format(editButtonLocator, rowNumber)));
            editButtonByRowNumber.ClickWithScroll();
        }

        private int GetColumnId(string columnName)
        {
            var columnsXpath = "//div[@role='columnheader']";
            var allcolumns = _tableElement.FindElements(By.XPath(columnsXpath));
            var selectColumn = allcolumns.Select(x => x).FirstOrDefault(x => x.Text == columnName);
            var index = allcolumns.IndexOf(selectColumn);

            return index + 1;
        }

        private void WaitUntillDataGridIsDisplayed()
        {
            var rowElement = new OrangeWebElement(By.XPath("//div[@role='row']"));
            if (!rowElement.Displayed)
            {
                WebDriverFactory.Driver.GetWebDriverWait(pollingInterval: TimeSpan.FromSeconds(1)).Until(_ => rowElement.Displayed);
            }
        }
    }
}
