using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Tools;
using NLog;
using UiAutoTests.Helpers;

namespace UiAutoTests.Extensions
{
    public static class TabItemExtensions
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private static readonly LoggerHelper _loggerHelper = new();

        /// <summary>
        /// Проверяет, является ли элемент вкладкой
        /// </summary>
        public static bool IsTabItem(this AutomationElement automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var isTabItem = automationElement.ControlType == ControlType.TabItem;
            _logger.Info($"[{automationElement.AutomationId}] IsTabItem - [{isTabItem}]");
            return isTabItem;
        }

        /// <summary>
        /// Выбирает вкладку
        /// </summary>
        public static void Select(this TabItem automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var tabItem = automationElement.EnsureTabItem();

            if (!tabItem.IsEnabled)
                throw new InvalidOperationException("TabItem is disabled");

            _logger.Info($"Selecting tab: {tabItem.Name}");
            tabItem.Select();
            _logger.Info("Tab selected");
        }

        /// <summary>
        /// Получает текст вкладки
        /// </summary>
        public static string GetTabItemText(this TabItem automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var tabItem = automationElement.EnsureTabItem();

            var text = tabItem.Name;
            _logger.Info($"[{tabItem.AutomationId}] Tab text - [{text}]");
            return text;
        }

        /// <summary>
        /// Проверяет, активна ли вкладка
        /// </summary>
        public static bool IsTabItemEnabled(this TabItem automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var tabItem = automationElement.EnsureTabItem();

            var isEnabled = tabItem.IsEnabled;
            _logger.Info($"[{tabItem.AutomationId}] IsEnabled - [{isEnabled}]");
            return isEnabled;
        }

        /// <summary>
        /// Проверяет, видима ли вкладка
        /// </summary>
        public static bool IsTabItemVisible(this TabItem automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var tabItem = automationElement.EnsureTabItem();

            var isVisible = !tabItem.IsOffscreen;
            _logger.Info($"[{tabItem.AutomationId}] IsVisible - [{isVisible}]");
            return isVisible;
        }

        /// <summary>
        /// Проверяет, выбрана ли вкладка
        /// </summary>
        public static bool IsSelected(this TabItem automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var tabItem = automationElement.EnsureTabItem();

            var isSelected = tabItem.IsSelected;
            _logger.Info($"[{tabItem.AutomationId}] IsSelected - [{isSelected}]");
            return isSelected;
        }

        /// <summary>
        /// Ожидает, пока вкладка не станет активной
        /// </summary>
        public static bool WaitUntilEnabled(this TabItem automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var tabItem = automationElement.EnsureTabItem();

            var result = Retry.WhileFalse(
                () => tabItem.IsEnabled,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{tabItem.AutomationId}] Wait until enabled result - [{result}]");
            return result;
        }

        /// <summary>
        /// Ожидает, пока вкладка не будет выбрана
        /// </summary>
        public static bool WaitUntilSelected(this TabItem automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var tabItem = automationElement.EnsureTabItem();

            var result = Retry.WhileFalse(
                () => tabItem.IsSelected,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{tabItem.AutomationId}] Wait until selected result - [{result}]");
            return result;
        }

        /// <summary>
        /// Проверяет, содержит ли вкладка указанный текст
        /// </summary>
        public static bool ContainsText(this TabItem automationElement, string expectedText)
        {
            _loggerHelper.LogEnteringTheMethod();
            var tabItem = automationElement.EnsureTabItem();

            var contains = tabItem.Name.Contains(expectedText);
            _logger.Info($"[{tabItem.AutomationId}] Contains text '{expectedText}' - [{contains}]");
            return contains;
        }

        /// <summary>
        /// Ожидает, пока вкладка не будет содержать указанный текст
        /// </summary>
        public static bool WaitUntilContainsText(this TabItem automationElement, string expectedText, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var tabItem = automationElement.EnsureTabItem();

            var result = Retry.WhileFalse(
                () => tabItem.Name.Contains(expectedText),
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{tabItem.AutomationId}] Wait until contains text '{expectedText}' result - [{result}]");
            return result;
        }
    }
} 