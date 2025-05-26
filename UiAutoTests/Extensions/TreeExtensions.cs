using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Tools;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UiAutoTests.Helpers;

namespace UiAutoTests.Extensions
{
    public static class TreeExtensions
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private static readonly LoggerHelper _loggerHelper = new();

        /// <summary>
        /// Проверить, что элемент — Tree
        /// </summary>
        public static bool IsTree(this AutomationElement element)
        {
            _loggerHelper.LogEnteringTheMethod();
            var isTree = element.ControlType == ControlType.Tree;
            _logger.Info($"[{element.AutomationId}] IsTree - [{isTree}]");
            return isTree;
        }

        /// <summary>
        /// Получить корневые элементы дерева
        /// </summary>
        public static AutomationElement[] GetRootNodes(this Tree tree)
        {
            _loggerHelper.LogEnteringTheMethod();

            var treeElement = tree.EnsureTree();
            var roots = treeElement?.Items ?? Array.Empty<AutomationElement>();
            _logger.Info($"Tree has {roots.Length} root nodes.");
            return roots;
        }

        /// <summary>
        /// Проверить, видим ли элемент дерева (проверка для конкретного узла)
        /// </summary>
        public static bool IsNodeVisible(this AutomationElement treeNode)
        {
            _loggerHelper.LogEnteringTheMethod();

            if (treeNode == null)
                throw new ArgumentNullException(nameof(treeNode));

            var isVisible = !treeNode.IsOffscreen;
            _logger.Info($"[{treeNode.AutomationId}] Node is visible: {isVisible}");
            return isVisible;
        }


        /// <summary>
        /// Ожидать, пока дерево будет доступно (например, пока корневые узлы загрузятся)
        /// </summary>
        public static bool WaitUntilHasRootNodes(this Tree tree, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();

            var treeElement = tree.EnsureTree();
            var result = Retry.WhileFalse(
                () => treeElement.Items?.Length > 0,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"WaitUntilHasRootNodes result - [{result}]");
            return result;
        }

        /// <summary>
        /// Найти узел дерева по имени (AutomationId или Name)
        /// </summary>
        public static AutomationElement FindNodeByName(this Tree tree, string name)
        {
            _loggerHelper.LogEnteringTheMethod();

            var treeElement = tree.EnsureTree();
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name must not be null or empty.", nameof(name));

            var nodes = treeElement.Items;
            foreach (var node in nodes)
            {
                if (string.Equals(node.Name, name, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(node.AutomationId, name, StringComparison.OrdinalIgnoreCase))
                {
                    _logger.Info($"Found node with name or AutomationId '{name}'.");
                    return node;
                }
            }

            _logger.Warn($"Node with name or AutomationId '{name}' not found.");
            return null;
        }

        /// <summary>
        /// Получить первый корневой узел дерева
        /// </summary>
        public static AutomationElement GetFirstRootNode(this Tree tree)
        {
            _loggerHelper.LogEnteringTheMethod();

            var treeElement = tree.EnsureTree();
            var roots = treeElement.Items;
            if (roots == null || roots.Length == 0)
                throw new InvalidOperationException("Tree has no root nodes.");

            _logger.Info($"First root node AutomationId: [{roots[0].AutomationId}]");
            return roots[0];
        }

        /// <summary>
        /// Найти узел по AutomationId среди корневых
        /// </summary>
        public static AutomationElement FindRootNodeByAutomationId(this Tree tree, string automationId)
        {
            _loggerHelper.LogEnteringTheMethod();

            var treeElement = tree.EnsureTree();
            var roots = treeElement.Items;
            if (roots == null)
                return null;

            var node = roots.FirstOrDefault(item => item.AutomationId == automationId);
            _logger.Info(node != null
                ? $"Found root node with AutomationId: [{automationId}]"
                : $"Root node with AutomationId: [{automationId}] not found");

            return node;
        }
    }
}
