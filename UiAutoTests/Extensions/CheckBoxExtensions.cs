using FlaUI.Core.AutomationElements;
using FlaUI.Core.Tools;
using NLog;
using UiAutoTests.Helpers;

namespace UiAutoTests.Extensions
{
    public static class CheckBoxExtensions
    {
        private static LoggerHelper _loggerHelper = new();
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Устанавливает чекбокс в отмеченное состояние
        /// </summary>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах</param>
        public static void CheckBoxChecked(this CheckBox automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var checkBox = automationElement.EnsureCheckBox();

            if (!checkBox.WaitUntilEnabled(timeoutMs))
            {
                throw new TimeoutException($"Чекбокс {checkBox.AutomationId} не стал активным за {timeoutMs}мс");
            }

            _logger.Info("current state = [{0}]", checkBox.IsChecked);
            if (checkBox.IsChecked != true)
            {
                checkBox.IsChecked = true;
                _logger.Info("CheckBox set to Checked.");
            }
            else
            {
                _logger.Info("CheckBox is already Checked.");
            }
        }

        /// <summary>
        /// Устанавливает чекбокс в неотмеченное состояние
        /// </summary>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах</param>
        public static void CheckBoxUnchecked(this CheckBox automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var checkBox = automationElement.EnsureCheckBox();

            if (!checkBox.WaitUntilEnabled(timeoutMs))
            {
                throw new TimeoutException($"Чекбокс {checkBox.AutomationId} не стал активным за {timeoutMs}мс");
            }

            _logger.Info("current state = [{0}]", checkBox.IsChecked);
            if (checkBox.IsChecked != false)
            {
                checkBox.IsChecked = false;
                _logger.Info("CheckBox set to Unchecked.");
            }
            else
            {
                _logger.Info("CheckBox is already Unchecked.");
            }
        }

        /// <summary>
        /// Проверяет, активен ли чекбокс
        /// </summary>
        public static bool CheckBoxIsEnabled(this CheckBox automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var checkBox = automationElement.EnsureCheckBox();
            var state = checkBox.IsEnabled;
            _logger.Info($"[{checkBox.AutomationId}] is [{state}]");
            return state;
        }

        /// <summary>
        /// Проверяет, отмечен ли чекбокс
        /// </summary>
        public static bool CheckBoxIsChecked(this CheckBox automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var checkBox = automationElement.EnsureCheckBox();
            bool? state = checkBox.IsChecked;
            _logger.Info($"[{checkBox.AutomationId}] is [{state}]");
            return state == true;
        }

        /// <summary>
        /// Проверяет, видим ли чекбокс
        /// </summary>
        public static bool IsCheckBoxVisible(this CheckBox automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var checkBox = automationElement.EnsureCheckBox();
            var isVisible = !checkBox.IsOffscreen;
            _logger.Info($"[{checkBox.AutomationId}] IsVisible - [{isVisible}]");
            return isVisible;
        }

        /// <summary>
        /// Ожидает, пока чекбокс не станет отмеченным
        /// </summary>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах</param>
        public static bool WaitUntilChecked(this CheckBox automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var checkBox = automationElement.EnsureCheckBox();
            
            var result = Retry.WhileFalse(
                () => checkBox.IsChecked == true,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
                
            _logger.Info($"[{checkBox.AutomationId}] Wait until checked result - [{result}]");
            return result;
        }

        /// <summary>
        /// Ожидает, пока чекбокс не станет неотмеченным
        /// </summary>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах</param>
        public static bool WaitUntilUnchecked(this CheckBox automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var checkBox = automationElement.EnsureCheckBox();
            
            var result = Retry.WhileFalse(
                () => checkBox.IsChecked == false,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
                
            _logger.Info($"[{checkBox.AutomationId}] Wait until unchecked result - [{result}]");
            return result;
        }

        /// <summary>
        /// Переключает состояние чекбокса
        /// </summary>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах</param>
        public static void ToggleCheckBox(this CheckBox automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var checkBox = automationElement.EnsureCheckBox();

            if (!checkBox.WaitUntilEnabled(timeoutMs))
            {
                throw new TimeoutException($"Чекбокс {checkBox.AutomationId} не стал активным за {timeoutMs}мс");
            }

            checkBox.Toggle();
            _logger.Info($"[{checkBox.AutomationId}] Toggled to [{checkBox.IsChecked}]");
        }

        /// <summary>
        /// Получает текст чекбокса
        /// </summary>
        public static string GetCheckBoxText(this CheckBox automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var checkBox = automationElement.EnsureCheckBox();
            var text = checkBox.Name;
            _logger.Info($"[{checkBox.AutomationId}] Text - [{text}]");
            return text;
        }

        /// <summary>
        /// Проверяет, содержит ли чекбокс указанный текст
        /// </summary>
        /// <param name="text">Текст для поиска</param>
        public static bool ContainsText(this CheckBox automationElement, string text)
        {
            _loggerHelper.LogEnteringTheMethod();
            var checkBox = automationElement.EnsureCheckBox();
            var contains = checkBox.Name.Contains(text);
            _logger.Info($"[{checkBox.AutomationId}] Contains text '{text}' - [{contains}]");
            return contains;
        }
    }
}
