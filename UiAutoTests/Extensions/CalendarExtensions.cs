using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Tools;
using NLog;
using UiAutoTests.Helpers;

namespace UiAutoTests.Extensions
{
    public static class CalendarExtensions
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private static readonly LoggerHelper _loggerHelper = new();

        /// <summary>
        /// Проверяет, является ли элемент календарем
        /// </summary>
        public static bool IsCalendar(this AutomationElement element)
        {
            _loggerHelper.LogEnteringTheMethod();
            var isCalendar = element.ControlType == ControlType.Calendar;
            _logger.Info($"[{element.AutomationId}] IsCalendar - [{isCalendar}]");
            return isCalendar;
        }

        /// <summary>
        /// Выбирает дату в календаре
        /// </summary>
        public static void SelectDate(this Calendar calendar, DateTime date)
        {
            _loggerHelper.LogEnteringTheMethod();
            var calendarElement = calendar.EnsureCalendar();

            if (!calendarElement.IsEnabled)
                throw new InvalidOperationException("Calendar is disabled");

            _logger.Info($"Selecting date: {date.ToShortDateString()}");
            calendarElement.Patterns.Value.Pattern.SetValue(date.ToShortDateString());
            _logger.Info("Date selected");
        }

        /// <summary>
        /// Получает выбранную дату из календаря
        /// </summary>
        public static DateTime GetSelectedDate(this Calendar calendar)
        {
            _loggerHelper.LogEnteringTheMethod();
            var calendarElement = calendar.EnsureCalendar();

            var selectedDate = DateTime.Parse(calendarElement.Patterns.Value.Pattern.Value);
            _logger.Info($"Selected date: {selectedDate.ToShortDateString()}");
            return selectedDate;
        }

        /// <summary>
        /// Проверяет, активен ли календарь
        /// </summary>
        public static bool IsCalendarEnabled(this Calendar calendar)
        {
            _loggerHelper.LogEnteringTheMethod();
            var calendarElement = calendar.EnsureCalendar();

            var isEnabled = calendarElement.IsEnabled;
            _logger.Info($"[{calendarElement.AutomationId}] IsEnabled - [{isEnabled}]");
            return isEnabled;
        }

        /// <summary>
        /// Проверяет, видим ли календарь
        /// </summary>
        public static bool IsCalendarVisible(this Calendar calendar)
        {
            _loggerHelper.LogEnteringTheMethod();
            var calendarElement = calendar.EnsureCalendar();

            var isVisible = !calendarElement.IsOffscreen;
            _logger.Info($"[{calendarElement.AutomationId}] IsVisible - [{isVisible}]");
            return isVisible;
        }

        /// <summary>
        /// Ожидает, пока календарь не станет активным
        /// </summary>
        public static bool WaitUntilEnabled(this Calendar calendar, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var calendarElement = calendar.EnsureCalendar();

            var result = Retry.WhileFalse(
                () => calendarElement.IsEnabled,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{calendarElement.AutomationId}] Wait until enabled result - [{result}]");
            return result;
        }

        /// <summary>
        /// Ожидает, пока не будет выбрана дата в календаре
        /// </summary>
        public static bool WaitUntilDateSelected(this Calendar calendar, DateTime expectedDate, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var calendarElement = calendar.EnsureCalendar();

            var result = Retry.WhileFalse(
                () => DateTime.Parse(calendarElement.Patterns.Value.Pattern.Value) == expectedDate,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            _logger.Info($"[{calendarElement.AutomationId}] Wait until date selected result - [{result}]");
            return result;
        }

        /// <summary>
        /// Переключает месяц в календаре
        /// </summary>
        public static void SwitchMonth(this Calendar calendar, int monthsToAdd)
        {
            _loggerHelper.LogEnteringTheMethod();
            var calendarElement = calendar.EnsureCalendar();

            _logger.Info($"Switching month by {monthsToAdd} months");
            var currentDate = DateTime.Parse(calendarElement.Patterns.Value.Pattern.Value);
            calendarElement.Patterns.Value.Pattern.SetValue(currentDate.AddMonths(monthsToAdd).ToShortDateString());
            _logger.Info("Month switched");
        }
    }
} 