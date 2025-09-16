using UiAutoTests.Assertions;
using UiAutoTests.Controllers;

namespace UiAutoTests.ControllerAssertions
{
    public static class AboutAppWindowControllerAssertions
    {

        public static AboutAppWindowController AssertThatAppWindowIsNull(this AboutAppWindowController controller, string message = null)
        {
            var element = controller.GetAboutAppWindow();

            AssertAutoElement.IsNull(element, message);
            return controller;
        }

        public static AboutAppWindowController AssertThatAppWindowIsNotNull(this AboutAppWindowController controller, string message = null)
        {
            var element = controller.GetAboutAppWindow();

            AssertAutoElement.IsNotNull(element, message);
            return controller;
        }
    }
}
