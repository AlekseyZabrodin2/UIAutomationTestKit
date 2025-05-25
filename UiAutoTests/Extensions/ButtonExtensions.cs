using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Tools;
using NLog;
using UiAutoTests.Helpers;

namespace UiAutoTests.Extensions
{
    public static class ButtonExtensions
    {
        private static LoggerHelper _loggerHelper = new();
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Проверяет, активна ли кнопка
        /// </summary>
        public static bool IsButtonEnabled(this Button automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var button = automationElement.EnsureButton();
            _logger.Info($"[{button.AutomationId}] IsEnabled - [{button.IsEnabled}]");
            return button.IsEnabled;
        }

        /// <summary>
        /// Кликает по кнопке с ожиданием кликабельности
        /// </summary>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах</param>
        public static void ClickButton(this Button automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var button = automationElement.EnsureButton();
            
            if (!button.WaitUntilClickable(timeoutMs))
            {
                throw new TimeoutException($"Кнопка {button.AutomationId} не стала кликабельной за {timeoutMs}мс");
            }
            
            button.Invoke();
            _logger.Info($"[{button.AutomationId}] is Invoked");
        }

        /// <summary>
        /// Получает текст кнопки
        /// </summary>
        public static string GetButtonText(this Button automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var button = automationElement.EnsureButton();
            var text = button.Name;
            _logger.Info($"[{button.AutomationId}] Text - [{text}]");
            return text;
        }

        /// <summary>
        /// Проверяет, видима ли кнопка
        /// </summary>
        public static bool IsButtonVisible(this Button automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var button = automationElement.EnsureButton();
            var isVisible = !button.IsOffscreen;
            _logger.Info($"[{button.AutomationId}] IsVisible - [{isVisible}]");
            return isVisible;
        }

        /// <summary>
        /// Ожидает появления указанного текста на кнопке
        /// </summary>
        /// <param name="expectedText">Ожидаемый текст</param>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах</param>
        public static bool WaitUntilButtonText(this Button automationElement, string expectedText, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var button = automationElement.EnsureButton();
            
            var result = Retry.WhileFalse(
                () => button.Name == expectedText,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
                
            _logger.Info($"[{button.AutomationId}] Text wait result - [{result}]");
            return result;
        }

        /// <summary>
        /// Проверяет, нажата ли кнопка
        /// </summary>
        public static bool IsButtonPressed(this Button automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var button = automationElement.EnsureButton();
            var isPressed = button.Patterns.Toggle.Pattern.ToggleState == ToggleState.On;
            _logger.Info($"[{button.AutomationId}] IsPressed - [{isPressed}]");
            return isPressed;
        }

        /// <summary>
        /// Выполняет двойной клик по кнопке с ожиданием кликабельности
        /// </summary>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах</param>
        public static void DoubleClickButton(this Button automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var button = automationElement.EnsureButton();
            
            if (!button.WaitUntilClickable(timeoutMs))
            {
                throw new TimeoutException($"Кнопка {button.AutomationId} не стала кликабельной за {timeoutMs}мс");
            }
            
            button.DoubleClick();
            _logger.Info($"[{button.AutomationId}] is DoubleClicked");
        }

        /// <summary>
        /// Проверяет, содержит ли кнопка указанный текст
        /// </summary>
        /// <param name="text">Текст для поиска</param>
        public static bool ContainsText(this Button automationElement, string text)
        {
            _loggerHelper.LogEnteringTheMethod();
            var button = automationElement.EnsureButton();
            var contains = button.Name.Contains(text);
            _logger.Info($"[{button.AutomationId}] Contains text '{text}' - [{contains}]");
            return contains;
        }
    }
}
