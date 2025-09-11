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



        protected T GetController<T>() where T : class, IClientState
        {
            if (_mainWindow == null)
                throw new InvalidOperationException("MainWindow is not initialized");

            return _mainWindow as T
                ?? throw new InvalidCastException($"Expected {typeof(T).Name}, but got {_mainWindow.GetType().Name}");
        }



        [TearDown]
        public void AfterTest()
        {
            _initializeService.DisposeClientAndReportResults(_testClient, _reportService);
        }
    }
}
