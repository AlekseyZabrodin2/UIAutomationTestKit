using NLog;
using System.Diagnostics;
using UiAutoTests.Core;
using UiAutoTests.Helpers;

namespace UiAutoTests.Services
{
    public class TestsInitializeService
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private LoggerHelper _loggerHelper = new();


        public string GetTestMethodName()
        {
            var testName = TestContext.CurrentContext.Test.MethodName!;

            var parameters = TestContext.CurrentContext.Test.Arguments;
            if (parameters.Length > 0)
            {
                testName += " [" + string.Join("_", parameters.Select(p => p?.ToString())) + "]";
            }

            _logger.Trace($"\r\n=========================== Start Test - [{testName}] ===========================");

            return testName;
        }

        public IClientState StartClientWithReportInitialization(string testName, string testClass, 
            HtmlReportService reportCore, ITestClient testClient)
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
        }

        public bool IgnoreSetUpInTest(params string[] testName)
        {
            var currentTestName = TestContext.CurrentContext.Test.Name;
            return testName.Contains(currentTestName);
        }

        public bool IgnoreSetUpInTestWithParameters(params string[] testName)
        {
            var currentTestName = TestContext.CurrentContext.Test.Name;
            return testName.Any(test => currentTestName.StartsWith(test));
        }

        public void DisposeClientAndReportResults(ITestClient testClient, HtmlReportService reportCore)
        {
            _loggerHelper.LogEnteringTheMethod();

            StopTestClient(testClient);
            reportCore.GetTestsStatus();

            _logger.Trace("\r\n=========================== Finish TestCase ===========================\r\n");
        }

        private void StopTestClient(ITestClient testClient)
        {
            _loggerHelper.LogEnteringTheMethod();

            var nameTestClient = ClientConfigurationHelper.TestClientProperties.TestClientPath;
            var processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(nameTestClient));
            foreach (var process in processes)
            {
                try
                {
                    process.Kill();
                    process.WaitForExit();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Can`t stop process {processId}", process.Id);
                }
                _logger.Info("WebHost stoped");
            }
        }
    }
}
