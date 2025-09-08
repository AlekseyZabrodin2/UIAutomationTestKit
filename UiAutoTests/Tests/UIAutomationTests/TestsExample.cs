using UiAutoTests.Clients;
using UiAutoTests.ControllerAssertions;
using UiAutoTests.Controllers;
using UiAutoTests.Core;
using UiAutoTests.Helpers;
using UiAutoTests.Services;
using UiAutoTests.TestCasesData;

namespace UiAutoTests.Tests.UIAutomationTests
{

    public class TestsExample
    {

        private readonly NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
        private ITestClient _testClient;
        private IClientState _mainWindow;
        public string _testName;
        public string _testClass;
        private LoggerHelper _loggerHelper = new();
        public HtmlReportService _reportService = new();
        private TestsInitializeService _initializeService = new();
        private MainWindowController _mainWindowController;


        public TestsExample()
        {
            _testClient = new AutomationTestClient(ClientConfigurationHelper.TestClientProperties.TestClientPath);
        }



        [SetUp]
        public void Setup()
        {
            _testClass = GetType().Name;
            _testName = _initializeService.GetTestMethodName();

            if (_initializeService.IgnoreSetUpInTest(
                nameof(Test03_IgnoreSetUpAndLoadingTestClient)))
                return;

            if (_initializeService.IgnoreSetUpInTestWithParameters(
                "Test05"))
                return;

            _mainWindow = _initializeService.StartClientWithReportInitialization(_testName, _testClass, _reportService, _testClient);

            _mainWindowController = _mainWindow as MainWindowController
                ?? throw new InvalidCastException("Client state is not MainWindowController.");
        }



        [Test]
        public void Test01_Before()
        {
            try
            {
                if (_mainWindow is MainWindowController mainWindowController)
                {
                    var inputText = "test Id";

                    mainWindowController.SetUserId(inputText);
                    mainWindowController.Pause(500);

                    var actual = mainWindowController.GetUserIdText();
                    Assert.That(actual, Is.EqualTo(inputText), "Text is not equal");

                    mainWindowController.ClickCleanButton();

                    mainWindowController.Pause(1000);

                    var resultTest = mainWindowController.GetUserIdText();
                    Assert.That(resultTest, Is.Empty, "Text is not empty");

                    // Pause for demonstration
                    mainWindowController.Pause(1000);

                    _logger.Debug($"{_testName} Completed");
                    _reportService.LogStatusPass(_testName + " Completed");
                }
            }
            catch (Exception exception)
            {
                _logger.Error(exception, $"{_testName} Failed");
                _reportService.LogStatusFail(exception, _testName + " Failed");
                throw;
            }
            finally
            {
                _mainWindowController.EnsureClientStoping(_testClient);
            }
        }

        [Test]
        public void Test02_After()
        {
            try
            {
                var inputText = "test Id";

                _mainWindowController
                    .SetUserId(inputText)
                    .Pause(500)
                    .AssertUserIdEquals(inputText, $"Expected Text to be [{inputText}]")
                    .ClickCleanButton()
                    .WaitUntilTextIsEmpty(500)
                    .AssertUserIdIsEmpty("Expected TextBox to be Empty")
                    .Pause(500);

                _loggerHelper.LogCompletedResult(_testName, _reportService);
            }
            catch (Exception exception)
            {
                _loggerHelper.LogFailedResult(_testName, exception, _reportService);
                throw;
            }
            finally
            {
                _mainWindowController.EnsureClientStoping(_testClient);
            }
        }

        [Test]
        public void Test02_After_New()
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

        [Test]
        public async Task Test03_IgnoreSetUpAndLoadingTestClient()
        {
            try
            {
                var inputText = "test Id";

                var mainWindow = await _testClient.StartAsync(TimeSpan.FromSeconds(30));
                Assert.That(mainWindow, Is.Not.Null, "Test client Not loaded");

                var mainWindowState = mainWindow.IsState(mainWindow.GetMainWindow());
                Assert.That(mainWindowState, Is.True, "Wrong state");

                var mainState = await mainWindow.GoToStateAsync("MainWindowState", TimeSpan.FromSeconds(30));
                _logger.Info("Test Client started");

                _reportService.InitializeTests(_testName, _testClass);

                if (mainState is MainWindowController mainWindowController)
                {
                    mainWindowController
                        .SetUserId(inputText)
                        .Pause(500)
                        .AssertUserIdEquals(inputText, $"Expected Text to be [{inputText}]")
                        .ClickCleanButton()
                        .WaitUntilTextIsEmpty(500)
                        .AssertUserIdIsEmpty("Expected TextBox to be Empty")
                        .Pause(500);           
                    
                    _loggerHelper.LogCompletedResult(_testName, _reportService);
                }
            }
            catch (Exception exception)
            {
                _loggerHelper.LogFailedResult(_testName, exception, _reportService);
                throw;
            }
            finally
            {
                _mainWindowController.EnsureClientStoping(_testClient);
            }
        }

