using FlaUI.Core.AutomationElements;
using FlaUI.Core.Tools;
using NLog;
using UiAutoTests.Helpers;
using FlaUI.Core.Definitions;

namespace UiAutoTests.Extensions
{
    public static class DataGridExtensions
    {
        private static LoggerHelper _loggerHelper = new();
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Получает значение ячейки по индексам строки и столбца
        /// </summary>
        /// <param name="rowIndex">Индекс строки</param>
        /// <param name="columnIndex">Индекс столбца</param>
        public static string GetCellValue(this DataGridView automationElement, int rowIndex, int columnIndex)
        {
            _loggerHelper.LogEnteringTheMethod();
            var dataGrid = automationElement.EnsureDataGridView();

            var rows = dataGrid.Rows;
            if (rowIndex < 0 || rowIndex >= rows.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(rowIndex), $"Индекс строки {rowIndex} вне диапазона [0, {rows.Length - 1}]");
            }

            var cells = rows[rowIndex].Cells;
            if (columnIndex < 0 || columnIndex >= cells.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(columnIndex), $"Индекс столбца {columnIndex} вне диапазона [0, {cells.Length - 1}]");
            }

            var value = cells[columnIndex].Name;
            _logger.Info($"[{dataGrid.AutomationId}] Cell[{rowIndex}, {columnIndex}] value - [{value}]");
            return value;
        }

        /// <summary>
        /// Устанавливает значение ячейки по индексам строки и столбца
        /// </summary>
        /// <param name="rowIndex">Индекс строки</param>
        /// <param name="columnIndex">Индекс столбца</param>
        /// <param name="value">Значение для установки</param>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах</param>
        public static void SetCellValue(this DataGridView automationElement, int rowIndex, int columnIndex, string value, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var dataGrid = automationElement.EnsureDataGridView();

            var rows = dataGrid.Rows;
            if (rowIndex < 0 || rowIndex >= rows.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(rowIndex), $"Индекс строки {rowIndex} вне диапазона [0, {rows.Length - 1}]");
            }

            var cells = rows[rowIndex].Cells;
            if (columnIndex < 0 || columnIndex >= cells.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(columnIndex), $"Индекс столбца {columnIndex} вне диапазона [0, {cells.Length - 1}]");
            }

            var cell = cells[columnIndex];
            if (!cell.WaitUntilEnabled(timeoutMs))
            {
                throw new TimeoutException($"Ячейка [{rowIndex}, {columnIndex}] не стала активной за {timeoutMs}мс");
            }

            _logger.Info($"Setting cell[{rowIndex}, {columnIndex}] value to: {value}");
            cell.Patterns.Value.Pattern.SetValue(value);
            _logger.Info($"Cell value set to: {cell.Name}");
        }

        /// <summary>
        /// Получает количество строк в таблице
        /// </summary>
        public static int GetRowCount(this DataGridView automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var dataGrid = automationElement.EnsureDataGridView();
            var count = dataGrid.Rows.Length;
            _logger.Info($"[{dataGrid.AutomationId}] Row count - [{count}]");
            return count;
        }

        /// <summary>
        /// Получает количество столбцов в таблице
        /// </summary>
        public static int GetColumnCount(this DataGridView automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var dataGrid = automationElement.EnsureDataGridView();
            var count = dataGrid.Rows.Length > 0 ? dataGrid.Rows[0].Cells.Length : 0;
            _logger.Info($"[{dataGrid.AutomationId}] Column count - [{count}]");
            return count;
        }

        /// <summary>
        /// Выбирает строку по индексу
        /// </summary>
        /// <param name="rowIndex">Индекс строки</param>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах</param>
        public static void SelectRow(this DataGridView automationElement, int rowIndex, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var dataGrid = automationElement.EnsureDataGridView();

            var rows = dataGrid.Rows;
            if (rowIndex < 0 || rowIndex >= rows.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(rowIndex), $"Индекс строки {rowIndex} вне диапазона [0, {rows.Length - 1}]");
            }

            var row = rows[rowIndex];
            if (!row.WaitUntilEnabled(timeoutMs))
            {
                throw new TimeoutException($"Строка {rowIndex} не стала активной за {timeoutMs}мс");
            }

            _logger.Info($"Selecting row: {rowIndex}");
            row.Patterns.SelectionItem.Pattern.Select();
            _logger.Info("Row selected");
        }

        /// <summary>
        /// Ожидает, пока количество строк не станет равным указанному
        /// </summary>
        /// <param name="expectedCount">Ожидаемое количество строк</param>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах</param>
        public static bool WaitUntilRowCount(this DataGridView automationElement, int expectedCount, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var dataGrid = automationElement.EnsureDataGridView();
            
            var result = Retry.WhileFalse(
                () => dataGrid.Rows.Length == expectedCount,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
                
            _logger.Info($"[{dataGrid.AutomationId}] Wait until row count equals {expectedCount} result - [{result}]");
            return result;
        }

        /// <summary>
        /// Получает заголовки столбцов
        /// </summary>
        public static IEnumerable<string> GetColumnHeaders(this DataGridView automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var dataGrid = automationElement.EnsureDataGridView();
            
            var headers = new List<string>();
            if (dataGrid.Rows.Length > 0)
            {
                headers = dataGrid.Rows[0].Cells.Select(c => c.Name).ToList();
            }
            
            _logger.Info($"[{dataGrid.AutomationId}] Column headers - [{string.Join(", ", headers)}]");
            return headers;
        }

        /// <summary>
        /// Проверяет, активна ли таблица
        /// </summary>
        public static bool IsDataGridEnabled(this DataGridView automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var dataGrid = automationElement.EnsureDataGridView();
            var state = dataGrid.IsEnabled;
            _logger.Info($"[{dataGrid.AutomationId}] is [{state}]");
            return state;
        }

        /// <summary>
        /// Проверяет, видима ли таблица
        /// </summary>
        public static bool IsDataGridVisible(this DataGridView automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var dataGrid = automationElement.EnsureDataGridView();
            var isVisible = !dataGrid.IsOffscreen;
            _logger.Info($"[{dataGrid.AutomationId}] IsVisible - [{isVisible}]");
            return isVisible;
        }

        /// <summary>
        /// Ожидает, пока таблица не станет активной
        /// </summary>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах</param>
        public static bool WaitUntilEnabled(this DataGridView automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var dataGrid = automationElement.EnsureDataGridView();
            
            var result = Retry.WhileFalse(
                () => dataGrid.IsEnabled,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
                
            _logger.Info($"[{dataGrid.AutomationId}] Wait until enabled result - [{result}]");
            return result;
        }

        /// <summary>
        /// Проверяет, содержит ли таблица строки
        /// </summary>
        public static bool HasRows(this DataGridView automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var dataGrid = automationElement.EnsureDataGridView();
            var hasRows = dataGrid.Rows.Length > 0;
            _logger.Info($"[{dataGrid.AutomationId}] Has rows - [{hasRows}]");
            return hasRows;
        }

        /// <summary>
        /// Ожидает появления строк в таблице
        /// </summary>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах</param>
        public static bool WaitUntilHasRows(this DataGridView automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var dataGrid = automationElement.EnsureDataGridView();
            
            var result = Retry.WhileFalse(
                () => dataGrid.Rows.Length > 0,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
                
            _logger.Info($"[{dataGrid.AutomationId}] Wait until has rows result - [{result}]");
            return result;
        }
    }
} 