using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Tools;
using NLog;
using UiAutoTests.Helpers;

namespace UiAutoTests.Extensions
{
    public static class TreeItemExtensions
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private static readonly LoggerHelper _loggerHelper = new();

        /// <summary>
        /// Проверяет, является ли элемент элементом дерева
        /// </summary>
        public static bool IsTreeItem(this AutomationElement automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var isTreeItem = automationElement.ControlType == ControlType.TreeItem;
            _logger.Info($"[{automationElement.AutomationId}] IsTreeItem - [{isTreeItem}]");
            return isTreeItem;
        }

        /// <summary>
        /// Выбирает элемент дерева
        /// </summary>
        public static void Select(this TreeItem automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var treeItem = automationElement.EnsureTreeItem();

            if (!treeItem.IsEnabled)
                throw new InvalidOperationException("TreeItem is disabled");

            _logger.Info($"Selecting tree item: {treeItem.Name}");
            treeItem.Select();
            _logger.Info("Tree item selected");
        }

        /// <summary>
        /// Получает текст элемента дерева
        /// </summary>
        public static string GetTreeItemText(this TreeItem automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var treeItem = automationElement.EnsureTreeItem();

            var text = treeItem.Name;
            _logger.Info($"[{treeItem.AutomationId}] Tree item text - [{text}]");
            return text;
        }

        /// <summary>
        /// Проверяет, активен ли элемент дерева
        /// </summary>
        public static bool IsTreeItemEnabled(this TreeItem automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var treeItem = automationElement.EnsureTreeItem();

            var isEnabled = treeItem.IsEnabled;
            _logger.Info($"[{treeItem.AutomationId}] IsEnabled - [{isEnabled}]");
            return isEnabled;
        }

        /// <summary>
        /// Проверяет, видим ли элемент дерева
        /// </summary>
        public static bool IsTreeItemVisible(this TreeItem automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var treeItem = automationElement.EnsureTreeItem();

            var isVisible = !treeItem.IsOffscreen;
            _logger.Info($"[{treeItem.AutomationId}] IsVisible - [{isVisible}]");
            return isVisible;
        }

        /// <summary>
        /// Проверяет, развернут ли элемент дерева
        /// </summary>
        public static bool IsExpanded(this TreeItem automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var treeItem = automationElement.EnsureTreeItem();

            var isExpanded = treeItem.Patterns.ExpandCollapse.Pattern.ExpandCollapseState == ExpandCollapseState.Expanded;
            _logger.Info($"[{treeItem.AutomationId}] IsExpanded - [{isExpanded}]");
            return isExpanded;
        }

        /// <summary>
        /// Разворачивает элемент дерева
        /// </summary>
        public static void Expand(this TreeItem automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var treeItem = automationElement.EnsureTreeItem();

            if (!treeItem.IsEnabled)
                throw new InvalidOperationException("TreeItem is disabled");

            _logger.Info($"Expanding tree item: {treeItem.Name}");
            treeItem.Patterns.ExpandCollapse.Pattern.Expand();
            _logger.Info("Tree item expanded");
        }

        /// <summary>
        /// Сворачивает элемент дерева
        /// </summary>
        public static void Collapse(this TreeItem automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var treeItem = automationElement.EnsureTreeItem();

            if (!treeItem.IsEnabled)
                throw new InvalidOperationException("TreeItem is disabled");

            _logger.Info($"Collapsing tree item: {treeItem.Name}");
            treeItem.Patterns.ExpandCollapse.Pattern.Collapse();
            _logger.Info("Tree item collapsed");
        }

        /// <summary>
        /// Проверяет, имеет ли элемент дерева дочерние элементы
        /// </summary>
        public static bool HasChildItems(this TreeItem automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var treeItem = automationElement.EnsureTreeItem();

            var hasChildItems = treeItem.Items.Length > 0;
            _logger.Info($"[{treeItem.AutomationId}] Has child items - [{hasChildItems}]");
            return hasChildItems;
        }

        /// <summary>
        /// Получает все дочерние элементы дерева
        /// </summary>
        public static IEnumerable<TreeItem> GetChildItems(this TreeItem automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var treeItem = automationElement.EnsureTreeItem();

            var items = treeItem.Items;
            _logger.Info($"[{treeItem.AutomationId}] Child items count - [{items.Length}]");
            return items;
        }

        /// <summary>
        /// Ожидает, пока элемент дерева не станет активным
        /// </summary>
        public static bool WaitUntilEnabled(this TreeItem automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var treeItem = automationElement.EnsureTreeItem();

            var result = Retry.WhileFalse(
                () => treeItem.IsEnabled,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{treeItem.AutomationId}] Wait until enabled result - [{result}]");
            return result;
        }

        /// <summary>
        /// Ожидает, пока элемент дерева не будет развернут
        /// </summary>
        public static bool WaitUntilExpanded(this TreeItem automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var treeItem = automationElement.EnsureTreeItem();

            var result = Retry.WhileFalse(
                () => treeItem.Patterns.ExpandCollapse.Pattern.ExpandCollapseState == ExpandCollapseState.Expanded,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{treeItem.AutomationId}] Wait until expanded result - [{result}]");
            return result;
        }

        /// <summary>
        /// Ожидает, пока элемент дерева не будет свернут
        /// </summary>
        public static bool WaitUntilCollapsed(this TreeItem automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var treeItem = automationElement.EnsureTreeItem();

            var result = Retry.WhileFalse(
                () => treeItem.Patterns.ExpandCollapse.Pattern.ExpandCollapseState == ExpandCollapseState.Collapsed,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{treeItem.AutomationId}] Wait until collapsed result - [{result}]");
            return result;
        }

        /// <summary>
        /// Проверяет, содержит ли элемент дерева указанный текст
        /// </summary>
        public static bool ContainsText(this TreeItem automationElement, string expectedText)
        {
            _loggerHelper.LogEnteringTheMethod();
            var treeItem = automationElement.EnsureTreeItem();

            var contains = treeItem.Name.Contains(expectedText);
            _logger.Info($"[{treeItem.AutomationId}] Contains text '{expectedText}' - [{contains}]");
            return contains;
        }

        /// <summary>
        /// Ожидает, пока элемент дерева не будет содержать указанный текст
        /// </summary>
        public static bool WaitUntilContainsText(this TreeItem automationElement, string expectedText, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var treeItem = automationElement.EnsureTreeItem();

            var result = Retry.WhileFalse(
                () => treeItem.Name.Contains(expectedText),
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{treeItem.AutomationId}] Wait until contains text '{expectedText}' result - [{result}]");
            return result;
        }
    }
} 