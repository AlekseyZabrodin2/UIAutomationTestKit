using FlaUI.Core.AutomationElements;
using NLog;
using UiAutoTests.Helpers;

namespace UiAutoTests.Extensions
{
    public static class ButtonExtensions
    {

        private static LoggerHelper _loggerHelper = new();
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();



        public static bool IsButtonEnabled(this Button automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();

            var button = automationElement.EnsureButton();

            _logger.Info($"[{button.AutomationId}] IsEnabled - [{button.IsEnabled}]");
            return button.IsEnabled;
        }

        public static void ClickButton(this Button automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();

            var button = automationElement.EnsureButton();

            button.Invoke();
            _logger.Info($"[{button.AutomationId}] is Invoked");
        }














    }
}
