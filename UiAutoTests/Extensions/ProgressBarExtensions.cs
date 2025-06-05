using FlaUI.Core.AutomationElements;
using FlaUI.Core.Tools;
using NLog;
using UiAutoTests.Helpers;

namespace UiAutoTests.Extensions
{
    public static class ProgressBarExtensions
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private static readonly LoggerHelper _loggerHelper = new();

        /// <summary>
        /// Проверяет, что элемент является ProgressBar
        /// </summary>
        public static bool IsProgressBar(this AutomationElement element)
        {
            _loggerHelper.LogEnteringTheMethod();
            var isProgressBar = element.ControlType == FlaUI.Core.Definitions.ControlType.ProgressBar;
            _logger.Info($"[{element.AutomationId}] IsProgressBar - [{isProgressBar}]");
            return isProgressBar;
        }

        /// <summary>
        /// Получить текущее значение ProgressBar
        /// </summary>
        public static double GetValue(this ProgressBar progressBar)
        {
            _loggerHelper.LogEnteringTheMethod();

            var bar = progressBar.EnsureProgressBar();
            var value = bar.Value;
            _logger.Info($"[{bar.AutomationId}] Current value: {value}");
            return value;
        }

        /// <summary>
        /// Получить минимальное значение ProgressBar
        /// </summary>
        public static double GetMinimum(this ProgressBar progressBar)
        {
            _loggerHelper.LogEnteringTheMethod();

            var bar = progressBar.EnsureProgressBar();
            var min = bar.Minimum;
            _logger.Info($"[{bar.AutomationId}] Minimum value: {min}");
            return min;
        }

        /// <summary>
        /// Получить максимальное значение ProgressBar
        /// </summary>
        public static double GetMaximum(this ProgressBar progressBar)
        {
            _loggerHelper.LogEnteringTheMethod();

            var bar = progressBar.EnsureProgressBar();
            var max = bar.Maximum;
            _logger.Info($"[{bar.AutomationId}] Maximum value: {max}");
            return max;
        }

        /// <summary>
        /// Проверить, виден ли ProgressBar
        /// </summary>
        public static bool IsVisible(this ProgressBar progressBar)
        {
            _loggerHelper.LogEnteringTheMethod();

            var bar = progressBar.EnsureProgressBar();
            var isVisible = !bar.IsOffscreen;
            _logger.Info($"[{bar.AutomationId}] IsVisible - {isVisible}");
            return isVisible;
        }

        /// <summary>
        /// Ожидать, пока значение ProgressBar достигнет указанного значения
        /// </summary>
        public static bool WaitUntilValueIs(this ProgressBar progressBar, double expectedValue, int timeoutMs = 20000)
        {
            _loggerHelper.LogEnteringTheMethod();

            var bar = progressBar.EnsureProgressBar();
            var result = Retry.WhileFalse(
                () => Math.Abs(bar.Value - expectedValue) < 0.001,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{bar.AutomationId}] WaitUntilValueIs({expectedValue}) result: {result}");
            return result;
        }

        /// <summary>
        /// Проверить, находится ли ProgressBar на 100%
        /// </summary>
        public static bool IsComplete(this ProgressBar progressBar)
        {
            _loggerHelper.LogEnteringTheMethod();

            var bar = progressBar.EnsureProgressBar();
            var isFull = Math.Abs(bar.Value - bar.Maximum) < 0.001;
            _logger.Info($"[{bar.AutomationId}] IsComplete: {isFull}");
            return isFull;
        }
    }
}
