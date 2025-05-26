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
            _testClient = new AutomationTestClient("..\\..\\..\\..\\UIAutomationTestKit\\bin\\Debug\\net9.0-windows\\UIAutomationTestKit.exe");
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
                _testClient.Kill();
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
                _testClient.Kill();
            }
        }

        [TestCaseSource(typeof(MainWindowTestCases), nameof(MainWindowTestCases.ValidRegistrationFieldCases))]
        [Test]
        public void Test03_Registration_WithDtoFromClass(RegistrationCaseDto registrDto)
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
                _testClient.Kill();
            }
        }

        [TestCase("001", "Smith", "James", "John")]
        [TestCase("002", "Johnson", "Lee", "Michael")]
        [TestCase("003", "Williams", "Anne", "Emily")]
        [TestCase("004", "", "Marie", "Sophia")]
        [Test]
        public void Test04_WithParametersInTestCase(string id, string lastName, string middleName, string firstName)
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
                _testClient.Kill();
            }
        }

        [TestCaseSource(typeof(MainWindowTestCases), nameof(MainWindowTestCases.ValidRegistrationCases))]
        [Test]
        public void Test05_ParametersFromClass(RegistrationCaseDto registrDto)
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

                        .SetAdressUser(registrDto.Address)
                        .SetPhoneUser(registrDto.Phone)
                        .SetInfoUser(registrDto.Info)

                        .SelectGender(registrDto.SelectedGender)

                        .AssertUserIdEquals(registrDto.Id, $"Expected Text to be [{registrDto.Id}]")

                        .AssertIsRegistrationButtonEnabled()

                        .ClickRegistrationButton()
                        .Pause(1000);

                _loggerHelper.LogCompletedResult(_testName, _reportService);
            }
            catch (Exception exception)
            {
                _loggerHelper.LogFailedResult(_testName, exception, _reportService);
                throw;
            }
            finally
            {
                _testClient.Kill();
            }
        }

        [TestCaseSource(typeof(MainWindowTestCases), nameof(MainWindowTestCases.ValidRegistrationCasesFromJson))]
        [Test]
        public void Test06_Registration_WithDtoFromJson(RegistrationCaseFromJson dataFromJson)
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

                        .SetAdressUser(dataFromJson.Address)
                        .SetPhoneUser(dataFromJson.Phone)
                        .SetInfoUser(dataFromJson.Info)

                        .SelectGender(dataFromJson.SelectedGender)

                        .AssertUserIdEquals(dataFromJson.Id, $"Expected Text to be [{dataFromJson.Id}]")

                        .AssertIsRegistrationButtonEnabled()

                        .ClickRegistrationButton()
                        .Pause(1000);

                _loggerHelper.LogCompletedResult(_testName, _reportService);
            }
            catch (Exception exception)
            {
                _loggerHelper.LogFailedResult(_testName, exception, _reportService);
                throw;
            }
            finally
            {
                _testClient.Kill();
            }
        }

        [Test]
        public void Test07_CombiningIntoOneMethod()
        {
            try
            {
                _mainWindowController
                        .SetValidDataInUserForm()
                        .AssertIsRegistrationButtonEnabled()
                        .ClickRegistrationButton()
                        .Pause(1000);

                _loggerHelper.LogCompletedResult(_testName, _reportService);
            }
            catch (Exception exception)
            {
                _loggerHelper.LogFailedResult(_testName, exception, _reportService);
                throw;
            }
            finally
            {
                _testClient.Kill();
            }
        }




        [TearDown]
        public void AfterTest()
        {
            _initializeService.DisposeClientAndReportResults(_testClient, _reportService);
        }

    }
}
