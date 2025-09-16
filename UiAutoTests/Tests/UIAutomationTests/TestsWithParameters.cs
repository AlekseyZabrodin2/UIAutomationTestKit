using UiAutoTests.Clients;
using UiAutoTests.ControllerAssertions;
using UiAutoTests.Controllers;
using UiAutoTests.Core;
using UiAutoTests.Helpers;
using UiAutoTests.Services;
using UiAutoTests.TestCasesData;

namespace UiAutoTests.Tests.UIAutomationTests
{
    public class TestsWithParameters : InitializeBaseTest
    {
        private readonly NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
        private ITestClient _testClient;
        public HtmlReportService _reportService = new();
        private TestsInitializeService _initializeService = new();
        private MainWindowController _mainWindowController;


        public TestsWithParameters()
        {
            _testClient = new AutomationTestClient(ClientConfigurationHelper.TestClientProperties.TestClientPath);
        }


        
        // Способ параметризации: TestCaseSource (из статического класса)
        // Источник: Статическое свойство ValidRegistrationFieldCases в классе MainWindowTestCases
        // Формат данных: Возвращает IEnumerable<RegistrationCaseDto> - готовые DTO-объекты
        [TestCaseSource(typeof(MainWindowTestCases), nameof(MainWindowTestCases.ValidRegistrationFieldCases))]
        [Test]
        public async Task Test01_Registration_WithDtoFromClass(RegistrationCaseDto registrDto)
        {
            _mainWindowController = await GetControllerState<MainWindowController>(Main);
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                    .SetUserId(registrDto.Id)
                    .SetLastName(registrDto.LastName)
                    .SetMiddleName(registrDto.MiddleName)
                    .SetFirstName(registrDto.FirstName)

                    .CheckedBirthdate()

                    .AssertUserIdEquals(registrDto.Id, $"Expected Text to be [{registrDto.Id}]")

                    .ClickCleanButton()
                    .WaitUntilTextIsEmpty(500)
                    .AssertUserIdIsEmpty("Expected TextBox to be Empty")
                    .Pause(500);
            });
        }

        // Способ параметризации: TestCaseSource (из статического класса)
        // Особенность: Тест полностью игнорирует стандартные методы SetUp и выполняет собственную инициализацию
        // Использование: Для тестов, требующих особой настройки контекста, отличающейся от основной массы тестов
        // Примечание: Нарушает принцип DRY, но иногда необходимо для специфичных сценариев
        [TestCaseSource(typeof(MainWindowTestCases), nameof(MainWindowTestCases.IgnoreSetUpCases))]
        [Test]
        public async Task Test02_IgnoreSetUpWhenTestWithParameter(RegistrationCaseDto registrDto)
        {
            var mainWindow = await _testClient.StartAsync(TimeSpan.FromSeconds(30));
            Assert.That(mainWindow, Is.Not.Null, "Test client Not loaded");

            var mainWindowState = mainWindow.IsState(mainWindow.GetMainWindow());
            Assert.That(mainWindowState, Is.True, "Wrong state");

            var mainState = await mainWindow.GoToStateAsync(Main, TimeSpan.FromSeconds(30));
            _logger.Info("Test Client started");

            _reportService.InitializeTests(_testName, _testClass);

            if (mainState is MainWindowController mainWindowController)
            {
                _mainWindowController = mainWindowController;
                _mainWindowController.ExecuteTest(_testClient, _testName, () =>
                {
                    _mainWindowController
                        .SetUserId(registrDto.Id)
                        .SetLastName(registrDto.LastName)
                        .SetMiddleName(registrDto.MiddleName)
                        .SetFirstName(registrDto.FirstName)

                        .CheckedBirthdate()

                        .AssertUserIdEquals(registrDto.Id, $"Expected Text to be [{registrDto.Id}]")

                        .ClickCleanButton()
                        .WaitUntilTextIsEmpty(500)
                        .AssertUserIdIsEmpty("Expected TextBox to be Empty")
                        .Pause(500);
                });
            }
        }

        // Способ параметризации: Multiple TestCase
        // Формат: Явное перечисление параметров прямо в атрибутах теста
        // Преимущества: Максимальная наглядность, все данные видны непосредственно в коде теста
        // Недостатки: Дублирование кода при большом количестве тест-кейсов
        // Использование: Для небольшого количества важных/ключевых сценариев
        [TestCase("001", "Smith", "James", "John")]
        [TestCase("002", "Johnson", "Lee", "Michael")]
        [TestCase("003", "Williams", "Anne", "Emily")]
        [TestCase("004", "", "Marie", "Sophia")]
        [Test]
        public async Task Test03_WithParametersInTestCase(string id, string lastName, string middleName, string firstName)
        {
            _mainWindowController = await GetControllerState<MainWindowController>(Main);
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                        .SetUserId(id)
                        .SetLastName(lastName)
                        .SetMiddleName(middleName)
                        .SetFirstName(firstName)

                        .CheckedBirthdate()

                        .AssertUserIdEquals(id, $"Expected Text to be [{id}]")

                        .ClickCleanButton()
                        .WaitUntilTextIsEmpty(500)
                        .AssertUserIdIsEmpty("Expected TextBox to be Empty")
                        .Pause(500);
            });
        }

        // Способ параметризации: TestCaseSource с комплексными объектами
        // Особенность: Тестирование полного процесса регистрации с заполнением всех полей формы
        // Использование: End-to-end тестирование комплексных сценариев с подготовленными данными
        [TestCaseSource(typeof(MainWindowTestCases), nameof(MainWindowTestCases.ValidRegistrationCases))]
        [Test]
        public async Task Test04_ParametersFromClass(RegistrationCaseDto registrDto)
        {
            _mainWindowController = await GetControllerState<MainWindowController>(Main);
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                        .SetUserId(registrDto.Id)
                        .SetLastName(registrDto.LastName)
                        .SetMiddleName(registrDto.MiddleName)
                        .SetFirstName(registrDto.FirstName)

                        .CheckedBirthdate()
                        .SetBirthdateText(registrDto.BirthdateText)
                        .SelectGender(registrDto.SelectedGender)

                        .SetAddressUser(registrDto.Address)
                        .SetPhoneUser(registrDto.Phone)
                        .SetInfoUser(registrDto.Info)

                        .SelectRadioButtonByIndex(1)
                        .SelectUserCountBySlider(3)

                        .AssertUserIdEquals(registrDto.Id, $"Expected Text to be [{registrDto.Id}]")
                        .AssertIsRegistrationButtonEnabled()

                        .ClickRegistrationButton()
                        .WaitUntilProgressBarIs(3);
            });
        }

        // Способ параметризации: TestCaseSource с данными из JSON-файлов
        // Преимущества: Отделение данных от кода, легкое редактирование тестовых данных
        // Использование: Для данных, которые часто меняются или управляются не-разработчиками
        [TestCaseSource(typeof(MainWindowTestCases), nameof(MainWindowTestCases.ValidRegistrationCasesFromJson))]
        [Test]
        public async Task Test05_Registration_WithDtoFromJson(RegistrationCaseFromJson dataFromJson)
        {
            _mainWindowController = await GetControllerState<MainWindowController>(Main);
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                        .SetUserId(dataFromJson.Id)
                        .SetLastName(dataFromJson.LastName)
                        .SetMiddleName(dataFromJson.MiddleName)
                        .SetFirstName(dataFromJson.FirstName)

                        .CheckedBirthdate()
                        .SetBirthdateText(dataFromJson.BirthdateText)
                        .SelectGender(dataFromJson.SelectedGender)

                        .SetAddressUser(dataFromJson.Address)
                        .SetPhoneUser(dataFromJson.Phone)
                        .SetInfoUser(dataFromJson.Info)

                        .SelectRandomRadioButton()
                        .SelectUserCountBySlider(3)

                        .AssertUserIdEquals(dataFromJson.Id, $"Expected Text to be [{dataFromJson.Id}]")
                        .AssertIsRegistrationButtonEnabled()

                        .ClickRegistrationButton()
                        .WaitUntilProgressBarIs(3);
            });
        }

        // Способ параметризации: Values (один параметр)
        // Особенность: Тестирование массовой регистрации пользователей
        // Использование: Когда нужно протестировать поведение системы с разным количеством однотипных объектов
        [Test]
        public async Task Test06_RegistrationSeveralUsers([Values(5)] int number)
        {
            _mainWindowController = await GetControllerState<MainWindowController>(Main);
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                        .SetValidDataInUserForm(2, number)
                        .AssertIsRegistrationButtonEnabled()
                        .ClickRegistrationButton()
                        .WaitUntilProgressBarIs(number);
            });
        }

        // Способ параметризации: Combinatorial (по умолчанию) + Explicit
        // Особенность: Explicit - Явное указание что тест должен запускаться только вручную
        // Результат: 3×3×2×2 = 36 комбинаций (полный перебор)
        // Использование: Для exhaustive testing когда нужно проверить ВСЕ возможные комбинации
        [Test, Explicit]        
        public async Task Test07_TestWithCombinationValues(
            [Values("1", "18", "32")] string id,
            [Values("John", "Mon", "Don")] string lastName,
            [Values("Makaronavich", "Ivanavich")] string middleName,
            [Values("Firstov", "Secondovich")] string firstName)
        {
            _mainWindowController = await GetControllerState<MainWindowController>(Main);
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                        .SetUserId(id)
                        .SetLastName(lastName)
                        .SetMiddleName(middleName)
                        .SetFirstName(firstName)

                        .CheckedBirthdate()

                        .AssertUserIdEquals(id, $"Expected Text to be [{id}]")

                        .ClickCleanButton()
                        .WaitUntilTextIsEmpty(500)
                        .AssertUserIdIsEmpty("Expected TextBox to be Empty")
                        .Pause(500);
            });
        }

        // Способ параметризации: Sequential
        // Особенность: Параметры берутся попарно из каждого массива Values
        // Результат: 3 теста (по длине самого длинного массива)
        // Использование: Когда нужно проверить конкретные заранее известные комбинации
        [Test, Sequential]
        public async Task Test08_TestWithSequentialCombinationValues(
            [Values("1", "18", "32")] string id,
            [Values("John", "Mon", "Don")] string lastName,
            [Values("Makaronavich", "Ivanavich")] string middleName,
            [Values("Firstov", "Secondovich")] string firstName)
        {
            _mainWindowController = await GetControllerState<MainWindowController>(Main);
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                        .SetUserId(id)
                        .SetLastName(lastName)
                        .SetMiddleName(middleName)
                        .SetFirstName(firstName)

                        .CheckedBirthdate()

                        .AssertUserIdEquals(id, $"Expected Text to be [{id}]")

                        .ClickCleanButton()
                        .WaitUntilTextIsEmpty(500)
                        .AssertUserIdIsEmpty("Expected TextBox to be Empty")
                        .Pause(500);
            });
        }

        // Способ параметризации: Pairwise
        // Особенность: Генерация оптимального набора комбинаций, покрывающих все пары параметров
        // Результат: ~8-12 тестов вместо 36 (экономия времени при сохранении coverage)
        // Использование: Для выявления ошибок взаимодействия параметров при экономии времени выполнения
        [Test, Pairwise]
        public async Task Test09_TestWithPairwiseCombinationValues(
            [Values("1", "18", "32")] string id,
            [Values("John", "Mon", "Don")] string lastName,
            [Values("Makaronavich", "Ivanavich")] string middleName,
            [Values("Firstov", "Secondovich")] string firstName)
        {
            _mainWindowController = await GetControllerState<MainWindowController>(Main);
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                        .SetUserId(id)
                        .SetLastName(lastName)
                        .SetMiddleName(middleName)
                        .SetFirstName(firstName)

                        .CheckedBirthdate()

                        .AssertUserIdEquals(id, $"Expected Text to be [{id}]")

                        .ClickCleanButton()
                        .WaitUntilTextIsEmpty(500)
                        .AssertUserIdIsEmpty("Expected TextBox to be Empty")
                        .Pause(500);
            });
        }

        // Способ параметризации: Range
        // Особенность: Генерация значений в диапазоне с заданным шагом
        // Результат: 5 тестов (1, 3, 5, 7, 9)
        // Использование: Для тестирования с числовыми параметрами, где важно проверить разные точки диапазона
        [Test]
        public async Task Test10_TestWithCountRange([Range(1, 10, 2)] int number)
        {
            _mainWindowController = await GetControllerState<MainWindowController>(Main);
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                        .SetValidDataInUserForm(2, number)
                        .AssertIsRegistrationButtonEnabled()
                        .ClickRegistrationButton()
                        .WaitUntilProgressBarIs(number);
            });
        }

        // Способ параметризации: Random
        // Особенность: Генерация случайных значений в заданном диапазоне
        // Результат: 3 теста со случайными значениями от 2 до 10
        // Использование: Для fuzz-тестирования и проверки устойчивости системы к разным входным данным
        [Test]
        public async Task Test11_TestWithRandomCount([Random(2, 10, 3)] int number)
        {
            _mainWindowController = await GetControllerState<MainWindowController>(Main);
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                        .SetValidDataInUserForm(2, number)
                        .AssertIsRegistrationButtonEnabled()
                        .ClickRegistrationButton()
                        .WaitUntilProgressBarIs(number);
            });
        }
    }
}
