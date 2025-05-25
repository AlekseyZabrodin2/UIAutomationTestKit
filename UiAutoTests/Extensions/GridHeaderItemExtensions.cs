using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Tools;
using NLog;
using UiAutoTests.Helpers;

namespace UiAutoTests.Extensions
{
    public static class GridHeaderItemExtensions
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private static readonly LoggerHelper _loggerHelper = new();

        /// <summary>
        /// Проверяет, является ли элемент элементом заголовка таблицы
        /// </summary>
        public static bool IsGridHeaderItem(this AutomationElement automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var isGridHeaderItem = automationElement.ControlType == ControlType.HeaderItem;
            _logger.Info($"[{automationElement.AutomationId}] IsGridHeaderItem - [{isGridHeaderItem}]");
            return isGridHeaderItem;
        }

        /// <summary>
        /// Получает текст элемента заголовка
        /// </summary>
        public static string GetHeaderItemText(this GridHeaderItem automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var gridHeaderItem = automationElement.EnsureGridHeaderItem();

            var text = gridHeaderItem.Name;
            _logger.Info($"[{gridHeaderItem.AutomationId}] Header item text - [{text}]");
            return text;
        }

        /// <summary>
        /// Проверяет, активен ли элемент заголовка
        /// </summary>
        public static bool IsGridHeaderItemEnabled(this GridHeaderItem automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var gridHeaderItem = automationElement.EnsureGridHeaderItem();

            var isEnabled = gridHeaderItem.IsEnabled;
            _logger.Info($"[{gridHeaderItem.AutomationId}] IsEnabled - [{isEnabled}]");
            return isEnabled;
        }

        /// <summary>
        /// Проверяет, видим ли элемент заголовка
        /// </summary>
        public static bool IsGridHeaderItemVisible(this GridHeaderItem automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var gridHeaderItem = automationElement.EnsureGridHeaderItem();

            var isVisible = !gridHeaderItem.IsOffscreen;
            _logger.Info($"[{gridHeaderItem.AutomationId}] IsVisible - [{isVisible}]");
            return isVisible;
        }

        /// <summary>
        /// Ожидает, пока элемент заголовка не станет активным
        /// </summary>
        public static bool WaitUntilEnabled(this GridHeaderItem automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var gridHeaderItem = automationElement.EnsureGridHeaderItem();

            var result = Retry.WhileFalse(
                () => gridHeaderItem.IsEnabled,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{gridHeaderItem.AutomationId}] Wait until enabled result - [{result}]");
            return result;
        }

        /// <summary>
        /// Проверяет, содержит ли элемент заголовка указанный текст
        /// </summary>
        public static bool ContainsText(this GridHeaderItem automationElement, string expectedText)
        {
            _loggerHelper.LogEnteringTheMethod();
            var gridHeaderItem = automationElement.EnsureGridHeaderItem();

            var contains = gridHeaderItem.Name.Contains(expectedText);
            _logger.Info($"[{gridHeaderItem.AutomationId}] Contains text '{expectedText}' - [{contains}]");
            return contains;
        }

        /// <summary>
        /// Ожидает, пока элемент заголовка не будет содержать указанный текст
        /// </summary>
        public static bool WaitUntilContainsText(this GridHeaderItem automationElement, string expectedText, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var gridHeaderItem = automationElement.EnsureGridHeaderItem();

            var result = Retry.WhileFalse(
                () => gridHeaderItem.Name.Contains(expectedText),
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{gridHeaderItem.AutomationId}] Wait until contains text '{expectedText}' result - [{result}]");
            return result;
        }
    }
} 