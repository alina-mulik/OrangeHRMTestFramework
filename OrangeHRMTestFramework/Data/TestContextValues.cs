using NUnit.Framework;

namespace OrangeHRMTestFramework.Data
{
    public class TestContextValues
    {
        public static string ExecutableClassName => TestContext.CurrentContext.Test.ClassName;
    }
}
