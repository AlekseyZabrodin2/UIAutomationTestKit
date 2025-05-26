using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Tools;
using NLog;
using UiAutoTests.Helpers;

namespace UiAutoTests.Extensions
{
    public static class LabelExtensions
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private static readonly LoggerHelper _loggerHelper = new();

        /// <summary>
        /// Проверяет, является ли элемент меткой (Label)
        /// </summary>
        public static bool IsLabel(this AutomationElement element)
        {
            _loggerHelper.LogEnteringTheMethod();
            var isLabel = element.ControlType == ControlType.Text; // Label обычно представлен ControlType.Text
            _logger.Info($"[{element.AutomationId}] IsLabel - [{isLabel}]");
            return isLabel;
        }

        /// <summary>
        /// Получить текст метки
        /// </summary>
        public static string GetLabelText(this Label label)
        {
            _loggerHelper.LogEnteringTheMethod();
            var labelElement = label.EnsureLabel();

            var text = labelElement.Text;
            _logger.Info($"[{labelElement.AutomationId}] Label text: {text}");
            return text;
        }

        /// <summary>
        /// Проверить, доступна ли метка
        /// </summary>
        public static bool IsLabelEnabled(this Label label)
        {
            _loggerHelper.LogEnteringTheMethod();
            var labelElement = label.EnsureLabel();

            var isEnabled = labelElement.IsEnabled;
            _logger.Info($"[{labelElement.AutomationId}] IsEnabled - [{isEnabled}]");
            return isEnabled;
        }

        /// <summary>
        /// Проверить, видима ли метка
        /// </summary>
        public static bool IsLabelVisible(this Label label)
        {
            _loggerHelper.LogEnteringTheMethod();
            var labelElement = label.EnsureLabel();

            var isVisible = !labelElement.IsOffscreen;
            _logger.Info($"[{labelElement.AutomationId}] IsVisible - [{isVisible}]");
            return isVisible;
        }

        /// <summary>
        /// Ожидать, пока метка не станет видимой
        /// </summary>
        public static bool WaitUntilVisible(this Label label, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var labelElement = label.EnsureLabel();

            var result = Retry.WhileFalse(
                () => !labelElement.IsOffscreen,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{labelElement.AutomationId}] Wait until visible result - [{result}]");
            return result;
        }

        /// <summary>
        /// Ожидать, пока текст метки станет равен заданному
        /// </summary>
        public static bool WaitUntilTextIs(this Label label, string expectedText, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var labelElement = label.EnsureLabel();

            var result = Retry.WhileFalse(
                () => labelElement.Text == expectedText,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{labelElement.AutomationId}] Wait until text is '{expectedText}' result - [{result}]");
            return result;
        }
    }
}