        [TestCaseSource(typeof(MainWindowTestCases), nameof(MainWindowTestCases.ValidRegistrationFieldCases))]
        [Test]
        public void Test04_Registration_WithDtoFromClass(RegistrationCaseDto registrDto)
        {
            try
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

                _loggerHelper.LogCompletedResult(_testName, _reportService);
            }
            catch (Exception exception)
            {
                _loggerHelper.LogFailedResult(_testName, exception, _reportService);
                throw;
            }
            finally
            {
                _mainWindowController.EnsureClientStoping(_testClient);
            }
        }

        [TestCaseSource(typeof(MainWindowTestCases), nameof(MainWindowTestCases.IgnoreSetUpCases))]
        [Test]
        public async Task Test05_IgnoreSetUpWhenTestWithParameter(RegistrationCaseDto registrDto)
        {
            try
            {
                var mainWindow = await _testClient.StartAsync(TimeSpan.FromSeconds(30));
                Assert.That(mainWindow, Is.Not.Null, "Test client Not loaded");

                var mainWindowState = mainWindow.IsState(mainWindow.GetMainWindow());
                Assert.That(mainWindowState, Is.True, "Wrong state");

                var mainState = await mainWindow.GoToStateAsync("MainWindowState", TimeSpan.FromSeconds(30));
                _logger.Info("Test Client started");

                _reportService.InitializeTests(_testName, _testClass);

                if (mainState is MainWindowController mainWindowController)
                {
                    mainWindowController
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

                    _loggerHelper.LogCompletedResult(_testName, _reportService);
                }
            }
            catch (Exception exception)
            {
                _loggerHelper.LogFailedResult(_testName, exception, _reportService);
                throw;
            }
            finally
            {
                _mainWindowController.EnsureClientStoping(_testClient);
            }
        }

        [TestCase("001", "Smith", "James", "John")]
        [TestCase("002", "Johnson", "Lee", "Michael")]
        [TestCase("003", "Williams", "Anne", "Emily")]
        [TestCase("004", "", "Marie", "Sophia")]
        [Test]
        public void Test06_WithParametersInTestCase(string id, string lastName, string middleName, string firstName)
        {
            try
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

                _loggerHelper.LogCompletedResult(_testName, _reportService);
            }
            catch (Exception exception)
            {
                _loggerHelper.LogFailedResult(_testName, exception, _reportService);
                throw;
            }
            finally
            {
                _mainWindowController.EnsureClientStoping(_testClient);
            }
        }

        [TestCaseSource(typeof(MainWindowTestCases), nameof(MainWindowTestCases.ValidRegistrationCases))]
        [Test]
        public void Test07_ParametersFromClass(RegistrationCaseDto registrDto)
        {
            try
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

                _loggerHelper.LogCompletedResult(_testName, _reportService);
            }
            catch (Exception exception)
            {
                _loggerHelper.LogFailedResult(_testName, exception, _reportService);
                throw;
            }
            finally
            {
                _mainWindowController.EnsureClientStoping(_testClient);
            }
        }

        [TestCaseSource(typeof(MainWindowTestCases), nameof(MainWindowTestCases.ValidRegistrationCasesFromJson))]
        [Test]
        public void Test08_Registration_WithDtoFromJson(RegistrationCaseFromJson dataFromJson)
        {
            try
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

                _loggerHelper.LogCompletedResult(_testName, _reportService);
            }
            catch (Exception exception)
            {
                _loggerHelper.LogFailedResult(_testName, exception, _reportService);
                throw;
            }
            finally
            {
                _mainWindowController.EnsureClientStoping(_testClient);
            }
        }

        [Test]
        public void Test09_CombiningIntoOneMethod()
        {
            try
            {
                _mainWindowController
                        .SetValidDataInUserForm(1, 3)
                        .AssertIsRegistrationButtonEnabled()
                        .ClickRegistrationButton()
                        .WaitUntilProgressBarIs(3);

                _loggerHelper.LogCompletedResult(_testName, _reportService);
            }
            catch (Exception exception)
            {
                _loggerHelper.LogFailedResult(_testName, exception, _reportService);
                throw;
            }
            finally
            {
                _mainWindowController.EnsureClientStoping(_testClient);
            }
        }

        [Test]
        public void Test10_RegistrationSeveralUsers([Values(3)] int number)
        {
            try
            {
                _mainWindowController
                        .SetValidDataInUserForm(2, number)
                        .AssertIsRegistrationButtonEnabled()
                        .ClickRegistrationButton()
                        .WaitUntilProgressBarIs(number);

                _loggerHelper.LogCompletedResult(_testName, _reportService);
            }
            catch (Exception exception)
            {
                _loggerHelper.LogFailedResult(_testName, exception, _reportService);
                throw;
            }
            finally
            {
                _mainWindowController.EnsureClientStoping(_testClient);
            }
        }


        [TearDown]
        public void AfterTest()
        {
            _mainWindowController.EnsureClientStoping(_testClient);
            _initializeService.DisposeClientAndReportResults(_testClient, _reportService);
        }

    }
}
