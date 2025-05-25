using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Tools;
using NLog;
using UiAutoTests.Helpers;

namespace UiAutoTests.Extensions
{
    public static class ListBoxItemExtensions
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private static readonly LoggerHelper _loggerHelper = new();

        /// <summary>
        /// Проверяет, является ли элемент элементом списка
        /// </summary>
        public static bool IsListBoxItem(this AutomationElement automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var isListBoxItem = automationElement.ControlType == ControlType.ListItem;
            _logger.Info($"[{automationElement.AutomationId}] IsListBoxItem - [{isListBoxItem}]");
            return isListBoxItem;
        }

        /// <summary>
        /// Выбирает элемент списка
        /// </summary>
        public static void Select(this ListBoxItem automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var listBoxItem = automationElement.EnsureListBoxItem();

            if (!listBoxItem.IsEnabled)
                throw new InvalidOperationException("ListBoxItem is disabled");

            _logger.Info($"Selecting list box item: {listBoxItem.Name}");
            listBoxItem.Select();
            _logger.Info("List box item selected");
        }

        /// <summary>
        /// Получает текст элемента списка
        /// </summary>
        public static string GetListBoxItemText(this ListBoxItem automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var listBoxItem = automationElement.EnsureListBoxItem();

            var text = listBoxItem.Name;
            _logger.Info($"[{listBoxItem.AutomationId}] List box item text - [{text}]");
            return text;
        }

        /// <summary>
        /// Проверяет, активен ли элемент списка
        /// </summary>
        public static bool IsListBoxItemEnabled(this ListBoxItem automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var listBoxItem = automationElement.EnsureListBoxItem();

            var isEnabled = listBoxItem.IsEnabled;
            _logger.Info($"[{listBoxItem.AutomationId}] IsEnabled - [{isEnabled}]");
            return isEnabled;
        }

        /// <summary>
        /// Проверяет, видим ли элемент списка
        /// </summary>
        public static bool IsListBoxItemVisible(this ListBoxItem automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var listBoxItem = automationElement.EnsureListBoxItem();

            var isVisible = !listBoxItem.IsOffscreen;
            _logger.Info($"[{listBoxItem.AutomationId}] IsVisible - [{isVisible}]");
            return isVisible;
        }

        /// <summary>
        /// Проверяет, выбран ли элемент списка
        /// </summary>
        public static bool IsSelected(this ListBoxItem automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var listBoxItem = automationElement.EnsureListBoxItem();

            var isSelected = listBoxItem.IsSelected;
            _logger.Info($"[{listBoxItem.AutomationId}] IsSelected - [{isSelected}]");
            return isSelected;
        }

        /// <summary>
        /// Ожидает, пока элемент списка не станет активным
        /// </summary>
        public static bool WaitUntilEnabled(this ListBoxItem automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var listBoxItem = automationElement.EnsureListBoxItem();

            var result = Retry.WhileFalse(
                () => listBoxItem.IsEnabled,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{listBoxItem.AutomationId}] Wait until enabled result - [{result}]");
            return result;
        }

        /// <summary>
        /// Ожидает, пока элемент списка не будет выбран
        /// </summary>
        public static bool WaitUntilSelected(this ListBoxItem automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var listBoxItem = automationElement.EnsureListBoxItem();

            var result = Retry.WhileFalse(
                () => listBoxItem.IsSelected,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{listBoxItem.AutomationId}] Wait until selected result - [{result}]");
            return result;
        }

        /// <summary>
        /// Проверяет, содержит ли элемент списка указанный текст
        /// </summary>
        public static bool ContainsText(this ListBoxItem automationElement, string expectedText)
        {
            _loggerHelper.LogEnteringTheMethod();
            var listBoxItem = automationElement.EnsureListBoxItem();

            var contains = listBoxItem.Name.Contains(expectedText);
            _logger.Info($"[{listBoxItem.AutomationId}] Contains text '{expectedText}' - [{contains}]");
            return contains;
        }

        /// <summary>
        /// Ожидает, пока элемент списка не будет содержать указанный текст
        /// </summary>
        public static bool WaitUntilContainsText(this ListBoxItem automationElement, string expectedText, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var listBoxItem = automationElement.EnsureListBoxItem();

            var result = Retry.WhileFalse(
                () => listBoxItem.Name.Contains(expectedText),
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{listBoxItem.AutomationId}] Wait until contains text '{expectedText}' result - [{result}]");
            return result;
        }
    }
} 