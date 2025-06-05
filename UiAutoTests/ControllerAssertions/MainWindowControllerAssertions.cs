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
    }
}
