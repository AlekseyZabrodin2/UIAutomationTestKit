using UiAutoTests.Assertions;
using UiAutoTests.Controllers;

namespace UiAutoTests.ControllerAssertions
{
    public static class MainWindowControllerAssertions
    {


        public static MainWindowController AssertIsTrue(this MainWindowController controller, bool result, string message = null)
        {
            AssertHelpers.IsTrue(result, message);

            return controller;
        }

        public static MainWindowController AssertAreEqual<T>(this MainWindowController controller, T expected, T actual, string message = null)
        {
            AssertHelpers.AreEqual(actual, expected, message);

            return controller;
        }

        public static MainWindowController AssertUserIdEquals(this MainWindowController controller, string expected, string message = null)
        {
            AssertHelpers.AreEqual(expected, controller.GetUserIdText(), message);
            return controller;
        }

        public static MainWindowController AssertUserIdIsEmpty(this MainWindowController controller, string message = null)
        {
            AssertHelpers.IsEmpty(controller.GetUserIdText(), message);
            return controller;
        }

        public static MainWindowController AssertIsRegistrationButtonEnabled(this MainWindowController controller, string message = null)
        {
            AssertHelpers.IsTrue(controller.IsRegistrationButtonEnabled(), message);
            return controller;
        }

        public static MainWindowController AssertRowCountIs(this MainWindowController controller, int expected, string message = null)
        {
            var actual = controller.GetRowCount();
            AssertHelpers.AreEqual(expected, actual, message);
            return controller;
        }

        public static MainWindowController AssertRowCountIsNot(this MainWindowController controller, int notExpected, string message = null)
        {
            var actual = controller.GetRowCount();
            AssertHelpers.AreNotEqual(notExpected, actual, message);
            return controller;
        }

        public static MainWindowController AssertMainMenuItemsCountIs(this MainWindowController controller, int expectedCount, string message = null)
        {
            var menu = controller.GetMainMenu();
            AssertForMenu.MainMenuItemsCount(menu, expectedCount, message);
            return controller;
        }

        public static MainWindowController AssertSubItemsCountIs(this MainWindowController controller, string subItem, int expectedCount, string message = null)
        {
            var menuItem = controller.GetMenuItemById(subItem);
            AssertForMenu.SubmenuItemsCount(menuItem, expectedCount, message);
            return controller;
        }

        public static MainWindowController AssertThatElementTextMatches(this MainWindowController controller, string expectedText, string message = null)
        {
            var elementText = controller.GetElementText();

            AssertForString.Matches(elementText, expectedText, message);
            return controller;
        }

        public static MainWindowController AssertThatElementIsNull(this MainWindowController controller, string message = null)
        {
            var element = controller.GetAboutAppWindow();

            AssertAutoElement.IsNull(element, message);
            return controller;
        }
    }
}
