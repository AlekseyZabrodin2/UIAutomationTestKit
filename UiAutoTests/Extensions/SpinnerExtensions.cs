using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiAutoTests.Helpers;

namespace UiAutoTests.Extensions
{
    public static class SpinnerExtensions
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private static readonly LoggerHelper _loggerHelper = new();

        /// <summary>
        /// Проверяет, что элемент является Spinner
        /// </summary>
        public static bool IsSpinner(this AutomationElement element)
        {
            _loggerHelper.LogEnteringTheMethod();
            var isSpinner = element.ControlType == ControlType.Spinner;
            _logger.Info($"[{element.AutomationId}] IsSpinner - [{isSpinner}]");
            return isSpinner;
        }

        /// <summary>
        /// Проверяет, что Spinner имеет точное значение
        /// </summary>
        public static bool IsValueEqual(this Spinner spinner, double expectedValue)
        {
            _loggerHelper.LogEnteringTheMethod();
            var sp = spinner.EnsureSpinner();

            var result = sp.Value == expectedValue;
            _logger.Info($"[{sp.AutomationId}] IsValueEqual - expected: {expectedValue}, actual: {sp.Value}, result: {result}");
            return result;
        }

        /// <summary>
        /// Проверяет, что значение Spinner больше или равно порогу
        /// </summary>
        public static bool IsValueGreaterOrEqual(this Spinner spinner, double threshold)
        {
            _loggerHelper.LogEnteringTheMethod();
            var sp = spinner.EnsureSpinner();

            var result = sp.Value >= threshold;
            _logger.Info($"[{sp.AutomationId}] IsValueGreaterOrEqual - threshold: {threshold}, actual: {sp.Value}, result: {result}");
            return result;
        }

        /// <summary>
        /// Проверяет, что значение Spinner меньше порога
        /// </summary>
        public static bool IsValueLessThan(this Spinner spinner, double threshold)
        {
            _loggerHelper.LogEnteringTheMethod();
            var sp = spinner.EnsureSpinner();

            var result = sp.Value < threshold;
            _logger.Info($"[{sp.AutomationId}] IsValueLessThan - threshold: {threshold}, actual: {sp.Value}, result: {result}");
            return result;
        }

        /// <summary>
        /// Проверяет, что Spinner включен
        /// </summary>
        public static bool IsEnabled(this Spinner spinner)
        {
            _loggerHelper.LogEnteringTheMethod();
            var sp = spinner.EnsureSpinner();

            var result = sp.IsEnabled;
            _logger.Info($"[{sp.AutomationId}] IsEnabled - [{result}]");
            return result;
        }

        /// <summary>
        /// Проверяет, что Spinner выключен
        /// </summary>
        public static bool IsDisabled(this Spinner spinner)
        {
            _loggerHelper.LogEnteringTheMethod();
            var sp = spinner.EnsureSpinner();

            var result = !sp.IsEnabled;
            _logger.Info($"[{sp.AutomationId}] IsDisabled - [{result}]");
            return result;
        }

        /// <summary>
        /// Проверяет, что Spinner видим (не скрыт)
        /// </summary>
        public static bool IsVisible(this Spinner spinner)
        {
            _loggerHelper.LogEnteringTheMethod();
            var sp = spinner.EnsureSpinner();

            var result = !sp.IsOffscreen;
            _logger.Info($"[{sp.AutomationId}] IsVisible - [{result}]");
            return result;
        }
    }
}
