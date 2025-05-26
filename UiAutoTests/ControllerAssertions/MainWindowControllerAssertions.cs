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
    }
}
