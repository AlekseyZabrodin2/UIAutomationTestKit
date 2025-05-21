using NLog;
using System;
using System.Runtime.CompilerServices;
using UiAutoTests.Core;
using UiAutoTests.Services;

namespace UiAutoTests.Helpers
{
    public class LoggerHelper : ILoggerHelper
    {

        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();


        public void LogEnteringTheMethod([CallerMemberName] string methodName = "")
        {
            //_logger.Info($"Entering the method: [\"{methodName}\"]");
        }

        public void LogExitingTheMethod([CallerMemberName] string methodName = "")
        {
            _logger.Info($"Exiting the method: [\"{methodName}\"]");
        }

        public void LogCompletedResult(string testName, HtmlReportService reportService)
        {
            _logger.Trace("\r\n=========================== Test Result ===========================");

            _logger.Debug($"{testName} Completed");
            reportService.LogStatusPass(testName + " Completed");
        }

        public void LogFailedResult(string testName, Exception exception, HtmlReportService reportService)
        {
            _logger.Trace("\r\n=========================== Test Result ===========================");

            _logger.Error(exception, $"{testName} Failed");
            reportService.LogStatusFail(exception, testName + " Failed");
        }
    }
}
