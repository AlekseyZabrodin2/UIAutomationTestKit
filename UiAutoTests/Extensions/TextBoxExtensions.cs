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
    public static class TextBoxExtensions
    {
        private static LoggerHelper _loggerHelper = new();
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Вводит текст в текстовое поле
        /// </summary>
        /// <param name="text">Текст для ввода</param>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах</param>
        public static void EnterText(this TextBox automationElement, string text, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var textBox = automationElement.EnsureTextBox();

            if (!textBox.WaitUntilEnabled(timeoutMs))
            {
                throw new TimeoutException($"Текстовое поле {textBox.AutomationId} не стало активным за {timeoutMs}мс");
            }

            _logger.Info($"Entering text: {text}");
            textBox.Text = text;
            _logger.Info($"Text entered: {textBox.Text}");
        }

        /// <summary>
        /// Очищает текст в текстовом поле
        /// </summary>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах</param>
        public static void ClearText(this TextBox automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var textBox = automationElement.EnsureTextBox();

            if (!textBox.WaitUntilEnabled(timeoutMs))
            {
                throw new TimeoutException($"Текстовое поле {textBox.AutomationId} не стало активным за {timeoutMs}мс");
            }

            _logger.Info("Clearing text");
            textBox.Text = string.Empty;
            _logger.Info("Text cleared");
        }

        /// <summary>
        /// Проверяет, активно ли текстовое поле
        /// </summary>
        public static bool IsTextBoxEnabled(this TextBox automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var textBox = automationElement.EnsureTextBox();
            var state = textBox.IsEnabled;
            _logger.Info($"[{textBox.AutomationId}] is [{state}]");
            return state;
        }

        /// <summary>
        /// Проверяет, видимо ли текстовое поле
        /// </summary>
        public static bool IsTextBoxVisible(this TextBox automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var textBox = automationElement.EnsureTextBox();
            var isVisible = !textBox.IsOffscreen;
            _logger.Info($"[{textBox.AutomationId}] IsVisible - [{isVisible}]");
            return isVisible;
        }

        /// <summary>
        /// Проверяет, доступно ли текстовое поле только для чтения
        /// </summary>
        public static bool IsReadOnly(this TextBox automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var textBox = automationElement.EnsureTextBox();
            var isReadOnly = textBox.Patterns.Value.Pattern.IsReadOnly;
            _logger.Info($"[{textBox.AutomationId}] IsReadOnly - [{isReadOnly}]");
            return isReadOnly;
        }

        /// <summary>
        /// Ожидает, пока текстовое поле не станет активным
        /// </summary>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах</param>
        public static bool WaitUntilEnabled(this TextBox automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var textBox = automationElement.EnsureTextBox();
            
            var result = Retry.WhileFalse(
                () => textBox.IsEnabled,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
                
            _logger.Info($"[{textBox.AutomationId}] Wait until enabled result - [{result}]");
            return result;
        }

        /// <summary>
        /// Ожидает, пока текст в поле не станет равным указанному
        /// </summary>
        /// <param name="expectedText">Ожидаемый текст</param>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах</param>
        public static bool WaitUntilTextEquals(this TextBox automationElement, string expectedText, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var textBox = automationElement.EnsureTextBox();
            
            var result = Retry.WhileFalse(
                () => textBox.Text == expectedText,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
                
            _logger.Info($"[{textBox.AutomationId}] Wait until text equals '{expectedText}' result - [{result}]");
            return result;
        }

        /// <summary>
        /// Ожидает, пока текст в поле не станет пустым
        /// </summary>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах</param>
        public static bool WaitUntilEmpty(this TextBox automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var textBox = automationElement.EnsureTextBox();
            
            var result = Retry.WhileFalse(
                () => string.IsNullOrEmpty(textBox.Text),
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
                
            _logger.Info($"[{textBox.AutomationId}] Wait until empty result - [{result}]");
            return result;
        }

        /// <summary>
        /// Ожидает, пока текст в поле не станет непустым
        /// </summary>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах</param>
        public static bool WaitUntilNotEmpty(this TextBox automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var textBox = automationElement.EnsureTextBox();
            
            var result = Retry.WhileFalse(
                () => !string.IsNullOrEmpty(textBox.Text),
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
                
            _logger.Info($"[{textBox.AutomationId}] Wait until not empty result - [{result}]");
            return result;
        }

        /// <summary>
        /// Проверяет, содержит ли текстовое поле указанный текст
        /// </summary>
        /// <param name="text">Текст для поиска</param>
        public static bool ContainsText(this TextBox automationElement, string text)
        {
            _loggerHelper.LogEnteringTheMethod();
            var textBox = automationElement.EnsureTextBox();
            var contains = textBox.Text.Contains(text);
            _logger.Info($"[{textBox.AutomationId}] Contains text '{text}' - [{contains}]");
            return contains;
        }

        /// <summary>
        /// Проверяет, начинается ли текст в поле с указанной строки
        /// </summary>
        /// <param name="text">Текст для проверки</param>
        public static bool StartsWith(this TextBox automationElement, string text)
        {
            _loggerHelper.LogEnteringTheMethod();
            var textBox = automationElement.EnsureTextBox();
            var startsWith = textBox.Text.StartsWith(text);
            _logger.Info($"[{textBox.AutomationId}] Starts with '{text}' - [{startsWith}]");
            return startsWith;
        }

        /// <summary>
        /// Проверяет, заканчивается ли текст в поле указанной строкой
        /// </summary>
        /// <param name="text">Текст для проверки</param>
        public static bool EndsWith(this TextBox automationElement, string text)
        {
            _loggerHelper.LogEnteringTheMethod();
            var textBox = automationElement.EnsureTextBox();
            var endsWith = textBox.Text.EndsWith(text);
            _logger.Info($"[{textBox.AutomationId}] Ends with '{text}' - [{endsWith}]");
            return endsWith;
        }

        /// <summary>
        /// Получает длину текста в поле
        /// </summary>
        public static int GetTextLength(this TextBox automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var textBox = automationElement.EnsureTextBox();
            var length = textBox.Text.Length;
            _logger.Info($"[{textBox.AutomationId}] Text length - [{length}]");
            return length;
        }

        /// <summary>
        /// Проверяет, соответствует ли текст в поле регулярному выражению
        /// </summary>
        /// <param name="pattern">Регулярное выражение</param>
        public static bool MatchesPattern(this TextBox automationElement, string pattern)
        {
            _loggerHelper.LogEnteringTheMethod();
            var textBox = automationElement.EnsureTextBox();
            var matches = System.Text.RegularExpressions.Regex.IsMatch(textBox.Text, pattern);
            _logger.Info($"[{textBox.AutomationId}] Matches pattern '{pattern}' - [{matches}]");
            return matches;
        }

        /// <summary>
        /// Устанавливает фокус на текстовое поле и позиционирует курсор
        /// </summary>
        public static void FocusTextBoxAndSetCursor(this TextBox automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var textBox = automationElement.EnsureTextBox();
            textBox.Focus();
            _logger.Info("TextBox получил фокус");
        }

        /// <summary>
        /// Получает плейсхолдер текстового поля
        /// </summary>
        public static string GetPlaceholder(this TextBox automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var textBox = automationElement.EnsureTextBox();
            var placeholder = textBox.HelpText ?? string.Empty;
            _logger.Info($"Получен placeholder: \"{placeholder}\"");
            return placeholder;
        }

        /// <summary>
        /// Получает текущий текст из текстового поля
        /// </summary>
        public static string GetText(this TextBox automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var textBox = automationElement.EnsureTextBox();
            var currentText = textBox.Text;
            _logger.Info($"Получен текст: \"{currentText}\"");
            return currentText;
        }
    }
}
