using FlaUI.Core.AutomationElements;
using UiAutoTests.Helpers;

namespace UiAutoTests.Extensions
{
    public static class ButtonExtensions
    {

        private static LoggerHelper _loggerHelper = new();




        public static bool IsButtonEnabled(this AutomationElement automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();

            var button = automationElement.EnsureButton();

            return button.IsEnabled;
        }

        public static void ClickButton(this AutomationElement automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();

            var button = automationElement.EnsureButton();

            button.Invoke();
        }














    }
}
