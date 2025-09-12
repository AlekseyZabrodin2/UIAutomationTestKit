using FlaUI.Core.AutomationElements;
using NLog;
using UiAutoTests.Extensions;
using UiAutoTests.Helpers;

namespace UiAutoTests.Assertions
{
    public static class AssertForProgressBar
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private static readonly LoggerHelper _loggerHelper = new();

        /// <summary>
        /// Проверяет, что ProgressBar завершен (Value == Maximum)
        /// </summary>
        public static void ShouldBeComplete(this ProgressBar progressBar)
        {
            _loggerHelper.LogEnteringTheMethod();
            var bar = progressBar.EnsureProgressBar();

            _logger.Info($"[{bar.AutomationId}] ShouldBeComplete: Value={bar.Value}, Max={bar.Maximum}");

            AssertHelpers.AreEqual(bar.Maximum, bar.Value,
                $"ProgressBar '{bar.AutomationId}' должен быть полным. Value: {bar.Value}, Max: {bar.Maximum}");
        }
                
        /// <summary>
        /// Проверяет, что ProgressBar видим
        /// </summary>
        public static void ShouldBeVisible(this ProgressBar progressBar)
        {
            _loggerHelper.LogEnteringTheMethod();
            var bar = progressBar.EnsureProgressBar();

            _logger.Info($"[{bar.AutomationId}] ShouldBeVisible: IsOffscreen={bar.IsOffscreen}");

            AssertHelpers.IsTrue(!bar.IsOffscreen, $"ProgressBar '{bar.AutomationId}' должен быть видимым.");
        }

        /// <summary>
        /// Проверяет, что ProgressBar НЕ завершен (Value < Maximum)
        /// </summary>
        public static void ShouldNotBeComplete(this ProgressBar progressBar)
        {
            _loggerHelper.LogEnteringTheMethod();
            var bar = progressBar.EnsureProgressBar();

            _logger.Info($"[{bar.AutomationId}] ShouldNotBeComplete: Value={bar.Value}, Max={bar.Maximum}");

            AssertHelpers.IsTrue(bar.Value < bar.Maximum,
                $"ProgressBar '{bar.AutomationId}' не должен быть завершён. Текущее значение: {bar.Value}, максимум: {bar.Maximum}");
        }

        /// <summary>
        /// Проверяет, что значение ProgressBar >= заданного порога
        /// </summary>
        public static void ShouldBeGreaterOrEqual(this ProgressBar progressBar, double threshold)
        {
            _loggerHelper.LogEnteringTheMethod();
            var bar = progressBar.EnsureProgressBar();

            _logger.Info($"[{bar.AutomationId}] ShouldBeGreaterOrEqual: Value={bar.Value}, Threshold={threshold}");

            AssertHelpers.IsTrue(bar.Value >= threshold,
                $"ProgressBar '{bar.AutomationId}' должен быть >= {threshold}. Текущее значение: {bar.Value}");
        }

        /// <summary>
        /// Проверяет, что значение ProgressBar < заданного порога
        /// </summary>
        public static void ShouldBeLessThan(this ProgressBar progressBar, double threshold)
        {
            _loggerHelper.LogEnteringTheMethod();
            var bar = progressBar.EnsureProgressBar();

            _logger.Info($"[{bar.AutomationId}] ShouldBeLessThan: Value={bar.Value}, Threshold={threshold}");

            AssertHelpers.IsTrue(bar.Value < threshold,
                $"ProgressBar '{bar.AutomationId}' должен быть < {threshold}. Текущее значение: {bar.Value}");
        }

        /// <summary>
        /// Проверяет, что значение ProgressBar точно равно заданному значению
        /// </summary>
        public static void ShouldBeEqual(this ProgressBar progressBar, double expectedValue)
        {
            _loggerHelper.LogEnteringTheMethod();
            var bar = progressBar.EnsureProgressBar();

            _logger.Info($"[{bar.AutomationId}] ShouldBeEqual: Value={bar.Value}, Expected={expectedValue}");

            AssertHelpers.AreEqual(expectedValue, bar.Value,
                $"ProgressBar '{bar.AutomationId}' должен быть равен {expectedValue}. Текущее значение: {bar.Value}");
        }
    }
}
