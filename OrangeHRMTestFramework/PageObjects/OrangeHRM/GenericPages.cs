namespace OrangeHRMTestFramework.PageObjects.OrangeHRM
{
    public static class GenericPages
    {
        public static AddUserPage AddUserPage => GetPage<AddUserPage>();

        public static AddEmployeePage AddEmployeePage => GetPage<AddEmployeePage>();

        public static EmployeeListPage EmployeeListPage => GetPage<EmployeeListPage>();

        public static EditPersonalDetailsPage EditPersonalDetailsPage => GetPage<EditPersonalDetailsPage>();

        public static UserManagementPage UserManagementPage => GetPage<UserManagementPage>();

        public static SendPasswordResetPage SendPasswordResetPage => GetPage<SendPasswordResetPage>();

        public static ResetPasswordPage ResetPasswordPage => GetPage<ResetPasswordPage>();

        public static LoginPage LoginPage => GetPage<LoginPage>();

        public static BaseOrangePage BaseOrangePage => GetPage<BaseOrangePage>();

        public static T GetPage<T>() where T: new() => new T();
    }
}
