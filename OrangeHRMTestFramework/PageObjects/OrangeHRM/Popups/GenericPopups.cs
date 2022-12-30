namespace OrangeHRMTestFramework.PageObjects.OrangeHRM.Popups
{
    public static class GenericPopups
    {
        public static DeleteEntryPopup DeleteEntryPopup => GetPopup<DeleteEntryPopup>();

        public static T GetPopup<T>() where T : new() => new T();
    }
}
