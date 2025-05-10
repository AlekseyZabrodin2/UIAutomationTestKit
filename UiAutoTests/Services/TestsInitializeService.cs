using NLog;
using UiAutoTests.Core;
using UiAutoTests.Helpers;

namespace UiAutoTests.Services
{
    public class TestsInitializeService
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private LoggerHelper _loggerHelper = new();



        public string CreateTestName()
        {
            var testName = TestContext.CurrentContext.Test.MethodName!;

            var parameters = TestContext.CurrentContext.Test.Arguments;
            if (parameters.Length > 0)
            {
                testName += " [" + string.Join("_", parameters.Select(p => p?.ToString())) + "]";
            }

            return testName;
        }

        public IClientState SetUpBeforeTest(string testName, string testClass, HtmlReportService reportCore, ITestClient testClient)
        {
            _loggerHelper.LogEnteringTheMethod();

            var mainWindow = StartTestClient(testClient).GetAwaiter().GetResult();

            InitializeReportingTests(testName, testClass, reportCore);

            return mainWindow;
        }

        public async Task<IClientState> StartTestClient(ITestClient testClient)
        {
            _loggerHelper.LogEnteringTheMethod();

            IClientState? mainState = null;

            try
            {
                var mainWindow = await testClient.StartAsync(TimeSpan.FromSeconds(30));
                Assert.That(mainWindow, Is.Not.Null, "Test client Not louded");

                var mainWindowState = mainWindow.IsState(mainWindow.GetMainWindow());
                Assert.That(mainWindowState, Is.True, "Wrong state");

                mainState = await mainWindow.GoToStateAsync("MainWindowState", TimeSpan.FromSeconds(30));
                _logger.Info("Test Client started");
            }
            catch (Exception ex)
            {
                _logger.Error($"Test Client is not started: {ex.Message}");
            }

            return mainState;
        }

        public void InitializeReportingTests(string testName, string testClass, HtmlReportService reportCore)
        {
            _loggerHelper.LogEnteringTheMethod();

            reportCore.InitializeTests(testName, testClass);
            _logger.Info($"Entering in Test [{testName}]");
        }

        public void CleanupAfterTest(ITestClient testClient, HtmlReportService reportCore)
        {
            _logger.Trace("\r\n=========================== Test Result ===========================");

            _loggerHelper.LogEnteringTheMethod();

            if (testClient != null)
            {
                testClient.Kill();
            }
            
            reportCore.GetTestsStatus();

            _logger.Trace("\r\n=========================== END TestCase ===========================\r\n");
        }


    }
}
