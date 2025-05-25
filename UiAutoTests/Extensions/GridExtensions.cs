using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Tools;
using NLog;
using UiAutoTests.Helpers;

namespace UiAutoTests.Extensions
{
    public static class GridExtensions
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private static readonly LoggerHelper _loggerHelper = new();

        /// <summary>
        /// Проверяет, является ли элемент сеткой
        /// </summary>
        public static bool IsGrid(this AutomationElement automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var isGrid = automationElement.ControlType == ControlType.Custom;
            _logger.Info($"[{automationElement.AutomationId}] IsGrid - [{isGrid}]");
            return isGrid;
        }

        /// <summary>
        /// Получает все строки сетки
        /// </summary>
        public static IEnumerable<GridRow> GetRows(this Grid automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var grid = automationElement.EnsureGrid();

            var rows = grid.Rows;
            _logger.Info($"[{grid.AutomationId}] Rows count - [{rows.Length}]");
            return rows;
        }

        /// <summary>
        /// Получает строку сетки по индексу
        /// </summary>
        public static GridRow GetRowByIndex(this Grid automationElement, int index)
        {
            _loggerHelper.LogEnteringTheMethod();
            var grid = automationElement.EnsureGrid();

            if (index < 0 || index >= grid.Rows.Length)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range");

            var row = grid.Rows[index];
            _logger.Info($"[{grid.AutomationId}] Getting row at index {index}");
            return row;
        }

        /// <summary>
        /// Получает ячейку сетки по индексам строки и столбца
        /// </summary>
        public static GridCell GetCell(this Grid automationElement, int rowIndex, int columnIndex)
        {
            _loggerHelper.LogEnteringTheMethod();
            var grid = automationElement.EnsureGrid();

            if (rowIndex < 0 || rowIndex >= grid.Rows.Length)
                throw new ArgumentOutOfRangeException(nameof(rowIndex), "Row index is out of range");

            var row = grid.Rows[rowIndex];
            if (columnIndex < 0 || columnIndex >= row.Cells.Length)
                throw new ArgumentOutOfRangeException(nameof(columnIndex), "Column index is out of range");

            var cell = row.Cells[columnIndex];
            _logger.Info($"[{grid.AutomationId}] Getting cell at row {rowIndex}, column {columnIndex}");
            return cell;
        }

        /// <summary>
        /// Получает текст ячейки сетки по индексам строки и столбца
        /// </summary>
        public static string GetCellText(this Grid automationElement, int rowIndex, int columnIndex)
        {
            _loggerHelper.LogEnteringTheMethod();
            var grid = automationElement.EnsureGrid();

            var cell = grid.GetCell(rowIndex, columnIndex);
            var text = cell.Value;
            _logger.Info($"[{grid.AutomationId}] Cell text at row {rowIndex}, column {columnIndex} - [{text}]");
            return text;
        }

        /// <summary>
        /// Проверяет, активна ли сетка
        /// </summary>
        public static bool IsGridEnabled(this Grid automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var grid = automationElement.EnsureGrid();

            var isEnabled = grid.IsEnabled;
            _logger.Info($"[{grid.AutomationId}] IsEnabled - [{isEnabled}]");
            return isEnabled;
        }

        /// <summary>
        /// Проверяет, видима ли сетка
        /// </summary>
        public static bool IsGridVisible(this Grid automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var grid = automationElement.EnsureGrid();

            var isVisible = !grid.IsOffscreen;
            _logger.Info($"[{grid.AutomationId}] IsVisible - [{isVisible}]");
            return isVisible;
        }

        /// <summary>
        /// Ожидает, пока сетка не станет активной
        /// </summary>
        public static bool WaitUntilEnabled(this Grid automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var grid = automationElement.EnsureGrid();

            var result = Retry.WhileFalse(
                () => grid.IsEnabled,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{grid.AutomationId}] Wait until enabled result - [{result}]");
            return result;
        }

        /// <summary>
        /// Проверяет, содержит ли ячейка сетки указанный текст
        /// </summary>
        public static bool CellContainsText(this Grid automationElement, int rowIndex, int columnIndex, string expectedText)
        {
            _loggerHelper.LogEnteringTheMethod();
            var grid = automationElement.EnsureGrid();

            var cell = grid.GetCell(rowIndex, columnIndex);
            var contains = cell.Value.Contains(expectedText);
            _logger.Info($"[{grid.AutomationId}] Cell at row {rowIndex}, column {columnIndex} contains text '{expectedText}' - [{contains}]");
            return contains;
        }

        /// <summary>
        /// Ожидает, пока ячейка сетки не будет содержать указанный текст
        /// </summary>
        public static bool WaitUntilCellContainsText(this Grid automationElement, int rowIndex, int columnIndex, string expectedText, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var grid = automationElement.EnsureGrid();

            var result = Retry.WhileFalse(
                () => grid.GetCell(rowIndex, columnIndex).Value.Contains(expectedText),
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{grid.AutomationId}] Wait until cell at row {rowIndex}, column {columnIndex} contains text '{expectedText}' result - [{result}]");
            return result;
        }

        /// <summary>
        /// Получает количество строк в сетке
        /// </summary>
        public static int GetRowCount(this Grid automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var grid = automationElement.EnsureGrid();

            var count = grid.Rows.Length;
            _logger.Info($"[{grid.AutomationId}] Row count - [{count}]");
            return count;
        }

        /// <summary>
        /// Получает количество столбцов в сетке
        /// </summary>
        public static int GetColumnCount(this Grid automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var grid = automationElement.EnsureGrid();

            var count = grid.Rows.Length > 0 ? grid.Rows[0].Cells.Length : 0;
            _logger.Info($"[{grid.AutomationId}] Column count - [{count}]");
            return count;
        }

        /// <summary>
        /// Проверяет, содержит ли сетка строки
        /// </summary>
        public static bool HasRows(this Grid automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var grid = automationElement.EnsureGrid();

            var hasRows = grid.Rows.Length > 0;
            _logger.Info($"[{grid.AutomationId}] Has rows - [{hasRows}]");
            return hasRows;
        }

        /// <summary>
        /// Ожидает появления строк в сетке
        /// </summary>
        public static bool WaitUntilHasRows(this Grid automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var grid = automationElement.EnsureGrid();

            var result = Retry.WhileFalse(
                () => grid.Rows.Length > 0,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{grid.AutomationId}] Wait until has rows result - [{result}]");
            return result;
        }
    }
} 