using OrangeHRMTestFramework.Data;

namespace OrangeHRMTestFramework.Models
{
    public class UserInfo
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public static UserInfo AdminUserInfo => new UserInfo(TestSettings.OrangeAdminUserName, TestSettings.OrangeAdminPassword);

        public UserInfo(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
