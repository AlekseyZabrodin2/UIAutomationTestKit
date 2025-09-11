using System.Globalization;
using UiAutoTests.Clients;
using UiAutoTests.ControllerAssertions;
using UiAutoTests.Controllers;
using UiAutoTests.Core;
using UiAutoTests.Helpers;
using UiAutoTests.Services;

namespace UiAutoTests.Tests.UIAutomationTests
{
    public class TestsWithAttributes : InitializeBaseTest
    {
        private ITestClient _testClient;
        private MainWindowController _mainWindowController;


        public TestsWithAttributes()
        {
            _testClient = new AutomationTestClient(ClientConfigurationHelper.TestClientProperties.TestClientPath);
        }



        // Атрибуты: Category - для организации тестов в группы
        [Test]
        [Category("Smoke")]
        [Category("UI")]
        public void Test01_TestWithCategory()
        {
            _mainWindowController = GetController<MainWindowController>();
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                var inputText = "test Id";

                _mainWindowController.SetUserId(inputText)
                    .Pause(500)
                    .AssertUserIdEquals(inputText, $"Expected Text to be [{inputText}]")
                    .ClickCleanButton()
                    .WaitUntilTextIsEmpty(500)
                    .AssertUserIdIsEmpty("Expected TextBox to be Empty")
                    .Pause(500);
            });
        }

        // Атрибут: Ignore - тест пропускается с указанием причины
        [Test]
        [Ignore("Требуется доработка логина на стороне API. TODO: JIRA-123")]
        public void Test02_IgnoredTest()
        {
            _mainWindowController = GetController<MainWindowController>();
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                        .SetValidDataInUserForm(1, 1)
                        .AssertIsRegistrationButtonEnabled()
                        .ClickRegistrationButton()
                        .WaitUntilProgressBarIs(1);
            });
        }

        // Атрибут: Explicit - не запускается в общем прогоне всех тестов
        // Особенность: Явное указание что тест должен запускаться только вручную
        [Test, Explicit]
        public void Test03_ManualTest()
        {
            _mainWindowController = GetController<MainWindowController>();
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                        .SetValidDataInUserForm(1, 1)
                        .AssertIsRegistrationButtonEnabled()
                        .ClickRegistrationButton()
                        .WaitUntilProgressBarIs(1);
            });
        }

        // Атрибут: Order - управление порядком выполнения
        [Test, Order(1)]
        public void Test04_FirstSequentialTest()
        {
            _mainWindowController = GetController<MainWindowController>();
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                        .SetValidDataInUserForm(1, 1)
                        .AssertIsRegistrationButtonEnabled()
                        .ClickRegistrationButton()
                        .WaitUntilProgressBarIs(1);
            });
        }

        // Атрибут: Order - управление порядком выполнения
        [Test, Order(2)]
        public void Test05_SecondSequentialTest()
        {
            _mainWindowController = GetController<MainWindowController>();
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                        .SetValidDataInUserForm(1, 1)
                        .AssertIsRegistrationButtonEnabled()
                        .ClickRegistrationButton()
                        .WaitUntilProgressBarIs(1);
            });
        }

        // Атрибут: Retry - борется с ложными падениями из-за временных проблем
        // Особенность: Автоматически перезапускает упавший тест
        [Test, Retry(3)]
        public void Test06_RetryTest()
        {
            _mainWindowController = GetController<MainWindowController>();
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                var inputText = "test Id";
                var wrongResult = "Somefing wrong";

                _mainWindowController.SetUserId(inputText)
                    .Pause(500)
                    .AssertUserIdEquals(wrongResult, $"Expected Text to be [{inputText}]")
                    .ClickCleanButton()
                    .WaitUntilTextIsEmpty(500)
                    .AssertUserIdIsEmpty("Expected TextBox to be Empty")
                    .Pause(500);
            });
        }

        // Атрибут: Repeat - многократный запуск одного теста
        [Test, Repeat(2)]
        public void Test07_StabilityTest()
        {
            _mainWindowController = GetController<MainWindowController>();
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                        .SetValidDataInUserForm(1, 1)
                        .AssertIsRegistrationButtonEnabled()
                        .ClickRegistrationButton()
                        .WaitUntilProgressBarIs(1);
            });
        }

        // Атрибут: Timeout - тест падает если превышено время выполнения
        // Особенность: Ограничивает время выполнения теста и прерывает поток принудительно
        // Аварийное завершение: NUnit не дает тесту завершиться нормально
        // Нет контроля: Процесс прерывается на системном уровне
        // Ресурсы не освобождаются: Соединения, файлы, дескрипторы могут остаться висеть
        // Придеться чистить вручную в следующем [SetUp] блоке, потому что даже в [TearDown] не зайдет
        [Test, Timeout(15000)]
        public void Test08_PerformanceTest()
        {
            _mainWindowController = GetController<MainWindowController>();
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                        .SetValidDataInUserForm(1, 1)
                        .AssertIsRegistrationButtonEnabled()
                        .ClickRegistrationButton()
                        .WaitUntilProgressBarIs(3);
            });
        }

        // Альтернатива Timeout - тест будет ждать отведенное время , но завершится корректно
        [Test, CancelAfter(15000)]
        public void Test09_TestAlternativeTimeOut()
        {
            CancellationToken cancellationToken = TestContext.CurrentContext.CancellationToken;

            _mainWindowController = GetController<MainWindowController>();
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                _mainWindowController
                        .SetValidDataInUserForm(1, 1)
                        .AssertIsRegistrationButtonEnabled()
                        .ClickRegistrationButton()
                        .WaitProgressBarWithToken(3, cancellationToken); // ждем три Users, но так как не дождемся, тест завершится по отмене (cancellationToken)
            });
        }

        // Атрибут: MaxTime - тест ДОЛЖЕН выполниться за отведенное время,
        // Иначе даже при успехе будет - Failed из-за превышения максимального времени.
        [Test, MaxTime(10000)]
        public void Test10_TestWithMaxTimeAttribute()
        {
            _mainWindowController = GetController<MainWindowController>();
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                        .SetValidDataInUserForm(1, 10)
                        .AssertIsRegistrationButtonEnabled()
                        .ClickRegistrationButton()
                        .WaitUntilProgressBarIs(10);
            });
        }

        // Атрибут: Author - метаданные для отчетов и организации работы команды
        [Test, Author("Alice Smith")]
        public void Test11_AuthoredTest()
        {
            _mainWindowController = GetController<MainWindowController>();
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                        .SetValidDataInUserForm(1, 1)
                        .AssertIsRegistrationButtonEnabled()
                        .ClickRegistrationButton()
                        .WaitUntilProgressBarIs(1);
            });
        }

        // Атрибут: Description - добавляет контекст для читателей отчетов
        [Test, Description("Информация необходимая для пояснения особенностей теста")]
        public void Test12_TestWithDescription()
        {
            _mainWindowController = GetController<MainWindowController>();
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                        .SetValidDataInUserForm(1, 1)
                        .AssertIsRegistrationButtonEnabled()
                        .ClickRegistrationButton()
                        .WaitUntilProgressBarIs(1);
            });
        }

        // Атрибут: Platform - Позволяет указывать, на каких платформах (ОС) должен запускаться тест.
        // Очень полезно для кросс-платформенных проектов.
        [Test]
        [Platform("Win")]
        public void Test13_WindowsSpecificTest()
        {
            _mainWindowController = GetController<MainWindowController>();
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                        .SetValidDataInUserForm(1, 1)
                        .AssertIsRegistrationButtonEnabled()
                        .ClickRegistrationButton()
                        .WaitUntilProgressBarIs(1);
            });
        }

        [Test]
        [Platform("Linux")]
        public void Test14_LinuxSpecificTest()
        {
            _mainWindowController = GetController<MainWindowController>();
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                        .SetValidDataInUserForm(1, 1)
                        .AssertIsRegistrationButtonEnabled()
                        .ClickRegistrationButton()
                        .WaitUntilProgressBarIs(1);
            });
        }

        // Везде, кроме Linux и Unix
        [Test]
        [Platform(Exclude = "Linux,Unix")]
        public void Test15_NotOnLinuxTest()
        {
            _mainWindowController = GetController<MainWindowController>();
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                        .SetValidDataInUserForm(1, 1)
                        .AssertIsRegistrationButtonEnabled()
                        .ClickRegistrationButton()
                        .WaitUntilProgressBarIs(1);
            });
        }

        // Атрибут: SetCulture - временно устанавливает CurrentCulture для потока
        // Влияет на: парсинг чисел/дат, форматирование, сравнения строк
        // В данном тесте: американский формат (точка для десятичных, мм/дд/гггг для дат)
        // Без атрибута тест использует системную культуру и может падать на разных машинах
        [Test, SetCulture("en-US")]
        public void Test16_ParseAndFormat_WithEnglishCulture()
        {
            // Десятичная точка и формат даты мм/дд/гггг
            var number = double.Parse("1.23");                  // ok в en-US
            var date = DateTime.Parse("01/19/2021");            // ok в en-US (январь 19)

            Assert.That(number, Is.EqualTo(1.23).Within(0.0001));
            Assert.That(date, Is.EqualTo(new DateTime(2021, 1, 19)));

            // Форматирование по американской культуре
            var formattedNumber = 12345.67.ToString("N2");      // "12,345.67"
            var formattedDate = new DateTime(2025, 2, 1).ToString("d"); // "2/1/2025"

            Assert.That(formattedNumber, Is.EqualTo("12,345.67"));
            Assert.That(formattedDate, Is.EqualTo("2/1/2025"));
        }
    }
}
