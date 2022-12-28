namespace OrangeHRMTestFramework.PageObjects.OrangeHRM
{
    public static class GenericPages
    {
        public static EmployeeManagementPage EmployeeManagementPage => GetPage<EmployeeManagementPage>();

        public static EmployeePersonalDetailsPage EmployeePersonalDetailsPage => GetPage<EmployeePersonalDetailsPage>();

        public static ChangeEmployeeProfilePicturePage ChangeEmployeeProfilePicturePage => GetPage<ChangeEmployeeProfilePicturePage>();

        public static UserManagementPage UserManagementPage => GetPage<UserManagementPage>();

        public static SendPasswordResetPage SendPasswordResetPage => GetPage<SendPasswordResetPage>();

        public static LoginPage LoginPage => GetPage<LoginPage>();

        public static ResetPasswordPage ResetPasswordPage => GetPage<ResetPasswordPage>();

        public static AddUserPage AddUserPage => GetPage<AddUserPage>();

        public static AddEmployeePage AddEmployeePage => GetPage<AddEmployeePage>();

        public static BaseOrangePage BaseOrangePage => GetPage<BaseOrangePage>();

        public static T GetPage<T>() where T: new() => new T();
    }
}
