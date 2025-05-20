using NLog;
using NUnit.Framework.Legacy;

namespace UiAutoTests.Helpers
{
    public class AssertHelpers
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

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
                
        public static void IsEmpty<T>( T actual, string message = null)
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

        public static void IsNotEmpty<T>( T actual, string message = null)
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

        public static void Fail(string message)
        {
            _logger.Error($"[Assert FAIL] {message}");
            Assert.Fail(message);
        }

        public static void Pass(string message)
        {
            _logger.Info($"[Assert PASS] {message}");
            Assert.Pass(message);
        }

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
