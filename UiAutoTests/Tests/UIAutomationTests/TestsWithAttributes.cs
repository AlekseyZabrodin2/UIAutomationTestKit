using UiAutoTests.Clients;
using UiAutoTests.ControllerAssertions;
using UiAutoTests.Controllers;
using UiAutoTests.Core;
using UiAutoTests.Helpers;
using UiAutoTests.Services;

namespace UiAutoTests.Tests.UIAutomationTests
{
    public class TestsWithAttributes
    {
        private ITestClient _testClient;
        private IClientState _mainWindow;
        public string _testName;
        public string _testClass;
        public HtmlReportService _reportService = new();
        private TestsInitializeService _initializeService = new();
        private MainWindowController _mainWindowController;


        public TestsWithAttributes()
        {
            _testClient = new AutomationTestClient(ClientConfigurationHelper.TestClientProperties.TestClientPath);
        }



        [SetUp]
        public void Setup()
        {
            _testClass = GetType().Name;
            _testName = _initializeService.GetTestMethodName();

            _mainWindow = _initializeService.StartClientWithReportInitialization(_testName, _testClass, _reportService, _testClient);

            _mainWindowController = _mainWindow as MainWindowController
                ?? throw new InvalidCastException("Client state is not MainWindowController.");
        }


        // Атрибуты: Category - для организации тестов в группы
        [Test]
        [Category("Smoke")]
        [Category("UI")]
        public void Test01_TestWithCategory()
        {
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
        // Особенность: Автоматически перезапускает упавший тест до 3 раз
        [Test, Retry(3)]
        public void Test06_RetryTest()
        {
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
        // Придеться чистить вручную в [SetUp] блоке, потому что даже в [TearDown] не зайдет
        [Test, Timeout(15000)]
        public void Test08_PerformanceTest()
        {
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

            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                _mainWindowController
                        .SetValidDataInUserForm(1, 1)
                        .AssertIsRegistrationButtonEnabled()
                        .ClickRegistrationButton()
                        .WaitProgressBarWithToken(3, cancellationToken);
            });
        }

        // Атрибут: Author - метаданные для отчетов и организации работы команды
        [Test, Author("Alice Smith")]
        public void Test10_AuthoredTest()
        {
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
        public void Test11_TestWithDescription()
        {
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                        .SetValidDataInUserForm(1, 1)
                        .AssertIsRegistrationButtonEnabled()
                        .ClickRegistrationButton()
                        .WaitUntilProgressBarIs(1);
            });
        }



        [TearDown]
        public void AfterTest()
        {
            _mainWindowController?.EnsureClientStopped(_testClient);
            _initializeService.DisposeClientAndReportResults(_testClient, _reportService);
        }

    }
}
