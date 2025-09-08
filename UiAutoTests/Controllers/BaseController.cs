using NLog;
using System.ComponentModel;
using UiAutoTests.Core;
using UiAutoTests.Helpers;
using UiAutoTests.Services;

namespace UiAutoTests.Controllers
{
    public class BaseController
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private LoggerHelper _loggerHelper = new();
        public HtmlReportService _reportService = new();



        public void ExecuteTest(ITestClient testClient, string testName, Action testAction)
        {
            _loggerHelper.LogEnteringTheMethod();

            try
            {
                testAction.Invoke();

                _loggerHelper.LogCompletedResult(testName, _reportService);
            }
            catch (Exception exception)
            {
                _loggerHelper.LogFailedResult(testName, exception, _reportService);
                throw;
            }
            finally
            {
                EnsureClientStopped(testClient);
            }
        }

        public void EnsureClientStopped(ITestClient testClient, string clientName = "default")
        {
            _loggerHelper.LogEnteringTheMethod();

            try
            {
                if (testClient == null)
                {
                    _logger.Debug($"Client '{clientName}' is null - nothing to stop");
                    return;
                }

                testClient.Kill();
                _logger.Debug($"Client '{clientName}' stopped successfully");
            }
            catch (ObjectDisposedException)
            {
                _logger.Debug($"Client '{clientName}' already disposed");
            }
            catch (InvalidOperationException ex)
            {
                _logger.Debug(ex, $"Client '{clientName}' already stopped or in invalid state");
            }
            catch (Win32Exception ex)
            {
                _logger.Warn(ex, $"Win32 exception during client '{clientName}' shutdown");
            }
            catch (Exception ex)
            {
                _logger.Warn(ex, $"Failed to stop client '{clientName}' safely");
            }
        }

    }
}
