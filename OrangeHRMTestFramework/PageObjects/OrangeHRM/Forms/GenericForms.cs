namespace OrangeHRMTestFramework.PageObjects.OrangeHRM.Forms
{
    public class GenericForms
    {
        public static EmployeePersonalDetailsForm EmployeePersonalDetailsForm => GetForm<EmployeePersonalDetailsForm>();

        public static ResetPasswordForm ResetPasswordForm => GetForm<ResetPasswordForm>();

        public static AddEmployeeForm AddEmployeeForm => GetForm<AddEmployeeForm>();

        public static AddUserForm AddUserForm => GetForm<AddUserForm>();

        public static LoginForm LoginForm => GetForm<LoginForm>();

        public static BaseForm BaseForm => GetForm<BaseForm>();

        public static T GetForm<T>() where T : new() => new T();
    }
}
