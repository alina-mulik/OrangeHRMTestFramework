namespace OrangeHRMTestFramework.PageObjects.OrangeHRM
{
    public static class GenericPages
    {
        public static EmployeeManagementPage EmployeeManagementPage => GetPage<EmployeeManagementPage>();

        public static ChangeEmployeeProfilePicturePage ChangeEmployeeProfilePicturePage => GetPage<ChangeEmployeeProfilePicturePage>();

        public static UserManagementPage UserManagementPage => GetPage<UserManagementPage>();

        public static SendPasswordResetPage SendPasswordResetPage => GetPage<SendPasswordResetPage>();

        public static LoginPage LoginPage => GetPage<LoginPage>();

        public static BaseOrangePage BaseOrangePage => GetPage<BaseOrangePage>();

        public static T GetPage<T>() where T: new() => new T();
    }
}
