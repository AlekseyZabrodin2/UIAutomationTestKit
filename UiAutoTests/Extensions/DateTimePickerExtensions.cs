using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Tools;
using NLog;
using UiAutoTests.Helpers;

namespace UiAutoTests.Extensions
{
    public static class DateTimePickerExtensions
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private static readonly LoggerHelper _loggerHelper = new();

        /// <summary>
        /// Проверяет, является ли элемент элементом выбора даты и времени
        /// </summary>
        public static bool IsDateTimePicker(this AutomationElement automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var isDateTimePicker = automationElement.ControlType == ControlType.Custom;
            _logger.Info($"[{automationElement.AutomationId}] IsDateTimePicker - [{isDateTimePicker}]");
            return isDateTimePicker;
        }

        /// <summary>
        /// Устанавливает дату и время
        /// </summary>
        public static void SetDateTime(this DateTimePicker automationElement, DateTime dateTime)
        {
            _loggerHelper.LogEnteringTheMethod();
            var dateTimePicker = automationElement.EnsureDateTimePicker();

            if (!dateTimePicker.IsEnabled)
                throw new InvalidOperationException("DateTimePicker is disabled");

            _logger.Info($"Setting date and time: {dateTime}");
            dateTimePicker.Patterns.Value.Pattern.SetValue(dateTime.ToString());
            _logger.Info("Date and time set");
        }

        /// <summary>
        /// Получает установленную дату и время
        /// </summary>
        public static DateTime GetDateTime(this DateTimePicker automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var dateTimePicker = automationElement.EnsureDateTimePicker();

            var dateTime = DateTime.Parse(dateTimePicker.Patterns.Value.Pattern.Value);
            _logger.Info($"[{dateTimePicker.AutomationId}] DateTime - [{dateTime}]");
            return dateTime;
        }

        /// <summary>
        /// Проверяет, активен ли элемент выбора даты и времени
        /// </summary>
        public static bool IsDateTimePickerEnabled(this DateTimePicker automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var dateTimePicker = automationElement.EnsureDateTimePicker();

            var isEnabled = dateTimePicker.IsEnabled;
            _logger.Info($"[{dateTimePicker.AutomationId}] IsEnabled - [{isEnabled}]");
            return isEnabled;
        }

        /// <summary>
        /// Проверяет, видим ли элемент выбора даты и времени
        /// </summary>
        public static bool IsDateTimePickerVisible(this DateTimePicker automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var dateTimePicker = automationElement.EnsureDateTimePicker();

            var isVisible = !dateTimePicker.IsOffscreen;
            _logger.Info($"[{dateTimePicker.AutomationId}] IsVisible - [{isVisible}]");
            return isVisible;
        }

        /// <summary>
        /// Ожидает, пока элемент выбора даты и времени не станет активным
        /// </summary>
        public static bool WaitUntilEnabled(this DateTimePicker automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var dateTimePicker = automationElement.EnsureDateTimePicker();

            var result = Retry.WhileFalse(
                () => dateTimePicker.IsEnabled,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{dateTimePicker.AutomationId}] Wait until enabled result - [{result}]");
            return result;
        }

        /// <summary>
        /// Проверяет, содержит ли элемент выбора даты и времени указанную дату
        /// </summary>
        public static bool ContainsDateTime(this DateTimePicker automationElement, DateTime expectedDateTime)
        {
            _loggerHelper.LogEnteringTheMethod();
            var dateTimePicker = automationElement.EnsureDateTimePicker();

            var currentDateTime = DateTime.Parse(dateTimePicker.Patterns.Value.Pattern.Value);
            var contains = currentDateTime == expectedDateTime;
            _logger.Info($"[{dateTimePicker.AutomationId}] Contains date time '{expectedDateTime}' - [{contains}]");
            return contains;
        }

        /// <summary>
        /// Ожидает, пока элемент выбора даты и времени не будет содержать указанную дату
        /// </summary>
        public static bool WaitUntilContainsDateTime(this DateTimePicker automationElement, DateTime expectedDateTime, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var dateTimePicker = automationElement.EnsureDateTimePicker();

            var result = Retry.WhileFalse(
                () => DateTime.Parse(dateTimePicker.Patterns.Value.Pattern.Value) == expectedDateTime,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{dateTimePicker.AutomationId}] Wait until contains date time '{expectedDateTime}' result - [{result}]");
            return result;
        }
    }
} 