using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Tools;
using NLog;
using UiAutoTests.Helpers;

namespace UiAutoTests.Extensions
{
    public static class ComboBoxExtensions
    {
        private static LoggerHelper _loggerHelper = new();
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Выбирает элемент в комбобоксе по тексту
        /// </summary>
        /// <param name="text">Текст элемента для выбора</param>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах</param>
        public static void SelectItemByText(this ComboBox automationElement, string text, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var comboBox = automationElement.EnsureComboBox();

            if (!comboBox.WaitUntilEnabled(timeoutMs))
            {
                throw new TimeoutException($"Комбобокс {comboBox.AutomationId} не стал активным за {timeoutMs}мс");
            }

            _logger.Info($"Selecting item with text: {text}");
            comboBox.Select(text);
            _logger.Info($"Selected item: {comboBox.SelectedItem?.Text}");
        }

        /// <summary>
        /// Выбирает элемент в комбобоксе по индексу
        /// </summary>
        /// <param name="index">Индекс элемента для выбора</param>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах</param>
        public static void SelectItemByIndex(this ComboBox automationElement, int index, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var comboBox = automationElement.EnsureComboBox();

            if (!comboBox.WaitUntilEnabled(timeoutMs))
            {
                throw new TimeoutException($"Комбобокс {comboBox.AutomationId} не стал активным за {timeoutMs}мс");
            }

            _logger.Info($"Selecting item at index: {index}");
            comboBox.Select(index);
            _logger.Info($"Selected item: {comboBox.SelectedItem?.Text}");
        }

        /// <summary>
        /// Проверяет, активен ли комбобокс
        /// </summary>
        public static bool IsComboBoxEnabled(this ComboBox automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var comboBox = automationElement.EnsureComboBox();
            var state = comboBox.IsEnabled;
            _logger.Info($"[{comboBox.AutomationId}] is [{state}]");
            return state;
        }

        /// <summary>
        /// Проверяет, видим ли комбобокс
        /// </summary>
        public static bool IsComboBoxVisible(this ComboBox automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var comboBox = automationElement.EnsureComboBox();
            var isVisible = !comboBox.IsOffscreen;
            _logger.Info($"[{comboBox.AutomationId}] IsVisible - [{isVisible}]");
            return isVisible;
        }

        /// <summary>
        /// Проверяет, раскрыт ли комбобокс
        /// </summary>
        public static bool IsComboBoxExpanded(this ComboBox automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var comboBox = automationElement.EnsureComboBox();
            var isExpanded = comboBox.Patterns.ExpandCollapse.Pattern.ExpandCollapseState == ExpandCollapseState.Expanded;
            _logger.Info($"[{comboBox.AutomationId}] IsExpanded - [{isExpanded}]");
            return isExpanded;
        }

        /// <summary>
        /// Ожидает, пока комбобокс не станет активным
        /// </summary>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах</param>
        public static bool WaitUntilEnabled(this ComboBox automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var comboBox = automationElement.EnsureComboBox();
            
            var result = Retry.WhileFalse(
                () => comboBox.IsEnabled,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
                
            _logger.Info($"[{comboBox.AutomationId}] Wait until enabled result - [{result}]");
            return result;
        }

        /// <summary>
        /// Ожидает, пока комбобокс не станет раскрытым
        /// </summary>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах</param>
        public static bool WaitUntilExpanded(this ComboBox automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var comboBox = automationElement.EnsureComboBox();
            
            var result = Retry.WhileFalse(
                () => comboBox.Patterns.ExpandCollapse.Pattern.ExpandCollapseState == ExpandCollapseState.Expanded,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
                
            _logger.Info($"[{comboBox.AutomationId}] Wait until expanded result - [{result}]");
            return result;
        }

        /// <summary>
        /// Ожидает, пока комбобокс не станет закрытым
        /// </summary>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах</param>
        public static bool WaitUntilCollapsed(this ComboBox automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var comboBox = automationElement.EnsureComboBox();
            
            var result = Retry.WhileFalse(
                () => comboBox.Patterns.ExpandCollapse.Pattern.ExpandCollapseState == ExpandCollapseState.Collapsed,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
                
            _logger.Info($"[{comboBox.AutomationId}] Wait until collapsed result - [{result}]");
            return result;
        }

        /// <summary>
        /// Получает текст выбранного элемента
        /// </summary>
        public static string GetSelectedItemText(this ComboBox automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var comboBox = automationElement.EnsureComboBox();
            var text = comboBox.SelectedItem?.Text;
            _logger.Info($"[{comboBox.AutomationId}] Selected item text - [{text}]");
            return text;
        }

        /// <summary>
        /// Проверяет, содержит ли комбобокс указанный элемент
        /// </summary>
        /// <param name="text">Текст элемента для поиска</param>
        public static bool ContainsItem(this ComboBox automationElement, string text)
        {
            _loggerHelper.LogEnteringTheMethod();
            var comboBox = automationElement.EnsureComboBox();
            var contains = comboBox.Items.Any(item => item.Text == text);
            _logger.Info($"[{comboBox.AutomationId}] Contains item '{text}' - [{contains}]");
            return contains;
        }

        /// <summary>
        /// Ожидает появления элемента в комбобоксе
        /// </summary>
        /// <param name="text">Текст элемента для ожидания</param>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах</param>
        public static bool WaitUntilItemExists(this ComboBox automationElement, string text, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var comboBox = automationElement.EnsureComboBox();
            
            var result = Retry.WhileFalse(
                () => comboBox.Items.Any(item => item.Text == text),
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
                
            _logger.Info($"[{comboBox.AutomationId}] Wait until item '{text}' exists result - [{result}]");
            return result;
        }

        /// <summary>
        /// Получает все элементы комбобокса
        /// </summary>
        public static IEnumerable<string> GetAllItems(this ComboBox automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var comboBox = automationElement.EnsureComboBox();
            var items = comboBox.Items.Select(item => item.Text).ToList();
            _logger.Info($"[{comboBox.AutomationId}] Items count - [{items.Count}]");
            return items;
        }

        /// <summary>
        /// Получает количество элементов в комбобоксе
        /// </summary>
        public static int GetItemsCount(this ComboBox automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var comboBox = automationElement.EnsureComboBox();
            var count = comboBox.Items.Length;
            _logger.Info($"[{comboBox.AutomationId}] Items count - [{count}]");
            return count;
        }

        /// <summary>
        /// Проверяет, содержит ли комбобокс элементы
        /// </summary>
        public static bool HasItems(this ComboBox automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var comboBox = automationElement.EnsureComboBox();
            var hasItems = comboBox.Items.Length > 0;
            _logger.Info($"[{comboBox.AutomationId}] Has items - [{hasItems}]");
            return hasItems;
        }

        /// <summary>
        /// Ожидает появления элементов в комбобоксе
        /// </summary>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах</param>
        public static bool WaitUntilHasItems(this ComboBox automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var comboBox = automationElement.EnsureComboBox();
            
            var result = Retry.WhileFalse(
                () => comboBox.Items.Length > 0,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
                
            _logger.Info($"[{comboBox.AutomationId}] Wait until has items result - [{result}]");
            return result;
        }
    }
}
