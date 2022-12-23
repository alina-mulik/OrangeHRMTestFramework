using OrangeHRMTestFramework.PageObjects.OrangeHRM.Filters;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM.DataGrids
{
    public static class GenericDataGrids
    {
        public static BasicDataGrid BasicDataGrid => GetDataGrid<BasicDataGrid>();

        public static T GetDataGrid<T>() where T : new() => new T();
    }
}
