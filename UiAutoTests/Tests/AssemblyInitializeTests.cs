using NLog;
using UiAutoTests.Helpers;
using UiAutoTests.Services;

namespace UiAutoTests.Tests
{
    [SetUpFixture]
    public class AssemblyInitializeTests
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private static LoggerHelper _loggerHelper = new();
        public static HtmlReportService _reportService;
        private ClientConfigurationHelper _clientConfigurationHelper = new();

        public static string? _oldNameFullPath;
        public static string? _newNameFullPath;


        [OneTimeSetUp]
        public void BeforeTestSuites()
        {
            _logger.Trace($"\r\n=========================== New Test Suite start  ===========================");

            _loggerHelper.LogEnteringTheMethod();

            _clientConfigurationHelper.InitializeAppConfig();

            _reportService = new();
            _reportService.ReportLogger("UI Test");

            _oldNameFullPath = ".\\Report.html";
            _newNameFullPath = ".\\logs\\ReportResults\\TestReport" + DateTime.Now.ToString("_dd.MM.yyyy_HH.mm.ss") + ".html";

            var directoryPath = Path.GetDirectoryName(_newNameFullPath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            _reportService.ReplaceCssStyleDir();
        }

        

        [OneTimeTearDown]
        public void AfterAllTests()
        {
            _loggerHelper.LogEnteringTheMethod();

            if (_reportService != null)
            {
                //_reportService.CreateReport();

                //File.Move(_oldNameFullPath!, _newNameFullPath!);
            }
            else
            {
                var error = new Exception("HtmlReport is not initialized.");
                _logger.Error(error.Message);
            }
        }
    }
}
