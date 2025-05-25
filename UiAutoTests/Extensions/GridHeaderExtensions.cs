using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Tools;
using NLog;
using UiAutoTests.Helpers;

namespace UiAutoTests.Extensions
{
    public static class GridHeaderExtensions
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private static readonly LoggerHelper _loggerHelper = new();

        /// <summary>
        /// Проверяет, является ли элемент заголовком таблицы
        /// </summary>
        public static bool IsGridHeader(this AutomationElement automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var isGridHeader = automationElement.ControlType == ControlType.Header;
            _logger.Info($"[{automationElement.AutomationId}] IsGridHeader - [{isGridHeader}]");
            return isGridHeader;
        }

        /// <summary>
        /// Получает все элементы заголовка
        /// </summary>
        public static IEnumerable<GridHeaderItem> GetHeaderItems(this GridHeader automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var gridHeader = automationElement.EnsureGridHeader();

            var items = gridHeader.FindAllChildren(cf => cf.ByControlType(ControlType.HeaderItem))
                .Select(x => x.AsGridHeaderItem())
                .Where(x => x != null);
            _logger.Info($"[{gridHeader.AutomationId}] Header items count - [{items.Count()}]");
            return items;
        }

        /// <summary>
        /// Получает элемент заголовка по индексу
        /// </summary>
        public static GridHeaderItem GetHeaderItemByIndex(this GridHeader automationElement, int index)
        {
            _loggerHelper.LogEnteringTheMethod();
            var gridHeader = automationElement.EnsureGridHeader();

            var items = gridHeader.GetHeaderItems().ToArray();
            if (index < 0 || index >= items.Length)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range");

            var item = items[index];
            _logger.Info($"[{gridHeader.AutomationId}] Getting header item at index {index}");
            return item;
        }

        /// <summary>
        /// Получает текст элемента заголовка по индексу
        /// </summary>
        public static string GetHeaderItemText(this GridHeader automationElement, int index)
        {
            _loggerHelper.LogEnteringTheMethod();
            var gridHeader = automationElement.EnsureGridHeader();

            var item = gridHeader.GetHeaderItemByIndex(index);
            var text = item.Name;
            _logger.Info($"[{gridHeader.AutomationId}] Header item text at index {index} - [{text}]");
            return text;
        }

        /// <summary>
        /// Проверяет, активен ли заголовок
        /// </summary>
        public static bool IsGridHeaderEnabled(this GridHeader automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var gridHeader = automationElement.EnsureGridHeader();

            var isEnabled = gridHeader.IsEnabled;
            _logger.Info($"[{gridHeader.AutomationId}] IsEnabled - [{isEnabled}]");
            return isEnabled;
        }

        /// <summary>
        /// Проверяет, видим ли заголовок
        /// </summary>
        public static bool IsGridHeaderVisible(this GridHeader automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var gridHeader = automationElement.EnsureGridHeader();

            var isVisible = !gridHeader.IsOffscreen;
            _logger.Info($"[{gridHeader.AutomationId}] IsVisible - [{isVisible}]");
            return isVisible;
        }

        /// <summary>
        /// Ожидает, пока заголовок не станет активным
        /// </summary>
        public static bool WaitUntilEnabled(this GridHeader automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var gridHeader = automationElement.EnsureGridHeader();

            var result = Retry.WhileFalse(
                () => gridHeader.IsEnabled,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{gridHeader.AutomationId}] Wait until enabled result - [{result}]");
            return result;
        }

        /// <summary>
        /// Получает количество элементов заголовка
        /// </summary>
        public static int GetHeaderItemCount(this GridHeader automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var gridHeader = automationElement.EnsureGridHeader();

            var count = gridHeader.GetHeaderItems().Count();
            _logger.Info($"[{gridHeader.AutomationId}] Header item count - [{count}]");
            return count;
        }

        /// <summary>
        /// Проверяет, содержит ли заголовок элементы
        /// </summary>
        public static bool HasHeaderItems(this GridHeader automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var gridHeader = automationElement.EnsureGridHeader();

            var hasItems = gridHeader.GetHeaderItems().Any();
            _logger.Info($"[{gridHeader.AutomationId}] Has header items - [{hasItems}]");
            return hasItems;
        }

        /// <summary>
        /// Ожидает появления элементов заголовка
        /// </summary>
        public static bool WaitUntilHasHeaderItems(this GridHeader automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var gridHeader = automationElement.EnsureGridHeader();

            var result = Retry.WhileFalse(
                () => gridHeader.GetHeaderItems().Any(),
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{gridHeader.AutomationId}] Wait until has header items result - [{result}]");
            return result;
        }
    }
} 