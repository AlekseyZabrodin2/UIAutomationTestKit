using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Tools;
using NLog;
using UiAutoTests.Helpers;

namespace UiAutoTests.Extensions
{
    public static class ListBoxExtensions
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private static readonly LoggerHelper _loggerHelper = new();

        /// <summary>
        /// Проверяет, является ли элемент списком
        /// </summary>
        public static bool IsListBox(this AutomationElement element)
        {
            _loggerHelper.LogEnteringTheMethod();
            var isListBox = element.ControlType == ControlType.List;
            _logger.Info($"[{element.AutomationId}] IsListBox - [{isListBox}]");
            return isListBox;
        }

        /// <summary>
        /// Выбирает элемент списка по индексу
        /// </summary>
        public static void SelectItemByIndex(this ListBox listBox, int index)
        {
            _loggerHelper.LogEnteringTheMethod();
            var listBoxElement = listBox.EnsureListBox();

            if (!listBoxElement.IsEnabled)
                throw new InvalidOperationException("ListBox is disabled");

            var items = listBoxElement.Items;
            if (index < 0 || index >= items.Length)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range");

            _logger.Info($"Selecting item at index: {index}");
            items[index].Select();
            _logger.Info("Item selected");
        }

        /// <summary>
        /// Выбирает элемент списка по тексту
        /// </summary>
        public static void SelectItemByText(this ListBox listBox, string text)
        {
            _loggerHelper.LogEnteringTheMethod();
            var listBoxElement = listBox.EnsureListBox();

            if (!listBoxElement.IsEnabled)
                throw new InvalidOperationException("ListBox is disabled");

            var item = listBoxElement.Items.FirstOrDefault(i => i.Text == text);
            if (item == null)
                throw new ArgumentException($"Item with text '{text}' not found", nameof(text));

            _logger.Info($"Selecting item with text: {text}");
            item.Select();
            _logger.Info("Item selected");
        }

        /// <summary>
        /// Получает текст выбранного элемента
        /// </summary>
        public static string GetSelectedItemText(this ListBox listBox)
        {
            _loggerHelper.LogEnteringTheMethod();
            var listBoxElement = listBox.EnsureListBox();

            var selectedItem = listBoxElement.SelectedItems.FirstOrDefault();
            var text = selectedItem?.Text ?? string.Empty;
            _logger.Info($"Selected item text: {text}");
            return text;
        }

        /// <summary>
        /// Проверяет, активен ли список
        /// </summary>
        public static bool IsListBoxEnabled(this ListBox listBox)
        {
            _loggerHelper.LogEnteringTheMethod();
            var listBoxElement = listBox.EnsureListBox();

            var isEnabled = listBoxElement.IsEnabled;
            _logger.Info($"[{listBoxElement.AutomationId}] IsEnabled - [{isEnabled}]");
            return isEnabled;
        }

        /// <summary>
        /// Проверяет, видим ли список
        /// </summary>
        public static bool IsListBoxVisible(this ListBox listBox)
        {
            _loggerHelper.LogEnteringTheMethod();
            var listBoxElement = listBox.EnsureListBox();

            var isVisible = !listBoxElement.IsOffscreen;
            _logger.Info($"[{listBoxElement.AutomationId}] IsVisible - [{isVisible}]");
            return isVisible;
        }

        /// <summary>
        /// Ожидает, пока список не станет активным
        /// </summary>
        public static bool WaitUntilEnabled(this ListBox listBox, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var listBoxElement = listBox.EnsureListBox();

            var result = Retry.WhileFalse(
                () => listBoxElement.IsEnabled,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{listBoxElement.AutomationId}] Wait until enabled result - [{result}]");
            return result;
        }

        /// <summary>
        /// Ожидает, пока не будет выбран элемент с указанным текстом
        /// </summary>
        public static bool WaitUntilItemSelected(this ListBox listBox, string expectedText, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var listBoxElement = listBox.EnsureListBox();

            var result = Retry.WhileFalse(
                () => listBoxElement.SelectedItems.FirstOrDefault()?.Text == expectedText,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{listBoxElement.AutomationId}] Wait until item selected result - [{result}]");
            return result;
        }
    }
} 