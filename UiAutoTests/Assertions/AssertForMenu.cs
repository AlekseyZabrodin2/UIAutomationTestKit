using FlaUI.Core.AutomationElements;
using NLog;
using UiAutoTests.Extensions;
using UiAutoTests.Helpers;

namespace UiAutoTests.Assertions
{
    public static class AssertForMenu
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private static readonly LoggerHelper _loggerHelper = new();


        /// <summary>
        /// Проверяет, сколько в Меню вкладок первого уровня
        /// </summary>
        public static void MainMenuItemsCount(this Menu menu, int expectedCount, string message = null)
        {
            _loggerHelper.LogEnteringTheMethod();
            
            var mainMenu = menu.EnsureMenu();
            var menuItems = mainMenu.FindAllChildren();

            var actualCount = menuItems.Count(item => item.ClassName.Contains("MenuItem"));

            AssertHelpers.AreEqual(expectedCount, actualCount, $"Count of MenuItems is - [{actualCount}].");
        }

        /// <summary>
        /// Проверяет, сколько в Меню вкладок второго уровня
        /// </summary>
        public static void SubmenuItemsCount(this MenuItem menuItem, int expectedCount, string message = null)
        {
            _loggerHelper.LogEnteringTheMethod();

            var item = menuItem.EnsureMenuItem();
            var subItems = item.FindAllChildren();

            var actualCount = subItems.Count(item => item.ClassName.Contains("MenuItem"));

            AssertHelpers.AreEqual(expectedCount, actualCount, $"Count of Subitems is - [{actualCount}].");
        }




























    }
}
