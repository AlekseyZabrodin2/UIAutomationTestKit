using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Tools;
using NLog;
using UiAutoTests.Helpers;

namespace UiAutoTests.Extensions
{
    public static class RadioButtonExtensions
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private static readonly LoggerHelper _loggerHelper = new();

        /// <summary>
        /// Проверяет, что элемент — RadioButton
        /// </summary>
        public static bool IsRadioButton(this AutomationElement element)
        {
            _loggerHelper.LogEnteringTheMethod();

            var isRadio = element.ControlType == ControlType.RadioButton;
            _logger.Info($"[{element.AutomationId}] IsRadioButton - [{isRadio}]");
            return isRadio;
        }

        /// <summary>
        /// Получить выбран ли RadioButton
        /// </summary>
        public static bool IsRadioButtonSelected(this RadioButton radioButton)
        {
            _loggerHelper.LogEnteringTheMethod();

            var rb = radioButton.EnsureRadioButton();
            var isChecked = rb.IsChecked;
            _logger.Info($"[{rb.AutomationId}] IsSelected - [{isChecked}]");
            return isChecked;
        }

        /// <summary>
        /// Установить RadioButton как выбранный
        /// </summary>
        public static void SelectRadioButton(this RadioButton radioButton)
        {
            _loggerHelper.LogEnteringTheMethod();

            var rb = radioButton.EnsureRadioButton();
            rb.Patterns.SelectionItem.Pattern.Select();
            _logger.Info($"[{rb.AutomationId}] RadioButton selected.");
        }

        /// <summary>
        /// Проверка доступности RadioButton
        /// </summary>
        public static bool IsRadioButtonEnabled(this RadioButton radioButton)
        {
            _loggerHelper.LogEnteringTheMethod();

            var rb = radioButton.EnsureRadioButton();
            var isEnabled = rb.IsEnabled;
            _logger.Info($"[{rb.AutomationId}] IsEnabled - [{isEnabled}]");
            return isEnabled;
        }

        /// <summary>
        /// Ожидание пока RadioButton станет выбранным
        /// </summary>
        public static bool WaitUntilRadioButtonSelected(this RadioButton radioButton, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();

            var rb = radioButton.EnsureRadioButton();
            var result = Retry.WhileFalse(() => rb.IsChecked, TimeSpan.FromMilliseconds(timeoutMs)).Success;
            _logger.Info($"[{rb.AutomationId}] WaitUntilSelected result - [{result}]");
            return result;
        }

        /// <summary>
        /// Ожидание пока RadioButton станет доступным
        /// </summary>
        public static bool WaitUntilRadioButtonEnabled(this RadioButton radioButton, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();

            var rb = radioButton.EnsureRadioButton();
            var result = Retry.WhileTrue(() => !rb.IsEnabled, TimeSpan.FromMilliseconds(timeoutMs)).Success;
            _logger.Info($"[{rb.AutomationId}] WaitUntilEnabled result - [{result}]");
            return result;
        }

        /// <summary>
        /// Получить текст (Name) радиокнопки
        /// </summary>
        public static string GetRadioButtonText(this RadioButton radioButton)
        {
            _loggerHelper.LogEnteringTheMethod();

            var rb = radioButton.EnsureRadioButton();
            var text = rb.Name;
            _logger.Info($"[{rb.AutomationId}] Text: {text}");
            return text;
        }

        /// <summary>
        /// Ожидать, пока радиокнопка станет видимой
        /// </summary>
        public static bool WaitUntilRadioButtonVisible(this RadioButton radioButton, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();

            var rb = radioButton.EnsureRadioButton();
            var result = Retry.WhileTrue(() => rb.IsOffscreen, TimeSpan.FromMilliseconds(timeoutMs)).Success;
            _logger.Info($"[{rb.AutomationId}] WaitUntilVisible result - [{result}]");
            return result;
        }

        /// <summary>
        /// Проверить, видим ли элемент
        /// </summary>
        public static bool IsRadioButtonVisible(this RadioButton radioButton)
        {
            _loggerHelper.LogEnteringTheMethod();

            var rb = radioButton.EnsureRadioButton();
            var visible = !rb.IsOffscreen;
            _logger.Info($"[{rb.AutomationId}] IsVisible - [{visible}]");
            return visible;
        }

        /// <summary>
        /// Ожидать, пока RadioButton станет доступной и выбрать её
        /// </summary>
        public static bool WaitAndSelectRadioButton(this RadioButton radioButton, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();

            var rb = radioButton.EnsureRadioButton();
            var waitResult = Retry.WhileTrue(() => !rb.IsEnabled, TimeSpan.FromMilliseconds(timeoutMs)).Success;

            if (waitResult)
            {
                rb.SelectRadioButton();
                _logger.Info($"[{rb.AutomationId}] Waited and selected.");
                return true;
            }

            _logger.Warn($"[{rb.AutomationId}] Could not select: not enabled within timeout.");
            return false;
        }
    }
}
