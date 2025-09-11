using NLog;

namespace UiAutoTests.Assertions
{
    public static class AssertForString
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();


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
    }
}
