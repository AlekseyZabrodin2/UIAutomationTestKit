using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using NLog;
using UiAutoTests.Helpers;

namespace UiAutoTests.Extensions
{
    public static class TabControlExtensions
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private static readonly LoggerHelper _loggerHelper = new();

        /// <summary>
        /// Проверяет, является ли элемент вкладкой
        /// </summary>
        public static bool IsTabItem(this AutomationElement element)
        {
            _loggerHelper.LogEnteringTheMethod();
            var isTabItem = element.ControlType == ControlType.TabItem;
            _logger.Info($"[{element.AutomationId}] IsTabItem - [{isTabItem}]");
            return isTabItem;
        }

        /// <summary>
        /// Проверяет, является ли элемент панелью вкладок
        /// </summary>
        public static bool IsTab(this AutomationElement element)
        {
            _loggerHelper.LogEnteringTheMethod();
            var isTab = element.ControlType == ControlType.Tab;
            _logger.Info($"[{element.AutomationId}] IsTab - [{isTab}]");
            return isTab;
        }

        /// <summary>
        /// Выбирает вкладку по индексу
        /// </summary>
        public static void SelectTabByIndex(this Tab tab, int index)
        {
            _loggerHelper.LogEnteringTheMethod();
            var tabElement = tab.EnsureTab();

            if (!tabElement.IsEnabled)
                throw new InvalidOperationException("Tab is disabled");

            var items = tabElement.TabItems;
            if (index < 0 || index >= items.Length)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range");

            _logger.Info($"Selecting tab at index: {index}");
            items[index].Select();
            _logger.Info("Tab selected");
        }

        /// <summary>
        /// Выбирает вкладку по тексту
        /// </summary>
        public static void SelectTabByText(this Tab tab, string text)
        {
            _loggerHelper.LogEnteringTheMethod();
            var tabElement = tab.EnsureTab();

            if (!tabElement.IsEnabled)
                throw new InvalidOperationException("Tab is disabled");

            var item = tabElement.TabItems.FirstOrDefault(i => i.Name == text);
            if (item == null)
                throw new ArgumentException($"Tab with text '{text}' not found", nameof(text));

            _logger.Info($"Selecting tab with text: {text}");
            item.Select();
            _logger.Info("Tab selected");
        }

        /// <summary>
        /// Получает текст выбранной вкладки
        /// </summary>
        public static string GetSelectedTabText(this Tab tab)
        {
            _loggerHelper.LogEnteringTheMethod();
            var tabElement = tab.EnsureTab();

            var selectedItem = tabElement.SelectedTabItem;
            var text = selectedItem?.Name ?? string.Empty;
            _logger.Info($"Selected tab text: {text}");
            return text;
        }

        /// <summary>
        /// Проверяет, активна ли панель вкладок
        /// </summary>
        public static bool IsTabEnabled(this Tab tab)
        {
            _loggerHelper.LogEnteringTheMethod();
            var tabElement = tab.EnsureTab();

            var isEnabled = tabElement.IsEnabled;
            _logger.Info($"[{tabElement.AutomationId}] IsEnabled - [{isEnabled}]");
            return isEnabled;
        }

        /// <summary>
        /// Проверяет, видима ли панель вкладок
        /// </summary>
        public static bool IsTabVisible(this Tab tab)
        {
            _loggerHelper.LogEnteringTheMethod();
            var tabElement = tab.EnsureTab();

            var isVisible = !tabElement.IsOffscreen;
            _logger.Info($"[{tabElement.AutomationId}] IsVisible - [{isVisible}]");
            return isVisible;
        }

        /// <summary>
        /// Ожидает, пока панель вкладок не станет активной
        /// </summary>
        public static bool WaitUntilEnabled(this Tab tab, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var tabElement = tab.EnsureTab();

            var result = Retry.WhileFalse(
                () => tabElement.IsEnabled,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{tabElement.AutomationId}] Wait until enabled result - [{result}]");
            return result;
        }

        /// <summary>
        /// Ожидает, пока не будет выбрана вкладка с указанным текстом
        /// </summary>
        public static bool WaitUntilTabSelected(this Tab tab, string expectedText, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var tabElement = tab.EnsureTab();

            var result = Retry.WhileFalse(
                () => tabElement.SelectedTabItem?.Name == expectedText,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{tabElement.AutomationId}] Wait until tab selected result - [{result}]");
            return result;
        }
    }
} 