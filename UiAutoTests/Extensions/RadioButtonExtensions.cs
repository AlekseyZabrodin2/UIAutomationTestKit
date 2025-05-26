using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Tools;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public static bool IsSelected(this RadioButton radioButton)
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
        public static void Select(this RadioButton radioButton)
        {
            _loggerHelper.LogEnteringTheMethod();

            var rb = radioButton.EnsureRadioButton();
            rb.Select();
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
        public static bool WaitUntilSelected(this RadioButton radioButton, int timeoutMs = 5000)
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
        public static bool WaitUntilEnabled(this RadioButton radioButton, int timeoutMs = 5000)
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
        public static string GetText(this RadioButton radioButton)
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
        public static bool WaitUntilVisible(this RadioButton radioButton, int timeoutMs = 5000)
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
        public static bool IsVisible(this RadioButton radioButton)
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
        public static bool WaitAndSelect(this RadioButton radioButton, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();

            var rb = radioButton.EnsureRadioButton();
            var waitResult = Retry.WhileTrue(() => !rb.IsEnabled, TimeSpan.FromMilliseconds(timeoutMs)).Success;

            if (waitResult)
            {
                rb.Select();
                _logger.Info($"[{rb.AutomationId}] Waited and selected.");
                return true;
            }

            _logger.Warn($"[{rb.AutomationId}] Could not select: not enabled within timeout.");
            return false;
        }
    }
}
