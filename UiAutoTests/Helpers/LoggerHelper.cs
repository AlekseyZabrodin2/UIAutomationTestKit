using NLog;
using System.Runtime.CompilerServices;
using UiAutoTests.Core;

namespace UiAutoTests.Helpers
{
    public class LoggerHelper : ILoggerHelper
    {

        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();


        public void LogEnteringTheMethod([CallerMemberName] string methodName = "")
        {
            _logger.Info($"Entering the method: [\"{methodName}\"]");
        }

        public void LogExitingTheMethod([CallerMemberName] string methodName = "")
        {
            _logger.Info($"Exiting the method: [\"{methodName}\"]");
        }
    }
}
