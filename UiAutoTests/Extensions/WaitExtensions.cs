using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.Core.WindowsAPI;
using NLog;

namespace UiAutoTests.Extensions
{
    /// <summary>
    /// Класс, содержащий методы ожидания для UI элементов
    /// </summary>
    public static class WaitExtensions
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private const int DefaultTimeout = 5000; // 5 секунд по умолчанию
                
        /// <summary>
        /// Пауза на указанное время
        /// </summary>
        public static void Pause(int timeoutInSeconds)
        {
            _logger.Info($"Пауза {timeoutInSeconds} сек...");
            Retry.WhileTrue(() => true, TimeSpan.FromMilliseconds(timeoutInSeconds));
        }

        // ====== General Wait Helpers ======

        /// <summary>
        /// Ожидание появления элемента
        /// </summary>
        public static bool WaitUntilExists(this AutomationElement parent, Func<AutomationElement> findFunc, int timeoutMs = DefaultTimeout)
        {
            _logger.Info($"Ожидание появления элемента");
            var result = Retry.WhileNull(findFunc, TimeSpan.FromMilliseconds(timeoutMs));
            return result.Result != null;
        }

        /// <summary>
        /// Ожидание исчезновения элемента
        /// </summary>
        public static bool WaitUntilNotExists(this AutomationElement parent, Func<AutomationElement> findFunc, int timeoutMs = DefaultTimeout)
        {
            _logger.Info($"Ожидание исчезновения элемента");
            return Retry.WhileTrue(() => findFunc() != null, TimeSpan.FromMilliseconds(timeoutMs)).Success;
        }

