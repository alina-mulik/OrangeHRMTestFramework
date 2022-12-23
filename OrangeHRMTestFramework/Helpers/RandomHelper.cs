namespace OrangeHRMTestFramework.Helpers
{
    public static class RandomHelper
    {
        public static string GetRandomString(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var newRandomString = Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray();

            return new string(newRandomString);
        }
    }
}
