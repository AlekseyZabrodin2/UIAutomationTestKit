using NLog;

namespace UiAutoTests.Assertions
{
    public static class AssertForCollection
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();


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
    }
}