        /// <summary>
        /// Ожидание видимости элемента
        /// </summary>
        public static bool WaitUntilVisible(this AutomationElement element, int timeoutMs = DefaultTimeout)
        {
            _logger.Info($"Ожидание видимости элемента: {element.Properties.AutomationId}");
            return Retry.WhileFalse(
                () => element?.IsOffscreen == false,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
        }

        /// <summary>
        /// Ожидание видимости элемента с логированием
        /// </summary>
        public static bool WaitUntilVisibleWithLog(this AutomationElement element, int timeoutMs = DefaultTimeout)
        {
            _logger.Info($"[WAIT] Ожидаем, пока элемент {element?.AutomationId} станет видимым");
            var result = Retry.WhileFalse(
                () => element?.IsOffscreen == false,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;

            if (result)
                _logger.Info("[WAIT] Элемент стал видимым");
            else
                _logger.Warn($"[WAIT] Элемент не стал видимым за {timeoutMs} мс.");

            return result;
        }

        /// <summary>
        /// Ожидание активности элемента
        /// </summary>
        public static bool WaitUntilEnabled(this AutomationElement element, int timeoutMs = DefaultTimeout)
        {
            _logger.Info($"Ожидание активности элемента: {element.Properties.AutomationId}");
            return Retry.WhileFalse(
                () => element?.IsEnabled ?? false,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
        }

        /// <summary>
        /// Ожидание неактивности элемента
        /// </summary>
        public static bool WaitUntilDisabled(this AutomationElement element, int timeoutMs = DefaultTimeout)
        {
            _logger.Info($"Ожидание неактивности элемента: {element.Properties.AutomationId}");
            return Retry.WhileTrue(
                () => element?.IsEnabled == true,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
        }

        /// <summary>
        /// Ожидание кликабельности элемента
        /// </summary>
        public static bool WaitUntilClickable(this AutomationElement element, int timeoutMs = DefaultTimeout)
        {
            _logger.Info($"Ожидание кликабельности элемента: {element.Properties.AutomationId}");
            return Retry.WhileFalse(
                () => element?.IsEnabled == true && element?.IsOffscreen == false,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
        }

        /// <summary>
        /// Ожидание появления текста в элементе
        /// </summary>
        public static bool WaitUntilTextAppears(this AutomationElement element, string expectedText, int timeoutMs = DefaultTimeout)
        {
            _logger.Info($"Ожидание появления текста '{expectedText}' в элементе: {element.Properties.AutomationId}");
            return Retry.WhileFalse(
                () => element?.AsTextBox()?.Text == expectedText,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
        }

        /// <summary>
        /// Ожидание исчезновения текста из элемента
        /// </summary>
        public static bool WaitUntilTextDisappears(this AutomationElement element, string textToDisappear, int timeoutMs = DefaultTimeout)
        {
            _logger.Info($"Ожидание исчезновения текста '{textToDisappear}' из элемента: {element.Properties.AutomationId}");
            return Retry.WhileFalse(
                () => element?.AsTextBox()?.Text != textToDisappear,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
        }

        /// <summary>
        /// Ожидание изменения состояния элемента
        /// </summary>
        public static bool WaitUntilStateChanges(this AutomationElement element, ToggleState expectedState, int timeoutMs = DefaultTimeout)
        {
            _logger.Info($"Ожидание изменения состояния элемента {element.Properties.AutomationId} на {expectedState}");
            return Retry.WhileFalse(
                () => element?.AsToggleButton()?.ToggleState == expectedState,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
        }

        /// <summary>
        /// Ожидание появления дочернего элемента
        /// </summary>
        public static bool WaitUntilChildAppears(this AutomationElement parent, string childAutomationId, int timeoutMs = DefaultTimeout)
        {
            _logger.Info($"Ожидание появления дочернего элемента {childAutomationId} в {parent.Properties.AutomationId}");
            return Retry.WhileFalse(
                () => parent?.FindFirstDescendant(cf => cf.ByAutomationId(childAutomationId)) != null,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
        }

        /// <summary>
        /// Ожидание исчезновения дочернего элемента
        /// </summary>
        public static bool WaitUntilChildDisappears(this AutomationElement parent, string childAutomationId, int timeoutMs = DefaultTimeout)
        {
            _logger.Info($"Ожидание исчезновения дочернего элемента {childAutomationId} из {parent.Properties.AutomationId}");
            return Retry.WhileFalse(
                () => parent?.FindFirstDescendant(cf => cf.ByAutomationId(childAutomationId)) == null,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
        }

        /// <summary>
        /// Ожидание завершения анимации
        /// </summary>
        public static void WaitForAnimation(int durationMs = 500)
        {
            _logger.Info($"Ожидание завершения анимации ({durationMs}мс)");
            Thread.Sleep(durationMs);
        }

        /// <summary>
        /// Ожидание с повторными попытками выполнения действия
        /// </summary>
        public static T RetryAction<T>(Func<T> action, int maxAttempts = 3, int delayBetweenAttemptsMs = 1000)
        {
            _logger.Info($"Попытка выполнения действия с {maxAttempts} попытками");
            for (int i = 0; i < maxAttempts; i++)
            {
                try
                {
                    return action();
                }
                catch (Exception ex)
                {
                    if (i == maxAttempts - 1)
                        throw;
                    _logger.Warn($"Попытка {i + 1} не удалась: {ex.Message}");
                    Thread.Sleep(delayBetweenAttemptsMs);
                }
            }
            throw new Exception("Не удалось выполнить действие после всех попыток");
        }

        // ====== Action Helpers ======

        /// <summary>
        /// Клик по элементу, когда он готов
        /// </summary>
        public static bool ClickWhenReady(this AutomationElement element, int timeoutMs = DefaultTimeout)
        {
            if (element.WaitUntilClickable(timeoutMs))
            {
                element.Click();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Очистка текста с помощью клавиатуры
        /// </summary>
        public static void ClearTextWithKeyboard(this TextBox textBox)
        {
            textBox.Focus();
            Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
            Keyboard.Press(VirtualKeyShort.BACK);
        }

        /// <summary>
        /// Ввод текста, когда элемент готов
        /// </summary>
        public static void EnterTextWhenReady(this TextBox textBox, string text, int timeoutMs = DefaultTimeout)
        {
            if (textBox.WaitUntilEnabled(timeoutMs))
            {
                textBox.ClearTextWithKeyboard();
                textBox.Enter(text);
            }
            else
            {
                throw new TimeoutException("TextBox был не готов для ввода.");
            }
        }

        /// <summary>
        /// Ожидание очистки текста
        /// </summary>
        public static bool WaitUntilTextIsEmpty(this TextBox element, int timeoutMs = DefaultTimeout)
        {
            _logger.Info($"Ожидание очистки текста в элементе: {element.Properties.AutomationId}");
            return Retry.WhileFalse(
                () => element?.Text == string.Empty,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
        }

        /// <summary>
        /// Ожидание, пока элемент станет полностью видимым (в пределах видимой области)
        /// </summary>
        public static bool WaitUntilFullyVisible(this AutomationElement element, int timeoutMs = DefaultTimeout)
        {
            _logger.Info($"Ожидание полной видимости элемента: {element.Properties.AutomationId}");
            return Retry.WhileFalse(
                () => element?.IsOffscreen == false && element?.BoundingRectangle.Width > 0 && element?.BoundingRectangle.Height > 0,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
        }

        /// <summary>
        /// Ожидание, пока элемент станет кликабельным и будет в фокусе
        /// </summary>
        public static bool WaitUntilClickableAndFocused(this AutomationElement element, int timeoutMs = DefaultTimeout)
        {
            _logger.Info($"Ожидание кликабельности и фокуса элемента: {element.Properties.AutomationId}");
            return Retry.WhileFalse(
                () => element?.IsEnabled == true &&
                      element?.IsOffscreen == false &&
                      element?.Automation.FocusedElement()?.Equals(element) == true,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
        }

        /// <summary>
        /// Ожидание, пока текст элемента будет содержать указанную подстроку
        /// </summary>
        public static bool WaitUntilContainsText(this AutomationElement element, string substring, int timeoutMs = DefaultTimeout)
        {
            _logger.Info($"Ожидание появления подстроки '{substring}' в элементе: {element.Properties.AutomationId}");
            return Retry.WhileFalse(
                () => element?.AsTextBox()?.Text?.Contains(substring) == true,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
        }

        /// <summary>
        /// Ожидание, пока текст элемента будет соответствовать регулярному выражению
        /// </summary>
        public static bool WaitUntilTextMatches(this AutomationElement element, string pattern, int timeoutMs = DefaultTimeout)
        {
            _logger.Info($"Ожидание соответствия текста шаблону '{pattern}' в элементе: {element.Properties.AutomationId}");
            return Retry.WhileFalse(
                () => System.Text.RegularExpressions.Regex.IsMatch(element?.AsTextBox()?.Text ?? string.Empty, pattern),
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
        }

        /// <summary>
        /// Ожидание, пока элемент станет активным и будет содержать непустой текст
        /// </summary>
        public static bool WaitUntilEnabledAndHasText(this AutomationElement element, int timeoutMs = DefaultTimeout)
        {
            _logger.Info($"Ожидание активности и наличия текста в элементе: {element.Properties.AutomationId}");
            return Retry.WhileFalse(
                () => element?.IsEnabled == true && !string.IsNullOrWhiteSpace(element?.AsTextBox()?.Text),
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
        }

        /// <summary>
        /// Ожидание, пока элемент станет активным и будет содержать указанный текст
        /// </summary>
        public static bool WaitUntilEnabledAndHasText(this AutomationElement element, string expectedText, int timeoutMs = DefaultTimeout)
        {
            _logger.Info($"Ожидание активности и текста '{expectedText}' в элементе: {element.Properties.AutomationId}");
            return Retry.WhileFalse(
                () => element?.IsEnabled == true && element?.AsTextBox()?.Text == expectedText,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
        }

        /// <summary>
        /// Ожидание, пока элемент станет активным и будет содержать текст, соответствующий условию
        /// </summary>
        public static bool WaitUntilEnabledAndTextMatches(this AutomationElement element, Func<string, bool> textCondition, int timeoutMs = DefaultTimeout)
        {
            _logger.Info($"Ожидание активности и соответствия текста условию в элементе: {element.Properties.AutomationId}");
            return Retry.WhileFalse(
                () => element?.IsEnabled == true && textCondition(element?.AsTextBox()?.Text ?? string.Empty),
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
        }

        /// <summary>
        /// Ожидание, пока элемент станет активным и будет содержать текст определенной длины
        /// </summary>
        public static bool WaitUntilEnabledAndTextLength(this AutomationElement element, int expectedLength, int timeoutMs = DefaultTimeout)
        {
            _logger.Info($"Ожидание активности и длины текста {expectedLength} в элементе: {element.Properties.AutomationId}");
            return Retry.WhileFalse(
                () => element?.IsEnabled == true && (element?.AsTextBox()?.Text?.Length ?? 0) == expectedLength,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
        }
    }
}
