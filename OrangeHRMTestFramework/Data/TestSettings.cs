using Microsoft.Extensions.Configuration;
using OrangeHRMTestFramework.Data.Enums;

namespace OrangeHRMTestFramework.Data
{
    public static class TestSettings
    {
        public static Browsers Browser { get; set; }
        public static string OrangeAdminUserName { get; set; }
        public static string OrangeAdminPassword { get; set; }
        public static string OrangeLoginPageUrl { get; set; }
        public static string OrangeResetPasswordPageUrl { get; set; }
        public static string OrangeSendPasswordResetPageUrl { get; set; }
        public static string OrangeUserManagementPageUrl { get; set; }
        public static string OrangeAddUserPageUrl { get; set; }
        public static string OrangeDashboardUrl { get; set; }
        public static string OrangeAddEmployeePageUrl { get; set; }
        public static string OrangeEmployeeListPageUrl { get; set; }
        public static string OrangeBasePageUrl { get; set; }

        public static IConfiguration TestConfiguration { get; } = new ConfigurationBuilder().AddJsonFile(".\\Tests\\testsettings.json").Build();

        static TestSettings()
        {
            Enum.TryParse(TestConfiguration["Common:Browser"], out Browsers browser);
            Browser = browser;
            OrangeAdminUserName = TestConfiguration["TestData:UserData:Username"];
            OrangeAdminPassword = TestConfiguration["TestData:UserData:Password"];
            OrangeLoginPageUrl = TestConfiguration["Common:OrangeHRMUrls:OrangeLoginPage"];
            OrangeDashboardUrl = TestConfiguration["Common:OrangeHRMUrls:OrangeDashboardPage"];
            OrangeResetPasswordPageUrl = TestConfiguration["Common:OrangeHRMUrls:OrangeResetPasswordPage"];
            OrangeSendPasswordResetPageUrl = TestConfiguration["Common:OrangeHRMUrls:OrangeSendPasswordResetPage"];
            OrangeUserManagementPageUrl = TestConfiguration["Common:OrangeHRMUrls:OrangeUserManagementPage"];
            OrangeAddUserPageUrl = TestConfiguration["Common:OrangeHRMUrls:OrangeAddUserPage"];
            OrangeAddEmployeePageUrl = TestConfiguration["Common:OrangeHRMUrls:OrangeAddEmployeePage"];
            OrangeEmployeeListPageUrl = TestConfiguration["Common:OrangeHRMUrls:OrangeEmployeeListPage"];
            OrangeBasePageUrl = TestConfiguration["Common:OrangeHRMUrls:OrangeBasePage"];
        }
    }
}
