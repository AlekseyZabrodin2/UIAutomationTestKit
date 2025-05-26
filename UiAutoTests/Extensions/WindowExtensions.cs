using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Tools;
using NLog;
using UiAutoTests.Helpers;

namespace UiAutoTests.Extensions
{
    public static class WindowExtensions
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private static readonly LoggerHelper _loggerHelper = new();

        /// <summary>
        /// Проверяет, видно ли окно (не offscreen).
        /// </summary>
        public static bool IsVisible(this Window automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var window = automationElement.EnsureWindow();
            var isVisible = !window.IsOffscreen;
            _logger.Info($"[{window.AutomationId}] IsVisible - [{isVisible}]");
            return isVisible;
        }

        /// <summary>
        /// Активирует окно, если оно включено.
        /// </summary>
        /// <exception cref="InvalidOperationException">Выбрасывается, если окно отключено.</exception>
        public static void ActivateWindow(this Window automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var window = automationElement.EnsureWindow();

            if (!window.IsEnabled)
            {
                _logger.Warn($"[{window.AutomationId}] Window is disabled and cannot be activated");
                throw new InvalidOperationException("Window is disabled");
            }

            _logger.Info($"Activating window [{window.AutomationId}]");
            window.Focus();
            _logger.Info("Window activated");
        }

        /// <summary>
        /// Закрывает окно, если оно включено.
        /// </summary>
        /// <exception cref="InvalidOperationException">Выбрасывается, если окно отключено.</exception>
        public static void CloseWindow(this Window automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var window = automationElement.EnsureWindow();

            if (!window.IsEnabled)
            {
                _logger.Warn($"[{window.AutomationId}] Window is disabled and cannot be closed");
                throw new InvalidOperationException("Window is disabled");
            }

            _logger.Info($"Closing window [{window.AutomationId}]");
            window.Close();
            _logger.Info("Window closed");
        }

        /// <summary>
        /// Проверяет, находится ли окно в максимизированном состоянии.
        /// </summary>
        public static bool IsMaximized(this Window automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var window = automationElement.EnsureWindow();
            var visualState = window.Patterns.Window.Pattern.WindowVisualState.Value;
            var isMaximized = visualState == WindowVisualState.Maximized;
            _logger.Info($"[{window.AutomationId}] IsMaximized - [{isMaximized}]");
            return isMaximized;
        }

        /// <summary>
        /// Проверяет, находится ли окно в минимизированном состоянии.
        /// </summary>
        public static bool IsMinimized(this Window automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var window = automationElement.EnsureWindow();
            var visualState = window.Patterns.Window.Pattern.WindowVisualState.Value;
            var isMinimized = visualState == WindowVisualState.Minimized;
            _logger.Info($"[{window.AutomationId}] IsMinimized - [{isMinimized}]");
            return isMinimized;
        }

        /// <summary>
        /// Ожидает, пока окно станет видимым (не offscreen) в пределах заданного таймаута.
        /// </summary>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах.</param>
        /// <returns>true, если окно стало видимым за время ожидания; иначе false.</returns>
        public static bool WaitUntilVisible(this Window automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var window = automationElement.EnsureWindow();

            var result = Retry.WhileFalse(
                () => !window.IsOffscreen,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{window.AutomationId}] WaitUntilVisible result - [{result}]");
            return result;
        }
    }
}
