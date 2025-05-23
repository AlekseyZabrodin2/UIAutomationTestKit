using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.Core.WindowsAPI;
using NLog;

namespace UiAutoTests.Extensions
{
    public static class WaitExtensions
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

                
        public static void Pause(int timeoutInSeconds)
        {
            _logger.Info($"Пауза {timeoutInSeconds} сек...");

            Retry.WhileTrue(() => true, TimeSpan.FromMilliseconds(timeoutInSeconds));
        }

        // ====== General Wait Helpers ======

        public static bool WaitUntilExists(this AutomationElement parent, Func<AutomationElement> findFunc, int timeoutInSeconds)
        {
            var result = Retry.WhileNull(findFunc, TimeSpan.FromMilliseconds(timeoutInSeconds));
            return result.Result != null;
        }

        public static bool WaitUntilNotExists(this AutomationElement parent, Func<AutomationElement> findFunc, int timeoutInSeconds)
        {
            return Retry.WhileTrue(() => findFunc() != null, TimeSpan.FromMilliseconds(timeoutInSeconds)).Success;
        }

        public static bool WaitUntilVisible(this AutomationElement element, int timeoutInSeconds)
        {
            return Retry.WhileTrue(() => element?.IsOffscreen == true, TimeSpan.FromMilliseconds(timeoutInSeconds)).Success;
        }

        public static bool WaitUntilVisibleWithLog(this AutomationElement element, int timeoutInSeconds)
        {
            _logger.Info($"[WAIT] Ожидаем, пока элемент {element?.AutomationId} станет видимым");

            var result = Retry.WhileTrue(
                () => element?.IsOffscreen == true,
                TimeSpan.FromMilliseconds(timeoutInSeconds)).Success;

            if (result)
                _logger.Info("[WAIT] Элемент стал видимым");
            else
                _logger.Warn($"[WAIT] Элемент не стал видимым за {timeoutInSeconds} сек.");

            return result;
        }

        public static bool WaitUntilEnabled(this AutomationElement element, int timeoutInSeconds)
        {
            return Retry.WhileFalse(() => element?.IsEnabled ?? false, TimeSpan.FromMilliseconds(timeoutInSeconds)).Success;
        }

        public static bool WaitUntilDisabled(this AutomationElement element, int timeoutInSeconds)
        {
            return Retry.WhileTrue(() => element?.IsEnabled == true, TimeSpan.FromMilliseconds(timeoutInSeconds)).Success;
        }

        public static bool WaitUntilClickable(this AutomationElement element, int timeoutInSeconds)
        {
            return Retry.WhileFalse(
                () => element?.IsEnabled == true && element?.IsOffscreen == false,
                TimeSpan.FromMilliseconds(timeoutInSeconds)).Success;
        }

        public static bool WaitUntilHasText(this AutomationElement element, string expectedText, int timeoutInSeconds)
        {
            return Retry.WhileFalse(
                () => element?.Name == expectedText,
                TimeSpan.FromMilliseconds(timeoutInSeconds)).Success;
        }

        public static bool WaitUntilTextEquals(this AutomationElement element, string expectedText, int timeoutInSeconds)
        {
            return Retry.WhileFalse(
                () => element?.Name == expectedText,
                TimeSpan.FromMilliseconds(timeoutInSeconds)).Success;
        }

        public static bool WaitUntilSelectedItemIs(this ComboBox comboBox, string expectedText, int timeoutInSeconds)
        {
            return Retry.WhileFalse(
                () => comboBox.SelectedItem?.Text == expectedText,
                TimeSpan.FromMilliseconds(timeoutInSeconds)).Success;
        }

        public static bool WaitUntilTabSelected(this TabItem tabItem, int timeoutInSeconds)
        {
            return Retry.WhileFalse(
                () => tabItem.IsSelected,
                TimeSpan.FromMilliseconds(timeoutInSeconds)).Success;
        }

        // Ждёт, пока текст элемента содержит указанную подстроку
        public static bool WaitUntilContainsText(this AutomationElement element, string substring, int timeoutInSeconds)
        {
            return Retry.WhileFalse(() => element?.Name?.Contains(substring) == true, TimeSpan.FromMilliseconds(timeoutInSeconds)).Success;
        }
               
        // Ждёт, пока элемент получит фокус
        public static bool WaitUntilFocused(this AutomationElement element, int timeoutInSeconds)
        {            
            return Retry.WhileFalse(() => 
            {
                var focusElement = element.Automation.FocusedElement;
                return focusElement != null && focusElement.Equals(element);
            }, TimeSpan.FromMilliseconds(timeoutInSeconds)).Success;
        }

        // Ждёт, пока элемент уйдёт с экрана
        public static bool WaitUntilOffscreen(this AutomationElement element, int timeoutInSeconds)
        {
            return Retry.WhileFalse(() => element?.IsOffscreen == true, TimeSpan.FromMilliseconds(timeoutInSeconds)).Success;
        }

        // Обёртка для ожидания появления элемента
        public static bool WaitUntilElementFound(this AutomationElement parent, Func<AutomationElement> findFunc, int timeoutInSeconds)
        {
            return Retry.WhileNull(findFunc, TimeSpan.FromMilliseconds(timeoutInSeconds)).Success;
        }


        // ====== Action Helpers ======

        public static bool ClickWhenReady(this AutomationElement element, int timeoutInSeconds)
        {
            if (element.WaitUntilClickable(timeoutInSeconds))
            {
                element.Click();
                return true;
            }

            return false;
        }

        public static void ClearTextWithKeyboard(this TextBox textBox)
        {
            textBox.Focus();
            Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
            Keyboard.Press(VirtualKeyShort.BACK);
        }

        public static void EnterTextWhenReady(this TextBox textBox, string text, int timeoutInSeconds)
        {
            if (textBox.WaitUntilEnabled(timeoutInSeconds))
            {
                textBox.ClearTextWithKeyboard();
                textBox.Enter(text);
            }
            else
            {
                throw new TimeoutException("TextBox was not ready for input.");
            }
        }

        public static bool WaitUntilTextIsEmpty(this TextBox element, int timeoutInSeconds)
        {
            return Retry.WhileFalse(
                () => element?.Text == string.Empty,
                TimeSpan.FromMilliseconds(timeoutInSeconds)).Success;
        }
    }
}
