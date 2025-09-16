using NLog;
using UiAutoTests.Clients;
using UiAutoTests.Core;
using UiAutoTests.Helpers;
using UiAutoTests.Services;

namespace UiAutoTests.Tests
{
    public class InitializeBaseTest
    {

        private ITestClient _testClient;
        protected IClientState _mainWindow;
        protected string _testName;
        protected string _testClass; 
        private HtmlReportService _reportService = new();
        private TestsInitializeService _initializeService = new();
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public const string Main = "MainWindowState";
        public const string AboutApp = "AboutAppWindowState";



        public InitializeBaseTest()
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
                "Test02-1: First test",
                "Test02-2: Next test",
                "Test02_RegistrationSeveralUsersNextVariant("))
                return;

            if (_initializeService.IgnoreSetUpInTest("Test04_IgnoreSetUpAndLoadingTestClient"))
                return;

            _mainWindow = _initializeService.StartClientWithReportInitialization(_testName, _testClass, _reportService, _testClient);
        }



        protected async Task<T> GetControllerState<T>(string nameState) where T : class, IClientState
        {
            IClientState mainWindow = _mainWindow;

            if (_mainWindow == null)
                throw new InvalidOperationException("MainWindow is not initialized");

            if (_mainWindow.Name != nameState)
            {
                mainWindow = await _mainWindow.GoToStateAsync(nameState, TimeSpan.FromSeconds(5));
                _logger.Info($"State is - [{mainWindow.Name}]");
            }

            return mainWindow as T
                ?? throw new InvalidCastException($"Expected {mainWindow.Name}, but got {_mainWindow.Name}");
        }



        [TearDown]
        public void AfterTest()
        {
            _initializeService.DisposeClientAndReportResults(_testClient, _reportService);
        }
    }
}
