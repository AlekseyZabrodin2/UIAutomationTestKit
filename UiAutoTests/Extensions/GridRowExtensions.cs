using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Tools;
using NLog;
using UiAutoTests.Helpers;

namespace UiAutoTests.Extensions
{
    public static class GridRowExtensions
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private static readonly LoggerHelper _loggerHelper = new();

        /// <summary>
        /// Проверяет, является ли элемент строкой сетки
        /// </summary>
        public static bool IsGridRow(this AutomationElement automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var isGridRow = automationElement.ControlType == ControlType.Custom;
            _logger.Info($"[{automationElement.AutomationId}] IsGridRow - [{isGridRow}]");
            return isGridRow;
        }

        /// <summary>
        /// Получает все ячейки строки
        /// </summary>
        public static IEnumerable<GridCell> GetCells(this GridRow automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var gridRow = automationElement.EnsureGridRow();

            var cells = gridRow.Cells;
            _logger.Info($"[{gridRow.AutomationId}] Cells count - [{cells.Length}]");
            return cells;
        }

        /// <summary>
        /// Получает ячейку по индексу
        /// </summary>
        public static GridCell GetCellByIndex(this GridRow automationElement, int index)
        {
            _loggerHelper.LogEnteringTheMethod();
            var gridRow = automationElement.EnsureGridRow();

            if (index < 0 || index >= gridRow.Cells.Length)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range");

            var cell = gridRow.Cells[index];
            _logger.Info($"[{gridRow.AutomationId}] Getting cell at index {index}");
            return cell;
        }

        /// <summary>
        /// Получает текст ячейки по индексу
        /// </summary>
        public static string GetCellText(this GridRow automationElement, int index)
        {
            _loggerHelper.LogEnteringTheMethod();
            var gridRow = automationElement.EnsureGridRow();

            var cell = gridRow.GetCellByIndex(index);
            var text = cell.Value;
            _logger.Info($"[{gridRow.AutomationId}] Cell text at index {index} - [{text}]");
            return text;
        }

        /// <summary>
        /// Проверяет, активна ли строка
        /// </summary>
        public static bool IsGridRowEnabled(this GridRow automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var gridRow = automationElement.EnsureGridRow();

            var isEnabled = gridRow.IsEnabled;
            _logger.Info($"[{gridRow.AutomationId}] IsEnabled - [{isEnabled}]");
            return isEnabled;
        }

        /// <summary>
        /// Проверяет, видима ли строка
        /// </summary>
        public static bool IsGridRowVisible(this GridRow automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var gridRow = automationElement.EnsureGridRow();

            var isVisible = !gridRow.IsOffscreen;
            _logger.Info($"[{gridRow.AutomationId}] IsVisible - [{isVisible}]");
            return isVisible;
        }

        /// <summary>
        /// Ожидает, пока строка не станет активной
        /// </summary>
        public static bool WaitUntilEnabled(this GridRow automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var gridRow = automationElement.EnsureGridRow();

            var result = Retry.WhileFalse(
                () => gridRow.IsEnabled,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{gridRow.AutomationId}] Wait until enabled result - [{result}]");
            return result;
        }

        /// <summary>
        /// Проверяет, содержит ли ячейка указанный текст
        /// </summary>
        public static bool CellContainsText(this GridRow automationElement, int index, string expectedText)
        {
            _loggerHelper.LogEnteringTheMethod();
            var gridRow = automationElement.EnsureGridRow();

            var cell = gridRow.GetCellByIndex(index);
            var contains = cell.Value.Contains(expectedText);
            _logger.Info($"[{gridRow.AutomationId}] Cell at index {index} contains text '{expectedText}' - [{contains}]");
            return contains;
        }

        /// <summary>
        /// Ожидает, пока ячейка не будет содержать указанный текст
        /// </summary>
        public static bool WaitUntilCellContainsText(this GridRow automationElement, int index, string expectedText, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var gridRow = automationElement.EnsureGridRow();

            var result = Retry.WhileFalse(
                () => gridRow.GetCellByIndex(index).Value.Contains(expectedText),
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{gridRow.AutomationId}] Wait until cell at index {index} contains text '{expectedText}' result - [{result}]");
            return result;
        }

        /// <summary>
        /// Получает количество ячеек в строке
        /// </summary>
        public static int GetCellCount(this GridRow automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var gridRow = automationElement.EnsureGridRow();

            var count = gridRow.Cells.Length;
            _logger.Info($"[{gridRow.AutomationId}] Cell count - [{count}]");
            return count;
        }

        /// <summary>
        /// Проверяет, содержит ли строка ячейки
        /// </summary>
        public static bool HasCells(this GridRow automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var gridRow = automationElement.EnsureGridRow();

            var hasCells = gridRow.Cells.Length > 0;
            _logger.Info($"[{gridRow.AutomationId}] Has cells - [{hasCells}]");
            return hasCells;
        }

        /// <summary>
        /// Ожидает появления ячеек в строке
        /// </summary>
        public static bool WaitUntilHasCells(this GridRow automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var gridRow = automationElement.EnsureGridRow();

            var result = Retry.WhileFalse(
                () => gridRow.Cells.Length > 0,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{gridRow.AutomationId}] Wait until has cells result - [{result}]");
            return result;
        }
    }
} 