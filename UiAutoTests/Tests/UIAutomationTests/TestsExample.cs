using UiAutoTests.Clients;
using UiAutoTests.ControllerAssertions;
using UiAutoTests.Controllers;
using UiAutoTests.Core;
using UiAutoTests.Helpers;
using UiAutoTests.Services;

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
                nameof(Test04_IgnoreSetUpAndLoadingTestClient)))
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
                _mainWindowController.EnsureClientStopped(_testClient);
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
                _mainWindowController.EnsureClientStopped(_testClient);
            }
        }

        [Test]
        public void Test03_AfterCreatingBaseController()
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
        public async Task Test04_IgnoreSetUpAndLoadingTestClient()
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
                _mainWindowController = mainWindowController;
                _mainWindowController.ExecuteTest(_testClient, _testName, () =>
                {
                    _mainWindowController
                    .SetUserId(inputText)
                    .Pause(500)
                    .AssertUserIdEquals(inputText, $"Expected Text to be [{inputText}]")
                    .ClickCleanButton()
                    .WaitUntilTextIsEmpty(500)
                    .AssertUserIdIsEmpty("Expected TextBox to be Empty")
                    .Pause(500);
                });
            }
        }

        [Test]
        public void Test05_RegistrationWithLongWay()
        {
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                        .SetUserId("001")
                        .SetLastName("Smith")
                        .SetMiddleName("Jamesonnova")
                        .SetFirstName("Emily")

                        .CheckedBirthdate()
                        .SetBirthdateText("19.01.2021")
                        .SelectGender(2)

                        .SetAddressUser("Minsk")
                        .SetPhoneUser("769879879")
                        .SetInfoUser("Testing text")

                        .SelectRadioButtonByIndex(1)
                        .SelectUserCountBySlider(3)

                        .AssertUserIdEquals("001", $"Expected Text to be [001]")
                        .AssertIsRegistrationButtonEnabled()

                        .ClickRegistrationButton()
                        .WaitUntilProgressBarIs(3);
            });
        }

        [Test]
        public void Test06_CombiningIntoOneMethod()
        {
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                        .SetValidDataInUserForm(1, 3)
                        .AssertIsRegistrationButtonEnabled()
                        .ClickRegistrationButton()
                        .WaitUntilProgressBarIs(3);
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
