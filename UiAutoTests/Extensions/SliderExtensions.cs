using FlaUI.Core.AutomationElements;
using FlaUI.Core.Tools;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiAutoTests.Helpers;

namespace UiAutoTests.Extensions
{
    public static class SliderExtensions
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private static readonly LoggerHelper _loggerHelper = new();

        /// <summary>
        /// Проверяет, является ли элемент слайдером
        /// </summary>
        public static bool IsSlider(this AutomationElement element)
        {
            _loggerHelper.LogEnteringTheMethod();
            var isSlider = element.ControlType == FlaUI.Core.Definitions.ControlType.Slider;
            _logger.Info($"[{element.AutomationId}] IsSlider - [{isSlider}]");
            return isSlider;
        }

        /// <summary>
        /// Получить текущее значение слайдера
        /// </summary>
        public static double GetValue(this Slider slider)
        {
            _loggerHelper.LogEnteringTheMethod();

            var s = slider.EnsureSlider();
            var value = s.Value;
            _logger.Info($"[{s.AutomationId}] Current slider value: {value}");
            return value;
        }

        /// <summary>
        /// Установить значение слайдера
        /// </summary>
        public static void SetValue(this Slider slider, double newValue)
        {
            _loggerHelper.LogEnteringTheMethod();

            var s = slider.EnsureSlider();
            s.Value = newValue;
            _logger.Info($"[{s.AutomationId}] Set slider value to: {newValue}");
        }

        /// <summary>
        /// Получить минимальное значение слайдера
        /// </summary>
        public static double GetMinimum(this Slider slider)
        {
            _loggerHelper.LogEnteringTheMethod();

            var s = slider.EnsureSlider();
            var min = s.Minimum;
            _logger.Info($"[{s.AutomationId}] Minimum slider value: {min}");
            return min;
        }

        /// <summary>
        /// Получить максимальное значение слайдера
        /// </summary>
        public static double GetMaximum(this Slider slider)
        {
            _loggerHelper.LogEnteringTheMethod();

            var s = slider.EnsureSlider();
            var max = s.Maximum;
            _logger.Info($"[{s.AutomationId}] Maximum slider value: {max}");
            return max;
        }

        /// <summary>
        /// Ожидать, пока слайдер получит определённое значение
        /// </summary>
        public static bool WaitUntilValueIs(this Slider slider, double expectedValue, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();

            var s = slider.EnsureSlider();
            var result = Retry.WhileFalse(() => Math.Abs(s.Value - expectedValue) < 0.001,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{s.AutomationId}] WaitUntilValueIs({expectedValue}) result: {result}");
            return result;
        }

        /// <summary>
        /// Проверить, доступен ли слайдер
        /// </summary>
        public static bool IsEnabled(this Slider slider)
        {
            _loggerHelper.LogEnteringTheMethod();

            var s = slider.EnsureSlider();
            var isEnabled = s.IsEnabled;
            _logger.Info($"[{s.AutomationId}] IsEnabled - {isEnabled}");
            return isEnabled;
        }

        /// <summary>
        /// Проверить, видим ли слайдер
        /// </summary>
        public static bool IsVisible(this Slider slider)
        {
            _loggerHelper.LogEnteringTheMethod();

            var s = slider.EnsureSlider();
            var visible = !s.IsOffscreen;
            _logger.Info($"[{s.AutomationId}] IsVisible - {visible}");
            return visible;
        }
    }
}
