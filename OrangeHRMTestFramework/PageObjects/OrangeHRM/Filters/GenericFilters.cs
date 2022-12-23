using OrangeHRMTestFramework.PageObjects.OrangeHRM.Popups;

namespace OrangeHRMTestFramework.PageObjects.OrangeHRM.Filters
{
    public static class GenericFilters
    {
        public static BaseFilter BaseFilter => GetFilters<BaseFilter>();

        public static EmployeeListFilter EmployeeListFilter => GetFilters<EmployeeListFilter>();

        public static UserManagementFilter UserManagementFilter => GetFilters<UserManagementFilter>();

        public static T GetFilters<T>() where T : new() => new T();
    }
}
