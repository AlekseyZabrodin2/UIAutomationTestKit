using UiAutoTests.Clients;
using UiAutoTests.ControllerAssertions;
using UiAutoTests.Controllers;
using UiAutoTests.Core;
using UiAutoTests.Helpers;
using UiAutoTests.Services;

namespace UiAutoTests.Tests.UIAutomationTests
{
    public class TestsForDataGridElements
    {
        private ITestClient _testClient;
        private IClientState _mainWindow;
        public string _testName;
        public string _testClass;
        private LoggerHelper _loggerHelper = new();
        public HtmlReportService _reportService = new();
        private TestsInitializeService _initializeService = new();
        private ClientConfigurationHelper _clientConfigurationHelper = new();
        private MainWindowController _mainWindowController;



        public TestsForDataGridElements()
        {
            _testClient = new AutomationTestClient(ClientConfigurationHelper.TestClientProperties.TestClientPath);
        }


        [SetUp]
        public void Setup()
        {
            _testClass = GetType().Name;
            _testName = _initializeService.GetTestMethodName();

            if (_initializeService.IgnoreSetUpInTest())
                return;

            if (_initializeService.IgnoreSetUpInTestWithParameters(
                "Test02_RegistrationSeveralUsersNextVariant("))
                return;

            _mainWindow = _initializeService.StartClientWithReportInitialization(_testName, _testClass, _reportService, _testClient);

            _mainWindowController = _mainWindow as MainWindowController
                ?? throw new InvalidCastException("Client state is not MainWindowController.");
        }


        [Test]
        public void Test01_RegistrationSeveralUsersNextVariant([Values(10)] int number)
        {
            try
            {
                _mainWindowController
                        .RegistrationSeveralUsers(number)
                        .AssertRowCountIsNot(number);

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
        public void Test02_RegistrationSeveralUsersNextVariant([Values(10)] int number)
        {
            try
            {
                _mainWindow = _clientConfigurationHelper.StartClientWithCopyConfig(_testName, _testClass, _reportService, _testClient);

                _mainWindowController = _mainWindow as MainWindowController
                    ?? throw new InvalidCastException("Client state is not MainWindowController.");

                _mainWindowController
                        .RegistrationSeveralUsers(number)
                        .AssertRowCountIs(number);

                _clientConfigurationHelper.CopyDefaultConfigFile();

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
