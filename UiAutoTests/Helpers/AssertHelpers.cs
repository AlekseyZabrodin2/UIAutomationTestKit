using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using NLog;

namespace UiAutoTests.Helpers
{
    /// <summary>
    /// Вспомогательный класс для выполнения проверок с расширенным логированием
    /// </summary>
    public class AssertHelpers
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Проверяет, что условие истинно
        /// </summary>
        /// <param name="condition">Проверяемое условие</param>
        /// <param name="message">Сообщение об ошибке (опционально)</param>
        public static void IsTrue(bool condition, string message = null)
        {
            try
            {
                Assert.That(condition, Is.True, message);
                _logger.Info($"[Assert PASS] {message ?? "Условие истинно"}");
            }
            catch (AssertionException ex)
            {
                _logger.Error($"[Assert FAIL] {message ?? "Условие ложно"}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Проверяет, что условие ложно
        /// </summary>
        /// <param name="condition">Проверяемое условие</param>
        /// <param name="message">Сообщение об ошибке (опционально)</param>
        public static void IsFalse(bool condition, string message = null)
        {
            try
            {
                Assert.That(condition, Is.False, message);
                _logger.Info($"[Assert PASS] {message ?? "Условие ложно"}");
            }
            catch (AssertionException ex)
            {
                _logger.Error($"[Assert FAIL] {message ?? "Ожидалось ложное условие"}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Проверяет, что два значения равны
        /// </summary>
        /// <typeparam name="T">Тип сравниваемых значений</typeparam>
        /// <param name="expected">Ожидаемое значение</param>
        /// <param name="actual">Фактическое значение</param>
        /// <param name="message">Сообщение об ошибке (опционально)</param>
        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            try
            {
                Assert.That(actual, Is.EqualTo(expected), message);
                _logger.Info($"[Assert PASS] {message ?? $"Ожидалось: {expected}, Получено: {actual}"}");
            }
            catch (AssertionException ex)
            {
                _logger.Error($"[Assert FAIL] {message ?? $"Ожидалось: {expected}, Получено: {actual}"}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Проверяет, что два значения не равны
        /// </summary>
        /// <typeparam name="T">Тип сравниваемых значений</typeparam>
        /// <param name="notExpected">Неожидаемое значение</param>
        /// <param name="actual">Фактическое значение</param>
        /// <param name="message">Сообщение об ошибке (опционально)</param>
        public static void AreNotEqual<T>(T notExpected, T actual, string message = null)
        {
            try
            {
                Assert.That(actual, Is.Not.EqualTo(notExpected), message);
                _logger.Info($"[Assert PASS] {message ?? $"Значения различаются как ожидалось"}");
            }
            catch (AssertionException ex)
            {
                _logger.Error($"[Assert FAIL] {message ?? $"Значения совпали, что не ожидалось"}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Проверяет, что объект равен null
        /// </summary>
        /// <param name="obj">Проверяемый объект</param>
        /// <param name="message">Сообщение об ошибке (опционально)</param>
        public static void IsNull(object obj, string message = null)
        {
            try
            {
                Assert.That(obj, Is.Null, message);
                _logger.Info($"[Assert PASS] {message ?? "Объект равен null"}");
            }
            catch (AssertionException ex)
            {
                _logger.Error($"[Assert FAIL] {message ?? "Объект не равен null"}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Проверяет, что объект не равен null
        /// </summary>
        /// <param name="obj">Проверяемый объект</param>
        /// <param name="message">Сообщение об ошибке (опционально)</param>
        public static void IsNotNull(object obj, string message = null)
        {
            try
            {
                Assert.That(obj, Is.Not.Null, message);
                _logger.Info($"[Assert PASS] {message ?? "Объект не равен null"}");
            }
            catch (AssertionException ex)
            {
                _logger.Error($"[Assert FAIL] {message ?? "Ожидался не-null объект"}: {ex.Message}");
                throw;
            }
        }
                
        /// <summary>
        /// Проверяет, что значение пусто
        /// </summary>
        /// <typeparam name="T">Тип проверяемого значения</typeparam>
        /// <param name="actual">Проверяемое значение</param>
        /// <param name="message">Сообщение об ошибке (опционально)</param>
        public static void IsEmpty<T>(T actual, string message = null)
        {
            try
            {
                Assert.That(actual, Is.Empty, message);
                _logger.Info($"[Assert PASS] {message ?? "Значение пусто"}");
            }
            catch (AssertionException ex)
            {
                _logger.Error($"[Assert FAIL] {message ?? "Значение не пусто"}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Проверяет, что значение не пусто
        /// </summary>
        /// <typeparam name="T">Тип проверяемого значения</typeparam>
        /// <param name="actual">Проверяемое значение</param>
        /// <param name="message">Сообщение об ошибке (опционально)</param>
        public static void IsNotEmpty<T>(T actual, string message = null)
        {
            try
            {
                Assert.That(actual, Is.Not.Empty, message);
                _logger.Info($"[Assert PASS] {message ?? "Значение не пусто"}");
            }
            catch (AssertionException ex)
            {
                _logger.Error($"[Assert FAIL] {message ?? "Значение пусто"}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Проверяет, что значение больше ожидаемого
        /// </summary>
        /// <param name="actual">Фактическое значение</param>
        /// <param name="expected">Ожидаемое значение</param>
        /// <param name="message">Сообщение об ошибке (опционально)</param>
        public static void Greater(int actual, int expected, string message = null)
        {
            try
            {
                Assert.That(actual, Is.GreaterThan(expected), message);
                _logger.Info($"[Assert PASS] {message ?? $"{actual} > {expected}"}");
            }
            catch (AssertionException ex)
            {
                _logger.Error($"[Assert FAIL] {message ?? $"{actual} не больше {expected}"}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Проверяет, что значение меньше ожидаемого
        /// </summary>
        /// <param name="actual">Фактическое значение</param>
        /// <param name="expected">Ожидаемое значение</param>
        /// <param name="message">Сообщение об ошибке (опционально)</param>
        public static void Less(int actual, int expected, string message = null)
        {
            try
            {
                Assert.That(actual,Is.LessThan(expected), message);
                _logger.Info($"[Assert PASS] {message ?? $"{actual} < {expected}"}");
            }
            catch (AssertionException ex)
            {
                _logger.Error($"[Assert FAIL] {message ?? $"{actual} не меньше {expected}"}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Проверяет, что строка содержит указанную подстроку
        /// </summary>
        /// <param name="expectedSubstring">Ожидаемая подстрока</param>
        /// <param name="actualString">Проверяемая строка</param>
        /// <param name="message">Сообщение об ошибке (опционально)</param>
        public static void Contains(string expectedSubstring, string actualString, string message = null)
        {
            try
            {
                Assert.That(expectedSubstring, Does.Contain(actualString), message);
                _logger.Info($"[Assert PASS] {message ?? $"Строка содержит '{expectedSubstring}'"}");
            }
            catch (AssertionException ex)
            {
                _logger.Error($"[Assert FAIL] {message ?? $"Строка не содержит '{expectedSubstring}'"}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Проверяет, что строка не содержит указанную подстроку
        /// </summary>
        /// <param name="unexpectedSubstring">Неожидаемая подстрока</param>
        /// <param name="actualString">Проверяемая строка</param>
        /// <param name="message">Сообщение об ошибке (опционально)</param>
        public static void DoesNotContain(string unexpectedSubstring, string actualString, string message = null)
        {
            try
            {
                Assert.That(unexpectedSubstring, Does.Not.Contain(actualString), message);
                _logger.Info($"[Assert PASS] {message ?? $"Строка не содержит '{unexpectedSubstring}'"}");
            }
            catch (AssertionException ex)
            {
                _logger.Error($"[Assert FAIL] {message ?? $"Строка содержит '{unexpectedSubstring}', что не ожидалось"}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Вызывает ошибку проверки с указанным сообщением
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        public static void Fail(string message)
        {
            _logger.Error($"[Assert FAIL] {message}");
            Assert.Fail(message);
        }

        /// <summary>
        /// Вызывает успешное завершение проверки с указанным сообщением
        /// </summary>
        /// <param name="message">Сообщение об успехе</param>
        public static void Pass(string message)
        {
            _logger.Info($"[Assert PASS] {message}");
            Assert.Pass(message);
        }

        /// <summary>
        /// Выполняет несколько проверок и собирает все ошибки
        /// </summary>
        /// <param name="checks">Массив проверок для выполнения</param>
        public static void Multiple(params Action[] checks)
        {
            try
            {
                Assert.Multiple(() =>
                {
                    foreach (var check in checks)
                    {
                        check();
                    }
                });

                _logger.Info($"[Assert PASS] Все множественные проверки прошли");
            }
            catch (AssertionException ex)
            {
                _logger.Error($"[Assert FAIL] Некоторые множественные проверки не прошли: {ex.Message}");
                throw;
            }
        }

        #region UI Element Assertions

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
        /// Проверяет, что элемент содержит указанный текст
        /// </summary>
        /// <param name="element">Проверяемый элемент</param>
        /// <param name="expectedText">Ожидаемый текст</param>
        /// <param name="message">Сообщение об ошибке (опционально)</param>
        public static void ContainsText(AutomationElement element, string expectedText, string message = null)
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

        #endregion

        #region Collection Assertions

        /// <summary>
        /// Проверяет, что коллекция содержит указанный элемент
        /// </summary>
        /// <typeparam name="T">Тип элементов коллекции</typeparam>
        /// <param name="collection">Проверяемая коллекция</param>
        /// <param name="item">Элемент для поиска</param>
        /// <param name="message">Сообщение об ошибке (опционально)</param>
        public static void Contains<T>(IEnumerable<T> collection, T item, string message = null)
        {
            try
            {
                Assert.That(collection, Does.Contain(item), message);
                _logger.Info($"[Assert PASS] {message ?? $"Коллекция содержит элемент {item}"}");
            }
            catch (AssertionException ex)
            {
                _logger.Error($"[Assert FAIL] {message ?? $"Коллекция не содержит элемент {item}"}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Проверяет, что коллекция не содержит указанный элемент
        /// </summary>
        /// <typeparam name="T">Тип элементов коллекции</typeparam>
        /// <param name="collection">Проверяемая коллекция</param>
        /// <param name="item">Элемент для поиска</param>
        /// <param name="message">Сообщение об ошибке (опционально)</param>
        public static void DoesNotContain<T>(IEnumerable<T> collection, T item, string message = null)
        {
            try
            {
                Assert.That(collection, Does.Not.Contain(item), message);
                _logger.Info($"[Assert PASS] {message ?? $"Коллекция не содержит элемент {item}"}");
            }
            catch (AssertionException ex)
            {
                _logger.Error($"[Assert FAIL] {message ?? $"Коллекция содержит элемент {item}"}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Проверяет, что коллекция содержит все указанные элементы
        /// </summary>
        /// <typeparam name="T">Тип элементов коллекции</typeparam>
        /// <param name="collection">Проверяемая коллекция</param>
        /// <param name="items">Элементы для поиска</param>
        /// <param name="message">Сообщение об ошибке (опционально)</param>
        public static void ContainsAll<T>(IEnumerable<T> collection, IEnumerable<T> items, string message = null)
        {
            try
            {
                Assert.That(collection, Is.SupersetOf(items), message);
                _logger.Info($"[Assert PASS] {message ?? "Коллекция содержит все указанные элементы"}");
            }
            catch (AssertionException ex)
            {
                _logger.Error($"[Assert FAIL] {message ?? "Коллекция не содержит все указанные элементы"}: {ex.Message}");
                throw;
            }
        }

        #endregion

        #region String Assertions

        /// <summary>
        /// Проверяет, что строка соответствует регулярному выражению
        /// </summary>
        /// <param name="actual">Проверяемая строка</param>
        /// <param name="pattern">Регулярное выражение</param>
        /// <param name="message">Сообщение об ошибке (опционально)</param>
        public static void Matches(string actual, string pattern, string message = null)
        {
            try
            {
                Assert.That(actual, Does.Match(pattern), message);
                _logger.Info($"[Assert PASS] {message ?? $"Строка соответствует шаблону '{pattern}'"}");
            }
            catch (AssertionException ex)
            {
                _logger.Error($"[Assert FAIL] {message ?? $"Строка не соответствует шаблону '{pattern}'"}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Проверяет, что строка не соответствует регулярному выражению
        /// </summary>
        /// <param name="actual">Проверяемая строка</param>
        /// <param name="pattern">Регулярное выражение</param>
        /// <param name="message">Сообщение об ошибке (опционально)</param>
        public static void DoesNotMatch(string actual, string pattern, string message = null)
        {
            try
            {
                Assert.That(actual, Does.Not.Match(pattern), message);
                _logger.Info($"[Assert PASS] {message ?? $"Строка не соответствует шаблону '{pattern}'"}");
            }
            catch (AssertionException ex)
            {
                _logger.Error($"[Assert FAIL] {message ?? $"Строка соответствует шаблону '{pattern}'"}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Проверяет, что строка начинается с указанного префикса
        /// </summary>
        /// <param name="actual">Проверяемая строка</param>
        /// <param name="expectedPrefix">Ожидаемый префикс</param>
        /// <param name="message">Сообщение об ошибке (опционально)</param>
        public static void StartsWith(string actual, string expectedPrefix, string message = null)
        {
            try
            {
                Assert.That(actual, Does.StartWith(expectedPrefix), message);
                _logger.Info($"[Assert PASS] {message ?? $"Строка начинается с '{expectedPrefix}'"}");
            }
            catch (AssertionException ex)
            {
                _logger.Error($"[Assert FAIL] {message ?? $"Строка не начинается с '{expectedPrefix}'"}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Проверяет, что строка заканчивается указанным суффиксом
        /// </summary>
        /// <param name="actual">Проверяемая строка</param>
        /// <param name="expectedSuffix">Ожидаемый суффикс</param>
        /// <param name="message">Сообщение об ошибке (опционально)</param>
        public static void EndsWith(string actual, string expectedSuffix, string message = null)
        {
            try
            {
                Assert.That(actual, Does.EndWith(expectedSuffix), message);
                _logger.Info($"[Assert PASS] {message ?? $"Строка заканчивается на '{expectedSuffix}'"}");
            }
            catch (AssertionException ex)
            {
                _logger.Error($"[Assert FAIL] {message ?? $"Строка не заканчивается на '{expectedSuffix}'"}: {ex.Message}");
                throw;
            }
        }

        #endregion
    }
}
