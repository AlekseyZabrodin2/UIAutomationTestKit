using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using NLog;

namespace UiAutoTests.Assertions
{
    public static class AssertAutoElement
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Проверяет, что элемент видим
        /// </summary>
        /// <param name="element">Проверяемый элемент</param>
        /// <param name="message">Сообщение об ошибке (опционально)</param>
        public static void IsVisible(AutomationElement element, string message = null)
        {
            try
            {
                Assert.That(element.IsOffscreen, Is.False, message);
                _logger.Info($"[Assert PASS] {message ?? $"Элемент {element.Properties.AutomationId} видим"}");
            }
            catch (AssertionException ex)
            {
                _logger.Error($"[Assert FAIL] {message ?? $"Элемент {element.Properties.AutomationId} не видим"}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Проверяет, что элемент активен
        /// </summary>
        /// <param name="element">Проверяемый элемент</param>
        /// <param name="message">Сообщение об ошибке (опционально)</param>
        public static void IsEnabled(AutomationElement element, string message = null)
        {
            try
            {
                Assert.That(element.IsEnabled, Is.True, message);
                _logger.Info($"[Assert PASS] {message ?? $"Элемент {element.Properties.AutomationId} активен"}");
            }
            catch (AssertionException ex)
            {
                _logger.Error($"[Assert FAIL] {message ?? $"Элемент {element.Properties.AutomationId} неактивен"}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Проверяет, что элемент неактивен
        /// </summary>
        /// <param name="element">Проверяемый элемент</param>
        /// <param name="message">Сообщение об ошибке (опционально)</param>
        public static void IsDisabled(AutomationElement element, string message = null)
        {
            try
            {
                Assert.That(element.IsEnabled, Is.False, message);
                _logger.Info($"[Assert PASS] {message ?? $"Элемент {element.Properties.AutomationId} неактивен"}");
            }
            catch (AssertionException ex)
            {
                _logger.Error($"[Assert FAIL] {message ?? $"Элемент {element.Properties.AutomationId} активен"}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Проверяет, что элемент имеет указанный тип
        /// </summary>
        /// <param name="element">Проверяемый элемент</param>
        /// <param name="expectedType">Ожидаемый тип</param>
        /// <param name="message">Сообщение об ошибке (опционально)</param>
        public static void HasControlType(AutomationElement element, ControlType expectedType, string message = null)
        {
            try
            {
                Assert.That(element.ControlType, Is.EqualTo(expectedType), message);
                _logger.Info($"[Assert PASS] {message ?? $"Элемент {element.Properties.AutomationId} имеет тип {expectedType}"}");
            }
            catch (AssertionException ex)
            {
                _logger.Error($"[Assert FAIL] {message ?? $"Элемент {element.Properties.AutomationId} имеет неверный тип"}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Проверяет, что элемент TextBox содержит указанный текст
        /// </summary>
        /// <param name="element">Проверяемый элемент</param>
        /// <param name="expectedText">Ожидаемый текст</param>
        /// <param name="message">Сообщение об ошибке (опционально)</param>
        public static void TextBoxContainsText(AutomationElement element, string expectedText, string message = null)
        {
            try
            {
                var actualText = element.AsTextBox()?.Text ?? string.Empty;
                Assert.That(actualText, Does.Contain(expectedText), message);
                _logger.Info($"[Assert PASS] {message ?? $"Элемент {element.Properties.AutomationId} содержит текст '{expectedText}'"}");
            }
            catch (AssertionException ex)
            {
                _logger.Error($"[Assert FAIL] {message ?? $"Элемент {element.Properties.AutomationId} не содержит текст '{expectedText}'"}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Проверяет, что чекбокс отмечен
        /// </summary>
        /// <param name="element">Проверяемый элемент</param>
        /// <param name="message">Сообщение об ошибке (опционально)</param>
        public static void IsChecked(AutomationElement element, string message = null)
        {
            try
            {
                var isChecked = element.AsCheckBox()?.IsChecked ?? false;
                Assert.That(isChecked, Is.True, message);
                _logger.Info($"[Assert PASS] {message ?? $"Чекбокс {element.Properties.AutomationId} отмечен"}");
            }
            catch (AssertionException ex)
            {
                _logger.Error($"[Assert FAIL] {message ?? $"Чекбокс {element.Properties.AutomationId} не отмечен"}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Проверяет, что чекбокс не отмечен
        /// </summary>
        /// <param name="element">Проверяемый элемент</param>
        /// <param name="message">Сообщение об ошибке (опционально)</param>
        public static void IsUnchecked(AutomationElement element, string message = null)
        {
            try
            {
                var isChecked = element.AsCheckBox()?.IsChecked ?? true;
                Assert.That(isChecked, Is.False, message);
                _logger.Info($"[Assert PASS] {message ?? $"Чекбокс {element.Properties.AutomationId} не отмечен"}");
            }
            catch (AssertionException ex)
            {
                _logger.Error($"[Assert FAIL] {message ?? $"Чекбокс {element.Properties.AutomationId} отмечен"}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Проверяет, что в комбобоксе выбран указанный элемент
        /// </summary>
        /// <param name="element">Проверяемый элемент</param>
        /// <param name="expectedItem">Ожидаемый элемент</param>
        /// <param name="message">Сообщение об ошибке (опционально)</param>
        public static void HasSelectedItem(AutomationElement element, string expectedItem, string message = null)
        {
            try
            {
                var selectedItem = element.AsComboBox()?.SelectedItem?.Text;
                Assert.That(selectedItem, Is.EqualTo(expectedItem), message);
                _logger.Info($"[Assert PASS] {message ?? $"В комбобоксе {element.Properties.AutomationId} выбран элемент '{expectedItem}'"}");
            }
            catch (AssertionException ex)
            {
                _logger.Error($"[Assert FAIL] {message ?? $"В комбобоксе {element.Properties.AutomationId} выбран неверный элемент"}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Проверяет, что в AutomationElement.Name содержит искомый текст
        /// </summary>
        /// <param name="element">Проверяемый элемент</param>
        /// <param name="expectedItem">Ожидаемый элемент</param>
        /// <param name="message">Сообщение об ошибке (опционально)</param>
        public static void ContainsText(AutomationElement element, string expectedText, string message = null)
        {
            try
            {
                var actualText = element.Name ?? string.Empty;
                Assert.That(actualText, Does.Contain(expectedText), message);
                _logger.Info($"[Assert PASS] {message ?? $"Элемент {element.Properties.AutomationId} содержит текст '{expectedText}'"}");
            }
            catch (AssertionException ex)
            {
                _logger.Error($"[Assert FAIL] {message ?? $"Элемент {element.Properties.AutomationId} не содержит текст '{expectedText}'"}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Проверяет, что элемент равен Null
        /// </summary>
        /// <param name="element">Проверяемый элемент</param>
        /// <param name="message">Сообщение об ошибке (опционально)</param>
        public static void IsNull(AutomationElement element, string message = null)
        {
            try
            {
                Assert.That(element, Is.Null, message);
                _logger.Info($"[Assert PASS] {message ?? $"Element Is NULL"}");
            }
            catch (AssertionException ex)
            {
                _logger.Error($"[Assert FAIL] {message ?? $"Element [{element.Properties.AutomationId}] != NULL"}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Проверяет, что элемент не равен Null
        /// </summary>
        /// <param name="element">Проверяемый элемент</param>
        /// <param name="message">Сообщение об ошибке (опционально)</param>
        public static void IsNotNull(AutomationElement element, string message = null)
        {
            try
            {
                Assert.That(element, Is.Not.Null, message);
                _logger.Info($"[Assert PASS] {message ?? $"Element [{element.Properties.AutomationId}] != NULL"}");
            }
            catch (AssertionException ex)
            {
                _logger.Error($"[Assert FAIL] {message ?? $"Element is NULL"}: {ex.Message}");
                throw;
            }
        }
    }
}
