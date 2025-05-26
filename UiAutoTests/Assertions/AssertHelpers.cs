using NLog;

namespace UiAutoTests.Assertions
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
    }
}
