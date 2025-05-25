using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Tools;
using NLog;
using UiAutoTests.Helpers;

namespace UiAutoTests.Extensions
{
    public static class ToolTipExtensions
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private static readonly LoggerHelper _loggerHelper = new();

        /// <summary>
        /// Проверяет, является ли элемент подсказкой
        /// </summary>
        public static bool IsToolTip(this AutomationElement automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var isToolTip = automationElement.ControlType == ControlType.ToolTip;
            _logger.Info($"[{automationElement.AutomationId}] IsToolTip - [{isToolTip}]");
            return isToolTip;
        }

        /// <summary>
        /// Получает текст подсказки
        /// </summary>
        public static string GetToolTipText(this AutomationElement automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var toolTip = automationElement.EnsureToolTip();

            var text = toolTip.Name;
            _logger.Info($"[{toolTip.AutomationId}] ToolTip text - [{text}]");
            return text;
        }

        /// <summary>
        /// Проверяет, видима ли подсказка
        /// </summary>
        public static bool IsToolTipVisible(this AutomationElement automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var toolTip = automationElement.EnsureToolTip();

            var isVisible = !toolTip.IsOffscreen;
            _logger.Info($"[{toolTip.AutomationId}] IsVisible - [{isVisible}]");
            return isVisible;
        }

        /// <summary>
        /// Ожидает, пока подсказка не станет видимой
        /// </summary>
        public static bool WaitUntilVisible(this AutomationElement automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var toolTip = automationElement.EnsureToolTip();

            var result = Retry.WhileFalse(
                () => !toolTip.IsOffscreen,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{toolTip.AutomationId}] Wait until visible result - [{result}]");
            return result;
        }

        /// <summary>
        /// Ожидает, пока подсказка не исчезнет
        /// </summary>
        public static bool WaitUntilHidden(this AutomationElement automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var toolTip = automationElement.EnsureToolTip();

            var result = Retry.WhileTrue(
                () => !toolTip.IsOffscreen,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{toolTip.AutomationId}] Wait until hidden result - [{result}]");
            return result;
        }

        /// <summary>
        /// Проверяет, содержит ли подсказка указанный текст
        /// </summary>
        public static bool ContainsText(this AutomationElement automationElement, string expectedText)
        {
            _loggerHelper.LogEnteringTheMethod();
            var toolTip = automationElement.EnsureToolTip();

            var contains = toolTip.Name.Contains(expectedText);
            _logger.Info($"[{toolTip.AutomationId}] Contains text '{expectedText}' - [{contains}]");
            return contains;
        }

        /// <summary>
        /// Ожидает, пока подсказка не будет содержать указанный текст
        /// </summary>
        public static bool WaitUntilContainsText(this AutomationElement automationElement, string expectedText, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var toolTip = automationElement.EnsureToolTip();

            var result = Retry.WhileFalse(
                () => toolTip.Name.Contains(expectedText),
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{toolTip.AutomationId}] Wait until contains text '{expectedText}' result - [{result}]");
            return result;
        }
    }
} 